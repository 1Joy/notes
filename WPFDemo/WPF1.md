# 布局控件

所有容器都支持在子控件上设置**Panel.ZIndex**，值越大，元素显示越上层

1. grid

   - GridSplitter

     分割行或者列，并且使其可以任意拖动，改变其宽度或者高度；并且通过设置**Grid.IsSharedSizeScope=true**和设置**SharedSizeGroup**使其他行或者列可以共享一个GridSplitter

     分割列：

     ```
     <Grid>
                 <Grid.ColumnDefinitions>
                     <ColumnDefinition Width="30" SharedSizeGroup="A"/>
                     <ColumnDefinition Width="40"/>
                     <ColumnDefinition/>
                 </Grid.ColumnDefinitions>
                 <Border Background="Orange"/>
                 <!--grid伸缩列-->
                 <GridSplitter HorizontalAlignment="Right" VerticalAlignment="Stretch" Width="5" Background="Gray"/>
                 <Border Grid.Column="1" Background="Red"/>
             </Grid>
     ```

     共享列宽度：

     为要共享的列设置SharedSizeGroup，实现共享。

     ```
     <Grid Grid.IsSharedSizeScope="True">
                 <Grid.RowDefinitions>
                     <RowDefinition Height="30"/>
                     <RowDefinition/>
                 </Grid.RowDefinitions>
             <Grid>
                 <Grid.ColumnDefinitions>
                     <ColumnDefinition Width="30" SharedSizeGroup="A"/>
                     <ColumnDefinition Width="40"/>
                     <ColumnDefinition/>
                 </Grid.ColumnDefinitions>
                 <Border Background="Orange"/>
                 <!--grid伸缩-->
                 <GridSplitter HorizontalAlignment="Right" VerticalAlignment="Stretch" Width="5" Background="Gray"/>
                 <Border Grid.Column="1" Background="Red"/>
             </Grid>
             <Grid Grid.Row="1">
                 <Grid.ColumnDefinitions>
                     <ColumnDefinition SharedSizeGroup="A"/>
                     <ColumnDefinition Width="40"/>
                     <ColumnDefinition/>
                 </Grid.ColumnDefinitions>
                 <Border Background="Orange"/>
                 <Border Grid.Column="1" Background="Red"/>
             </Grid>
             </Grid>
     ```

     分割行：

     ```
     <Grid Grid.Column="1">
                 <Grid.RowDefinitions>
                     <RowDefinition Height="30"/>
                     <RowDefinition Height="40"/>
                 </Grid.RowDefinitions>
                 <Border Background="Blue"/>
                 <GridSplitter Height="5" Background="Red" VerticalAlignment="Bottom" HorizontalAlignment="Stretch"/>
                 <Border Grid.Row="1" Background="AliceBlue" />
             </Grid>
     ```

2. stackPanel/virtualizingStackPanel(虚拟化StackPanel，可以优化stackpanel)

   - stackPanel.order：控件顺序
   - orientation：布局显示的方向，水平/垂直

   两者的区别：stackPanel会渲染所有的子元素，virtualizingStackPanel如果子元素没有在界面中呈现就不会渲染

3. DockPanel

   - Dockpanel.Dock：停靠，Top,left(默认),right,bottom；停靠的是当前布局剩下的部分
   - lastChildFill：最后一个控件是否填充剩余控件；true/false

4. WrapPanel

   不能被grid替代，按行排列时尺寸不够会换行；按列排列尺寸不够时会换一列继续排

   - orientation：布局显示的方向，水平/垂直

5. uniformGrid

   自生成同意一致的行列

6. Canvas

   通过坐标位置放置子元素

   - 定位：Canvas.left/top/right/bottom

     ```
     <Canvas>
             <Button Content="button1" Canvas.Left="120"/>
             <Button Content="button2" Canvas.Right="200"/>
             <Button Content="button3" Canvas.Top="60"/>
             <Button Content="button4" Canvas.Bottom="300"/>
             <Button Content="button5"/>
         </Canvas>
     ```

     如果同时设置left和right，以left的值为准；同时设置top和bottom，以top为准

