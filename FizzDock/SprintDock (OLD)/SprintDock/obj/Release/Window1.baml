   M S B A M L   `   `   `  ����   
SprintDock    SprintDock.Window1   W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    -   $��Window1��  � Title$  Window1��$	��353��$	��593�� � 	Resources    ListBoxTemplate �� 
TargetType �� Padding �� CornerRadius � RowDefinitions �� ElementName �� Path �� Triggers	 :� RoutedEvent
 �� 
TargetName �� TargetProperty �� From g� Duration	 �� To P� Property P� Value �� 
TargetName �� Property �� Value 	 Icons��%L  &	        &	 �    �� " ��.R��� $��bg��$��5q�$	��152��$ 3q�
�������$	��148��$��2q����#858585$ 4��._��  �� $��24 ��� �� �� .+��� $
��Text��$��0,0,7,0q�$��Calibri)�$��13(���. �� $��IconDel��$y�Hidden=�$��0,0,7,0q�$��Right=�$��Center=�$��4��.��DEL�� $��MoveUp��$y�Hidden=�$��29,0,0,0q�$
��Left=�$��Center=�$��4��.���� $��2,0,2,0q�$��Calibri)�$
��Bold"�/\�� $��MoveDown��$��7,0,0,0q�$y�Hidden=�$
��Left=�$��Center=�$��4��.���� $��2,0,2,0q�$��Calibri)�$
��Bold"�\/� $��Image��$y�Hidden=�$��7,1,2,5q�$��1��$
��Left=�$��58��$��58��.+��� ���#888888$��1.5,1.5,1.5,1.5q�$	 5.5���� $��5q�$��UniformToFill=�$��Stretch=�$��Stretch=�$��48��$��48������ $	 img��$ Source��� $
��Path��$��	12,3,18,8q�$y�Hidden=�$	��C:\�� :� $	 ListBoxItem.Selected��[��� .���� .���� $
 bg��$ BorderBrush.Color�$ #858585X�$ 0:0:0.3D�$ WhiteX�:� $	 ListBoxItem.Unselected��[��� .���� .���� $
 bg��$ BorderBrush.Color�$ WhiteX�$ 0:0:0.5D�$ #858585X�P�  �@��
IsSelected . . ��� $ bg�� � ��$ -1,6,0,6q��� $ bg�� � ��$ 3,3,3,3q��� $ bg�� �@��CornerRadius$ 10���� $ bg�� � ��
 ������� $ bg�� � ��$	 152���� $ IconDel�� � y�$ Visible=��� $ MoveUp�� � y�$ Visible=��� $ MoveDown�� � y�$ Visible=��� $ Image�� � y�$ Visible=��� $
 Text�� � ��$ 16(��� $
 Text�� � ��$
 Bold"�P�  �@��
IsSelected .  . ��� $ bg�� � ��$ 24���� $
 Text�� � ��$ -1q��� $ bg�� � ��$ 0,0,0,0q��� " ��.R��� $��border��
������ $��58��$��58��$ 4��$	��1.5q�$��5q�$ 2q�$��58��$��58�� :� $	 ListBoxItem.Selected��[��� .���� .���� $
 border��$ BorderBrush.Color�$ TransparentX�$ 0:0:0.5D�$ #777777X�:� $	 ListBoxItem.Unselected��[��� .���� .���� $
 border��$ BorderBrush.Color�$ #777777X�$ 0:0:0.5D�$ TransparentX�.��� -   $��grdMain��.+��� -   $��lstName�����#e9e9e9$��7q�$
��Left=�$	��197��$��1q��� -   $��lstIcon��$��80,138,0,77q� �� SelectedItem$ 0��$
��Left=�$	��128��$y�Hidden=��� -   $��lstPath��$��5,106,0,109q�$
��Left=�$	��125��$y�Hidden=��� -   $��lstIcons��$��4.5,4.5,4.5,4.5q�$��	203,0,0,0q�$	��Top=�$
��Auto=�$��Disabled=�$
��Left=����� .R�� �� -   $	��img��$��Right=�$��0,130,73,35q�$
��Fill=�$	��200��$y�Hidden=��� -   $
��img2��$
��Left=�$��10,10,0,155q�$
��Fill=�$y�Hidden=�$	��200���� -	   $��	lstIcons2��$y�Visible=�$	��100��$
��Left=�$��	12,0,0,23q�$ 0��$��Bottom=�$	��128��