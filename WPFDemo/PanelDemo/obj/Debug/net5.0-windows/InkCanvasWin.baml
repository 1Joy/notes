   M S B A M L   `   `   `  ����D  @PanelDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:PanelDemo	PanelDemo  5   	       PanelDemo.InkCanvasWin   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:PanelDemo     5   	   
������   � AllowsTransparency$
  True��5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�5   	    � WindowState$ Normal=�5   	    � Title$ InkCanvasWin��5   	   $	��450��6   $	��800��6+   .��5      �  � RowDefinitions 5   
   �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   ��  �� EditingMode$	 Ink=�6    �� DefaultDrawingAttributes 5      *  $System.Windows.Ink.DrawingAttributes  5      	   Color$
	 Blue��6$   
   Width$
 10Q�61      Height$ 10Q�6<   .H�5      �� $��画笔��6   5      �� 5   
   $��1��6   $ Select=�6$   .H�5      �� $��托拽��6   �� 5      $��button1��6   �� 5       $��button2��6   �� 5!      $��button3��6   5"      �� 5#   
   $��1��6   $ GestureOnly=�6$   .H�5$      �� $��托拽��6   �� 5%      $��button1��6   �� 5&      $��button2��6   �� 5'      $��button3��6   5(      5)      5*      