   M S B A M L   `   `   `  ����   
SprintDock     SprintDock.wpfIconSettings   W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    -   $��Window1��  � Topmost  .  � WindowStartupLocation$ CenterScreen=� � Title$ Window1��$	��404�� � WindowStyle$ 
ToolWindow=�$	��586�� � ShowInTaskbar .   � 	Resources    ListBoxTemplate �� 
TargetType �� Padding �� CornerRadius	 � RowDefinitions
 �� ElementName �� Path �� Triggers :� RoutedEvent �� 
TargetName �� TargetProperty �� From g� Duration	 �� To P� Property P� Value �� 
TargetName �� Property �� Value��%~  &	        �� " ��.R��� $��bg��$��5q�$	��152��$ 3q�
�������$	��148��$��2q����#858585$ 4��._�� 	 �� $��24 ��� �� �� .+��� $
��Text��$��0,0,7,0q�$��Stretch=�$��Calibri)�$��13(���. �� $��IconDel��-   $y�Hidden=�$��0,0,7,0q�$��Right=�$��Center=�$��4��.��DEL�� $��PathChng��$y�Visible=�$��0,0,35,0q�$��Right=�$��Center=�$��4��.��CHG�� $��Rename��-   $y�Hidden=�$��0,0,7,0q�$��Right=�$��Center=�.��RNM�� $��MoveUp��$y�Hidden=�$��29,0,0,0q�$
��Left=�$��Center=�$��4��.���� $��2,0,2,0q�$��Calibri)�$
��Bold"�/\�� $��MoveDown��$��7,0,0,0q�$y�Hidden=�$
��Left=�$��Center=�$��3��.���� $��2,0,2,0q�$��Calibri)�$
��Bold"�\/� $��Image��$y�Hidden=�$��7,1,2,5q�$��1��$
��Left=�$��58��$��58��.+��� ���#888888$��1.5,1.5,1.5,1.5q�$	 5.5���� $��ImageSource��$y�Hidden=�$��5q�$��UniformToFill=�$��Stretch=�$��Stretch=�$��48��$��48������ $	
 img��$ Source��� $
��Path��$
��Wrap=�$��2��$��	12,3,18,8q�$y�Visible=����� $
 lstPath��$ SelectedItem� :� $ ListBoxItem.Selected��[��� .���� .���� $ bg��$ BorderBrush.Color�$ #858585X�$ 0:0:0.3D�$ WhiteX�:� $ ListBoxItem.Unselected��[��� .���� .���� $ bg��$ BorderBrush.Color�$ WhiteX�$ 0:0:0.5D�$ #858585X�P�  �@��
IsSelected . . ��� $ bg�� � ��$ -1,6,0,6q��� $ bg�� � ��$ 3,3,3,3q��� $ bg�� �@��CornerRadius$ 10���� $ bg�� � ��
 ������� $ bg�� � ��$	 152���� $ IconDel�� � y�$ Visible=��� $ MoveUp�� � y�$ Visible=��� $ Rename�� � y�$ Visible=��� $ MoveDown�� � y�$ Visible=��� $ Image�� � y�$ Visible=��� $ ImageSource�� � y�$ Visible=��� $
 Text�� � ��$ 16(��� $
 Text�� � ��$
 Bold"�P�  �@��
IsSelected .  . ��� $ bg�� � ��$ 24���� $
 Text�� � ��$ -1q��� $ bg�� � ��$ 0,0,0,0q�P�  �@J�	IsVisible .  . ��� $ ImageSource�� �� $	
 img��$ Source� � ��.��� -   $��grdMain��.+��� -   $��lstName�����#e9e9e9$��7q�$
��Left=�$	��197��$��1q��� -   $��lstIcon��$��0,25.277,48,0q� �� SelectedItem$ 0��$��Right=�$	��128��$y�Hidden=�$��86.723��$	��Top=��� -   $��lstPath��$��264,54,182,0q�$y�Hidden=�$��56��$	��Top=��� -   $	��img��$��Right=�$��0,130,73,35q�$
��Fill=�$	��200��$y�Hidden=��� -	   $
��img2��$
��Left=�$��10,10,0,155q�$
��Fill=�$y�Hidden=�$	��200���� -
   $��TabControl1��$��	203,0,0,0q� �� SelectedIndex$ 0��.@��� -   $��TabItem1��$��Icons��.��� -   $��grdIcons��{�.  .+��� -   $��lstIcons��$��Stretch=�$	��4.5q�$��Disabled=�$
��Auto=�$	��Top=�$��0,0,0,35q����� .R�� �� -   $��cmdAdd��$��23��$��Right=�$��0,0,6,6q�$��Bottom=�$��75��.��
Add Icon�� -   $��	cmdRemove��$��23��$
��Left=�$��10,0,0,6q�$��Bottom=�$��75��.��Remove�� -   $��TabItem2��$	��New��.��� .+��� $��
Properties��$��	8,6,6,157q�.��� .+��� -   $��txtName��$��23��$��121,9.447,12,0q�$	��Top=�$��Center=��� -   $��	txtTarget��$��23��$��121,39,36,0q�$	��Top=����	#FF7F9DB9$��Center=�.@��� .��Shutdown Command�� .��Log Off Command�� .��Restart Command�� -   $��Label1��$��23��$��	16,10,0,0q�$	��Top=�$
��Left=�$��99��$	��Top=�$��Right=�.��Name�� -   $��Label2��$��23��$
��Left=�$��Right=�$��	16,39,0,0q�$	��Top=�$	��Top=�$��99��.��Target�� $��Preview��$��73��$��Right=�$��	0,73,12,6q�$��66��.��� .+��� -   $��
imgPreview��$��Center=�$��Center=�$��48��$��48��$
��Fill=��� -   $��
cmdAddIcon��$��23��$
��Left=�$��6,0,0,6q�$��Bottom=�$��75��.��Add�� -   $��	cmdBrowse��$��23��$��Right=�$��290,39,12,0q�$	��Top=�.��...�� -   $��cmdSave��$��20��$��Right=�$	��Top=�$��60��.��Save