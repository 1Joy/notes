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

       DataContext数据源处理：

       ​	一个页面的dataContext只能绑定一个

       ​	如果没有明确的为控件指定绑定数据源，就会从自身层级向上查找dataContext作为数据源

       RelativeSource数据源处理：

       ​	通常如果使用集合控件绑定数据集合，那么集合控件的数据模板的dataContext为数据集合的数据类型，如果需要在集合控件中绑定数据集合外的属性，就需要用到relativeSource来指定数据源。

       AncestorType：指定要查找的对象的类型

       Mode：指定查找相对源的方式

       - FindAncestor：查找上层祖先
       - TemplateParent：模板，等同于TemplateBinding
       - Self：查找自身控件作为数据源
       - PreviousData：前一个对象作为数据源

       ```xaml
       <ItemsControl ItemsSource="{Binding ListValue}">
           <ItemsControl.ItemTemplate>
               <DataTemplate>
                   <WrapPanel>
                       <TextBlock Text="{Binding Value}"/>
                       <TextBlock Text="{Binding Path=DataContext.BM1.Value,
                                        RelativeSource={RelativeSource AncestorType=Window,
                                        Mode=FindAncestor}}"/>
       
                   </WrapPanel>
       
               </DataTemplate>
           </ItemsControl.ItemTemplate>
       </ItemsControl>
       ```

       常用数据源：

       1. 依赖对象作为数据源

          elementName

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

     - 数据访问路径

       指定数据访问路径：Path、XPath（xml路径）

     - 绑定方式——Mode

       - TwoWay：两个方向：目标属性->数据源；数据源->目标属性

       - OneWay：一个方向：数据源->目标属性

         >绑定移除：如果直接对有绑定表达式的目标属性直接赋值，会覆盖掉原来设置的绑定表达式，即如果再对数据源进行更新，该控件不会触发更新
         >
         >```xaml
         ><TextBlock x:Name="tb1" Text="{Binding BM1.Value,Mode=OneWay}"/>
         >
         >this.tb1.Text="123";   //会覆盖绑定表达式
         >
         > //手动移除绑定关系
         >BindingOperations.ClearBinding(tb, TextBox.TextProperty);
         >```

       - OneTime：一次，只接收数据源的初始值

       - OneWayToSource：一个方向：目标属性->数据源

       - Default：默认，不同的控件默认的方式不同

     - 目标属性触发更新的方式—— UpdateSourceTrigger

       - Default：默认，不同控件默认方式不同

       - PropertyChanged：属性一更新就写入数据源

       - LostFocus：当控件失去焦点时触发写入数据源

       - Explicit：需要通过明确的事件进行触发

         ```xaml
         <TextBox x:Name="tb" Text="{Binding BM1.Value,UpdateSourceTrigger=Explicit}"/>
         <TextBlock Text="{Binding BM1.Value}"/>
         <Button Content="button" Click="Button_Click"/>
         ```

         ```c#
         private void Button_Click(object sender, RoutedEventArgs e)
         {
             //获取目标控件上的某个属性的绑定表达式
             BindingExpression bindingExpression = tb.GetBindingExpression(
                 TextBox.TextProperty);
             //手动触发更新数据源
             bindingExpression.UpdateSource();
         }
         ```

     - 绑定转换器——Converter

       数据源属性与目标属性出现类型冲突或者值冲突时可以使用

       > 步骤1：创建一个Converter类，实现IValueConverter
       >
       > ```c#
       > public class BindModelConverter : IValueConverter
       > {
       >     /// <summary>
       >     /// 从源到目标的数据转换
       >     /// </summary>
       >     /// <param name="value">数据源的值</param>
       >     /// <param name="targetType">目标类型</param>
       >     /// <param name="parameter">转换参数</param>
       >     /// <param name="culture"></param>
       >     /// <returns>将要给目标属性的值</returns>
       >     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
       >     {
       >         if (int.Parse(value.ToString()) > 200)
       >             return Brushes.Red;
       >         return Brushes.Green;
       > 
       >     }
       > 
       >     /// <summary>
       >     /// 从目标到数据源的数据转换
       >     /// </summary>
       >     /// <param name="value"></param>
       >     /// <param name="targetType"></param>
       >     /// <param name="parameter"></param>
       >     /// <param name="culture"></param>
       >     /// <returns></returns>
       >     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       >     {
       >         throw new NotImplementedException();
       >     }
       > }
       > 
       > 
       > public class GenderConverter : IValueConverter
       > {
       >     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
       >     {
       >         //如果数据源的值与传入的参数值一致，就说明是选中状态
       >         if (value.ToString() == parameter.ToString())
       >             return true;
       >         return false;
       >     }
       > 
       >     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       >     {
       >         return parameter;
       >     }
       > }
       > ```
       >
       > 步骤2：界面上引入资源
       >
       > ```xaml
       > <Window.Resources>
       >     <local:BindModelConverter x:Key="converter1"/>
       >     <local:GenderConverter x:Key="converter2"/>
       > </Window.Resources>
       > ```
       >
       > 步骤3：使用
       >
       > ```xaml
       > <TextBlock x:Name="tb2" Text="{Binding BM1.Value}"
       >            Foreground="{Binding BM1.Value,Converter={StaticResource converter1}}"/>
       > 
       > <RadioButton Content="男" IsChecked="{Binding BM1.Gender,Converter={StaticResource converter2}
       >                                     ,ConverterParameter=1}"/>
       > <RadioButton Content="女" IsChecked="{Binding BM1.Gender,Converter={StaticResource converter2}
       >                                     ,ConverterParameter=2}"/>
       > ```
       >
       
     - 字符串格式化——StringFormat

       格式化字符串或者拼接字符串

       ```xaml
       <!--保留两位小数，#表示不限制数字-->
       <TextBlock Text="{Binding BM1.Value,StringFormat={}{0:#.00}}"/>
       <TextBlock Text="{Binding BM1.Value,StringFormat={}{0:f2}}"/>
       
       <!--货币显示两位小数-->
       <TextBlock Text="{Binding BM1.Value,StringFormat={}{0:C2}}"/>
       
       <TextBlock Text="{Binding Time,StringFormat={}{0:yyyy-MM-dd HH:mm:ss}}"/>
       <TextBlock Text="{Binding Time,StringFormat=\{0:yyyy-MM-dd HH:mm:ss\}}"/>
       <TextBlock Text="{Binding Time,StringFormat=时间：{0:yyyy-MM-dd HH:mm:ss}}"/>
       ```

     - Delay

       停止界面控件输入后延迟多少毫秒触发从目标到数据源的写入。

     - FallbackValue

       设置当界面绑定错误是显示的值

     ```xaml
     <TextBlock Text="{Binding Tim,StringFormat=时间：{0:yyyy-MM-dd HH:mm:ss}
                 ,FallbackValue=00}"/>
     ```

     - TargetNullValue

       设置当目标属性值为null的时候显示的值

       ```xaml
       <TextBlock Text="{Binding Time,StringFormat=时间：{0:yyyy-MM-dd HH:mm:ss}
                        ,FallbackValue=00,TargetNullValue=123}"/>
       ```

     - 数据验证并获取异常信息描述

       1. 对依赖属性进行数据验证和获取异常描述

          > 步骤1：通过依赖属性的验证回调验证数据
          >
          > 步骤2：设置待验证的控件的验证规
          >
          > ```xaml
          > <TextBox x:Name="tb3" >
          >     <TextBox.Text>
          >         <Binding Path="Value11" UpdateSourceTrigger="PropertyChanged">
          >             <!--设置数据验证规则-->
          >             <Binding.ValidationRules>
          >                 <ExceptionValidationRule/>
          >             </Binding.ValidationRules>
          >         </Binding>
          >     </TextBox.Text>
          > </TextBox>
          >  ```
          > 步骤3：界面显示数据验证的异常信息
          > ```xaml
          > <!--获取某个控件的验证异常信息-->
          > <TextBlock Text="{Binding Path=(Validation.Errors)[0].ErrorContent,ElementName=tb3}"/>
          > ```

       2. 对普通属性进行数据验证和获取异常描述

          缺点：是通过异常的方式返回错误，在调试过程中不好操作并且会创建异常对象可能会产生内存问题

          > 步骤1：为属性做数据验证，并通过抛出异常的方式来表示验证失败
          >
          > ```c#
          > private int _value11=123;
          > 
          > public int Value11
          > {
          >     get { return _value11; }
          >     set {
          >         if (value == 200)
          >         {
          >             throw new Exception("报错了");
          >         }
          >         _value11 = value;
          >     }
          > }
          > ```
          >
          > 步骤2：设置待验证的控件的验证规则
          >
          > ```xaml
          > <TextBox x:Name="tb3" >
          >     <TextBox.Text>
          >         <Binding Path="Value11" UpdateSourceTrigger="PropertyChanged">
          >             <!--设置数据验证规则-->
          >             <Binding.ValidationRules>
          >                 <ExceptionValidationRule/>
          >             </Binding.ValidationRules>
          >         </Binding>
          >     </TextBox.Text>
          > </TextBox>
          > ```
          >
          > 步骤3：界面显示数据验证的异常信息
          >
          > ```xaml
          > <!--获取某个控件的验证异常信息-->
          > <TextBlock Text="{Binding Path=(Validation.Errors)[0].ErrorContent,ElementName=tb3}"/>
          > ```

       3. 通过自定义数据验证规则进行数据验证和异常描述

          缺点：要为每个属性定义数据验证规则比较麻烦

          > 步骤1：新建一个类继承ValidationRule，并重写验证函数
          >
          > ```c#
          > /// <summary>
          > /// 自定义数据验证规则
          > /// </summary>
          > public class ValidationTest : ValidationRule
          > {
          >     /// <summary>
          >     /// 
          >     /// </summary>
          >     /// <param name="value">目标属性的值</param>
          >     /// <param name="cultureInfo"></param>
          >     /// <returns></returns>
          >     public override ValidationResult Validate(object value, CultureInfo cultureInfo)
          >     {
          >         //自定义验证规则和异常描述信息
          >         if (value.ToString() == "200")
          >             return new ValidationResult(false, "错误描述");
          >         return new ValidationResult(true, "");
          >     }
          > }
          > ```
          >
          > 步骤2：界面使用自定义验证类
          >
          > ```xaml
          > <TextBox x:Name="tb3" >
          >     <TextBox.Text>
          >         <Binding Path="Value11" UpdateSourceTrigger="PropertyChanged">
          >             <!--设置数据验证规则-->
          >             <Binding.ValidationRules>
          >                 <local:ValidationTest/>
          >             </Binding.ValidationRules>
          >         </Binding>
          >     </TextBox.Text>
          > </TextBox>
          > ```
          >
          > 步骤3：获取异常描述显示到界面
          >
          > ```xaml
          > <!--获取某个控件的验证异常信息-->
          > <TextBlock Text="{Binding Path=(Validation.Errors)[0].ErrorContent,ElementName=tb3}"/>
          > ```
          >

       4. 通过实现IDataErrorInfo接口实现数据验证和异常信息获取

          > 步骤1：待验证的对象实现IDataErrorInfo接口，并实现验证逻辑
          >
          > ```c#
          > public class BindingModel:INotifyPropertyChanged,IDataErrorInfo
          > {
          >     public int Value22 { get; set; }
          >     public event PropertyChangedEventHandler PropertyChanged;
          >     public string Error => null;
          >     public string this[string columnName]
          >     {
          >         get
          >         {
          >             if (columnName == "Value22" && this.Value22 == 200)
          >                 //数据验证逻辑
          >                 return "出错了哈";
          >             return "";
          >         }
          >     }
          > }
          > ```
          >
          > 缺点：需要在数据验证逻辑处添加对每个属性名称的判断，不能统一进行处理
          >
          > 可以通过**反射+特性**的方式解决上述缺点
          >
          > > 步骤1：自定义数据验证特性类
          > >
          > > ```c#
          > > public class NotValueAttribute : Attribute
          > > {
          > >     public string Value { get; set; }
          > >     public NotValueAttribute(string value)
          > >     {
          > >         Value = value;
          > >     }
          > > 
          > >     public (bool,string) Do(object value)
          > >     {
          > >         if (value.ToString() == Value)
          > >             return (true, $"字段不能为{Value}");
          > >         return (false, "");
          > >     }
          > > }
          > > ```
          > >
          > > 步骤2：在需要进行数据验证的属性上增加特性
          > >
          > > ```c#
          > > [NotValue("200")]
          > > public int Value22 { get; set; } = 100;
          > > ```
          > >
          > > 步骤3：在索引里面通过反射来获取属性以及特性进行数据验证
          > >
          > > ```c#
          > > public string this[string columnName]
          > > {
          > >     get
          > >     {
          > >         //获取属性
          > >         var prop = this.GetType().GetProperty(columnName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
          > >         if (prop.IsDefined(typeof(NotValueAttribute), false))
          > >         {
          > >             //获取特性对象
          > >             var attr = prop.GetCustomAttributes(typeof(NotValueAttribute), false)[0] as NotValueAttribute;
          > >             var (isEqual, msg) = attr.Do(prop.GetValue(this));
          > >             if (isEqual)
          > >                 return msg;
          > >         }
          > >         return "";
          > >     }
          > > }
          > > ```
          >
          > 步骤2：界面控件绑定表达式设置ValidatesOnDataErrors=True
          >
          > ```xaml
          > <TextBox x:Name="tb4" Text="{Binding BM1.Value22,UpdateSourceTrigger=PropertyChanged,
          >                             ValidatesOnDataErrors=True}"/>
          > ```
          >
          > 步骤3：获取异常描述显示到界面
          >
          > ```xaml
          > <!--获取某个控件的验证异常信息-->
          > <TextBlock Text="{Binding Path=(Validation.Errors)[0].ErrorContent,ElementName=tb4}"/>
          > ```

     - 多重绑定

       将多个属性绑定到一个控件上去

       ```xaml
       <TextBlock>
           <TextBlock.Text>
               <MultiBinding StringFormat="{}{0}--{1}">
                   <Binding Path="Value2"/>
                   <Binding Path="Value3"/>
               </MultiBinding>
           </TextBlock.Text>
       </TextBlock>
       ```

       或者通过多值转换器绑定

       ```c#
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
       
       <TextBlock>
           <TextBlock.Text>
               <MultiBinding Converter="{StaticResource ValueMultiConverter}">
                   <Binding Path="Value2"/>
                   <Binding Path="Value3"/>
               </MultiBinding>
           </TextBlock.Text>
       </TextBlock>
       ```

     - 优先级绑定——PriorityBinding

       绑定的多个值并不是全部显示，而是根据优先级顺序显示。如果高优先级的值没有被成功赋值，会显示低优先级的值。

       ```xaml
       <TextBox>
           <PriorityBinding FallbackValue="正在获取数据.....">
               <!--显示顺序：正在获取数据->Value3的值->value2的值-->
               <Binding Path="Value2" IsAsync="True"/>
               <Binding Path="Value3" IsAsync="True"/>
           </PriorityBinding>
       </TextBox>
       ```


