   M S B A M L   `   `   `  ����  
SprintDock    SprintDock.wpfMain   5   	   W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      -   5        $��wpfMain��5   .     � WindowStyle$
  None=�5   <    � AllowsTransparency .  � Title$ wpfMain��5      $	��846��6   $	��729��6"    � WindowState$ 	Maximized=�6=   
������ .��5      � .+�5   
   � -   5        $��grdDock��5      
���   �$	��200��61   $��64��6=   $��Center=�6I   $	��Top=�6f   .+�5      � -   5        $��grdBG��5      $��64��6!   .+�5      �� -   5        $��	imgCenter��5      $
��Fill=�6)   $��64��68   6E   �� 5	      -   5        $��imgLeft��5	      $
��Fill=�6'   $
��Left=�67   $��64��6R   $��0,0,0,0q�6]   6p   �� 5
      -   5        $��imgRight��5
      $
��Fill=�6(   $��Right=�67   $��64��6S   6`   5      � 5      -   5        $��Icons��5   &   $��	70,0,70,8q�6   $��48��63   $��48��6>   $��Bottom=�6J   6g   5      �� 5   
   $	��525��6   $	��518��6    �� Padding$ 10q�6*    �� CornerRadius$ 5��67   $��Center=�6I   $��Center=�6d   ��5      �� 5       �� Color$ Black��6"    �� Opacity$	 0.8Q�60   ._�5      � -   5        $��grdSettings��5   d   $	��505��6   $	��498��6   $��Center=�6,   $��Center=�6G   .+�5      �� -	   5        $��	GroupBox2��5   8   $��General��6   $��73��6,   $	��Top=�6I   $��6,12,6,0q�6a   .��5      � .+�5      �� -
   5        $��chkAutoHide��5   6   $��	12,12,0,0q�6#   $��15.723��6I   $
��Left=�6Y   $	��Top=�6t   $	��120��6�   .��6�   	Auto Hide6�   �� 5      -   5        $��
chkStartup��5   a   $��15.723��6#   $
��Left=�63   $��	12,30,0,0q�6N   $	��Top=�6s   $	��120��6�   {�. .��6�   Run at Startup6�   5      5      �� 5      -   5        $��	GroupBox3��5   M   $��Effects��6   $	��159��6,   $��
6,91,6,249q�69   .��5      � .+�5      �� -   5        $��sliBlur��5   ?   $��22��6!   $��6,32,6,0q�6-   $	��Top=�6N    � Maximum$	 100Q�6f   �� 5      -   5        $��sliGlow��5   N   $��22��6!   $��	6,0,87,22q�6-   $	 100Q�6@   $��Bottom=�6]   	 � Minimum$	 0Q�6x   
 �� Interval$
 1��6�   �� 5      -   5        $��Label1��5   Y   $��14��6    $
��Left=�6,   $��6,12,0,0q�6G   $��0q�6g   $	��Top=�6s   $	��120��6�   .��6�   Blur Effect:6�   �� 5       -   5        $��Label2��5    M   $
��Left=�6    $��6,0,0,50q�6;   $��0q�6[   $	��120��6g   $��13.723��6s   $��Bottom=�6�   .��6�   Outer Glow Effect:6�   �� 5!      -   5        $��cmdGlowColor��5!   [   $��23��6!   $��Right=�6-   $��0,0,6,22q�6I   $��Bottom=�6o   $��75��6�   .��6�   
Glow Color6�   5"      5#      �� 5$      -   5        $��	GroupBox1��5$   <   $
��Skin��6   $��	6,256,6,6q�6)   $	��177��6M   $	��Top=�6Z   .��5%      � $��0,0,0,0q�6   .+�5&      �� -   5        $��cmbSkin��5&   5   $��6,28,6,0q�6#   $��22.723��6D   $	��Top=�6T   �� 5'      -   5        $��
sliOpacity��5'   3   $��6,79,6,0q�6!   $	 100Q�6E    � Value$	 100Q�6S    �� TickPlacement$ BottomRight=�6_   $��34��6{   $	��Top=�6�   �� 5(      -   5        $
��lbl1��5(   >   $��14��6    $��6,12,0,0q�6,   $	��Top=�6J   $��0q�6b   $
��Left=�6n   $	��120��6�   .��6�   Skin:6�   �� 5)      -   5        $
��lbl2��5)   >   $��14��6    $��6,68,0,0q�6,   $��0q�6J   $	��Top=�6V   $
��Left=�6n   $	��120��6�   .��6�   
Opacity:6�   �� 5*      -   5        $��
cmdInstall��5*   ?   $��23��6!   $��0,0,6,16q�6-   $��Bottom=�6Q   $��Right=�6m   $��87��6�   .��6�   Install Skin6�   5+      5,      �� 5-      -   5        $��	cmdCancel��5-   T   $��23��6   $��Right=�6%   $��	0,0,18,12q�6A   $��Bottom=�6e   $��75��6�   .��6�   Cancel6�   �� 5.      -   5        $��cmdOK��5.   T   $��23��6   $��Right=�6%   $��	0,0,99,12q�6A   $��Bottom=�6a   $��75��6|   .��6�   OK6�   � 5/      -   5        $��ColorDialog��5/      $	��200��6*   $	��200��66   $��300,145,0,0q�6C   
�������$y�Hidden=�6o   .+�50      � -   5        $��bgColor��50   /   $��
12,12,12,0q�6   $��75��6>   $	��Top=�6J   �� 51      -   5        $
��sliR��51   =   $��22��6   $��
36,0,12,68q�6)   $��Bottom=�6I   $	 255Q�6d   �� 52      -   5        $
��lbl3��52   ?   $��13.723��6   $��	20,0,0,72q�6,   $��0q�6K   $��Bottom=�6W   $
��Left=�6r   $��16��6�   .��6�   R:6�   �� 53      -   5        $
��sliG��53   =   $��22��6   $��
36,0,12,40q�6)   $��Bottom=�6I   $	 255Q�6d   �� 54      -   5        $��Label4��54   Z   $��13.723��6   $
��Left=�6,   $��	20,0,0,44q�6G   $��0q�6h   $��Bottom=�6t   $��16��6�   .��6�   G:6�   �� 55      -    5        $
��sliB��55   =   $��22��6   $��
36,0,12,12q�6)   $��Bottom=�6I   $	 255Q�6d   �� 56      -!   5        $��Label5��56   Z   $��13.723��6   $
��Left=�6,   $��	20,0,0,16q�6G   $��0q�6h   $��Bottom=�6t   $��16��6�   .��6�   B:6�   57      58      59      5:      5;      