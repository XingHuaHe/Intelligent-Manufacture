   M S B A M L   `   `   `  ����\  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:IntelligentManufactureApplication.Views.DialogViews3IntelligentManufactureApplication.Views.DialogViews  5   	   H    BIntelligentManufactureApplication.Views.DialogViews.RenameTreeView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   MlocalAclr-namespace:IntelligentManufactureApplication.Views.DialogViews  5   	     � Title$  RenameTreeView��5	   	   $	��250��6    $	��300��6-    � WindowStyle$
 None=�69    � WindowStartupLocation$ CenterScreen=�6L    � 
ResizeMode$ NoResize=�6q   .��5
      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��1* �6   �� 5      $��35 �6   .+�5   
   � -   5        $��30��5      ���#121F40.+�5      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��Center=�6N   $
��10,0q�6i   .+�5      �� -   5         /  +MaterialDesignFloatingActionMiniLightButton#��[  5      $��30��6^   $��30��6j   
���������6�   Z� ��6�   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6.   5      5      5      �� 5   
   -   5         ��Name$ 
RenameText��5      $��1��6   $��Center=�63   $��Center=�6P   $��Center=�6k   $��Center=�6�   $	��200��6�   �� 5   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   .+�5      �� -   5         ��Name$ OkButton��5      $��确定��6'   $��60��64   $��30��6?   $	��0 0q�6K   ���#0A0C22
������ 
���   �6�   �� 5      -   5        $ CancelButton��5      $��取消��6+   $��60��68   $��30��6C   $��5 0 5 0q�6O   ���#0A0C22
������ 
���   �6�   5      5      5       