# 图形

1. 基本图形对象

   ```xaml
   <!--直线-->
   <Line X1="0" Y1="0" X2="300" Y2="100" Stroke="Red" StrokeThickness="2"/>
   <Line X1="0" Y1="0" X2="{Binding RelativeSource={RelativeSource AncestorType=Window},
                           Path=ActualWidth}" Y2="100" Stroke="Red" StrokeThickness="2"/>
   
   <!--矩形：可以分别设置xy方向的圆角-->
   <Rectangle Height="80" Fill="Orange" RadiusX="30" RadiusY="50"/>
   
   <!--椭圆-->
   <Ellipse Width="300" Height="100" Fill="Blue"/>
   <Ellipse Width="100" Height="100" StrokeThickness="2" Stroke="Red"/>
   
   <!--多段线 Points：坐标1 坐标2 坐标3-->
   <Polyline Points="0,0 100,50 50,0 150,40" Stroke="AliceBlue" Fill="AntiqueWhite"/>
   
   <!--多边形，会封闭图形-->
   <Polygon Points="0,0 100,50 50,0 150,40" Stroke="Aqua" Fill="Aquamarine"/>
   <Polygon Points="0,0 100,50 50,0 150,40" Stroke="Aqua" StrokeThickness="2"/>
   ```

2. 几何图形

    ```xaml
   <StackPanel>
       <!--几何图形不能设置线的颜色和粗细，只能在path上去设置-->
       <Path Stroke="Red" StrokeThickness="2">
           <Path.Data>
               <!--直线-->
               <LineGeometry StartPoint="0,0" EndPoint="100,50"/>
           </Path.Data>
       </Path>
   
       <Path Stroke="Red" StrokeThickness="2" Fill="AliceBlue">
           <Path.Data>
               <!--矩形：左上角的点坐标，右下角的点坐标-->
               <RectangleGeometry Rect="0,0,100,50"/>
           </Path.Data>
       </Path>
   
       <Path Stroke="Red" StrokeThickness="2" Fill="Yellow">
           <Path.Data>
               <!--椭圆： Center：中心点坐标，RadiusX：X方向半径，RadiusY：Y轴方向半径-->
               <EllipseGeometry  Center="0,0" RadiusX="100" RadiusY="10"/>
           </Path.Data>
       </Path>
   
       <Path Stroke="Red" StrokeThickness="2" Fill="Yellow">
           <Path.Data>
               <!--组合图形-->
               <GeometryGroup>
                   <LineGeometry StartPoint="0,0" EndPoint="100,50"/>
                   <LineGeometry StartPoint="0,50" EndPoint="100,0"/>
                   <RectangleGeometry Rect="0,0,100,50"/>
                   <EllipseGeometry  Center="50,25" RadiusX="25" RadiusY="25"/>
               </GeometryGroup>
           </Path.Data>
       </Path>
   
       <Path Stroke="Red" StrokeThickness="2" Fill="Yellow">
           <Path.Data>
               <!--合并图形：
                    GeometryCombineMode.Intersect：保留交集
                    GeometryCombineMode.Union：保留并集
                    GeometryCombineMode.Exclude：保留除开重叠的部分
                    GeometryCombineMode.Xor：保留除开重叠的部分-->
               <CombinedGeometry GeometryCombineMode="Union">
                   <CombinedGeometry.Geometry1>
                       <RectangleGeometry Rect="0,0,100,50"/>
                   </CombinedGeometry.Geometry1>
                   <CombinedGeometry.Geometry2>                                        
                       <EllipseGeometry  Center="50,25" RadiusX="25" RadiusY="25"/>
                   </CombinedGeometry.Geometry2>
               </CombinedGeometry>
           </Path.Data>
       </Path>
   
       <Path Stroke="Red" StrokeThickness="2">
           <Path.Data>
               <PathGeometry>
                   <!--起始点为0，0，到200，50这个位置用直线绘制，到100，60这个位置用直线绘制，300，400这个位置用弧线绘制
   					每个segment的起始点为上一个segment的结束位置-->
                   <PathFigure StartPoint="0,0">
                       <LineSegment Point="200,50"/>
                       <LineSegment Point="100,60"/>
                       <!--RotationAngle：弧线的旋转角度
                           SweepDirection：弧线绘制方向；Clockwise：顺时针
                           IsLargeArc：是否显示大弧
                           Point：弧线的结束位置-->
                       <ArcSegment Size="100,40" RotationAngle="90" SweepDirection="Clockwise"
                                   IsLargeArc="True" Point="300,40"/>
                       <ArcSegment Size="100,40" RotationAngle="90" SweepDirection="Clockwise"
                                   IsLargeArc="True" Point="100,60"/>
                   </PathFigure>
               </PathGeometry>
           </Path.Data>
       </Path>
   </StackPanel>
   ```

   ```xaml
   <Path Stroke="Red" StrokeThickness="2">
       <Path.Data>
           <!--需要通过后台代码添加里面的绘制元素,效率比较高效-->
           <StreamGeometry x:Name="streamGeometry"/>
       </Path.Data>
   </Path>
   
   using(StreamGeometryContext sgc = streamGeometry.Open())
   {
   	sgc.BeginFigure(new Point(0, 0), true, true);
   
   	sgc.LineTo(new Point(200, 50), true, true);
   
   	sgc.ArcTo(new Point(100, 20), new Size(100, 40), 90, true, 		SweepDirection.Clockwise,
   true, true);
   }
   ```

   

