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
using System.Windows.Shapes;

namespace WinDemo
{
    /// <summary>
    /// TempleteWin.xaml 的交互逻辑
    /// </summary>
    public partial class TempleteWin : Window
    {
        public static readonly DependencyProperty ValueAttachProperty;


        //定义，依赖属性必须定义在继承自DependencyObject的类里面
        public static readonly DependencyProperty ValueProperty;
        //注册
        static TempleteWin()
        {
            ValueAttachProperty = DependencyProperty.RegisterAttached("ValueAttach", typeof(int), typeof(TempleteWin),new PropertyMetadata(1,new PropertyChangedCallback(ValueChanged)));


            //ValueProperty = DependencyProperty.Register("Value", 
            //    typeof(int), 
            //    typeof(TempleteWin),
            //    new PropertyMetadata(0,
            //        new PropertyChangedCallback(ValueChanged),
            //        new CoerceValueCallback(CoerceValue)  //强制回调
            //        ),
            //    new ValidateValueCallback(ValidateValue));
            ValueProperty = DependencyProperty.Register("Value",
                typeof(int),
                typeof(TempleteWin),
                new FrameworkPropertyMetadata(0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(ValueChanged),
                    new CoerceValueCallback(CoerceValue)  //强制回调
                    ),
                new ValidateValueCallback(ValidateValue));
        }


        public int GetValueAttach(DependencyObject d)
        {
            return (int)d.GetValue(ValueAttachProperty);
        }

        public void SetValueAttach(DependencyObject d,object value)
        {
            d.SetValue(ValueAttachProperty, value);
        }

        /// <summary>
        /// 属性包装依赖属性
        /// </summary>
        public int Value
        {
            //只能通过getValue来获取依赖属性里面的值
            get { return (int)this.GetValue(ValueProperty); }
            //只能通过setvalue来设置依赖属性里面的值
            set { this.SetValue(ValueProperty, value); }
        }

        public TempleteWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 强制回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="obj">当前属性的最新值</param>
        /// <returns>属性的合法值</returns>
        static object CoerceValue(DependencyObject sender,object obj)
        {
            if (Convert.ToInt32(obj) > 1000)
                return 1000;
            return obj;
        }

        /// <summary>
        /// 属性值验证回调
        /// </summary>
        /// <param name="obj">属性将要写入的值</param>
        /// <returns>是否允许将值写入属性</returns>
        static bool ValidateValue(object obj)
        {
            if (Convert.ToInt32(obj) > 1000)
            {
                //验证失败
                return false;
            }
            return true;
        }

        /// <summary>
        /// 属性改变触发回调
        /// 触发条件：每次对属性进行重新赋值才会触发该回调
        /// </summary>
        /// <param name="sender">属性所在对象</param>
        /// <param name="e">变化动作中所关联的数据</param>
        static void ValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //变化之前的数据
            var oldValue = e.OldValue;
            //变化之后的数据
            var newValue = e.NewValue;
            //由哪个属性触发改变
            var targetProp = e.Property;
        }
    }

    public class ListViewItemTempleteSelector : DataTemplateSelector
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate WarnTemplate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">每个控件子项所对应的数据子项</param>
        /// <param name="container"></param>
        /// <returns>当前的这个子项可使用的数据模板</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var p = item as Person;
            if (p.Age > 21)
            {
                return WarnTemplate;
            }
            return NormalTemplate;
        }
    }

    public class ListViewItemContainerStyleSelector : StyleSelector
    {
        public Style NormalStyle { get; set; }
        public Style WarnStyle { get; set; }
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var p = item as Person;
            if (p.Age > 21)
            {
                return WarnStyle;
            }
            return NormalStyle;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }


    }
}