7. InkCanvas

   支持笔画输入

   - EditingMode：编辑模式

     - EditingMode=Ink：支持画笔输入

       ```
       <InkCanvas EditingMode="Ink">
                   <TextBlock Text="画笔"/>
               </InkCanvas>
       ```

     - EditingMode=select：支持选中并且拖动子元素

       ```
        <InkCanvas Grid.Column="1" EditingMode="Select">
                   <TextBlock Text="托拽"/>
                   <Button Content="button1"/>
                   <Button Content="button2"/>
                   <Button Content="button3"/>
               </InkCanvas>
       ```

     - EditingMode=GestureOnly：支持手势

   - InkCanvas.DefaultDrawingAttributes

     设置笔画的样式

     ```
     <InkCanvas EditingMode="Ink">
                 <InkCanvas.DefaultDrawingAttributes>
                     <DrawingAttributes Color="Blue" Width="10" Height="10"/>
                 </InkCanvas.DefaultDrawingAttributes>
                 <TextBlock Text="画笔"/>
             </InkCanvas>
     ```

8. Border

   装饰控件

   - CornerRadius：圆角
   - BorderBrush：边框颜色
   - BorderThinkness：边框宽度

9. ScrollViewer

   包裹在其他容器控件外，使其出现滑动块

10. groupBox

   分组

11. expander

# 窗体

window、page

1. 窗体属性：

   - WindowStyle：窗体样式

     - None：无边框
     - SingleBorderWindow：默认边框样式
     - ThreeDBorderWindow：3d
     - ToolWindow：只有关闭按钮的边框样式

   - WindowState：窗体显示最大，最小，正常

     在无边框使用窗体最大化时，需要先正常加载窗体，再设置最大化，否则会挡住任务栏部分。

   - AllowsTransparency：窗体是否允许透明，常与Background="Transparent"一起用

2. 无边框窗体

   - 通过windowStyle=none实现

   - 通过windowChrome实现

     - captionHeight：边框可捕获焦点的高度

     - UseAeroCaptionButtons：是否使用默认的关闭，最小化，最大化的控制按钮

     - WindowChrome.IsHitTestVisibleInChrome="True"：是否允许其他在边框区域的控件获取焦点

      ```
   <WindowChrome.WindowChrome>
           <WindowChrome CaptionHeight="50" 
                         UseAeroCaptionButtons="False"/>
       </WindowChrome.WindowChrome>
       <Grid>
           <Button WindowChrome.IsHitTestVisibleInChrome="True"
                   Height="50" Width="70" HorizontalAlignment="Right"
                   VerticalAlignment="Top"/>
       </Grid>
      ```

# 资源

资源的递归搜索：自身资源->父级资源->窗口资源->应用程序资源->框架系统资源

1. 文件资源

   图片，字体，音视频（必须复制到本地加载）

   引用：pack UPI

   pack://application:,,,/[程序集名称;]component/XXX/XXX/XXX.png