3. Path

   ```xaml
   <StackPanel>
       <!--M：移动起始点到100,50这个位置；data必须以M开始
                               m：相对于上一个点移动起始点-->
       <Path Data="M100,50"/>
       <!--L：画直线；将起始点移动到200,0的位置画一条直线，终止位置为100,50
                               l：画直线；将起始点移动到200,0的位置（相当于坐标原点）画一条直线，终止位置为100,50-->
       <Path Data="M200,0L100,50" Stroke="Red" StrokeThickness="2"/>                        
       <Path Data="M200,0l100,50" Stroke="Green" StrokeThickness="2"/>                        
       <Path Data="M200,0L100,50 M200,50L100,0" Stroke="Red" StrokeThickness="2"/>
   
       <!--H：在水平方向到指定X坐标点画一条直线
                               h：在水平方向从起始位置(相当于是坐标原点)到指定X坐标点画一条直线
                               V：在垂直方向到指定Y坐标点画一条直线
                               v：在垂直方向从起始位置(相当于是坐标原点到指定Y坐标点画一条直线-->
       <Path Data="M50,0H300" Stroke="Green" StrokeThickness="5"/>
       <Path Data="M50,10h300" Stroke="Green" StrokeThickness="5"/>
       <Path Data="M0,10V50" Stroke="red" StrokeThickness="5"/>
       <Path Data="M10,10v50" Stroke="red" StrokeThickness="5"/>
   
       <!--A：画圆弧：圆弧的宽度，高度，旋转角度，是否是优势弧，绘制方向（0：逆时针；1：顺时针），圆弧的结束坐标
                               a：使用相对坐标画圆弧-->
       <Path Data="M0,0A100 50 45 1 0 300 50" Stroke="Red" StrokeThickness="2"/>
       <Path Data="M50,0A100 50 45 1 1 300 50" Stroke="Red" StrokeThickness="2"/>
       <Path Data="M50,0a100 50 45 1 1 300 50" Stroke="Green" StrokeThickness="2"/>
   
       <!--贝塞尔曲线相切与起始到控制点，控制点到结束点的直线
                               Q：画二次贝塞尔曲线：控制点坐标 结束点坐标
                               q：使用相对坐标-->
       <Path Data="M50,50Q200,50 100,100" Stroke="Red" StrokeThickness="2"/>
       <Path Data="M50,50q200,50 100,100" Stroke="Red" StrokeThickness="2"/>
   
       <!--T：平滑T；曲线通过两个点-->
       <Path Data="M50,50T200,50 100,100" Stroke="Red" StrokeThickness="2"/>
   
       <!--C：三次贝塞尔曲线：控制点1坐标，控制点2坐标，结束点坐标-->
       <Path Data="M50,50C200,50 100,100 300,200" Stroke="Red" StrokeThickness="2"/>
   
       <!--z：闭合图形-->
       <Path Data="M50,50C200,50 100,100 300,200z" Stroke="Red" StrokeThickness="2"/>
   </StackPanel>
   ```

4. 图形变换

   只要继承自virtual的控件都可进行变换

   > LayoutTransform与RenderTransform的区别：
   >
   > 前者会引起界面的重新绘制，而后者不会

   - 位移——TranslateTransform

     ```xaml
     <WrapPanel>
         <Button Content="位移" Width="80" Height="50"/>
         <Button Content="位移" Width="80" Height="50">
             <!--需要进行布局的重新排列-->
             <!--<Button.LayoutTransform>
                                         <TranslateTransform X="60"/>
                                     </Button.LayoutTransform>-->
     
             <Button.RenderTransform>
                 <TranslateTransform X="50" Y="5"/>
             </Button.RenderTransform>
         </Button>
     </WrapPanel>
     ```

   - 旋转

     ```xaml
     <WrapPanel>
         <Button Content="旋转" Width="80" Height="50"/>
         <!--RenderTransformOrigin：指定旋转中心点（百分比）；默认0,0-->
         <Button Content="旋转" Width="80" Height="50" RenderTransformOrigin="0.5,0.5">
             <Button.RenderTransform>
                 <RotateTransform Angle="45"/>
             </Button.RenderTransform>
         </Button>
         <Button Content="旋转" Width="80" Height="50">
             <Button.RenderTransform>
                 <RotateTransform Angle="45" CenterX="40" CenterY="25"/>
             </Button.RenderTransform>
         </Button>
     </WrapPanel>
     ```

   - 缩放

     **缩放的是坐标轴而不是控件的大小**

     ```xaml
     <WrapPanel>
         <Button Content="缩放" Width="80" Height="50"/>
         <Button Content="缩放" Width="80" Height="50">
             <Button.RenderTransform>
                 <!--X,Y方向缩放（百分比）；默认为1-->
                 <ScaleTransform ScaleX="2" ScaleY="2"/>
             </Button.RenderTransform>
         </Button>
     </WrapPanel>
     ```

   - 斜切

     ```xaml
     <WrapPanel>
         <Button Content="斜切" Width="80" Height="50"/>
         <!--默认变换起点在控件左上角0,0-->
         <Button Content="斜切" Width="80" Height="50">
             <Button.RenderTransform>
                 <SkewTransform AngleX="10" AngleY="20"/>
             </Button.RenderTransform>
         </Button>
         <Button Content="斜切" Width="80" Height="50">
             <Button.RenderTransform>
                 <SkewTransform AngleX="-10" AngleY="-20"/>
             </Button.RenderTransform>
         </Button>
         <!--RenderTransformOrigin：指定变换起点-->
         <Button Content="斜切" Width="80" Height="50" RenderTransformOrigin="0.5,0.5">
             <Button.RenderTransform>
                 <SkewTransform AngleX="-10" AngleY="-20"/>
             </Button.RenderTransform>
         </Button>
     </WrapPanel>
     ```

   - 矩阵

     旋转需要通过代码计算得出角度

     ```xaml
     <WrapPanel>
         <Button Content="矩阵" Width="80" Height="50"/>
         <Button Content="矩阵" Width="80" Height="50">
             <Button.RenderTransform>
                 <!--Matrix：X方向缩放参数，Y方向斜切，X方向斜切，Y方向缩放参数 位移X，位移Y-->
                 <MatrixTransform Matrix="2,0.5 0.6,3 0,0"/>
             </Button.RenderTransform>
         </Button>
     </WrapPanel>
     ```

   - 集合变换

     **集合变换的时候注意变换的顺序，顺序不同可能最后的结果会不同**

     ```xaml
     <WrapPanel>
         <Button Content="集合变换" Width="80" Height="50"/>
         <Button Content="集合变换" Width="80" Height="50" RenderTransformOrigin="0.5,0.5">
             <Button.RenderTransform>
                 <TransformGroup>
                     <SkewTransform AngleX="10" AngleY="20"/>
                     <ScaleTransform ScaleX="2" ScaleY="2"/>
                     <TranslateTransform X="20"/>
                 </TransformGroup>
             </Button.RenderTransform>
         </Button>
     
         <Button Content="集合变换" Width="80" Height="50" RenderTransformOrigin="0.5,0.5">
             <Button.RenderTransform>
                 <TransformGroup>
                     <TranslateTransform X="20"/>
                     <ScaleTransform ScaleX="2" ScaleY="2"/>
                     <SkewTransform AngleX="10" AngleY="20"/>                                      
     
                 </TransformGroup>
             </Button.RenderTransform>
         </Button>
     </WrapPanel>
     ```

