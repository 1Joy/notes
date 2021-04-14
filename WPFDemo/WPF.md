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
     - 内容控件：只能容纳一个其他控件或布局控件作为内容，需要借助布局控件来规划内容。基类：ContentControl
     - 带标题内容控件：相当于一个内容控件，但是可以加一个标题(Header)，标题部分可容纳一个布局或布局。基类：HeaderedContentControl
     - 条目控件：显示一列数据，一般情况下数据类型相同。基类：ItemsControl
     - 带标题条目控件：相当于一个条目控件加上一个标题显示区，如：TreeViewItem。基类：HeaderedItemsControl

 2. **WPF内容模型**  

     控件相当于一个容器，里面的东西就是控件的内容，其内容可以是数据也可以是控件。如果是控件就形成了控件的嵌套(子控件)。UI布局允许控件嵌套，所以会形成一个树形结构。 

     逻辑树：如果不考虑控件内部的组成结构，只观察有控件组成的树，这成为逻辑树 

     可视元素树：WPF控件往往是更基本的控件构成，即控件本身就是一棵树，如果考虑控件本身的树，这颗树就称为可视元素树

     把符合基类内容模型的UI元素成为一个族，每个族用他们的共同基类来命令
     - ContentControl族 

         特点：

         - 均派生自ContentControl类
         - 都是控件(Control)
         - 内容属性的名称为Content
         - 只能由单一元素充当其内容
         ```
          <Button>btn1</Button>
         ```

     - HeaderedContentControl族 

         特点： 

         - 都派生自HeaderedContentControl类，HeaderedContentControl是ContentControl类的派生类
         - 都是控件，用于显示带标题的数据
         - 除了用于显示主题内容的区域外，控件还具有一个显示标题的区域
         - 内容属性为Content和Header
         - content和Header都只能容纳一个元素
         ```
         <GroupBox Margin="5">
            <GroupBox.Header>Header</GroupBox.Header>
            <TextBlock Text="content"/>
         </GroupBox>
         ```

     - ItemsControl族 

         特点： 

         - 均派生自ItemsControl类
         - 都是控件，用于显示列表化的数据
         - 内容属性为Items和ItemsSource
         - 每种ItemsControl都对应有自己的条目内容(item container)

         ItemControl族里面，不论把什么数据集合交给ListBox，都会用一个ListBoxItem进行自动包装，ListBoxItem就是ListBox的条目内容。
         ```
         <!--传统的listbox只能将条目以字符串的形式显示，WPF里面的处理把条目用字符串形式显示，还可以用其他形式显示，如checkbox-->
         <ListBox Margin="5">
            <CheckBox Content="joy"/>
            <Button Content="joy"/>
            <RadioButton Content="joy"/>
            <ListBoxItem>
                <Button Content="joy"/>
            </ListBoxItem>
         </ListBox>
         ```
     - HeaderedItemsControl族 

         特点： 

         - 均派生自HeaderedItemsControl类
         - 具有ItemControl的特点，还可以显示一个标题
         - 内容属性为Items、ItemsSource、Header

         只有三个控件：MenuItem、TreeViewItem、ToolBar

     - Decorator族 

         此族中的元素是在UI上起装饰效果的。 

         特点：

         - 均派生自Decorator类
         - 起UI装饰作用
         - 内容属性为Child
         - 只能由单一元素充当内容
         ```
         <Border>
            <TextBlock Text="joy"/>
         </Border>
         ```

     - Shape族元素 

         特点：
         - 均派生自Shape类
         - 用于2D图形绘制
         - 他们不是控件
         - 没有内容属性，只能使用Fill属性填充，使用Stroke属性设置边线

     - Panel族元素 

         特点：
         - 均派生自Panel抽象类
         - 主要功能是控制UI布局
         - 内容属性为Children
         - 内容可以有多个元素，Panel元素将控制他们的布局

         panel元素于ItemsControl元素的区别： 

         - 相同点：内容都能容纳多个元素
         - 不同点：
           
             1. ItemsControl强调以列表的形式来显示数据，panel强调对包含在其中的元素进行布局
             2. ItemsControl的内容属性是Items和ItemsSource，Panel的内容属性是Children 

 3. **UI布局(Layout)**

     - 布局元素 
         - Grid：网格，以网格的形式对内部元素进行布局
         - StackPanel：栈式面板，将包含的元素按竖直或者水平方向上排成直线，移除一个元素后，后面的元素会自动向前移动填充空位
         - Canvas：画布，内部元素可以使用像素为单位的绝对坐标定位
         - DockPanel：停靠式面板
         - WrapPanel：自动折行面板，内部元素在排满一行后自动换行
     - Grid

         特点：

         - 可以定义任意数量的行和列
         - 行的高度和列的宽度可以使用数值、相对比例或自动调整，并支持最大最小值
         - 内部元素自己定义所在的行列，可跨行、跨列
         - 可设置内部元素的对齐方向

         适用场合：

         - UI布局的大框架设计
         - 大量的UI元素需要成行或成列对齐的情况
         - UI整体尺寸改变时，元素需要保持固定的高度或宽度比例
         - UI后期可能有较大的变更或扩展

         定义Grid的行和列
         ```
         <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="30px"/> //像素单位,默认单位
                <RowDefinition Height="0.5in"/> //英寸单位
            </Grid.RowDefinitions>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="1cm"/> //厘米单位
                <ColumnDefinition Width="30pt"/> //点单位
            </Grid.ColumnDefinitions>
         </Grid>

         //通过代码控制
         this.grid.RowDefinitions.Add(new RowDefinition());
         this.grid.ColumnDefinitions.Add(new ColumnDefinition());
         ```
         设置行高和列宽的三种方式： 

         - 绝对值：double数值加单位
         - 比例值：double数值后加*
         - 自动值：Auto

     - StackPanel 

         适用场合： 

         - 同类元素需要紧凑排列
         - 移除其中的元素后能够自动补缺的布局或动画

         使用以下几个属性来控制元素布局：
         - Orientation：Horizontal/Vertical，决定内部元素是横向还是纵向
         - HorizontalAlignment：Left/Right/Center/Stretch，决定内部元素水平方向上的对齐方式
         - VerticalAlignment：Top/Center/Buttom/Stretch，决定内部元素竖直方向上的对齐方式
     - Canvas
       
         适用场合：

         - 设计过后就基本不会再有改动的小型布局
         - 艺术性较强的布局
         - 需要大量使用横纵坐标进行绝对点定位的布局
         - 依赖于横纵坐标的动画

         ```
         <Canvas>
            <TextBlock Text="用户名：" Canvas.Left="12" Canvas.Top="12"/>
            <TextBox Height="23" Width="200" Canvas.Left="66" Canvas.Top="9"/>
         </Canvas>
         ```
     - DockPanel

         几个重要属性：
         - **Dock：** DockPanel内的元素会被附加上DockPanel.Dock属性（Left,Top,Right,Buttom），根据Dock属性值，DockPanel内的元素会向指定方向累积，切分DockPanel内部的剩余空间
         - **LastChildFill：** 默认值为**True**；LastChildFill=True：DockPanel内的最后一个元素的Dock属性会被忽略，这个元素会填满剩余的空间
     - WrapPanel

         WrapPanel内部采用的是流式布局

         几个重要属性：
         - **Orientation：** 控制流延伸的方向
         - **HorizontalAlignment：** 指定水平对齐方式
         - **VerticalAlignment：** 指定垂直对齐方式
         