2. 对象资源

   报关一系列有用的对象，方便使用

   c#中的所有具有无参构造函数的对象都可以在资源中定义。

   ```
   <Window x:Class="WinDemo.LayoutWin"
           xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
           xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
           xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
           xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
           xmlns:local="clr-namespace:WinDemo"
           xmlns:sys="clr-namespace:System;assembly=System.Runtime"
           mc:Ignorable="d"
           Title="LayoutWin" Height="450" Width="800">
       <Window.Resources>
           <x:Array x:Key="datas" Type="sys:Int32">
               <sys:Int32>1</sys:Int32>
               <sys:Int32>2</sys:Int32>
               <sys:Int32>3</sys:Int32>
               <sys:Int32>4</sys:Int32>
           </x:Array>
       </Window.Resources>
       <Grid>
           <ItemsControl ItemsSource="{StaticResource datas}">
               <ItemsControl.ItemTemplate>
                   <DataTemplate>
                       <Button Content="{Binding}"></Button>
                   </DataTemplate>
               </ItemsControl.ItemTemplate>
           </ItemsControl>
       </Grid>
   </Window>
   ```

   大部分控件都有Resources属性，都可以定义资源。且查找顺序都是向上查找，除非显示的指定使用哪个资源。

   ```
   <Window x:Class="WinDemo.LayoutWin"
           xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
           xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
           xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
           xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
           xmlns:local="clr-namespace:WinDemo"
           xmlns:sys="clr-namespace:System;assembly=System.Runtime"
           mc:Ignorable="d"
           Title="LayoutWin" Height="450" Width="800">
       <Window.Resources>
           <sys:Double x:Key="value">100</sys:Double>
       </Window.Resources>
       <Grid>
           <Border Height="{StaticResource value}" Background="Red">
               <Border.Resources>
                   <sys:Double x:Key="value">100</sys:Double>
               </Border.Resources>
               <Border.Width>
                   <StaticResource ResourceKey="value"></StaticResource>
               </Border.Width>
           </Border>
       </Grid>
   </Window>
   
   此时会显示一个高为100（向上查找到了window所定义的名为value的资源，并没有使用自身定义的资源），宽为200（宽度的指定写在了border资源定义的后面，所以向上查找查找到了自身）的红色矩形。
   资源查找的顺序是根据界面加载的顺序（从上到下的加载）来的
   ```

3. 静态资源和动态资源

   静态资源-StaticResource：程序编译时确定，程序编译后就生成baml就已经确定

   动态资源-DynamicResource ,：运行时可以监听资源的变化，并且修改可以生效

   

4. 资源字典

   资源字典合并(引用多个资源字典)：

   如果多个资源文件里面有同名的资源，那么生效的为最后引用的对象

   ```
   <Application x:Class="WinDemo.App"
                xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:local="clr-namespace:WinDemo"
                StartupUri="LayoutWin.xaml">
       <Application.Resources>
           <ResourceDictionary>
               <ResourceDictionary.MergedDictionaries>
                   <ResourceDictionary Source="pack://application:,,,/WPFDemo;component/Asset/xxx/ResourceDictionaryFile1.xamls"/>
                   <ResourceDictionary Source="pack://application:,,,/WPFDemo;component/Asset/xxx/ResourceDictionaryFile2.xamls"/>
               </ResourceDictionary.MergedDictionaries>
           </ResourceDictionary>
       </Application.Resources>
   </Application>
   ```

# 样式

组织和重用格式化选项的一种工具

样式优先级：在控件上设置样式优先级最高

