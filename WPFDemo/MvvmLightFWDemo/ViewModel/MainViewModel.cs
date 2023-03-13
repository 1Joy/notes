using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Windows;
using System.Windows.Input;

namespace MvvmLightFWDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string myVar;
        public string MyProperty
        {
            get { return myVar; }
            set {
                Set(ref myVar, value,broadcast:true);  //提供函数，进行复制以及通知属性变化
                myVar = value;
                RaisePropertyChanged<string>("MyProperty",broadcast:true); //通知属性变化
            }
        }

        //无参数的命令
        public ICommand BtnCommand
        {
            get => new RelayCommand(() =>
              {
                  ///todo 命令具体实现
                  
                  //消息发送
                  Messenger.Default.Send<string>("hello");
                  Messenger.Default.Send<string>("hello", "msgKey");


                  NotificationMessage nm = new NotificationMessage("notification");

                  NotificationMessage<int> nm1 = new NotificationMessage<int>(100, "notification");

                  Messenger.Default.Send<NotificationMessage>(nm);
                  Messenger.Default.Send<NotificationMessage<int>>(nm1);


                  NotificationMessageAction nma = new NotificationMessageAction("notification", () =>
                  {
                      //这个发送消息被接受处理后要执行的回调函数
                  });
                  Messenger.Default.Send<NotificationMessageAction>(nma);
              });
        }

        //带参数的命令
        public ICommand BtnCommand1
        {
            get => new RelayCommand<string>((param) =>
            {
                ///todo
            });
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //检查是否在UI线程上，如果不在就将操作挂在UI线程上执行
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                //操作
            });

            //等效于
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {

            }));
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        public override void Cleanup()
        {
            ///todo 资源释放逻辑
        }
    }
}