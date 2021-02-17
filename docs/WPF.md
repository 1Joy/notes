# WPF(Windows Presentation Foundation)
获取c#编辑器参数：csc /? 
## 基本内容 
**功能** 

用来编写应用程序的表示层，如：WPF,Windows Form, Sliverlight等

**.xaml文件** 

 1. **概述** 

     XAML：可扩展应用程序标记语言。xaml是声明式语言，声明了某标签就会创建某对象 

     是什么：是WPF技术中专门用于UI设计的语言 

     优点：实现了UI与逻辑的分离
 
 2. **xaml代码**
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
     - 标签 

         空标签：```<Button Content="Add" Width="120" Height="60"/>``` 

         非空标签：```<Button Width="120" Height="60">Add</Button>``` 
     - 属性/特性 

         属性(Property)：属于面向对象范围，类中用来表示事物状态的成员就是属性 

         特性(Attribute)：只与语言层面上的东西相关，比如在类上添加特性，只会影响类在程序中的用法 
     - 命名空间(xmlns)

         XML文档上的标签使用xmlns特性来定义命名空间，语法格式：```xmlns:[可选的映射前缀]="名称空间"```或者```xmlns:[映射名]="clr-namespace:类库中的命名空间名字;assembly=类库文件名"```
         xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"：表示引用了一组相关的类库 

 3. **在XAML中为对象属性赋值**
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
                 /// <param name="value">以字符串形式赋值的value</param>
                 /// <returns></returns>
                 public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
                 {
                     string name = value.ToString();
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
         jhqwmn2<Path Data="M 0,0 L 200,100 L 100,200 Z" Stroke="AliceBlue" Fill="Red"></Path>
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
 
 4. **事件处理器与代码后置** 
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

     2. 代码后置(code-Behind) 

         定义：将逻辑代码与UI代码分离，隐藏在UI代码后面的形式叫做code-behind 

         如何实现code-behind：是因为.net支持partial部分类，并且能将解析的xaml文件所生成的代码与x:Class所指定的类进行合并。

## x命名空间
**x命名空间的由来和作用** 
 - 对x命名空间的声明：xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
 - 作用：用来解析和分析xaml文档
 - 使用：x:元素名
 1. **x名称空间中的Attribute**
     - x:Class 

         告诉XAML编译器将XAML标签的编译结果与后台代码中指定的类合并，使用x:Class要满足一下要求：

         - 位于根节点
         - 类型一致
         - x:Class所指示的类必须要有partial关键字
    
     - x:ClassModifier 

         告诉XAML编译器由标签编译生成的类的访问级别，使用x:ClassModifier要满足以下要求：

          - 标签必须要有x:Class
          - x:ClassModifier的值必须与x:Class所指示的类的访问级别一致
     - x:Name 

         XAML是一种声明式语言，标签声明的是对象，一个标签对应着一个对象，这个对象一般是一个空间类的实例；但是XAML只负责声明这个对象但不声明这些对象的引用变量，此时就需要x:Name。 

         作用： 

          - 告诉XAML编译器，当一个标签带有x:Name时除了声明这个标签对应的实例，还要声明一个引用变量，变量名就是x:Name的值
          - 如果标签对应的对象上有Name属性也设为X:Name的值，并且把这个值注册到UI树上，方便查找
     - x:FieldModifier 

         用来改变标签实例的引用变量的访问级别的(默认为internal)，使用x:FieldModifier要满足以下要求：
          - 必须要使用x:Name或者Name属性，让其生成引用变量
     - x:Key 

         为资源添加上用于检索的索引
 2. **x名称空间的标记扩展** 

     - x:Type 

         为属性指定数据类型 

         ```<local:MyButton Content="show" UseWindowType="{x:Type TypeName=local:MyWindow}"/>```
     - x:Null 

         为属性显示的赋一个空值 

         ```让按钮没有Style样式：<Button Style="{x:Null}">btn2</Button>```
     - x:Static
         使用静态的属性或字段 

         ```<Button Style="{x:Null}" Content="{x:Static local:MainWindow.show}"/>```

## 控件与布局
 1. **控件** 

     定义：WPF里面把那些能够展示数据、响应用户操作的UI元素称为控件 

     分类：

     - 布局控件：可容纳多个控件或嵌套其他布局控件，用于在UI上组织和排列控件，如Grid。基类：Panel
     - 内容控件


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
