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

namespace WinDemo.UserControls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(UserControl1), new PropertyMetadata(string.Empty));


        public UserControl1()
        {
            InitializeComponent();
        }
    }
}