5. 画刷——Brush

   - 画刷——SolidColorBrush

     ```xaml
     <Border Height="50">
         <Border.Background>
             <SolidColorBrush Color="Orange"/>
         </Border.Background>
     </Border>
     ```

   - 线性渐变——LinearGradientBrush

     ```xaml
     <StackPanel>
         <Border Height="50">
             <Border.Background>
                 <!--垂直方向-->
                 <LinearGradientBrush StartPoint="0,0" EndPoint="0,1">
                     <GradientStop Color="Green" Offset="0"/>
                     <GradientStop Color="Red" Offset="0.5"/>
                     <GradientStop Color="Orange" Offset="1"/>
                 </LinearGradientBrush>
             </Border.Background>
         </Border>
     
         <Border Height="50" Margin="0,5">
             <Border.Background>
                 <!--水平方向-->
                 <LinearGradientBrush StartPoint="0,0" EndPoint="1,0">
                     <GradientStop Color="Green" Offset="0"/>
                     <GradientStop Color="Red" Offset="0.5"/>
                     <GradientStop Color="Orange" Offset="1"/>
                 </LinearGradientBrush>
             </Border.Background>
         </Border>
         <Border Height="50" Margin="0,5" Width="50">
             <Border.Background>
                 <!--水平方向-->
                 <LinearGradientBrush StartPoint="0,0.5" EndPoint="1,0">
                     <GradientStop Color="Green" Offset="0"/>
                     <GradientStop Color="Red" Offset="0.5"/>
                     <GradientStop Color="Orange" Offset="1"/>
                 </LinearGradientBrush>
             </Border.Background>
         </Border>
     </StackPanel>
     ```

   - 镜像渐变——RadialGradientBrush

     ```xaml
     <StackPanel>
         <Border Height="50" Margin="0,5">
             <Border.Background>
                 <RadialGradientBrush>
                     <GradientStop Color="Green" Offset="0"/>
                     <GradientStop Color="Orange" Offset="0.5"/>
                     <GradientStop Color="Red" Offset="1"/>
                 </RadialGradientBrush>
             </Border.Background>
         </Border>
     
         <Border Height="50" Margin="0,5">
             <Border.Background>
                 <!--GradientOrigin：镜像起始点
                                             Center：镜像圆的中心点-->
                 <RadialGradientBrush GradientOrigin="0,0.5" Center="0,0.5">
                     <GradientStop Color="Green" Offset="0"/>
                     <GradientStop Color="Orange" Offset="0.5"/>
                     <GradientStop Color="Red" Offset="1"/>
                 </RadialGradientBrush>
             </Border.Background>
         </Border>
     </StackPanel>
     ```

   - DrawingBrush

     ```xaml
     <Border Height="50">
         <Border.Background>
             <!--TileMode：以什么方式填充
                                     ViewportUnits：视口里面宽度是绝对宽度还是相对宽度
                                     Viewport：视口大小：起始位置，X方向宽度，Y方向宽度-->
             <DrawingBrush TileMode="FlipX" ViewportUnits="Absolute" Viewport="0,0,10,20">
                 <DrawingBrush.Drawing>
                     <GeometryDrawing>
                         <GeometryDrawing.Pen>
                             <Pen Brush="Red" Thickness="1"/>
                         </GeometryDrawing.Pen>
                         <GeometryDrawing.Geometry>
                             <GeometryGroup>
                                 <LineGeometry StartPoint="0,0" EndPoint="0,20"/>
                                 <LineGeometry StartPoint="0,20" EndPoint="10,20"/>
                             </GeometryGroup>
                         </GeometryDrawing.Geometry>
                     </GeometryDrawing>
                 </DrawingBrush.Drawing>
             </DrawingBrush>
         </Border.Background>
     </Border>
     ```

   - VisualBrush

     ```xaml
     <StackPanel>
         <TextBox Text="hello Joy" x:Name="tb">
             <TextBox.Effect>
                 <!--BlurEffect：模糊，值越大越模糊-->
                 <BlurEffect Radius="3"></BlurEffect>
             </TextBox.Effect>
         </TextBox>                            
         <Border Height="50">
             <Border.Background>
                 <!--把visual的子对象的所有内容渲染到背景上去-->
                 <VisualBrush Visual="{Binding ElementName=tb}"/>
             </Border.Background>
         </Border>
     
         <Border Height="50">
             <Border.Background>
                 <BitmapCacheBrush Target="{Binding ElementName=tb}">
                     <BitmapCacheBrush.BitmapCache>
                         <!--把对象渲染到背景
                                                 RenderAtScale：犀利程度-->
                         <BitmapCache SnapsToDevicePixels="True" RenderAtScale="2"/>
                     </BitmapCacheBrush.BitmapCache>
                 </BitmapCacheBrush>
             </Border.Background>
         </Border>
     </StackPanel>
     ```

   - 阴影

     ```xaml
     <StackPanel>
         <Border Height="50" Width="50">
             <Border.Background>
                 <LinearGradientBrush StartPoint="0,0.5" EndPoint="1,0">
                     <GradientStop Color="AliceBlue" Offset="0"/>
                     <GradientStop Color="YellowGreen" Offset="1"/>
                 </LinearGradientBrush>
             </Border.Background>
             <Border.Effect>
                 <!--BlurRadius：阴影模糊程度
                                             ShadowDepth：阴影往偏移方向的深度
                                             Direction：偏移方向
                                             Color：颜色-->
                 <DropShadowEffect BlurRadius="10" ShadowDepth="0" Direction="0" Color="Aquamarine"/>
             </Border.Effect>
         </Border>
     </StackPanel>
     ```

6. 3D

   - 材质

     散射材质、镜面材质、自发光材质

     1. TriangleIndices：指定组成材质图形的三角形的点绘制顺序

   - 光源

     散射光（AmbientLight）、平行光（DirectionalLight）、点光源（PointLight）、锥形辐射光（SpotLight）

   - 相机

     正交相机：

     透视相机：近大远小

   - 

# 事件

1. 生命周期事件

   **App生命周期事件：**

   - startup

     在调用Application对象的Run方法时发生

   常在加载Page页面时发生：

   - navigating

     在应用程序中的导航器请求新导航时发生

   - loadCompleted

     在已经加载、分析并开始呈现应用程序中的导航器导航到内容时发生

   - navigated

     在已经找到应用程序中的导航器要导航到的内容时发生，尽管此时该内容可能还没有完成加载

   - navigationFailed

     在应用程序中的导航器在导航到所请求内容时出现错误时发生

   - NavigationProgress

     在由应用程序中的导航器管理的下载过程中定期发生，以提供导航进度信息

   - NavigationStopped

     在调用应用程序中的导航器的StopLoading方法时发生，或者当导航器在当前导航正在进行期间请求了一个新导航时发生
     
     

   - sessionEnding

     用户注销或者关闭操作系统而结束Window会话时发生

     e.Cancel=true;  //拦截操作，取消当前的关闭进程

   - Activated

     当应用程序成为前台应用程序时发生（被激活）

   - Deactivated

     当应用程序停止作为前台应用程序时发生

   - exit

     应用程序关闭之前发生，无法取消

   

   **全局异常捕获：**

   - DispatcherUnhandlerException

     获取全局UI线程异常

   - AppDomain.CurrentDomain.UnhandlerException

     捕获所有线程里面的异常

   - TaskScheduler.UnobservedTaskException

     捕获Task异常，但是并不是立即会被触发异常捕获。只有在进行垃圾回收的时候才可以把异常推送到异常回调里面。

     也可以直接通过GC.Collect()触发垃圾回收

   

   **窗口生命周期事件：**

   - SourceInitialized：操作系统给窗口分配句柄时触发，可以通过此获取窗口句柄（只有窗口有句柄，控件没有句柄）

   - ContentRendered：窗口渲染

   - Loaded：窗口加载，修改窗口的控件大小时可能会再次触发

   - Activated

   - Deactivated

   - Closing

     e.Cancle=true：拦截取消关闭过程

   - Closed

2. 输入事件

   - 鼠标输入事件

     WPF事件触发机制：window消息发送给控件所在的窗口句柄，在由窗口句柄发送给待触发的控件触发对应的事件

     1. MouseEnter

     2. MouseLeave

     3. MouseDown

     4. MouseUp

     5. MouseMove

     6. Click

     7. MouseLeftButtonDown

        > Button控件的MouseLeftButtonDown、MouseLeftButtonUp事件会被其click事件拦截，从而不会触发,即使不指定click事件也不会被触发。如果想要触发鼠标左键按下的事件可以使用PreviewMouseLeftButtonDown、PreviewMouseLeftButtonUp事件进行触发
        >
        > ```xaml
        > <Button Content="[BUtton]MouseLeftButtonDown"
        >         Click="Button_Click"
        >         MouseLeftButtonDown="Button_MouseLeftButtonDown"
        >         PreviewMouseLeftButtonDown="Button_PreviewMouseLeftButtonDown"/>
        > 
        > //输出
        > //Button_PreviewMouseLeftButtonDown
        > //Button_Click
        > ```
        >
        > button控件事件触发顺序：
        >
        > window消息发送到指定控件->触发preview相应事件的委托->执行MouseLeftButtonDown事件->执行click事件的委托(**源码就是这样写的，执行鼠标左键事件时会线执行click事件，并且完成之后设置e.handled=true，拦截了后续继续事件委托的执行**)->返回

     8. ...

     9. MouseCapture

   - 键盘输入事件

     1. KeyDown

        TabIndex：为控件设置tab键选中顺序

        IsDefault：IsDefault=true：为控件设置按回车建默认执行的事件

     2. KeyUp

     3. TextInput

        TextInput事件会被TextChanged拦截

     4. TextChanged

     5. IsRepeat

   - 托拽事件

     1. Drop

        **允许托拽道德目标绘制对象必须要有背景色，如果背景色为null则不能托拽绘制成功**

        AllowDrop：true：允许托拽

        ```xaml
        <WrapPanel>
            <Border Width="50" Height="50" Background="Orange"
                    MouseLeftButtonDown="Border_MouseLeftButtonDown"/>
            <Canvas Width="250" Height="100" AllowDrop="True" Background="Transparent"
                    Drop="Canvas_Drop"/>
        </WrapPanel>
        ```

        ```c#
        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(string));
            //动态创建一个对象实例，因为一个控件实例不能同时存在于多个位置
            //添加到到canvas的children
            
        }
        
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            DragDrop.DoDragDrop(border, "UserControl", DragDropEffects.Copy);
        }
        ```

     2. DragLeave

     3. DragOver

     4. DragEnter

3. 路由事件

   **一次事件完整的响应过程：从窗口执行隧道事件到目标控件，再由目标控件执行冒泡事件到窗口结束**

   在事件执行的过程中可以设置e.handled=true阻止事件继续向后执行,执行结束

   冒泡事件：MouseLeftButtonDown等类似的事件；由进行响应的控件逐步沿着视觉树向上响应的事件称为冒泡事件

   隧道事件：PreviewMouseLeftButtonDown等类似的事件；由最外层窗口事件根据视觉树层级逐步进入直到命中目标控件触发响应的事件称为隧道事件

   > 自定义路由事件：
   >
   > 步骤1：新建一个UserControl，并且创建好事件
   >
   > ```c#
   > public partial class UCRouteEvent : UserControl
   > {
   > 
   >     //定义自定义路由事件
   >     /*
   >          * 事件名称
   >          * 路由策略：Bubble：冒泡；Tunnel：隧道；Direct：直接触发
   >          * 事件类型
   >          * 事件的所有者
   >          */
   >     public static readonly RoutedEvent routeEvent = EventManager.RegisterRoutedEvent("route", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UCRouteEvent));
   > 
   >     public event RoutedEventHandler Route
   >     {
   >         add { AddHandler(routeEvent, value); }
   >         remove
   >         {
   >             RemoveHandler(routeEvent, value);
   >         }
   >     }
   > 
   >     public UCRouteEvent()
   >     {
   >         InitializeComponent();
   >     }
   > 
   >     private void Button_Click(object sender, RoutedEventArgs e)
   >     {
   > 
   >         //触发我们自定义的路由事件
   >         //定义参数
   >         RoutedEventArgs args = new RoutedEventArgs(routeEvent);
   >         //触发
   >         this.RaiseEvent(args);
   >     }
   > }
   > ```
   >
   > 步骤2：在自定义空间上选中相应的按钮触发路由事件
   >
   > ```xaml
   > <UserControl x:Class="WinDemo.UCRouteEvent"
   >              xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
   >              xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
   >              xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
   >              xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
   >              xmlns:local="clr-namespace:WinDemo"
   >              mc:Ignorable="d" 
   >              d:DesignHeight="450" d:DesignWidth="800">
   >     <Grid>
   >         <Button Click="Button_Click">点我</Button>
   >     </Grid>
   > </UserControl>
   > ```
   >
   > 步骤3：在其他界面使用自定义控件，并且为定义的路由事件设置相应的事件响应
   >
   > ```xaml
   > <local:UCRouteEvent Route="UCRouteEvent_Route"></local:UCRouteEvent>
   > ```