1. style

   在一定作用范围内会影响对应的目标控件样式

   1. 属性

      - setters

        为控件的某一属性设置值
   
        - property：目标属性
        - value：为属性设置的值
        
      - resources
      
      - triggers：根据条件触发
      
        如果触发器触发时修改的样式属性与控件本身定义的触发器修改了同样的样式属性，可能会调用控件自身所定义的触发器（这个前提是没有修改控件的样式模板）
      
         触发的顺序根据触发定义的顺序进行触发
        
        - MultiTrigger：多条件同时触发
        
          ```
          <Style.Triggers>
                               <MultiTrigger>
                                   <MultiTrigger.Conditions>
                                       <Condition Property="IsMouseOver" Value="True"/>
                                       <Condition Property="IsPressed" Value="True"/>
                                   </MultiTrigger.Conditions>
                                   <MultiTrigger.Setters>
                                       <Setter Property="Foreground" Value="Red"/>
                                   </MultiTrigger.Setters>
                               </MultiTrigger>
                           </Style.Triggers>
          ```
        
          
        
        - dataTrigger
        
        - MultiDataTrigger
        
        - EventTrigger
        
          对应事件触发时的动画操作
        
          ```
          <Style x:Key="ButtonStyle" TargetType="{x:Type Button}">
                      <Setter Property="Background" Value="Green"/>
                      <Style.Triggers>
                          <MultiTrigger>
                              <MultiTrigger.Conditions>
                                  <Condition Property="IsMouseOver" Value="True"/>
                                  <Condition Property="IsPressed" Value="True"/>
                              </MultiTrigger.Conditions>
                              <MultiTrigger.Setters>
                                  <Setter Property="Foreground" Value="Red"/>
                              </MultiTrigger.Setters>
                          </MultiTrigger>
                          <EventTrigger RoutedEvent="Button.Click">
                              <BeginStoryboard>
                                  <Storyboard>
                                      <DoubleAnimation Duration="0:0:1" To="300" Storyboard.TargetProperty="Width"/>
                                  </Storyboard>
                              </BeginStoryboard>
                          </EventTrigger>
                      </Style.Triggers>
                  </Style>
                  
           <Window.Triggers>
                  <EventTrigger RoutedEvent="Button.Click" SourceName="Btn">
                      <BeginStoryboard>
                          <Storyboard>
                              <DoubleAnimation Duration="0:0:1" To="300" Storyboard.TargetName="Btn"
                                               Storyboard.TargetProperty="Width"/>
                          </Storyboard>
                      </BeginStoryboard>
                  </EventTrigger>
              </Window.Triggers>
          ```
        
          
        
          - RoutedEvent：控件的事件
          - SourceName：要触发的目标控件
          - Storyboard.TargetProperty：事件触发时要改变的控件属性
          - Storyboard.TargetName：修改属性的目标控件
          - Duration：动画花费的时间，时:分:秒
        
        - 
        
      - targetType：指定style作用于什么类型的控件
      
      - baseOn：style的继承，必须使用staticResource来继承style
   
