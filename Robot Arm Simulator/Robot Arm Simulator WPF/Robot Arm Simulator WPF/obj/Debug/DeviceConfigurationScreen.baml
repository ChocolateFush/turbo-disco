   M S B A M L   `   `   `  ����R  NRobot Arm Simulator WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Robot_Arm_Simulator_WPFRobot_Arm_Simulator_WPF  5   	   7    1Robot_Arm_Simulator_WPF.DeviceConfigurationScreen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Robot_Arm_Simulator_WPF     5   	     � Title$  DeviceConfigurationScreen��5   	   $	��450��6+   $	��800��68    � 	Resources 5	       �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%�  '��       '� <    '�� �    '�� M    �� 5
   
   " ��6   .�5      ��  � ��
 �����6;   �� 5       � ��$ ./#Segoe UI)�6+   6A   �� 5       � �� �#FE5F006=   �� 5       � �� �#FCBF496>    5      P� 5       �@J�IsMouseOver . . �5      ��  � �� �#FCBF4963   5      5      �� 5   
   " �6   .�5      ��  � �� �#0030496=   5      �� 5   
   " ��6   .�5      ��  � ��$ 4q�6'   63   �� 5       � ��$ 16q�6(   65   �� 5       � ��
 � ���6=   �� 5       � ��$ 1q�60   6<   5      �� 5   
   " ��6   .�5      ��  � ��
 �����6;   �� 5        � ��$ 16(�6)   66   5!      5"      .��5#      �  � RowDefinitions 5$   
   �� 5%      $��32 �6   6*   �� 5&      $��1* �6   6*   �� 5'      $��64 �6   6*   .+�5)   
   �� $��24, 0, 0, 0q�6   .��6%   
COM Ports:61   �� 5*   
   -   5        $��comPortListView��5*   3   $��1��6   $��	24,0,24,0q�6    6�   � 5+   
   $��2��6   	 � ColumnDefinitions	 5,      �� 5-      $��1* �6#   60   �� 5.      $��1* �6#   60   .+�50      �� -   5        $��32��50      $	��128��6!   .��6B   	Refresh6K   �� 51      -   5        $��1��51      $��32��6%   $	��128��61   .��6T   Close6[   52      53      54      