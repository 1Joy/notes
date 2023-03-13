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

namespace MVVMDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RoutedUICommand TestCommand { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //定义快捷键
            InputGestureCollection inputGestureCollection = new InputGestureCollection()
            {
                new KeyGesture(Key.T,ModifierKeys.Alt)
            };
            TestCommand = new RoutedUICommand("测试按钮", "TestBtn", typeof(MainWindow),inputGestureCollection);
        }

        private void Button_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //需要自己实现处理逻辑
        }

        private void Button_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //需要自己实现处理逻辑
            e.CanExecute = true;
        }
    }
}
