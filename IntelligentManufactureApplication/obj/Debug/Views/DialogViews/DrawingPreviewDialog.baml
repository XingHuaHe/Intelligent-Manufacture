   M S B A M L   `   `   `  ����\  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:IntelligentManufactureApplication.Views.DialogViews3IntelligentManufactureApplication.Views.DialogViews  5   	   E AMoonPdfLib, Version=0.2.3.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:MoonPdfLib;assembly=MoonPdfLib
MoonPdfLib 5   	   N    HIntelligentManufactureApplication.Views.DialogViews.DrawingPreviewDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   MlocalAclr-namespace:IntelligentManufactureApplication.Views.DialogViews  5   	   6mpp,clr-namespace:MoonPdfLib;assembly=MoonPdfLib  5   	     � Title$  DrawingPreviewDialog��5
   	   $	��450��6&   $	��800��63    � WindowStyle$
 None=�6?    � WindowStartupLocation$ CenterScreen=�6R    � 
ResizeMode$ NoResize=�6w   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   � -   5        $��30��5      ���#121F40.+�5      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��Center=�6N   $
��10,0q�6i   .+�5      �� -   5         /  +MaterialDesignFloatingActionMiniLightButton#��[  5      $��30��6^   $��30��6j   
���������6�   Z� ��6�   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6.   5      5      5      �� 5   
   $��1��6   ���#f0f0f0$��0 2 0 5q�63   ._�5      �� $
��Left=�6   $ 
Horizontal=�64   .+�5      �� -   5        $��放大��5      $��60��6&   $��25��61   $��5 0 5 0q�6=   ���#0A0C22
������ 
���   �6�   �� 5      -   5        $��缩小��5      $��60��6&   $��25��61   $��5 0 5 0q�6=   ���#0A0C22
������ 
���   �6�   �� 5       -   5        $
��100%��5       $��70��6(   $��25��63   $��5 0 5 0q�6?   ���#0A0C22
������ 
���   �6�   �� 5!      -   5        $��整页��5!      $��60��6&   $��25��61   $��5 0 5 0q�6=   ���#0A0C22
������ 
���   �6�   �� 5"      -   5        $��单页��5"      $��60��6&   $��25��61   $��5 0 5 0q�6=   ���#0A0C22
������ 
���   �6�   �� 5#      -   5        $��双页��5#      $��60��6&   $��25��61   $��5 0 5 0q�6=   ���#0A0C22
������ 
���   �6�   5$      5%      �� 5'   
   -	   5         ��Name$ 
ShowBorder��5'      $��2��6%   ���#d3d3d35)      �� 5+   
   $��3��6   $ 
Horizontal=�6"   $��Right=�6;   .+�5,      �� -
   5         ��Name$ ReviewButton��5,      $��审核��6+   $��60��68   $��25��6C   $��5 0 5 0q�6O   ���#0A0C22
������ 
���   �6�   5-      5.      5/      