4. 行为

   是Blend的设计特性，使用行为的地方，也可以使用触发器取代。

   为相关对象赋予相同的行为（将相同的行为封装起来，可以赋予给不同的对象）

   > 步骤1：添加nuget引用
   >
   > Microsoft.Xaml.Behaviors.Wpf 
   >
   > 步骤2：创建一个behavior类继承自Behavior<T>，并且注册相应的事件操作实现
   >
   > T：需要封装行为的控件类型
   >
   > ```c#
   > public class DragBehavior:Behavior<Border>
   > {
   >     /// <summary>
   >     /// 当挂载到对应的对象上时触发
   >     /// </summary>
   >     protected override void OnAttached()
   >     {
   >         base.OnAttached();
   >         //所关联的对象
   >         this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
   >         //
   >     }
   > 
   >     private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
   >     {
   >         //鼠标锁定
   >         this.AssociatedObject.CaptureMouse();
   >         //鼠标释放
   >         this.AssociatedObject.ReleaseMouseCapture();
   >     }
   > 
   >     /// <summary>
   >     /// 对象销毁时触发
   >     /// </summary>
   >     protected override void OnDetaching()
   >     {
   >         this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
   >     }
   > }
   > ```
   >
   > 步骤3：在界面上对应的控件类型上使用封装的行为类
   >
   > ```xaml
   > xmlns:be="http://schemas.microsoft.com/xaml/behaviors"
   > 
   > <Border>
   >     <be:Interaction.Behaviors>
   >         <local:DragBehavior/>
   >     </be:Interaction.Behaviors>                
   > </Border>
   > ```



# 动画

动画本质就是在一个时间段内对象的尺寸、位移、旋转、缩放、颜色、透明度等属性值的连续变化，也包括图形变换的属性

计算机动画：补间动画

WPF动画：

- 必要条件：

  - 对象必需实现IAnimatable接口
  - 关联属性必须时依赖属性
  - 需要有与属性对应的动画类

- 动画分类：

  - 简单线性动画：17个类型名+Animation

    1. DoubleAnimation

       ```xaml
       <Window.Resources>
           <Storyboard x:Key="sb">
               <!--花一秒钟从50变化到200-->
               <DoubleAnimation Duration="0:0:1"
                                From="50"
                                To="200"
                                Storyboard.TargetName="border1"
                                Storyboard.TargetProperty="Width">
               </DoubleAnimation>
               <!--再花一秒钟回到原始样子-->
               <DoubleAnimation Duration="0:0:1"
                                Storyboard.TargetName="border1"
                                Storyboard.TargetProperty="Width"></DoubleAnimation>
           </Storyboard>
       
           <Storyboard x:Key="sb1">
               <!--位移：可以操作translate对象的X或者Y-->
               <DoubleAnimation Duration="0:0:1"
                                To="200"
                                Storyboard.TargetName="b2Translate"
                                Storyboard.TargetProperty="X">                
               </DoubleAnimation>
               <!--沿X方向移动200后还原-->
               <DoubleAnimation Duration="0:0:1" BeginTime="0:0:1"
                                Storyboard.TargetName="b2Translate"
                                Storyboard.TargetProperty="X">
               </DoubleAnimation>
       
               <!--旋转：也可以通过给RotateTransform设置动画变换，改变角度-->
               <DoubleAnimation Duration="0:0:1"
                                By="200"
                                Storyboard.TargetName="b3Rotate"
                                Storyboard.TargetProperty="Angle">
               </DoubleAnimation>
               </Window.Resources>
       
           <Border x:Name="border1" Width="50" Height="50" Background="Orange"></Border>
           <Border x:Name="border2" Width="50" Height="50" Background="Orange">
               <Border.RenderTransform>
                   <TranslateTransform X="0" x:Name="b2Translate"/>
               </Border.RenderTransform>
           </Border>
           <Border x:Name="border3" Width="50" Height="50" Background="Orange">
               <Border.RenderTransform>
                   <RotateTransform Angle="0" x:Name="b3Rotate"/>
               </Border.RenderTransform>
           </Border>
           <Button x:Name="btn">点我</Button>
       ```

    2. ColorAnimation

       ```xaml
       <!--如果对背景色的动画，必须要先为Background设置初始颜色，再通过Background.Color作为属性来做动画
                   （因为Background没有对应的动画类）-->
       <ColorAnimation Duration="0:0:1"
                       To="Blue"
                       Storyboard.TargetName="border2"
                       Storyboard.TargetProperty="Background.Color"></ColorAnimation>
       ```

    3. 

  - 关键帧动画：22个类型名+AnimationUsingKeyFrames

    动画类型：

    1. Linear+[类型名称]+KeyFrame：线性变化关键帧（与简单线性动画处理基本一样）

    2. DiscreteDoubleKeyFrame：离散变化关键帧，不连续变化

       ```xaml
       <!--离散关键帧动画-->
       <StringAnimationUsingKeyFrames Storyboard.TargetName="txt"
                                      Storyboard.TargetProperty="Text">
           <DiscreteStringKeyFrame KeyTime="0:0:0" Value=""/>
           <DiscreteStringKeyFrame KeyTime="0:0:0.5" Value="t"/>
           <DiscreteStringKeyFrame KeyTime="0:0:1" Value="tx"/>
           <DiscreteStringKeyFrame KeyTime="0:0:1.5" Value="txt"/>
       </StringAnimationUsingKeyFrames>
       
       <TextBlock x:Name="txt"/>
       ```

       

    3. SplineDoubleKeyFrame：样条关键帧-》样条函数（二次贝塞尔曲线path）

       ```xaml
       <DoubleAnimationUsingKeyFrames BeginTime="0:0:0" Storyboard.TargetProperty="X"
                                      Storyboard.TargetName="tt1"
                                      RepeatBehavior="Forever">
           <!--样条关键帧动画-->
           <!--keyspline：表示二次贝塞尔曲线的两个控制点-->
           <SplineDoubleKeyFrame KeyTime="0:0:1" Value="310"
                                 KeySpline="0.1,0.6,0.9,0.4"/>
       </DoubleAnimationUsingKeyFrames>
       
       <Border Background="Orange" Width="310">
           <Ellipse Width="10" Height="10" Fill="White" HorizontalAlignment="Left">
               <Ellipse.RenderTransform>
                   <TranslateTransform x:Name="tt1" X="-10"/>
               </Ellipse.RenderTransform>
           </Ellipse>
       </Border>
       ```

       

    4. EasingDoubleKeyFrame：缓冲式关键帧，使用简单动画时的缓动效果

    5. ObjectAnimationUsingKeyFrames：任意类型的关键帧动画，可以对任意类型的属性进行动画变化

  - 路径动画：3个类型名+AnimationUsingPath

    根据Path数据，限定动画路径

    - doubleAnimationUsingPath

      ```xaml
      <!--double路径动画-->
      <!--source:从Path路径中取什么值-->
      <DoubleAnimationUsingPath Storyboard.TargetName="tt"
                                Storyboard.TargetProperty="X"
                                Source="X" Duration="0:0:5">
          <DoubleAnimationUsingPath.PathGeometry>
              <!--表示移动的路径-->
              <PathGeometry Figures="M0 0 100,100 A100 50 0 0 1 400 150"/>
          </DoubleAnimationUsingPath.PathGeometry>
      </DoubleAnimationUsingPath>
      <DoubleAnimationUsingPath Storyboard.TargetName="tt"
                                Storyboard.TargetProperty="Y"
                                Source="Y" Duration="0:0:5">
          <DoubleAnimationUsingPath.PathGeometry>
              <!--表示移动的路径-->
              <PathGeometry Figures="M0 0 100,100 A100 50 0 0 1 400 150"/>
          </DoubleAnimationUsingPath.PathGeometry>
      </DoubleAnimationUsingPath>
      <DoubleAnimationUsingPath Storyboard.TargetName="rt1"
                                Storyboard.TargetProperty="Angle"
                                Source="Angle" Duration="0:0:5">
          <DoubleAnimationUsingPath.PathGeometry>
              <!--表示移动的路径-->
              <PathGeometry Figures="M0 0 100,100 A100 50 0 0 1 400 150"/>
          </DoubleAnimationUsingPath.PathGeometry>
      </DoubleAnimationUsingPath>
      
      <Border Width="100" Height="40" Background="Orange"
              HorizontalAlignment="Left">
          <Border.RenderTransform>
              <TransformGroup>
                  <!--图形变换的顺序非常重要-->
                  <RotateTransform x:Name="rt1"/>
                  <TranslateTransform x:Name="tt"/>
      
              </TransformGroup>
          </Border.RenderTransform>
      </Border>
      ```

      

    - pointAnimationUsingPath

      (X,Y)动画作用在point上

      

    - MatrixAnimationUsingPath

      矩阵路径动画:对象按照位移、角度、矩阵的路径进行变换

      ```xaml
      <!--DoesRotateWithTangent：角度是否要参与动画-->
      <MatrixAnimationUsingPath Storyboard.TargetName="mt"
                                Storyboard.TargetProperty="Matrix"
                                DoesRotateWithTangent="True" Duration="0:0:5">
          <MatrixAnimationUsingPath.PathGeometry>
              <!--表示移动的路径-->
              <PathGeometry Figures="M0 0 100,100 A100 50 0 0 1 400 150"/>
          </MatrixAnimationUsingPath.PathGeometry>
      </MatrixAnimationUsingPath>
      
      <Border Width="100" Height="40" Background="Orange"
              HorizontalAlignment="Left">
          <Border.RenderTransform>
              <TransformGroup>
                  <!--图形变换的顺序非常重要-->
                  <!--<RotateTransform x:Name="rt1"/>
                                  <TranslateTransform x:Name="tt"/>-->
                  <MatrixTransform x:Name="mt"/>
              </TransformGroup>
          </Border.RenderTransform>
      </Border>
      ```

    

  - 

