   M S B A M L   `   `   `  ����F  BMapNetDrive, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MapNetDriveMapNetDrive  5   	       MapNetDrive.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MapNetDrive     5   	   -   5          � Title$  Login��5   	   $	��200��6   $	��350��6$    � WindowStyle$ 
ToolWindow=�62    � WindowStartupLocation$ CenterOwner=�5	   	   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      .+�5   
   �� $��0��6   $��0��6!   $��
User Name:��61   $��Center=�6C   $��Center=�6^   $��16(�6{   �� 5   
   $��1��6   $��0��6!   $��	Password:��61   $��Center=�6B   $��Center=�6]   $��16(�6z   �� 5   
   -   5         ��Name$ textUserName��5      $��0��6(   $��1��65   $	��200��6E   $��30��6T   $��Center=�6c   $
��Left=�6~   $��16(�6�   G� 5   
   -   5         G�Name$ textPassword��5      $��1��6,   $��1��69   $	��200��6I   $��30��6X   $��Center=�6g   $
��Left=�6�   $��16(�6�   �� 5   
   -   5         ��Name$ btnLogin��5      $��2��6:   $��0��6G   $��2��6W   $��30��6k   $	��100��6z   $��Login��6�   $��16(�6�   5      5      