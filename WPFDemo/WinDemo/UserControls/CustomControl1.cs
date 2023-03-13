using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WinDemo.UserControls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WinDemo.UserControls;assembly=WinDemo.UserControls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name = "elementName",Type =typeof(RepeatButton))]
    [TemplateVisualState(Name ="Positive",GroupName ="ValueStates")]
    public class CustomControl1 : Control
    {
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }

        //这里处理自定义控件的逻辑
        //界面显示在：Themes/Generi.xaml里面

        private RepeatButton button;

        public RepeatButton MyProperty
        {
            get { return button; }
            set {
                if (button != null)
                {
                    //如果之前有按钮实例对象，就先将按钮点击事件移除
                   // button.Click -= new RoutedEventHandler(methodName);
                }
                button = value;
                if (button != null)
                {
                    //为按钮对象重新注册新的点击事件
                    // button.Click += new RoutedEventHandler(methodName);
                }
            }
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //在这里可以根据名称获取到界面上的控件（名字不可以修改）
            button = GetTemplateChild("elementName") as RepeatButton;
        }

        private void UpdateStates()
        {
            bool tag = false;
            //根据不同的条件对视觉状态进行触发

            //参数：哪个控件触发的，视觉状态的名字，是否触发转换
            VisualStateManager.GoToState(this, "Positive", tag);
        }
    }
}