2. 模板

   控制控件的显示形式

   必须为模板指定key，只有使用了key的控件才会使用该模板

   Templete属性定义在Control类中；control继承于FrameworkElement；

   - contentPresenter：内容占位，在控件模板内会自动显示控件内容部分的内容
   - itemsPresenter：
   - scrollContentPresenter：
   - TempleteBinding：绑定方向（从控件到模板）

   模板选择器：

   

   模板种类：

   1. ControlTemplete：控件模板

      控件模板显示在数据模板的上层

   2. ItemsPanelTemplete-子项容器模板

      控制容器内的子项如何排列

      ```
      <ItemsControl ItemsSource="{StaticResource datas}">
                  <ItemsControl.ItemsPanel>
                      <!--控制集合控件里的子元素存放在什么布局容器里面-->
                      <ItemsPanelTemplate>
                          <UniformGrid Columns="2"/>
                      </ItemsPanelTemplate>
                  </ItemsControl.ItemsPanel>
                  ....
              </ItemsControl>
      ```

      ```
      <ListView ItemsSource="{StaticResource datas}">
                  <ListView.ItemsPanel>
                      <ItemsPanelTemplate>
                          <Canvas/>
                      </ItemsPanelTemplate>
                  </ListView.ItemsPanel>
                  <ListView.ItemContainerStyle>
                      <Style TargetType="ListViewItem">
                          <Setter Property="Canvas.Left" Value="{Binding Left}"/>
                          <Setter Property="Canvas.Top" Value="{Binding Top}"/>
                      </Style>
                  </ListView.ItemContainerStyle>
              </ListView>
      直接对每个item的容器修改Canvas属性，而不是直接对DataTemplete内的元素修改canvas属性，是因为在dataTemplete中定义的元素与Canvas之间还有一个容器控件包裹，所以需要在这一级上操作canvas的属性
      ```

      > 布局容器与子元素之间的层级：UniformGrid->中间容器层（ContentPresenter/ListViewItem等等）->DataTemplete里面定义的内容

   3. DataTemplete-数据模板

      控制容器里的子项数据如何呈现

      ```
      <ItemsControl ItemsSource="{StaticResource datas}">
                  <ItemsControl.ItemTemplate>
                      <DataTemplate>
                          <WrapPanel>
                              <TextBlock Text="{Binding Name}" Margin="5,0"/>
                              <TextBlock Text="{Binding Age}"/>
                          </WrapPanel>
                      </DataTemplate>
                  </ItemsControl.ItemTemplate>
              </ItemsControl>
      ```

      dataTrigger：数据触发器，当数据的某个值等于指定条件时触发

      itemContainer样式选择器：根据条件渲染某样式

      >**步骤1**：创建一个类继承StyleSelector，并重写SelectStyle函数
      >
      >```c#
      >public class ListViewItemContainerStyleSelector : StyleSelector
      >    {
      >        public Style NormalStyle { get; set; }
      >        public Style WarnStyle { get; set; }
      >        public override Style SelectStyle(object item, DependencyObject container)
      >        {
      >            var p = item as Person;
      >            if (p.Age > 21)
      >            {
      >                return WarnStyle;
      >            }
      >            return NormalStyle;
      >        }
      >    }
      >```
      >
      >**步骤2：**定义待选择的style资源
      >
      >```xaml
      ><Window.Resources>
      >        <Style x:Key="NormalStyle" TargetType="ListViewItem">
      >            <Setter Property="FontSize" Value="20"/>
      >            <Setter Property="Foreground" Value="Green"/>
      >            <Setter Property="Template">
      >                <Setter.Value>
      >                    <ControlTemplate TargetType="ListViewItem">
      >                        <WrapPanel>
      >                            <TextBlock Text="{Binding Name}"/>
      >                            <TextBlock Text="{Binding Age}"/>
      >                        </WrapPanel>
      >                    </ControlTemplate>
      >                </Setter.Value>
      >            </Setter>
      >        </Style>
      >
      >        <Style x:Key="WarnStyle" TargetType="ListViewItem" BasedOn="{StaticResource NormalStyle}">
      >            <Setter Property="Foreground" Value="Red"/>
      >        </Style>
      >    </Window.Resources>
      >```
      >
      >**步骤3：**在集合控件中指定使用的样式选择器，设置样式模板
      >
      >```xaml
      > <ListView ItemsSource="{StaticResource datas}">
      >         <ListView.ItemContainerStyleSelector>
      >             <local:ListViewItemContainerStyleSelector NormalStyle="{StaticResource NormalStyle}"
      >                                                       WarnStyle="{StaticResource WarnStyle}"/>
      >         </ListView.ItemContainerStyleSelector>
      >     </ListView>
      >```

      数据模板选择器：根据数据选择性的渲染某数据模板

      > 步骤1：创建一个类继承DataTemplateSelector，并重写SelectTemplate函数
      >
      > ```c#
      > public class ListViewItemTempleteSelector : DataTemplateSelector
      >     {
      >         public DataTemplate NormalTemplate { get; set; }
      >         public DataTemplate WarnTemplate { get; set; }
      >         /// <summary>
      >         /// 
      >         /// </summary>
      >         /// <param name="item">每个控件子项所对应的数据子项</param>
      >         /// <param name="container"></param>
      >         /// <returns>当前的这个子项可使用的数据模板</returns>
      >         public override DataTemplate SelectTemplate(object item, DependencyObject container)
      >         {
      >             var p = item as Person;
      >             if (p.Age > 21)
      >             {
      >                 return WarnTemplate;
      >             }
      >             return NormalTemplate;
      >         }
      >     }
      > ```
      >
      > 步骤2：定义待选择的数据模板资源
      >
      > ```c#
      > <Window.Resources>
      >         <DataTemplate x:Key="NormalTemp">
      >             <WrapPanel TextBlock.Foreground="Green">
      >                 <TextBlock Text="{Binding Name}"/>
      >                 <TextBlock Text="{Binding Age}"/>
      >             </WrapPanel>
      >         </DataTemplate>
      >         <DataTemplate x:Key="WarnTemp">
      >             <WrapPanel TextBlock.Foreground="Red">
      >                 <TextBlock Text="{Binding Name}"/>
      >                 <TextBlock Text="{Binding Age}"/>
      >             </WrapPanel>
      >         </DataTemplate>
      >     </Window.Resources>
      > ```
      >
      > 步骤3：在集合控件中指定使用的模板选择器，设置数据模板
      >
      > ```xaml
      > <ListView ItemsSource="{StaticResource datas}">
      >        <ListView.ItemTemplateSelector>
      >            <local:ListViewItemTempleteSelector NormalTemplate="{StaticResource NormalTemp}"
      >                                                WarnTemplate="{StaticResource WarnTemp}"/>
      >        </ListView.ItemTemplateSelector>
      >    </ListView>
      > ```
      >
      > 

      


