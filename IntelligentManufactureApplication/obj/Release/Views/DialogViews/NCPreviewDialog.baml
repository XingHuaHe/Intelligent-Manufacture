   M S B A M L   `   `   `  ���� \  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:IntelligentManufactureApplication.Views.DialogViews3IntelligentManufactureApplication.Views.DialogViews  I    CIntelligentManufactureApplication.Views.DialogViews.NCPreviewDialog   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  MlocalAclr-namespace:IntelligentManufactureApplication.Views.DialogViews    � Title$  NCPreviewDialog��$	��650��$	��850�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� $��30 ��� $��1* �.+�� -   $��30�����#121F40.+���  �� Orientation$ 
Horizontal=�$��Right=�$��Center=�$
��10,0q�.+��� -    /  +MaterialDesignFloatingActionMiniLightButton#��[  $��30��$��30��
���������Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�(  "MaterialDesignThemes.Wpf.ColorZone  $��1��   Mode$ Standard=�$��5 5 5 5q�   CornerRadius$ 2��	 K� ZIndex$	 1��$
��5 10q�$	��610��+  %MaterialDesignThemes.Wpf.ShadowAssist
   ShadowDepth$
 Depth3=�.���  �� $��40 ��� $
��Auto ��� $��1* ��� $��40 �.+��� -    ��Name$ FileNameTextBlock��$��文件名称：��$��20(�$��30��$��Center=��� $��1���� -    ��Name$ EditRichTextBox��$��2��$	��580�� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�$ �� HorizontalScrollBarVisibility$
 Auto=� �� 
IsReadOnly . �� $��3��$ 
Horizontal=�$��Right=�$	��5 2q�.+��� -    ��Name$ CancelButton��$��取消��$��60��$��30��$��5 0 5 0q����#0A0C22
������ 
���   ��� -   $ 
PassButton��$��通过��$��60��$��30��$��5 0 5 0q����#0A0C22
������ 
���   