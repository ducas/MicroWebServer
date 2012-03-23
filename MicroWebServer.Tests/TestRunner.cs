using System;
using Microsoft.SPOT;
using System.Reflection;

namespace MicroWebServer.Tests
{
    static class TestRunner
    {
        public static void Run()
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
                            try
                            {
                                method.Invoke(instance, new object[] { });
                            }
                            catch (AssertException ex)
                            {
                                Debug.Print("FAIL " + type.Name + "." + method.Name + ": " + ex.Message);
                                continue;
                            }
                            catch (Exception ex)
                            {
                                Debug.Print("FAIL " + type.Name + "." + method.Name + " - Threw Exception:" + ex.Message);
                                continue;
                            }
                            Debug.Print("PASS " + type.Name + "." + method.Name);
                        }
                    }
                }
            }
        }
    }
}