- 关键对象：

  - 动画类型：

    简单线性动画、关键帧、路径动画

    如何选择动画类型：先简单线性动画->关键帧动画->路径动画，在根据需要变化的属性来选择具体的动画类型

  - 故事板——StoryBoard

    - 作用：

      - 控制动画的运行

        1. 开始

           ```xaml
           <Window.Triggers>
               //窗口加载时触发动画开始
               <EventTrigger RoutedEvent="Loaded">
                   <BeginStoryboard Storyboard="{StaticResource sb}"/>
               </EventTrigger>
               //按钮点击时触发
               <EventTrigger RoutedEvent="Button.Click" SourceName="btn">
                   <BeginStoryboard Storyboard="{StaticResource sb1}"/>
               </EventTrigger>
           </Window.Triggers>
           ```

        2. 

      - 动画与对象之间的桥梁

  - 基本属性

    - Duration：消耗时间；时:分:秒(可以是小数)
    - BeginTime：动画开始的时间
    - From：变化的起始数值，也可以不写，不写就会按照控件当前的值开始变化
    - To：变化的目标数值
    - By：从控件当前的值开始增加数值
    - Storyboard.TargetName：动画执行的作用控件
    - Storyboard.TargetProperty：动画执行的控件属性
    - RepeatBehavior：动画重复；Forever：永远重复；某个数值+x：表示重复几次；0:0:0：表示重复执行多长时间。**也可以写在storyBoard上面，表示内部所有的动画都具有该属性，并且是等待所有的动画都执行完毕了才会进行下一次重复**
    - FillBehavior：停止时的保持状态；Stop：回到初始状态；HoldEnd：保持最后的状态
    - AutoReverse：是否自动返回到开始状态。**也可以写在storyBoard上面，表示内部所有的动画都具有该属性，并且是等待所有的动画都执行完毕了才会回到初始状态**
    - SpeedRatio：动画播放速度
    - AccelerationRatio：动画在前0.几加速速率
    - DecelerationRatio：动画在后0.几减速速率；DecelerationRatio与AccelerationRatio加起来不能等于1
    - IsAddtive：将目标属性的当前值添加到动画的初始值，**只能设置在animation里面**
    - IsCumulative：如果动画不断重复就累积结果，**只能设置在animation里面**

  - 

- 动画控制与事件

  - 生命周期事件

    1. Completed
    2. CurrentGlobalSpeedInvalidated：速度变化
    3. CurrentStateInvalidated：状态变化
    4. CurrentTimeInvalidated：时间线变化，基于时间线60帧
    5. RemoveRequested：动画正被一处的时候出发

  - 动画控制

    1. BeginStoryBoard：开始
    2. PauseStoryBoard：暂停
    3. ResumeStoryBoard：恢复
    4. StopStoryBoard：停止
    5. SeekStoryBoard：跳转到某个时刻
    6. SetStoryBoardSpeedRatio：加速、减速

  - 动画帧率

    Timeline.DesireFrameRate

- 

# 控件自定义

如何选择使用控件模板、用户控件还是自定义控件？

> - 自定义控件：注重控件对象的功能，必须遵守WPF的控件规则
>   - 自己实现一个控件，继承现有控件并进行功能扩展
>   - 后台代码与Generic.xaml进行组合
>   - **支持模板重写**
>   - 继承control
> - 用户控件：注重符合控件的组合使用，非常灵活，可以根据控件开发人员自己的意愿进行功能处理
>   - 对各现有控件的集合，组成一个可复用的控件组
>   - xaml和后台代码组成，绑定紧密
>   - 不支持模板重写和样式重写
>   - 继承自user control
> - 控件模板

- 控件模板

  三大模板：控件模板、数据模板、数据容器模板  

  

- UserControl——用户控件

  - 开发思路：

    1. 公用用户控件数据与行为的绑定：如果要对usercontrol进行数据绑定，需要在usercontrol里增加依赖属性，外部将数据绑定在usercontrol的依赖属性上面

       ```c#
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
       ```

       ```xaml
       <UserControl x:Class="WinDemo.UserControls.UserControl1"
                    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
                    xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
                    xmlns:local="clr-namespace:WinDemo.UserControls"
                    mc:Ignorable="d" 
                    d:DesignHeight="450" d:DesignWidth="800">
           <Grid>
               <StackPanel Orientation="Horizontal" Height="30">
                   <Button Width="50" Content="click me"/>
                   <Button Width="50" Content="click me"/>
                   <Button Width="50" Content="click me"/>
                   <TextBox Width="100" Text="{Binding TextValue,RelativeSource={RelativeSource AncestorType=UserControl}}"/>
               </StackPanel> 
           </Grid>
       </UserControl>
       
       ```

    2. 私用用户控件的数据和行为的绑定：直接绑定父界面的dataContext

  -  

  

- CustomControl——自定义控件

  - 关键特性：

    1. [TemplatePart(Name = "elementName",Type =typeof(RepeatButton))]

       用于说明自定义控件里面必须拥有哪个名字的指定类型的控件

    2. 视觉状态

       每个视觉状态都是互斥的，每次只能出发一个视觉状态，其余的视觉状态恢复,并且视觉状态并不是customControl所特有的，usercontrol、window也有；可以被trigger替代

       - 定义

         ```xaml
         <ResourceDictionary
                             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                             xmlns:local="clr-namespace:WinDemo"
                             xmlns:uc="clr-namespace:WinDemo.UserControls">
         
         
             <Style TargetType="{x:Type uc:CustomControl1}">
                 <Setter Property="Template">
                     <Setter.Value>
                         <ControlTemplate TargetType="{x:Type uc:CustomControl1}">
                             <Grid Background="{TemplateBinding Background}"
                                   >
                                 <VisualStateManager.VisualStateGroups>
                                     <VisualStateGroup x:Name="ValueStates">
                                         <VisualState x:Name="Positive">
                                             <!--触发具体的视觉展示，如果没有写就恢复到默认-->
                                         </VisualState>
                                         <VisualState x:Name="Negative"></VisualState>
                                     </VisualStateGroup>
                                 </VisualStateManager.VisualStateGroups>
                                 <!--自定义控件的界面展示和布局-->
                             </Grid>
                         </ControlTemplate>
                     </Setter.Value>
                 </Setter>
             </Style>
         </ResourceDictionary>
         ```

       - 触发视觉状态

         ```c#
         private void UpdateStates()
         {
             bool tag = false;
             //根据不同的条件对视觉状态进行触发
             //参数：哪个控件触发的，视觉状态的名字，是否触发转换
             VisualStateManager.GoToState(this, "Positive", tag);
         }
         ```

  ```c#
  [TemplatePart(Name = "elementName",Type =typeof(RepeatButton))]
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
  }
  ```

  ```xaml
  Generi.xaml
  
  <ResourceDictionary
                      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                      xmlns:local="clr-namespace:WinDemo"
                      xmlns:uc="clr-namespace:WinDemo.UserControls">
  
  
      <Style TargetType="{x:Type uc:CustomControl1}">
          <Setter Property="Template">
              <Setter.Value>
                  <ControlTemplate TargetType="{x:Type uc:CustomControl1}">
                      <Border Background="{TemplateBinding Background}"
                              BorderBrush="{TemplateBinding BorderBrush}"
                              BorderThickness="{TemplateBinding BorderThickness}">
                          <!--自定义控件的界面展示和布局-->
                      </Border>
                  </ControlTemplate>
              </Setter.Value>
          </Setter>
      </Style>
  </ResourceDictionary>
  
  ```

# MVVM

> 一种设计模式
>
> 基于控件的将界面分离出来，业务逻辑不关心界面实现

- 应用分层

  - Models：存放数据模型，如界面中的对象属性
  - Views：页面展示（window、page、usercontrol）
  - ViewModels：业务逻辑

- 数据处理

  - 数据绑定

    依赖属性：数据Model必须继承DependencyObject

    静态属性：属性名称+Changed事件

    数据模型类：实现INotifyPropertyChanged接口，在属性中发布修改事件

    ```c#
    /*
         * INotifyPropertyChanged：通知发布某属性的变化
         */
    public class MainWindoeModel:INotifyPropertyChanged
    {
        public double Value1 { get; set; } = 1;
        public double Value2 { get; set; } = 2;
        private double value3;
    
        public double Value3
        {
            get { return value3; }
            set { 
                value3 = value;
    
                //发布事件：告诉所有绑定了这个属性的界面对象属性需要更新值了
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("Value3"));
            }
        }
    
    
        public event PropertyChangedEventHandler PropertyChanged;
    }
    
    
    public class MainWindowViewModel
    {
        public MainWindoeModel MainModel { get; set; } = new MainWindoeModel();
    
        public void Add()
        {
            MainModel.Value3 = MainModel.Value1 + MainModel.Value2;
        }
    }
    ```

    ```xaml
    <Window.DataContext>
        <!--绑定数据源-->
        <viewmodels:MainWindowViewModel/>
    </Window.DataContext>
    <Grid>
        <StackPanel>
            <TextBox Text="{Binding MainModel.Value1}"/>
            <Slider Value="{Binding MainModel.Value2}" Minimum="0" Maximum="100"/>
            <TextBox Text="{Binding MainModel.Value3}"/>
        </StackPanel>
    </Grid>
    ```

  - Model绑定数据的数据验证

  - 数据模型类的辅助插件

    - PropertyChanged.Fody

      通过特性来实现INotifypropertyChanged的省略实现，减少代码量

      ```c#
      [AddINotifyPropertyChangedInterface]
      public class MainWindoeModel
      {
          /// <summary>
          /// AlsoBotifyForAttribute：实现通知的时候，同时通知指定属性
          /// DoNotNotify：指定不需要自动实现代码改变的通知
          /// DependsOn：指定哪些属性变化的时候，通知当前属性变化
          /// DoNotCheckEquality：强制不做旧值对比（默认情况会自动添加对比代码
          /// ）
          /// </summary>
          [DoNotNotify]
          public double Value1 { get; set; } = 1;
          public double Value2 { get; set; } = 2;
          private double value3;
      
          public double Value3
          {
              get { return value3; }
              set { 
                  value3 = value;
      
                  //发布事件：告诉所有绑定了这个属性的界面对象属性需要更新值了
                  PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("Value3"));
              }
          }
      
      
          public event PropertyChangedEventHandler PropertyChanged;
      }
      ```

