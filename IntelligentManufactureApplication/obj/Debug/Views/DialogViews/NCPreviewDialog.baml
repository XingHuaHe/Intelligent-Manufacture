   M S B A M L   `   `   `  ����\  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:IntelligentManufactureApplication.Views.DialogViews3IntelligentManufactureApplication.Views.DialogViews  5   	   I    CIntelligentManufactureApplication.Views.DialogViews.NCPreviewDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   MlocalAclr-namespace:IntelligentManufactureApplication.Views.DialogViews  5   	     � Title$  NCPreviewDialog��5	   	   $	��650��6!   $	��850��6.    � WindowStyle$
 None=�6:    � WindowStartupLocation$ CenterScreen=�6M    � 
ResizeMode$ NoResize=�6r   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��1* �6   .+�5   
   � -   5        $��30��5      ���#121F40.+�5      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��Center=�6N   $
��10,0q�6i   .+�5      �� -   5         /  +MaterialDesignFloatingActionMiniLightButton#��[  5      $��30��6^   $��30��6j   
���������6�   Z� ��6�   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6.   5      5      5      (  "MaterialDesignThemes.Wpf.ColorZone  5   
   $��1��6#      Mode$ Standard=�60   $��5 5 5 5q�6@      CornerRadius$ 2��6R   	 K� ZIndex$	 1��6c   $
��5 10q�6t   $	��610��6�   +  %MaterialDesignThemes.Wpf.ShadowAssist
   ShadowDepth$
 Depth3=�6�   .��5      �  5      �� 5      $��40 �6$   �� 5      $
��Auto �6$   �� 5      $��1* �6$   �� 5      $��40 �6$   .+�5"      �� -   5         ��Name$ FileNameTextBlock��5"      $��文件名称：��67   $��20(�6D   $��30��6R   $��Center=�6^   �� 5#      $��1��6   �� 5$      -   5         ��Name$ EditRichTextBox��5$   +   $��2��6   $	��580��6D    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6f   $ �� HorizontalScrollBarVisibility$
 Auto=�6�    �� 
IsReadOnly . 6�   �� 5%      $��3��6   $ 
Horizontal=�6*   $��Right=�6C   $	��5 2q�6_   .+�5&      �� -   5         ��Name$ CancelButton��5&      $��取消��63   $��60��6@   $��30��6K   $��5 0 5 0q�6W   ���#0A0C22
������ 
���   �6�   �� 5'      -   5        $ 
PassButton��5'      $��通过��61   $��60��6>   $��30��6I   $��5 0 5 0q�6U   ���#0A0C22
������ 
���   �6�   5(      5)      5*      5+      5,      