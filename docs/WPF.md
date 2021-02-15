# WPF
获取c#编辑器参数：csc /? 
## 基本内容 
**.xaml文件**  
  ``` 
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        
    </Grid>
</Window> 
  ``` 
  xaml是声明式语言，声明了某标签就会创建某对象 

  xmlns: 表示引用的命名空间 

  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"：表示引用了一组相关的类库 

  ## 在XAML中为对象属性赋值 
  1. Attribute=Value形式 

     - 为什么Attribute是以字符串的形式赋值? 

        因为WPF内部存在这一个类型转换，将自定义的类型转换类继承自TypeConverter,再通过特性添加到指定的类上面完成以字符串赋值的转换。 
        ```
        [TypeConverter(typeof(NameToHumanTypeConverter))]
        public class Human {
            public string Name { get; set; }
            public Human Child { get; set; }
        }

        public class NameToHumanTypeConverter : TypeConverter
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            /// <param name="culture"></param>
            /// <param name="value">以字符串形式赋值的value</param>
            /// <returns></returns>
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                string name = value.ToString();
                Human child = new Human();
                child.Name = name;
                return child;
                //return base.ConvertFrom(context, culture, value);
            }
        }
        //使用
        <local:Human x:Key="human" Name="Joy" Child="childjoy"/>
        ```


     ``` 
     <!--鼠标起始位置在0,0，用直线连接(200,100)、(100,200)，在闭合-->
     <Path Data="M 0,0 L 200,100 L 100,200 Z" Stroke="AliceBlue" Fill="Red"></Path>
     ```  
     
  2. 属性标签 
  
     形如：<类名.属性名></类名.属性名>  
     为什么要用：为了解决为属性赋予较复杂的内容 
     ```
     <Button>
        <!--button标签的内容-->
        <Button.Content>
            <!--为content属性赋值,Button.Content:叫做属性标签(类名.属性名)-->
            <Rectangle Width="20" Height="20" Fill="AliceBlue"></Rectangle>
        </Button.Content>
     </Button>
     <!--用属性标签填充渐变颜色-->
     <Rectangle Width="100" Height="100">
        <Rectangle.Fill>
            <!--StartPoint="0,0" EndPoint="1,1":起始点矩形的左上角到右下角-->
            <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
                <LinearGradientBrush.GradientStops>
                    <GradientStopCollection>
                        <GradientStop Offset="0.2" Color="LightBlue"/>
                        <GradientStop Offset="0.7" Color="DarkBlue"/>
                        <GradientStop Offset="1" Color="Blue"/>
                    </GradientStopCollection>
                </LinearGradientBrush.GradientStops>
            </LinearGradientBrush>
        </Rectangle.Fill>
     </Rectangle>
     ```
  3. 标签扩展 

     bing、StaticResource等等。
  ## 事件处理器与代码后置 

  1. 事件处理器 

     - 事件的拥有者：拥有事件的对象
     - 事件的响应者：订阅事件的对象
     - 事件订阅：响应者将事件的处理函数绑定到事件
     ```
     <!--
     事件的拥有者：button
     事件的处理者：Window
     事件订阅：Click="Button_Click"
     事件：Click
     -->
     <Button Click="Button_Click">click me</Button>
     ```

  2. 代码后置

  ## x命名空间
  1. x命名空间的由来和作用 

     - 对x命名空间的声明：xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
     - 作用：用来解析和分析xaml文档
     - 使用：x:元素名

  2. x命名空间内容
  3. x:Class：用来声明类，指定xaml文件生成的类与哪个类合并
  4. x:ClassModifier：声明类的访问级别
  5. x:Name：为xaml创建的实例生成引用变量；用变量的名字给实例的name属性赋值；
  6. x:FieldModifier

  # MVVM(Model-View-ViewModel)
  ## 使用MVVM好处
  - 团队层面：统一思维方式和实现方法
  - 架构层面：稳定、解耦(UI与业务逻辑分离)
  - 代码层面：可读、可测、可替换
  ## Model、View、ViewModel
  - Model：现实世界中对象的抽象结果，如：学生类
  - View：UI
  - ViewModel：Model for View，是一个view的抽象结果 

     view和viewmodel的沟通: 

     传递数据——数据属性
     ```
     /// <summary>
     /// 自定义类
     /// 用于通知View和ViewModel，主要用于做数据传输并且是所有viewmodel的基类
     /// 实现INotifyPropertyChanged接口
     /// </summary>
     class NotificationObject : INotifyPropertyChanged
     {
        //view上通过Binding绑定了viewmodel的属性，属性发生变化了之后就会通过这个事件将变化后的值发送到view上面
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 自定义响应事件
        /// </summary>
        /// <param name="propertyName">发生变化的属性名称</param>
        public void RaisePropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                //通知binding哪个属性发生了变化
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
     }

     class MainWindowViewModel:NotificationObject
     {
        /// <summary>
        /// 声明与view相关的数据属性
        /// </summary>
        private double input1;

        public double Input1
        {
            get { return input1; }
            set
            {
                input1 = value;
                //通知view的binding的某属性改变
                RaisePropertyChange("Input1");
            }
        }
     }

     <Slider x:Name="slider1" Value="{Binding Input1}" Grid.Row="0" Background="LightBlue" Minimum="-100" Maximum="100" Margin="4"/>
     ``` 

     传递操作——命令属性 
     ```
     /// <summary>
     /// 自定义类
     /// 主要通过binding传递命令操作
     /// </summary>
     class DelegateCommand : ICommand
     {
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 要执行命令的对象；判断命令能否执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null ? true : CanExecuteFunc.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAction.Invoke(parameter);
        }

        public Action<object> ExecuteAction { get; set; }
        public Func<object,bool> CanExecuteFunc { get; set; }
     }

     public MainWindowViewModel()
     {
        AddCommand = new DelegateCommand();
        SaveCommand = new DelegateCommand();
        AddCommand.ExecuteAction += Add;
        SaveCommand.ExecuteAction += Save;
     }

     <Button x:Name="addButton" Command="{Binding AddCommand}" Grid.Row="3" Content="Add" Width="120" Height="60"/>
     ```

     **Binding：如果在Binding的时候没有指定source，就会在自身对象或者上级对象上面去找DataContext属性**
