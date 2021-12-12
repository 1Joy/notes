using Hardcodet.Wpf.TaskbarNotification;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrismDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenNotifyIcon();
            //RegionManager.SetRegionName(header, "HeaderRegion");
        }

        //Hardcodet.NotifyIcon.Wpf.NetCore
        TaskbarIcon _notifyIcon;
        private void OpenNotifyIcon()
        {
            if(_notifyIcon is null) InitNotifyIcon();
        }        
        private void InitNotifyIcon()
        {
            _notifyIcon = new TaskbarIcon();
            _notifyIcon.Icon = new System.Drawing.Icon(@"./icon.ico");
            ContextMenu context = new ContextMenu();
            MenuItem show = new MenuItem();
            show.Header = "主页";
            show.Click += delegate (object sender, RoutedEventArgs e)
              {
                  Application.Current.MainWindow.Show();
                  Application.Current.MainWindow.Topmost = true;
                  Application.Current.MainWindow.Topmost = false;
              };
            context.Items.Add(show);
            MenuItem exit = new MenuItem();
            exit.Header = "退出";
            exit.Click += delegate (object sender, RoutedEventArgs e)
              {
                  Environment.Exit(0);
              };
            context.Items.Add(exit);
            _notifyIcon.ContextMenu = context;
        }

        private void Exit()
        {
            if (_notifyIcon is null) return;
            _notifyIcon.Visibility = Visibility.Collapsed;
            _notifyIcon.Dispose();
        }
    }
}
