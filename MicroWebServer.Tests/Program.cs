using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using MicroWebServer.Tests.Controllers;

namespace MicroWebServer.Tests
{
    public class Program
    {
        public static void Main()
        {
            var app = new TestApplication();
            var window = app.CreateWindow();
            app.Run(window);
        }

        public class TestApplication : Application
        {
            Window mainWindow;

            public Window CreateWindow()
            {
                mainWindow = new Window() { Height = SystemMetrics.ScreenHeight, Width = SystemMetrics.ScreenWidth, Visibility = Visibility.Visible };
                return mainWindow;
            }

            protected override void OnStartup(EventArgs e)
            {
                var controller = new TestRunController();
                controller.Show();
                base.OnStartup(e);
            }
        }
    }
}