# 属性

1. 依赖属性

   - 作用：

     1. 做数据绑定，相当于订阅和发布功能
     2. 动画效果，样式修改

   - 特点

     1. 可以被绑定
   2. 变化可以被通知
     
   - 使用

     1. 定义

        ```c#
        //定义，依赖属性必须定义在继承自DependencyObject的类里面
        public static readonly DependencyProperty ValueProperty;
        ```

     2. 注册依赖属性

        ```c#
        //(依赖属性注册的名字，属性的类型，属性所在的类)
        ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(TempleteWin));
        
        //或者给出默认值
        ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(TempleteWin),new PropertyMetadata(0));
        ```

     3. 包装依赖属性：便于操作

        ```c#
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
        ```
        
     4. 依赖属性变化的回调

        触发顺序：验证回调->强制回调(可以认为是在前面的验证回调之后再增加一个验证并且可以将非法的值修改为合法的值)->属性变化回调

        - 属性变化回调：propertyChangedCallback

          **回调触发的条件：对目标属性重新进行赋值才能触发变化，如果依赖属性是一个list集合，调用其.add().remove()函数均不会触发该回调，因为不是对依赖属性重新进行赋值**

          ```c#
          ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(TempleteWin),new PropertyMetadata(0,new PropertyChangedCallback(ValueChanged)));
          
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
          ```

        - 验证回调

          **验证失败之后，值不能写入属性**

          ```c#
          ValueProperty = DependencyProperty.Register("Value", typeof(int), 
                                                      typeof(TempleteWin),new PropertyMetadata(0,newPropertyChangedCallback(ValueChanged)),
                                                      new ValidateValueCallback(ValidateValue));
          
          
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
          ```

        - 强制回调

          当验证失败的时候，强制修改为合法的值。

          ```c#
          ValueProperty = DependencyProperty.Register("Value", 
                                                      typeof(int), 
                                                      typeof(TempleteWin),
                                                      new PropertyMetadata(0, 
                                                      new PropertyChangedCallback(ValueChanged),
                                                      new CoerceValueCallback(CoerceValue)  //强制回调
                                                      ),
                                                      new ValidateValueCallback(ValidateValue));
          
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
          ```

     5. 依赖属性元数据信息

        FrameworkPropertyMetadata

        > FrameworkPropertyMetadata.FrameworkPropertyMetadataOptions参数说明：
        >
        > AffectsArrange、AffectsMeasure、AffectsParantArrange、AffectsParentMeasure:属性变化的时候，需要通知容器、父容器进行重新测量和排列。
        >
        > AffectsRender：属性值变化导致元素进行重新渲染、重新绘制
        >
        > BindsTwoWayByDefault：默认情况以双向绑定的方式处理绑定行为
        >
        > Inherits：依赖属性继承，如： fontsize，父对象进行这个属性设置的时候，会对子对象进行相同影响
        >
        > IsAnimationProhibited：这个属性不能用于动画
        >
        > IsNotDataBindable：不能使用表达式设置依赖属性
        >
        > Journal：page开发，属性的值会被保存到日志
        >
        > SubPropertiesDoNotAffectRender：对象属性子属性变化时，不去重新渲染对象
        >
        > DefaultValue：默认值
        >
        > 
        >
        > 如果要同时使用多个该属性：AffectsArrange|AffectsMeasure|AffectsParantArrange

        

     6. 依赖属性的继承

        **子容器继承了父容器的依赖属性**：FrameworkPropertyMetadataOptions.Inherits

        ```c#
        public class InheritsControl1 : ContentControl {
        
            public double Value
            {
                get { return (double)GetValue(ValueProperty); }
                set { SetValue(ValueProperty, value); }
            }
            public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(double), typeof(InheritsControl1), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.Inherits));
        }
        public class InheritsControl2 : ContentControl {
            public double Value
            {
                get { return (double)GetValue(ValueProperty); }
                set { SetValue(ValueProperty, value); }
            }
        
            public static readonly DependencyProperty ValueProperty =
                InheritsControl1.ValueProperty.AddOwner(typeof(InheritsControl2), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.Inherits));
        }
        ```
        
        ```xaml
        <StackPanel>
            <local:InheritsControl1 Value="123.5">
                <local:InheritsControl2 x:Name="c2"/>
            </local:InheritsControl1>
            <TextBlock Text="{Binding ElementName=c2,Path=Value}"/>  //123.5
        
            <local:InheritsControl2 Value="234.9">
                <local:InheritsControl1 x:Name="c1"/>
            </local:InheritsControl2>
            <TextBlock Text="{Binding ElementName=c1,Path=Value}"/>  //234.9
        </StackPanel>
        ```

     7. 

   - 

