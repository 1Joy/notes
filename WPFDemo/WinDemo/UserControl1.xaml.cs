using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        //用户控件自定义命令绑定和参数

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(UserControl1), new PropertyMetadata(null));



        public object CommandParamter
        {
            get { return (object)GetValue(CommandParamterProperty); }
            set { SetValue(CommandParamterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParamter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParamterProperty =
            DependencyProperty.Register("CommandParamter", typeof(object), typeof(UserControl1), new PropertyMetadata(null));





        public UserControl1()
        {
            InitializeComponent();
            this.MouseLeftButtonUp += UserControl1_MouseLeftButtonUp;
        }

        private void UserControl1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Command?.Execute(CommandParamter);
        }
    }

    public class MethodClass
    {
        public static int StaticProperty { get; set; } = 123;
        /// <summary>
        /// 绑定静态属性并且通知
        /// </summary>
        private static int _myProperty;
        public static int MyProperty
        {
            get { return _myProperty; }
            set { _myProperty = value;
                // MyPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(MyProperty)));
                StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(MyProperty)));
            }
        }
        //方式一：框架内部自动根据属性名+Changed进行初始化了这个事件的实例，如果名字不一样就不能进行自动关联静态属性变化事件
        //public static event EventHandler<PropertyChangedEventArgs> MyPropertyChanged;

        //方式二：框架内部自动将静态属性变化事件进行初始化了，不与静态属性名强制关联，任何静态属性变化了都可以通知
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        public string GetWidth(string p)
        {
            return (int.Parse(p) * 0.5).ToString();
        }
    }

    public class InheritsControl1 : ContentControl {

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(InheritsControl1), new FrameworkPropertyMetadata(1.0,
                FrameworkPropertyMetadataOptions.Inherits));
    }
    public class InheritsControl2 : ContentControl {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            InheritsControl1.ValueProperty.AddOwner(typeof(InheritsControl2), new FrameworkPropertyMetadata(1.0,
                FrameworkPropertyMetadataOptions.Inherits));
    }

}