## Binding

 程序是被来自UI的事件(封装后的消息)驱使向前的，简称“消息驱动”或“事件驱动”。因为消息和事件大都来自于UI，所以统称他们为“UI驱动程序”

 程序逻辑层将数据发送给用户界面显示出来的方式叫做“数据驱动UI”

 这种方式让我们的数据(程序的核心)处于被动地位，要让数据回归到核心，需要用到data Binding

 **Bing基础**

 把binding比作数据的桥梁，那么它的两端分别是binding的源(source)和目标(target)

 一般情况下，binding源是逻辑层对象，目标是UI层控制对象，如此数据就会被从源发送到UI层，被UI层展示，同时也就完成了**数据驱动UI**的过程
 ```
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
 ```

 **Binding的源与路径**

 数据源：是一个对象，并且通过属性公开自己的数据，就能作为数据源

   1. **把控件作为数据源与binding标记扩展** 
        ```
         <StackPanel>
            <Slider x:Name="slider1" Maximum="100" Minimum="-100"/>
            <!--binding的标记扩展语法-->
            <TextBox x:Name="textBox1" Text="{Binding Path=Value,ElementName=slider1}"/>
         </StackPanel>
        ```
   2. **控制Binding的方向及数据更新**

         Binding在源与目标之间架起了沟通的桥梁，默认情况下数据既能通过binding送达到目标，也能够从目标返回数据源

         控制binding数据流向的属性**Mode**:
         - BindingMode.TwoWay：导致对源属性或目标属性的更改可自动更新对方。此绑定类型适用于可编辑窗体或其他完全交互式 UI 方案
         - BindingMode.OneWay：当绑定源（源）更改时，更新绑定目标（目标）属性
         - BindingMode.OnTime：仅当应用程序启动时或 DataContext 进行更改时更新目标属性
         - BindingMode.OneWayToSource：当目标属性更改时更新源属性
         - BindingMode.Default：binding的模式根据目标的实际情况来确定，比如目标是可编辑的属性，采用双向模式；目标是只读的采用单向模式
   3. **Binding的路径(Path)**
         
         路径：来指定Binding要关注的属性，即在绑定时为Path赋的值就是路径

         path的几种使用方式：
         - 对象属性：```{Binding Path=Value,ElementName=slider1}```
         - 索引器：集合类型的索引器也叫做带参属性；
            ```
             <!--显示字符串里的第4个字符-->
             {Binding Path=Text.[3],ElementName=textBox1}
            ```
         - 集合或者DataView作为源时，也可以把它的默认元素当作Path使用：
            ```
             List<string> list = new List<string>(){"tim"}
             textBox1.setBinding(TextBox.TextProperty,new Binding("/"){Source=list});
            ```
        - 没有Path属性：
             这是一种特殊情况，binding源本身就是数据且不需要path来指明。string，int等基本类型就是这样，他们的实例本身就是数据，所以无法指定属性来访问，所以将Path设置为.或者隐藏
   4. **为Binding指定源的几种方法**
     
         - 把普通的CLR类型单个对象指定为源

             包括了.NET Framework自带类型的对象和用户自定义类型的对象，如果类型实现了INotifyPropertyChanged接口，则可通过在属性的set语句里激发PropertyChanged事件来通知Binding数据已被更新

         - 把普通的CLR集合类型对象指定为源

             包括数组、List<T>、ObservableCollection<T>等集合类型；我们经常需要把一个集合作为ItemsControl源生类的数据源来使用，一般是把控件的ItemsSource属性使用Binding关联到一个集合对象上
             ```
             <TextBlock Text="Student ID:" Margin="5"/>
             <TextBox x:Name="textBoxId" Margin="5"/>
             <TextBlock Text="Student List:" Margin="5"/>
             <ListBox x:Name="listBoxStudents" Height="110" Margin="5"/>
             ```


             List<Student> students = new List<Student>() {
             new Student(){Id=0,Name="joy",Age=24},
             new Student(){Id=1,Name="joy1",Age=24},
             new Student(){Id=2,Name="joy2",Age=24},
             new Student(){Id=3,Name="joy3",Age=24},
             new Student(){Id=4,Name="joy4",Age=24},
             };
    
             listBoxStudents.ItemsSource = students;
             //这个属性被赋值之后，ListBoxItem以此属性值为Path创建binding
             listBoxStudents.DisplayMemberPath = "Name";
    
             textBoxId.SetBinding(TextBox.TextProperty, new Binding("SelectedItem.Id") { Source = listBoxStudents });
             ```
    
         - 把ADO.NET数据对象指定为源
    
             包括DataTable、DataView对象
    
         - 把依赖对象指定为源
    
             依赖对象不仅可以作为Binding的目标，同时也可以作为Binding的源。这样就有可能形成Binding链，依赖对象的依赖属性可以作为Binding的Path
    
         - 把容器的DataContext指定为源(WPF 数据绑定的默认行为)
    
             只给绑定设置路径，不设置源；绑定就会自己去寻找源，binding会自动把控件的DataContext当作自己的源(沿着控件树一层一层向外找，直到找到带有路径指定的属性对象为止)
             ```
             <StackPanel>
                <StackPanel.DataContext>
                    <local:Student Name="joy"/>
                </StackPanel.DataContext>
                <TextBox Text="{Binding Name}"/>
             </StackPanel>
             ```
             使用场景：
             - 当UI上的多个控件都使用Binding关注同一个对象时，可以使用DataContext
             - 当作为源的对象不能直接被访问
    
         - 通过ElementName指定源
    
             指定控件元素的x:Name作为源
    
         - 把ObjectDataProvider对象指定为源
    
             当数据源的数据不是通过属性而是方法暴露给外界，可用这两种对象来包装数据源在指定为源
    
         - 把使用LINQ检索得到的数据对象作为源


​            
   5. 

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
     
     

# Prism框架

## 搭建项目

- 不使用模板创建

  1. 新建WPF项目，添加Prism.DryIoc的Nuget包

  2. 修改App.xaml.cs的父类为PrismApplication，并实现函数

     ```
     		/// <summary>
             /// 创建程序主窗口
             /// </summary>
             /// <returns></returns>
             protected override Window CreateShell()
             {
                 return Container.Resolve<MainWindow>();
                 //throw new NotImplementedException();
             }
     
             /// <summary>
             /// 注册类型
             /// </summary>
             /// <param name="containerRegistry"></param>
             protected override void RegisterTypes(IContainerRegistry containerRegistry)
             {
                 //throw new NotImplementedException();
             }
     ```

     

  3. 修改App.xaml文件

     ```
     <prism:PrismApplication x:Class="PrismDemo.App"
                  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                  xmlns:local="clr-namespace:PrismDemo"
                  xmlns:prism="http://prismlibrary.com/">
         <Application.Resources>
              
         </Application.Resources>
     </prism:PrismApplication>
     ```

     

- 使用Prism Template创建项目

## Region 区域

- 定义

  是Prism模块化的核心功能，主要是为了弱化模块与模块之间的耦合关系

  定义Region有两种方式，一个是在XAML中指定，一个是在代码中指定

  ```
  //xaml中指定
  <ContentControl prism:RegionManager.RegionName="ContentRegion" />
  
  //代码中指定
  <HeaderedContentControl x:Name="header"/>
  //相应的.cs文件的构造函数内定义
  RegionManager.SetRegionName(header, "HeaderRegion");
  
  //添加视图进区域
  public MainWindowViewModel(IRegionManager regionManager)
  {
  this.regionManager = regionManager;
  
  this.regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewContent));
  this.regionManager.RegisterViewWithRegion("HeaderRegion", typeof(ViewHeader));
  }
  ```

- RegionManager的作用

  1. 定义区域

  2. 维护区域集合

  3. 提供对区域的访问

     ```
     this.regionManager.Regions["ContentRegion"]
     ```

     

  4. 合成视图

  5. 区域导航

- 



