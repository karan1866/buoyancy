   M S B A M L   `   `   `  ����[  WSystem.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms  5       FizzDock   FizzDock.wpfIconSettings   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      6x,http://schemas.microsoft.com/winfx/2006/xaml   5      Kwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms   5      -   5        $��Window1��5   )     � Topmost  .  � Icon$ Fizz.ico��5   2    � WindowStartupLocation$ CenterScreen=�5       � 
ResizeMode$ NoResize=�6*    � WindowStyle$
 None=�5   <    � ShowInTaskbar .   � Title$ Icon Settings��5      $	��434��6    � AllowsTransparency . 
������  � 	Resources 5         ListBoxTemplate	 �� 
TargetType
 �� Padding �� CornerRadius � RowDefinitions �� ElementName �� Path �� Triggers :� RoutedEvent �� 
TargetName �� TargetProperty �� From g� Duration	 �� To P� Property P� Value �� 
TargetName �� Property �� Value��%�  &	        �� 5   
   "	 ��62   .R�5	      �� $��bg��6_   $��5q�6   $	��152��6    $
 3q�6/   
�������$	��148��6S   $��2q�6i   ���#858585$ 4��6�   ._�5
      �  5      �� 5      $��24 �6(   �� 5      �� 5      �� 5      .+�5      �� $
��Text��68   $��0,0,7,0q�6'   $��Stretch=�6D   $��Calibri)�6b   $��13(�6�   ��. 6�   �� 5      $��IconDel��61   -   5        $y�Hidden=�5      $��0,0,7,0q�6@   $��Right=�6Q   $��Center=�6m   $��4��6�   .��6�   DEL6�   �� 5      $��PathChng��62   -   5        $y�Visible=�5      $��0,0,35,0q�6B   $��Right=�6T   $��Center=�6p   $��4��6�   .��6�   CHG6�   �� 5      $��Rename��61   -   5        $y�Hidden=�5      $��0,0,7,0q�6?   $��Right=�6P   $��Center=�6l   .��6�   RNM6�   �� 5      $��MoveUp��6C   -   5        $y�Hidden=�5      $��29,0,0,0q�61   $
��Left=�6Q   $��Center=�6l   $��4��6�   .��5      �� $��2,0,2,0q�6$   $��Calibri)�65   $
��Bold"�6J   /\6`   5      �� 5      $��MoveDown��6B   -   5        $��7,0,0,0q�5      $y�Hidden=�6.   $
��Left=�6R   $��Center=�6m   $��3��6�   .��5      �� $��2,0,2,0q�6$   $��Calibri)�65   $
��Bold"�6J   \/6`   5       � 5"      $��Image��6/   $y�Hidden=�6   $��7,1,2,5q�6<   $��1��6M   $
��Left=�6Z   $��58��6u   $��58��6�   .+�5#      �� ���#888888$��1.5,1.5,1.5,1.5q�67   $	 5.5��6Y   �� 5$      $��ImageSource��6?   $y�Hidden=�6    $��5q�64   $��Uniform=�6�   $��Stretch=�6�   $��Stretch=�6�   $��48��6�   $��48��6�   ��6R   �� $	 img��$ Source�6�   5%      �� 5'      $
��Path��6E   $
��Wrap=�6$   $��2��68   $��	12,3,18,8q�6Q   $y�Visible=�6d   ��6y   �� $ lstPath��$ SelectedItem�6�   5(      5)       5*      :� 5+      $ ListBoxItem.Selected��6   [�5,      �� 5-      .��5.      �� .��5/   "   �� $ bg��61   $ BorderBrush.Color�6L   $ #858585X�6z   $ 0:0:0.3D�6�   $ WhiteX�6�   50      51      53      :� 54      $ ListBoxItem.Unselected��6   [�55      �� 56      .��57      �� .��58   "   �� $ bg��61   $ BorderBrush.Color�6L   $ WhiteX�6z   $ 0:0:0.5D�6�   $ #858585X�6�   59      5:      5<      P� 5=       �@��
IsSelected . . �5>      �� $ bg��6    � ��$ -1,6,0,6q�6?   �� 5?      $ bg��6    � ��$ 3,3,3,3q�6H   �� 5@      $ bg��6    �@��CornerRadius$ 10��6E   �� 5B      $ bg��6    � ��
 �����6C   �� 5C      $ bg��6    � ��$	 152��6>   �� 5D      $ IconDel��6    � y�$ Visible=�6H   �� 5E      $ MoveUp��6    � y�$ Visible=�6G   �� 5F      $ Rename��6    � y�$ Visible=�6G   �� 5G      $ MoveDown��6    � y�$ Visible=�6I   �� 5H      $ Image��6    � y�$ Visible=�6F   �� 5I      $ ImageSource��6    � y�$ Visible=�6L   �� 5J      $
 Text��6    � ��$ 16(�6M   �� 5K      $
 Text��6    � ��$
 Bold"�6O   5L      P� 5M       �@��
IsSelected .  . �5N      �� $ bg��6    � ��$ 24��6?   �� 5O      $
 Text��6    � ��$ -1q�6A   �� 5P      $ bg��6    � ��$ 0,0,0,0q�6?   5Q      P� 5R       �@J�	IsVisible .  . �5S      �� $ ImageSource��6    6H   �� $	 img��$ Source� � ��5T      5V      5W      .��5Z      �� -   5        $	��brd��5Z      $	��434��6   $	��651��6%   ._�5[   
   � .+�5\      �� -   5        $
��Move��5\   &   $ 5��6   ��5]      �� 5^       �� Color$ Black��6&    �� Opacity$	 0.7Q�64   5`      �� 5a      $
 15q�6   ._�5b      � -	   5        $��grdMain��5b      .+�5c      �� -
   5        $��lstName��5c   Z   ���#e9e9e9$��7q�63   $
��Left=�6?   $	��197��6i   $��1q�6u   5e      �� 5f      -   5        $��lstIconList��5f   5   $��0,25.277,48,0q�6    �� SelectedItem$ 0��6H   $��Right=�6Y   $	��128��6u   $y�Hidden=�6�   $��86.723��6�   $	��Top=�6�   �� 5g      -   5        $��lstPath��5g   4   $��264,54,182,0q�6   $y�Hidden=�6C   $��56��6W   $	��Top=�6c   �� 5h      -   5        $	��img��5h   M   $��Right=�6   $��0,130,73,35q�68   $
��Fill=�6X   $	��200��6g   $y�Hidden=�6s   �� 5i      -   5        $
��img2��5i   K   $
��Left=�6   $��
10,0,0,-32q�67   $
��Fill=�6W   $y�Hidden=�6f   $	��200��6z   $	��201��6�   $��Bottom=�6�   �� 5j      -   5        $��TabControl1��5j   4   $��	203,0,0,0q�6!    �� SelectedIndex$ 0��6G   .@�5k      �� -   5        $��TabItem1��5k   1   $��Icons��6"   .��5l      � -   5        $��grdIcons��5l   6   
�������{�.  .+�5m   "   �� -   5        $��lstIcons��5m   O   $��Stretch=�6*   $	��4.5q�6_   $��Disabled=�6m   $
��Auto=�6�   $	��Top=�6�   $��0,0,0,0q�6�   ��5n   &   �� 5o   *   .R�5p   .   � 5q   +   5s   #   5t      5u      �� 5v      -   5        $��TabItem2��5v   /   $	��New��6"   .��5w      � 
�������.+�5x   "   �� $��
Properties��6+   $��8,6,6,0q�6?   $	��150��6P   $	��Top=�6]   .��5y   &   � 
�������.+�5z   *   �� -   5        $��txtName��5z   V   $��23��62   $��121,9.447,12,0q�6>   $	��Top=�6e   $��Center=�6}   ���	#FF7F9DB9$��1q�6�   �� 5{   *   -   5        $��	txtTarget��5{   T   $��23��63   $��121,39,36,0q�6?   $	��Top=�6e   ���	#FF7F9DB9$��Center=�6�    �� Text$  ��6�     �� IsDropDownOpen  .  ! �� 
IsEditable! . $��0q�6�   .@�5|   .   �� .��6;   My Computer6H   �� 5}   .   .��6;   My Documents6I   �� 5~   .   .��6;   My Network Places6N   �� 5   .   .��6;   My Pictures6H   �� 5�   .   .��6;   
My Music6E   �� 5�   .   .��6;   Control Panel6J   �� 5�   .   .��6;   Shutdown Command6M   �� 5�   .   .��6;   Log Off Command6L   �� 5�   .   .��6;   Restart Command6L   5�   +   �� 5�   *   -   5        $��Label1��5�   O   $��23��60   $��	16,10,0,0q�6<   $	��Top=�6]   $
��Left=�6u   $��99��6�   $	��Top=�6�   $��Right=�6�   .��6�   Name6�   �� 5�   *   -   5        $��	lblTarget��5�   �   $��23��60   $
��Left=�6<   $��Right=�6W   $��	16,39,0,0q�6z   $	��Top=�6�   $	��Top=�6�   $��99��6�   .��6�   Target6�   �� 5�   *   $y�Hidden=�63   $��Preview��6G   $��73��6X   $��Right=�6d   $��	0,0,12,34q�6�   $��66��6�   $��Bottom=�6�   .��5�   .   � 
�������.+�5�   2   �� -   5        $��
imgPreview��5�   p   $��Center=�68   $��Center=�6U   $��48��6�   $��48��6�   $
��Fill=�6�   5�   /   5�   +   �� 5�   *   -   5        $��
cmdAddIcon��5�   i   $��23��61   $
��Left=�6=   $��6,0,0,6q�6X   $��Bottom=�6{   $��75��6�   .��6�   Add6�   �� 5�   *   -   5        $��	cmdBrowse��5�   n   $��23��61   $��Right=�6=   $��290,39,12,0q�6Y   $	��Top=�6   .��6�   ...6�   	� 5�   *   -   5        $��	rdbTarget��5�   �   $y�Hidden=�66   $��16��6J   $
��Left=�6V   $��	26,45,0,0q�6q   " d� 	IsChecked$
" TrueY�6�   $	��Top=�6�   $��20��6�   6�   �� 5�   *   -   5        $��	txtFolder��5�   k   ���	#FF7F9DB9$��23��6J   $��121,68,36,0q�6V   $�� ��6|   $	��Top=�6�   $��Center=�6�   {�.  $y�Hidden=�6�   �� 5�   *   -   5        $��	lblFolder��5�   �   $��23��60   $
��Left=�6<   $��Right=�6W   $��	16,68,0,0q�6z   $	��Top=�6�   $	��Top=�6�   $��99��6�   {�.  $y�Hidden=�6�   .��6  Popup Folder6  �� 5�   *   -   5        $��
cmdBrowse2��5�   l   $��23��61   $��Right=�6=   $��	0,68,12,0q�6Y   $	��Top=�6~   $��17.99��6�   {�.  $y�Hidden=�6�   .��6�   ...6�   	� 5�   *   -   5        $��	rdbFolder��5�   p   $��16��66   $
��Left=�6B   $��	26,74,0,0q�6]   $	��Top=�6�   $��20��6�   $y�Hidden=�6�   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�      -    5        $	��cls��5�   ,   $��25��6   $��25��6    $��Center=�67   $��Center=�6[   $��Right=�6}   $��0,5,10,0q�6�   $	��Top=�6�   .��6�   X6�   5�      5�      5�      