using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinDemo
{
    /// <summary>
    /// UCRouteEvent.xaml 的交互逻辑
    /// </summary>
    public partial class UCRouteEvent : UserControl
    {

        //定义自定义路由事件
        /*
         * 事件名称
         * 路由策略：Bubble：冒泡；Tunnel：隧道；Direct：直接触发
         * 事件类型
         * 事件的所有者
         */
        public static readonly RoutedEvent routeEvent = EventManager.RegisterRoutedEvent("route", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UCRouteEvent));

        public event RoutedEventHandler Route
        {
            add { AddHandler(routeEvent, value); }
            remove
            {
                RemoveHandler(routeEvent, value);
            }
        }

        public UCRouteEvent()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //触发我们自定义的路由事件
            //定义参数
            RoutedEventArgs args = new RoutedEventArgs(routeEvent);
            //触发
            this.RaiseEvent(args);
        }
    }
}
