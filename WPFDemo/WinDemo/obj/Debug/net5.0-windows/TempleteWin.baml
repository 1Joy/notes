   M S B A M L   `   `   `  ����B  >WinDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WinDemoWinDemo  5   	       WinDemo.TempleteWin   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WinDemo     5   	     � Title$  TempleteWin��5   	   $	��450��6   $	��800��6*    � 	Resources 5	      �� 5
   
    �� Type   WinDemo.Person"  6      Key	datas  .s�5           Name$	 joy��6   
   Age$ 20��6&      Left$ 0��6/   
   Top$ 0��68     5      $
 joy1��6   $ 22��6'   $ 20��60   $ 60��6:     5      $
 joy2��6   $ 23��6'   $ 10��60   $ 30��6:     5      $
 joy3��6   $ 24��6'   $ 24��60   $ 50��6:   5      �� 5   
   
NormalTemp  .R�5      � 
��� � �.+�5      �� ��6   �� *Name+�� 5      ��6   �� *Age+5      5      �� 5   
   WarnTemp  .R�5      � 
���  ��.+�5      �� ��6   �� *Name+�� 5      ��6   �� *Age+5      5      �� 5   
   NormalStyle   �� 
TargetType" ��6$   .�5      ��  �� Property � ��	 �� Value$	 20(�6)   �� 5        � ��
	 � � �6+   �� 5!       � ��	 5"      �� 5#      
 �� 
TargetType"
 ��6&   .R�5$      � .+�5%      �� ��6(   �� *Name+�� 5&      ��6(   �� *Age+5'      5(      5*      5+      �� 5-   
   	WarnStyle  " ��6"     NormalStyle �� BasedOn# [ 6<   .�5.      ��  � ��
	 �  ��6+   5/      .��51      � .+�52   
   �� .@�53      �� $	��AAA��6   �� 54      $	��bbb��6   �� 55      $	��ccc��6   �� 56      $	��ddd��6   �� 57      $	��eee��6   58      5X      5Y      