using System;
using Microsoft.SPOT;
using System.Reflection;
using MicroWebServer.Tests.Models;

namespace MicroWebServer.Tests
{
    public class TestRunner
    {
        public event TestCompletedEventHandler TestCompleted;
        public event EventHandler AllTestsCompleted;

        public void Run()
        {
            var assembly = Assembly.GetAssembly(typeof(TestRunner));
            foreach (var type in assembly.GetTypes())
            {
                var index = type.Name.LastIndexOf("Tests");
                if (index > -1 && index == type.Name.Length - 5)
                {
                    var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                    var instance = type.GetConstructor(new Type[] { }).Invoke(new object[] { });
                    foreach (var method in methods)
                    {
                        if (method.ReturnType == typeof(void))
                        {
                            var result = new TestResult { TestClass = type.Name, TestMethod = method.Name, Message = string.Empty };
                            try
                            {
                                method.Invoke(instance, new object[] { });
                                result.Pass = true;
                                Debug.Print("PASS " + type.Name + "." + method.Name);
                            }
                            catch (AssertException ex)
                            {
                                result.Pass = false;
                                result.Message = ex.Message;
                                Debug.Print("FAIL " + type.Name + "." + method.Name + ": " + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                result.Pass = false;
                                result.Message = ex.ToString();
                                Debug.Print("FAIL " + type.Name + "." + method.Name + " - Threw Exception:" + ex.Message);
                            }
                            OnTestCompleted(result);
                        }
                    }
                }
            }
            OnAllTestsCompleted();
        }

        protected virtual void OnTestCompleted(TestResult result)
        {
            if (TestCompleted != null) TestCompleted(this, new TestCompletedEventArgs(result));
        }

        protected virtual void OnAllTestsCompleted()
        {
            if (AllTestsCompleted != null) AllTestsCompleted(this, EventArgs.Empty);
        }
    }

    public delegate void TestCompletedEventHandler(TestRunner sender, TestCompletedEventArgs args);
    public class TestCompletedEventArgs : EventArgs
    {
        public TestResult Result { get; private set; }

        public TestCompletedEventArgs(TestResult result)
        {
            Result = result;
        }
    }
}
