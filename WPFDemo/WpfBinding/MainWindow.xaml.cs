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

namespace WpfBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Student student;
        public MainWindow()
        {
            InitializeComponent();

            //准备数据源
            student = new Student();

            //准备binding
            Binding binding = new Binding();
            binding.Source = student;
            binding.Path = new PropertyPath("Name");

            //使用binding连接数据源与目标
            BindingOperations.SetBinding(textBoxName, TextBox.TextProperty, binding);

            //上面三个步骤可替换为
            //textBoxName.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = student = new Student() });



            List<Student> students = new List<Student>() {
            new Student(){Id=0,Name="joy",Age=24},
            new Student(){Id=1,Name="joy1",Age=24},
            new Student(){Id=2,Name="joy2",Age=24},
            new Student(){Id=3,Name="joy3",Age=24},
            new Student(){Id=4,Name="joy4",Age=24},
            };

            listBoxStudents.ItemsSource = students;
            listBoxStudents.DisplayMemberPath = "Name";

            textBoxId.SetBinding(TextBox.TextProperty, new Binding("SelectedItem.Id") { Source = listBoxStudents });

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            student.Name += "name";
        }
    }
}
