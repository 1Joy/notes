using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WinDemo
{
    /// <summary>
    /// EventWin.xaml 的交互逻辑
    /// </summary>
    public partial class EventWin : Window
    {
        public EventWin()
        {
            InitializeComponent();

            SourceInitialized += EventWin_SourceInitialized;

            ContentRendered += EventWin_ContentRendered;

            Loaded += EventWin_Loaded;

            Activated += EventWin_Activated;

            Deactivated += EventWin_Deactivated;

            Closing += EventWin_Closing;

            Closed += EventWin_Closed;
        }

        private void EventWin_Closed(object sender, EventArgs e)
        {
            
        }

        private void EventWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void EventWin_Deactivated(object sender, EventArgs e)
        {
            
        }

        private void EventWin_Activated(object sender, EventArgs e)
        {
           
        }

        private void EventWin_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void EventWin_ContentRendered(object sender, EventArgs e)
        {
            
        }

        private void EventWin_SourceInitialized(object sender, EventArgs e)
        {
            
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Button_MouseLeftButtonDown");
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Button_PreviewMouseLeftButtonDown");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_Click");

        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(string));
            //动态创建一个对象实例
            //添加到到canvas的children
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            DragDrop.DoDragDrop(border, "UserControl", DragDropEffects.Copy);
        }

        private void UCRouteEvent_Route(object sender, RoutedEventArgs e)
        {

        }
    }

    public class DragBehavior:Behavior<Border>
    {
        /// <summary>
        /// 当挂载到对应的对象上时触发
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            //所关联的对象
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            //
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //鼠标锁定
            this.AssociatedObject.CaptureMouse();
            //鼠标释放
            this.AssociatedObject.ReleaseMouseCapture();
        }

        /// <summary>
        /// 对象销毁时触发
        /// </summary>
        protected override void OnDetaching()
        {
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
        }
    }
}
