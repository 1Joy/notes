   M S B A M L   `   `   `  ����B  >WinDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WinDemoWinDemo  5   	       WinDemo.StyleWin   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WinDemo     5   	     � Title$  StyleWin��5   	   $	��450��6   $	��800��6'    � 	Resources 5	         ButtonStyle �� 
TargetType �� Property �� Value �� Triggers ]� 
Conditions �� Property �� Value	 :� RoutedEvent
 g� Duration	 W� To �� TargetProperty��%�  &	        �� 5
   
   " ��6$   .�5      ��  � ��
 � � �6+    5      ]� 5       5      �� 5       �@J�IsMouseOver$
 True��6;   �� 5       � ��$
 True��69   /�5      �� 5       � ��
 �  ��67   5      :� 5      $	 Button.Click��6   .[�5      �� .��5      �� .��5      W� $
 0:0:1D�6.    3006?   $ Width�6H   5      5      5      5      5      .��5       � .+�5!   
   �� -   5         ��Name$	 Btn��5!      #��[  6   $	��200��6C   $	��100��6O   .��6\   dianwo6d   5"       � Triggers 5#      :� 5$   
   $	 Button.Click��6    :� 
SourceName$	 Btn��62   .[�5%      �� .��5&      �� .��5'      W� $
 0:0:1D�6&    30067    �� 
TargetName$	 Btn��6@   $ Width�5(   &   5)      5*      5+      5-      