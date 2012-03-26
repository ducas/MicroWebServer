using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using MicroWebServer.Tests.Views;
using MicroWebServer.Tests.Models;
using System.Threading;

namespace MicroWebServer.Tests.Controllers
{
    public class TestRunController
    {
        private TestRunView view;
        private TestRunner runner;

        public void Show()
        {
            this.view = new TestRunView();
            Application.Current.MainWindow.Child = view;

            runner = new TestRunner();
            runner.TestCompleted += new TestCompletedEventHandler(runner_TestCompleted);
            runner.AllTestsCompleted += new EventHandler(runner_AllTestsCompleted);
            new Thread(new ThreadStart(runner.Run)).Start();
        }

        int passed = 0, failed = 0;
        void runner_TestCompleted(TestRunner sender, TestCompletedEventArgs args)
        {
            var testList = view as TestRunView;
            if (testList == null) return;
            if (args.Result.Pass)
                testList.PassedCount = (++passed).ToString();
            else
                testList.FailedCount = (++failed).ToString();
        }

        void runner_AllTestsCompleted(object sender, EventArgs e)
        {
            var testList = view as TestRunView;
            if (testList == null) return;
            testList.Complete();
        }
    }
}