2. 依赖附加属性

   - 作用

     有些对象没有依赖属性可使用依赖附加属性使其具有依赖属性

     为对象提供一些没有的属性

     两个方向：从数据源到控件；从控件到数据源的变化

   - 使用

     1. 定义

        依赖附加属性不一定要定义在依赖对象里面

        ```c#
        public static readonly DependencyProperty ValueAttachProperty;
        ```

     2. 注册

        ```c#
        ValueAttachProperty = DependencyProperty.RegisterAttached("ValueAttach", typeof(int), typeof(TempleteWin));
        
        
        ValueAttachProperty = DependencyProperty.RegisterAttached("ValueAttach", typeof(int), typeof(TempleteWin),new PropertyMetadata(1,new PropertyChangedCallback(ValueChanged)));
        ```

     3. 包装

        通过方法进行包装

        ```c#
        public int GetValueAttach(DependencyObject d)
        {
        	return (int)d.GetValue(ValueAttachProperty);
        }
        
        public void SetValueAttach(DependencyObject d,object value)
        {
        	d.SetValue(ValueAttachProperty, value);
        }
        ```

     4. 

   - 与依赖属性的区别

     1. 依赖属性一定要定义在依赖对象里，依赖附加属性不一定
  2. 依赖属性关注的是定义对象的本身（通过属性包装获取值），依赖附加属性关注的是附加的目标对象（通过函数进行包装获取值）
   
   - 

3. 类型转换器

   

4. 

# 绑定

