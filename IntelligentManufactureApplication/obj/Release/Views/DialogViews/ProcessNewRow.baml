   M S B A M L   `   `   `  ���� \  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:IntelligentManufactureApplication.Views.DialogViews3IntelligentManufactureApplication.Views.DialogViews  G    AIntelligentManufactureApplication.Views.DialogViews.ProcessNewRow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  MlocalAclr-namespace:IntelligentManufactureApplication.Views.DialogViews    � Title$  ProcessNewRow��$	��400��$	��350�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.��� .+�(  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Standard=�$��0 0 0 10q�$	��394��$	��342��$	��0 3q�
�������   CornerRadius$ 10�� K� ZIndex$ 1��+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=�.���  � RowDefinitions �� $��1* ��� $��35 �.+��� 	 �� Orientation$	 Vertical=�$	��Top=�.+��� $	 
Horizontal=�$
��Left=�$��	40 20 0 0q�.+��� $��工序号：��$��Center=��� -   
 ��Name$
 ProcessIDTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	工序号���� $	 
Horizontal=�$
��Left=�$��	40 20 0 0q�.+��� $��工序名称：��$��Center=��� -   $
 ProcessNameTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 工序名称���� $	 
Horizontal=�$
��Left=�$��	40 20 0 0q�.+��� $��适用机床：��$��Center=��� -   $
 ApplicabilityTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 适用机床���� $	 
Horizontal=�$
��Left=�$��	40 20 0 0q�.+��� $��	工时：��$��Center=��� -   $
 HoursTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 工时���� $	 
Horizontal=�$
��Left=�$��	40 20 0 0q�.+��� $��工时单价：��$��Center=��� -   $
 UnitPriceTextBox��$	��200��$��10 20 10 10q�$��Center=�$	��Top=�$
��Left=�$ 工时单价��A� $��1�� A� Columns$ 2��.+��� -    ��Name$ CancelButton��$��取消��$��60��$��30��$	��0 0q����#0A0C22
������ 
���   ��� -   $ OkButton��$��确定��$��60��$��30��$	��0 0q����#0A0C22
������ 
���   