- 行为处理

  - 命令

    1. 用途

       - 将语义和调用命令的对象与执行命令的逻辑分开，这允许多个和不同的源调用相同的命令逻辑，并允许针对不同的目标自定义命令逻辑
       - 统一的指示操作是否可用

    2. ICommand

       ```c#
       public class CommandBase : ICommand
       {
           /// <summary>
           /// 通知，触发一次CanExecute
           /// </summary>
           public event EventHandler CanExecuteChanged;
       
           public Action<object> DoExecute { get; set; }
       
           /// <summary>
           /// 具体执行是否可用判断的委托
           /// </summary>
           public Func<object,bool> DoCanExecute { get; set; }
       
           /// <summary>
           /// 绑定对象是否可用
           /// </summary>
           /// <param name="parameter"></param>
           /// <returns>true：可用</returns>
           public bool CanExecute(object parameter)
           {
               return DoCanExecute?.Invoke(parameter) == true;
           }
       
           /// <summary>
           /// 执行过程
           /// </summary>
           /// <param name="parameter">commandParameter参数</param>
           public void Execute(object parameter)
           {
               //具体的实现逻辑
               DoExecute?.Invoke(parameter);
           }
       
       
           /// <summary>
           /// 在命令对象内触发CanExecuteChanged事件
           /// 用于在外部触发命令可用通知
           /// </summary>
           public void DoCanExecuteChanged()
           {
               CanExecuteChanged?.Invoke(this, EventArgs.Empty);
           }
       }
       ```

    3. CommandManager

       为了对所有的属性CanExecute以及内置命令的Canexecute进行管理，默认会按照一定的事件间隔执行CanExecute()。

       ```c#
       public class CommandBase : ICommand
       {
           /// <summary>
           /// 通知，触发一次CanExecute
           /// </summary>
           public event EventHandler CanExecuteChanged
           {
               //将具体的CanExecute逻辑添加到CommandManager里面
               add { CommandManager.RequerySuggested += value; }
               remove { CommandManager.RequerySuggested -= value; }
           }
       
           public Action<object> DoExecute { get; set; }
       
           /// <summary>
           /// 绑定对象是否可用
           /// </summary>
           /// <param name="parameter"></param>
           /// <returns>true：可用</returns>
           public bool CanExecute(object parameter)
           {
               return DoCanExecute?.Invoke(parameter) == true;
           }
       
           /// <summary>
           /// 执行过程
           /// </summary>
           /// <param name="parameter">commandParameter参数</param>
           public void Execute(object parameter)
           {
               //具体的实现逻辑
               DoExecute?.Invoke(parameter);
           }
       
           /// <summary>
           /// 在命令对象内触发CanExecuteChanged事件
           /// 用于在外部触发命令可用通知
           /// </summary>
           public void DoCanExecuteChanged()
           {
               //CanExecuteChanged?.Invoke(this, EventArgs.Empty);
           }
       }
       ```

       强制触发CanExecute事件：

       ```c#
       //通过CommandManager强制触发CanExecuteChanged事件；也可以在一定周期内默认自动触发所有的canExecute事件有一定的性能损耗
       CommandManager.InvalidateRequerySuggested();
       ```

       添加：

       ```c#
       //通过CommandManager添加是否可用的判断逻辑
       BtnCommand.CanExecuteChanged += BtnCommand_CanExecuteChanged;
       ```

       

    4. 预置命令

       - ApplicationCommands：new、open、copy、cut、print等

         ```xaml
         <Button Command="ApplicationCommands.Open" Content="{Binding
                                                             RelativeSource={RelativeSource Self},Path=Command.Text}"
                 CommandManager.Executed="Button_Executed"
                 CommandManager.CanExecute="Button_CanExecute"/>
         <Button Command="ApplicationCommands.New" Content="{Binding
                                                            RelativeSource={RelativeSource Self},Path=Command.Text}"
                 CommandManager.Executed="Button_Executed"
                 CommandManager.CanExecute="Button_CanExecute"/>
         ```

         ```c#
         private void Button_Executed(object sender, ExecutedRoutedEventArgs e)
         {
             //需要自己实现处理逻辑
         }
         
         private void Button_CanExecute(object sender, CanExecuteRoutedEventArgs e)
         {
             //需要自己实现处理逻辑
             e.CanExecute = true;
         }
         ```

         

         可在Window节点对预置的命令进行统一的处理

         ```xaml
         <Grid>
             <Grid.CommandBindings>
                 <CommandBinding Command="ApplicationCommands.Open"
                                 Executed="Button_Executed"
                                 CanExecute="Button_CanExecute"/>
             </Grid.CommandBindings>
             <StackPanel>
                 <Button Command="ApplicationCommands.Open" Content="{Binding                                                       RelativeSource={RelativeSource Self},Path=Command.Text}"
                         CommandManager.Executed="Button_Executed"              CommandManager.CanExecute="Button_CanExecute"/>
                 <Button Command="ApplicationCommands.Open" Content="{Binding                                                          RelativeSource={RelativeSource Self},Path=Command.Text}"/>
                 <Button Command="ApplicationCommands.New" Content="{Binding                                                          RelativeSource={RelativeSource Self},Path=Command.Text}"
                         CommandManager.Executed="Button_Executed"              CommandManager.CanExecute="Button_CanExecute"/>
             </StackPanel>
         </Grid>
         ```

       - MediaCommands：play、stop等

       - NavigationCommands：gotopage、lastPage、Favorites

       - ComponentCommands：ScrollByLine、MoveDown、ExtendSelectionDown

       - EditingCommands：delete、ToggleBold

    5. 路由命令

       - RouteCommand

       - RouteUICommand

         继承于RoutedCommand，同样需要自定义command的具体实现

         自定义一个routedUICommand:

         ```c#
         public partial class MainWindow : Window
         {
             public RoutedUICommand TestCommand { get; set; }
         
             public MainWindow()
             {
                 InitializeComponent();
         
                 //定义快捷键
                 InputGestureCollection inputGestureCollection = new InputGestureCollection()
                 {
                     new KeyGesture(Key.T,ModifierKeys.Alt)
                 };
                 TestCommand = new RoutedUICommand("测试按钮", "TestBtn", typeof(MainWindow),inputGestureCollection);
             }
         
             private void Button_Executed(object sender, ExecutedRoutedEventArgs e)
             {
                 //需要自己实现处理逻辑
             }
         
             private void Button_CanExecute(object sender, CanExecuteRoutedEventArgs e)
             {
                 //需要自己实现处理逻辑
                 e.CanExecute = true;
             }
         }
         ```

         使用：

         ```xaml
         <Button Command="{Binding TestCommand,RelativeSource={RelativeSource AncestorType=Window}}"
                 CommandManager.Executed="Button_Executed"
                 CommandManager.CanExecute="Button_CanExecute"
                 Content="{Binding RelativeSource={RelativeSource Self},Path=Command.Text}"/>
         ```

    6. InputBindings

       主要用于将命令绑定到控件的其他鼠标事件上

       > 鼠标输入事件：
       >
       > None：不执行任何操作
       > LeftClick：单击鼠标左键
       > RightClick：单击鼠标右键
       > MiddleClick：单击鼠标中键
       > WheelClick：鼠标滚轮
       > LeftDoubleClick：双击鼠标左键
       > RightDoubleClick：双击鼠标右键
       > MiddleDoubleClick：双击鼠标中键

       ```xaml
       <Button Content="click me">
           <Button.InputBindings>
               <MouseBinding MouseAction="LeftClick"
                             Command="{Binding MainModel.BtnCommand}"/>
       
               <!--快捷键-->
               <KeyBinding Key="T" Modifiers="Alt"
                           Command="{Binding MainModel.BtnCommand}"/>
           </Button.InputBindings>
       </Button>
       ```

    7. 任意事件的绑定

       Microsoft.Xaml.Behaviors.Wpf

       System.Windows.Interactivity

- 跨模块交互

  在低耦合的情况下，VM与VM进行交互，VM与窗口交互

  - VM与窗口交互——窗口管理

    通过一个第三方对象来进行管理

    1. 第一种方式——windowManager

       ```c#
       public class WindowManager
       {
           /// <summary>
           /// 注册的窗口存放
           /// 通过反射实现
           /// </summary>
           static Dictionary<string, Type> _windows = new Dictionary<string, Type>();
       
           public static void Register(Type type)
           {
               if (!_windows.ContainsKey(type.Name))
               {
                   _windows.Add(type.Name, type);
               }
           }
       
           /// <summary>
           /// 显示窗口界面
           /// </summary>
           /// <param name="winKey">传窗口的名称而不是窗口的类型，为了解耦合</param>
           public static void ShowDialog(string winKey,object dataContext)
           {
               if (_windows.ContainsKey(winKey))
               {
                   Type type = _windows[winKey];
                   var win = (Window)Activator.CreateInstance(type);
                   win.DataContext = dataContext;
                   win.ShowDialog();
               }
           }
       }
       ```

       

    2. 第二种方式——IOC

  - VM与VM交互——动作管理

    也是通过一个第三方类来实现

    ```c#
    //也可以通过泛型来控制参数类型
    
    public class ActionManager
    {
        static Dictionary<string, Action> _actions = new Dictionary<string, Action>();
    
        public static void Register(string name,Action action)
        {
            if (!_actions.ContainsKey(name))
            {
                _actions.Add(name, action);
            }
        }
    
        public static void Invoke(string name)
        {
            if (_actions.ContainsKey(name))
                _actions[name]();
        }
    }
    ```

