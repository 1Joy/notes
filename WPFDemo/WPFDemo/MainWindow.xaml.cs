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

namespace WPFDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.grid.RowDefinitions.Add(new RowDefinition());
            this.grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> hours = new List<string>(24);
            for(int i = 0; i < 24; i++)
            {
                hours.Add(i.ToString("D2"));
            }
            comboBox1.ItemsSource = hours;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ii = comboBox1.SelectedItem;
        }
    }
}
