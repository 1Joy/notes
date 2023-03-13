using GalaSoft.MvvmLight.Messaging;
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

namespace MvvmLightFWDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //注册消息：接受消息的对象，收到消息要执行的逻辑处理
            Messenger.Default.Register<string>(this, ReceiveMsg);

            //注册消息：添加注册监听接受指定key的消息
            Messenger.Default.Register<string>(this, "msgKey", ReceiveMsg);

            Messenger.Default.Register<NotificationMessage>(this, ReceiveNotificationMsg);
            Messenger.Default.Register<NotificationMessage<int>>(this, ReceiveNotificationMsg);


            Messenger.Default.Register<NotificationMessageAction>(this, ReceiveNotificationMsg);



            Messenger.Default.Register<PropertyChangedMessage<string>>(this, (data) =>
            {
                if (data.PropertyName == "")
                {

                }
            });
        }

        private void ReceiveMsg(string msg)
        {

        }

        private void ReceiveNotificationMsg(NotificationMessage message)
        {

        }

        private void ReceiveNotificationMsg<T>(NotificationMessage<T> message)
        {

        }

        private void ReceiveNotificationMsg(NotificationMessageAction message)
        {
            ///接收方执行处理逻辑
            ///

            //执行回调
            message.Execute();  
        }
    }
}