- 框架

  1. MvvmLight

     - 主要程序库

       1. GalaSoft.MvvmLight.dll

          - GalaSoft.MvvmLight：最顶层命名控件，核心功能

            主要成员：ObservableObject、ViewModelBase(实现数据通知相关的内容)、ICleanup

          - GalaSoft.MvvmLight.Command：主要包含依赖命令的定义

            主要对象：RelayCommand、RelayCommand<>(对ICommand的封装)

          - GalaSoft.MvvmLight.Helpers：框架辅助类，共框架内部使用

          - GalaSoft.MvvmLight.Messaging：消息类，提供全局消息通知，可以认为是全局的event事件

            主要对象：Messager、NotificationMessage

          - GalaSoft.MvvmLight.Views：与view紧密结合

            主要对象：IDialogService、INavigationService

       2. GalaSoft.MvvmLight.Extras.dll

          - GalaSoft.MvvmLight.Ioc：依赖注入使用的容器

            主要对象：ISimpleIoc、SimpleIoc

       3. GalaSoft.MvvmLight.Platform.dll

          - GalaSoft.MvvmLight.Command

            主要对象：事件绑定的时候，需要传递事件参数

          - GalaSoft.MvvmLight.Threading

            主要对象：DispatchHelper

     - 项目结构

     - 常用对象

       1. ViewModelLocator

          - 创建IOC容器

            ```c#
            //依赖注入时，用来创建对象
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            //注册一个标识为key1的对象类型
            SimpleIoc.Default.Register<IService>(()=>new Service.Service(),"key1");
            ```

          - 将对象类型注册到IOC对象里面

            默认注册的对象是单例的

            ```c#
            //Ioc容器里注册了一个MainViewModel对象，此时并没有创建实例
            SimpleIoc.Default.Register<MainViewModel>();
            ```

            如何获取指定标识的对象实例：每个标识应该是同一个对象

            ```
            return ServiceLocator.Current.GetInstance<MainViewModel>("key1");
            ```

            如何注册瞬时对象：通过给每个对象设置标识

            ```c#
            return ServiceLocator.Current.GetInstance<MainViewModel>(Guid.NewGuid().ToString());
            ```

          - 从IOC里面声明并获取对象实例

            ```c#
            /// <summary>
            /// 从Ioc容器里面获取实例
            /// 只要一调用就会创建这个实例
            /// </summary>
            public MainViewModel Main
            {
                get
                {
                    return ServiceLocator.Current.GetInstance<MainViewModel>();
                }
            }
            ```

          - 如何使用IOC容器

            1. 在View里面通过IOC绑定DataContext

               ```xaml
               <Application x:Class="MvvmLightFWDemo.App" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" xmlns:local="clr-namespace:MvvmLightFWDemo" StartupUri="MainWindow.xaml" xmlns:d="http://schemas.microsoft.com/expression/blend/2008" d1p1:Ignorable="d" xmlns:d1p1="http://schemas.openxmlformats.org/markup-compatibility/2006">
                   <Application.Resources>
                       <ResourceDictionary>
                           <vm:ViewModelLocator x:Key="Locator" d:IsDataSource="True" xmlns:vm="clr-namespace:MvvmLightFWDemo.ViewModel" />
                       </ResourceDictionary>
                   </Application.Resources>
               </Application>
               ```

               ```xaml
               <Window x:Class="MvvmLightFWDemo.MainWindow"
                       xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                       xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                       xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
                       xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
                       xmlns:local="clr-namespace:MvvmLightFWDemo"
                       mc:Ignorable="d"
                       DataContext="{Binding Source={StaticResource Locator},Path=Main}"
                       Title="MainWindow" Height="450" Width="800">
                   <Grid>
               
                   </Grid>
               </Window>
               ```

            2. 

          - CleanUp

            统一的对某个注册的对象实例进行资源释放

            ```c#
            public static void Cleanup<T>() where T:ViewModelBase
            {
                // TODO Clear the ViewModels
            
                //统一进行资源释放
                ServiceLocator.Current.GetInstance<T>().Cleanup();
                SimpleIoc.Default.Unregister<T>();  //释放对象，让其在内存中没有任何引用
                SimpleIoc.Default.Register<T>();  //重新注册一个全新对象
            }
            ```

       2. ViewModelBase

          这个类继承了INotifyPropertyChanged接口，可以通知属性改变

          - Cleanup

            需要手动调用函数来触发资源的释放，一般通过手动触发ViewModelLocator里的CleanUp()进行触发

            ```c#
            ViewModelLocator.Cleanup<MainViewModel>();
            ```

            ```c#
            public class MainViewModel : ViewModelBase
            {
                /// <summary>
                /// Initializes a new instance of the MainViewModel class.
                /// </summary>
                public MainViewModel()
                {
                    ////if (IsInDesignMode)
                    ////{
                    ////    // Code runs in Blend --> create design time data.
                    ////}
                    ////else
                    ////{
                    ////    // Code runs "for real"
                    ////}
                }
            
                public override void Cleanup()
                {
                    ///todo 资源释放逻辑
                }
            }
            ```

          - 属性变化

            ```c#
            private int myVar;
            public int MyProperty
            {
                get { return myVar; }
                set {
                    // Set(ref myVar, value);  //提供函数，进行复制以及通知属性变化
                    myVar = value;
                    RaisePropertyChanged(); //通知属性变化
                }
            }
            ```

       3. ObservableObject

          这个类继承了INotifyPropertyChanged接口，可以通知属性改变,主要是针对Model层级的

       4. RelayCommand、RelayCommand<T>

          这个类继承ICommand接口，用于命令绑定；**其中如果是带参数的命令，一般参数是引用类型而不是值类型**

          ```c#
          //无参数的命令
          public ICommand BtnCommand
          {
              get => new RelayCommand(() =>
                                      {
                                          ///todo 命令具体实现
                                      });
          }
          
          //带参数的命令
          public ICommand BtnCommand1
          {
              get => new RelayCommand<string>((param) =>
                                              {
                                                  ///todo
                                              });
          }
          ```

       5. Messenger

          - 消息注册与发送

            ```c#
            //注册消息：接受消息的对象，收到消息要执行的逻辑处理
            Messenger.Default.Register<string>(this, ReceiveMsg);
            
            //消息发送
            Messenger.Default.Send<string>("hello");
            ```

            注册和发送指定名字的消息

            ```c#
            //注册消息：添加注册监听接受指定key的消息
            Messenger.Default.Register<string>(this, "msgKey", ReceiveMsg);
            Messenger.Default.Send<string>("hello", "msgKey");
            ```

            发送到指定对象的消息

            ```c#
            Messenger.Default.Send<string,TargetClassName>("hello");
            ```

          - 自定义消息参数

            因为Send可传泛型参数，所以可自定义消息类来发送，同时可通过委托的形式来实现获取发送消息执行后的返回值。

          - 参数对象

            - NotificationMessage、NotificationMessage<T>

              ```c#
              Messenger.Default.Register<NotificationMessage>(this, ReceiveNotificationMsg);
              Messenger.Default.Register<NotificationMessage<int>>(this, ReceiveNotificationMsg);
              
              private void ReceiveNotificationMsg(NotificationMessage message)
              {
              
              }
              
              private void ReceiveNotificationMsg<T>(NotificationMessage<T> message)
              {
              
              }
              ```

              ```c#
              NotificationMessage<int> nm1 = new NotificationMessage<int>(100, "notification");
                                Messenger.Default.Send<NotificationMessage>(nm);
              Messenger.Default.Send<NotificationMessage<int>>(nm1);
              ```

            - NotificationMessageAction、NotificationMessageAction<T>

              发送方带回调函数的消息发送

              ```c#
              Messenger.Default.Register<NotificationMessageAction>(this, ReceiveNotificationMsg);
              
              private void ReceiveNotificationMsg(NotificationMessageAction message)
              {
                  ///接收方执行处理逻辑
                  ///
              
                  //执行回调
                  message.Execute();  
              }
              ```

              ```c#
              NotificationMessageAction nma = new NotificationMessageAction("notification", () =>
              {
                      //这个发送消息被接受处理后要执行的回调函数
              });
              Messenger.Default.Send<NotificationMessageAction>(nma);
              ```

            - PropertyChangedMessage

              当属性变化时会被触发消息接收；

              ```c#
              Messenger.Default.Register<PropertyChangedMessage<string>>(this, (data) =>{ 
                  if (data.PropertyName == "")
                  {
              
                  }
              });
              ```

              被触发的条件：只有当broadcast赋值为true以及泛型类型与属性类型相同时才可以触发（所有相同类型的变化都会被触发，需要手动进行属性名称的判断）

              ```c#
              private string myVar;
              public string MyProperty
              {
                  get { return myVar; }
                  set {
                      Set(ref myVar, value,broadcast:true);  //提供函数，进行复制以及通知属性变化
                      myVar = value;
                      RaisePropertyChanged<string>("MyProperty",broadcast:true); //通知属性变化
                  }
              }
              ```

       6. IDialog

          

       7. DispatcherHelper

          框架提供调用UI线程

          ```c#
          app.xaml.cs
          //初始化DispatcherHelper的实例，如果不执行这个操作，后续的所有DispatcherHelper操作都不会生效
          DispatcherHelper.Initialize();
          ```

          ```c#
          //检查是否在UI线程上，如果不在就将操作挂在UI线程上执行
          DispatcherHelper.CheckBeginInvokeOnUI(() =>{
              //操作,这里是多线程的异步操作
          });
          //等效于
          Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                                                                {
          
                                                                }));
          ```

          

       8. 

     - 

  2. 

- 



# 控件库

- mahapps
- handycontrol

- livecharts
- lightingChart.net
- scottplot