1. 数据绑定

   - 定义：描述的是一种关系，通过某种关系将多个事物联系在一起,同时让界面与数据逻辑进行解耦

     - 页面对象的属性（**必须是依赖对象**）——目标属性
     - 需要在界面上做交互关联的数据对象——源属性

   - 绑定表达式

     将页面对象的某个属性与数据源建立联系的式子

     ```
     形如：Text={Binding Path=value,Source=data}
     ```

     - 绑定数据源

       指定数据源：source、elementName、dataContext、relativeSource

       1. 依赖对象作为数据源

       2. 普通数据类型或集合数据类型作为数据源

          **path=.或是省略path**表示将数据源直接绑定给元素属性，而不需要去找数据源里面的属性

          ```xaml
          <UserControl.Resources>
          	<sys:String x:Key="dataStr">hello</sys:String>
          </UserControl.Resources>
          
          <TextBlock Text="{Binding Source={StaticResource dataStr}}"/>
          <TextBlock Text="{Binding Source={StaticResource dataStr},Path=.}"/>
          <TextBlock Text="{Binding Source={StaticResource dataStr},Path=[1]}"/>   //绑定下标
          ```

       3. 单个数据对象作为数据源，INotifyPropertyChanged

          如果数据的变化要进行通知，建议使用INotifyPropertyChanged的方式

       4. ADO.NET数据对象作为数据源，DataTable,DataView

       5. xmlDataProvider做为数据源

          ```xaml
          <XmlDataProvider x:Key="dataXml"                      Source="pack://application:,,,/WinDemo;component/xmlFile.xml"></XmlDataProvider>
          
          //节点下标从1开始
          //获取节点属性：@propname
          <TextBlock Text="{Binding Source{StaticResourcedataXml},XPath=menu/menuItem/name}"/>
          <TextBlock Text="{Binding Source{StaticResourcedataXml},XPath=menu/menuItem/@attr}"/>
          <TextBlock Text="{Binding Source{StaticResourcedataXml},XPath=menu/menuItem[2]/name}"/>
          
          
          <XmlDataProvider x:Key="dataXmlList"                       Source="pack://application:,,,/WinDemo;component/xmlFile.xml"
                          XPath="menu/menuItem"/>
          <ListView ItemsSource="{Binding Source={StaticResource dataXmlList}}"/>
          ```

       6. ObjectDataProvider作为数据源

          直接再xaml里调用类里定义的函数

          > 步骤1：定义类和函数
          >
          > ```c#
          > public class MethodClass
          > {
          >     public string GetWidth(string p)
          >     {
          >         return (int.Parse(p) * 0.5).ToString();
          >     }
          > }
          > ```
          >
          > 步骤2：在xaml资源中引入对象函数
          >
          > ```xaml
          > <ObjectDataProvider x:Key="getWidthMethod"
          >                     ObjectType="{x:Typelocal:MethodClass}"
          >                     MethodName="GetWidth">
          >     <ObjectDataProvider.MethodParameters>
          >         <!--待调用的函数的参数，有几个参数就写几个,传入定值-->
          >         <sys:String>150.0</sys:String>
          >     </ObjectDataProvider.MethodParameters>
          > </ObjectDataProvider>
          > ```
          >
          > 步骤3：使用
          >
          > ```xaml
          > <!--绑定函数执行结果-->
          > <TextBlock Text="{Binding Source={StaticResource getWidthMethod}}"/>
          > 
          > <!--绑定函数传入的参数，通过下标指定参数，函数的传入参数和返回结果都的是字符串才可以
          > MethodParameters[0]=》绑定第一个参数
          > -->
          > <TextBox Text="{Binding Source={StaticResource getWidthMethod},Path=MethodParameters[0],
          >                UpdateSourceTrigger=PropertyChanged,BindsDirectlyToSource=True}"></TextBox>
          > ```

       7. Linq结果作为数据源

       8. 静态对象属性作为数据源

          path使用括号标记，意味着不需要数据源，不需要向上查找数据源

          ```c#
          public class MethodClass
          {
              public static int StaticProperty { get; set; } = 123;
          
              /// <summary>
              /// 绑定静态属性并且通知
              /// </summary>
              private static int _myProperty;
              public static int MyProperty
              {
                  get { return _myProperty; }
                  set { _myProperty = value;
                       // MyPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(MyProperty)));
                       StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(MyProperty)));
                      }
              }
              //方式一：框架内部自动根据属性名+Changed进行初始化了这个事件的实例，如果名字不一样就不能进行自动关联静态属性变化事件
              //public static event EventHandler<PropertyChangedEventArgs> MyPropertyChanged;
          
              //方式二：框架内部自动将静态属性变化事件进行初始化了，不与静态属性名强制关联，任何静态属性变化了都可以通知
              public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
          
              public string GetWidth(string p)
              {
                  return (int.Parse(p) * 0.5).ToString();
              }
          }
          
          <TextBlock Text="{Binding Path=(local:MethodClass.StaticProperty)}"/>
          
          <TextBlock Text="{Binding Path=(local:MethodClass.MyProperty)}"/>
          ```

          

       9. 

     - 数据访问路径

       指定数据访问路径：Path、XPath（xml路径）

     - 

   - 

   

2. 