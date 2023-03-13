using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// BindingWin.xaml 的交互逻辑
    /// </summary>
    public partial class BindingWin : Window
    {
        
        public BindingWin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //获取目标控件上的某个属性的绑定表达式
            BindingExpression bindingExpression = tb.GetBindingExpression(
                TextBox.TextProperty);
            //手动触发更新数据源
            bindingExpression.UpdateSource();

            //手动移除绑定关系
            BindingOperations.ClearBinding(tb, TextBox.TextProperty);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //判断一个依赖对象的依赖属性是否存在异常
            if (Validation.GetHasError(tb3))
            {
                _ = Validation.GetErrors(tb3);
            }
        }
    }

    public class BindModelConverter : IValueConverter
    {
        /// <summary>
        /// 从源到目标的数据转换
        /// </summary>
        /// <param name="value">数据源的值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">转换参数</param>
        /// <param name="culture"></param>
        /// <returns>将要给目标属性的值</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.Parse(value.ToString()) > 200)
                return Brushes.Red;
            return Brushes.Green;

        }

        /// <summary>
        /// 从目标到数据源的数据转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //如果数据源的值与传入的参数值一致，就说明是选中状态
            if (value.ToString() == parameter.ToString())
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }

    public class ValueMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string value = "";
            foreach (var item in values)
            {
                value += item.ToString();
            }
            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 自定义数据验证规则
    /// </summary>
    public class ValidationTest : ValidationRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">目标属性的值</param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //自定义验证规则和异常描述信息
            if (value.ToString() == "200")
                return new ValidationResult(false, "错误描述");
            return new ValidationResult(true, "");
        }
    }



    public class BindingWinViewModel
    {
        public int Value2 { get; set; } = 4;
        public int Value3 { get; set; }

        private int _value11=123;

        public int Value11
        {
            get { return _value11; }
            set {
                if (value == 200)
                {
                    throw new Exception("报错了");
                }
                _value11 = value;
            }
        }

        public DateTime Time { get; set; } = DateTime.Now;
        public BindingModel BM1 { get; set; } = new BindingModel();
        public List<BindingModel> ListValue { get; set; } = new List<BindingModel>
        {
            new BindingModel(){Value=1},
            new BindingModel(){Value=2},
            new BindingModel(){Value=3},
            new BindingModel(){Value=4},
            new BindingModel(){Value=5},
        };
    }

    public class BindingModel:INotifyPropertyChanged,IDataErrorInfo
    {
        [NotValue("200")]
        public int Value22 { get; set; } = 100;

        public int Value { get; set; } = 123;

        private int _gender = 1;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Gender
        {
            get { return _gender; }
            set { _gender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Gender)));
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                //if (columnName == "Value22" && this.Value22 == 200)
                //    //数据验证逻辑
                //    return "出错了哈";

                //获取属性
                var prop = this.GetType().GetProperty(columnName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (prop.IsDefined(typeof(NotValueAttribute), false))
                {
                    //获取特性对象
                    var attr = prop.GetCustomAttributes(typeof(NotValueAttribute), false)[0] as NotValueAttribute;
                    var (isEqual, msg) = attr.Do(prop.GetValue(this));
                    if (isEqual)
                        return msg;

                }

                return "";
            }
        }
    }

    public class NotValueAttribute : Attribute
    {
        public string Value { get; set; }
        public NotValueAttribute(string value)
        {
            Value = value;
        }

        public (bool,string) Do(object value)
        {
            if (value.ToString() == Value)
                return (true, $"字段不能为{Value}");
            return (false, "");
        }
    }


    
}
