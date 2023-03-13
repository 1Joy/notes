using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WinDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_Startup;

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
