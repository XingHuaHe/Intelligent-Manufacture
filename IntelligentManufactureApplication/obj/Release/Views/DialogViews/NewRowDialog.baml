   M S B A M L   `   `   `  ���� \  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:IntelligentManufactureApplication.Views.DialogViews3IntelligentManufactureApplication.Views.DialogViews  F    @IntelligentManufactureApplication.Views.DialogViews.NewRowDialog   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  MlocalAclr-namespace:IntelligentManufactureApplication.Views.DialogViews    � Title$  NewRowDialog��$	��650��$	��450�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.��� .+�(  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Standard=�$��0 0 0 10q�$	��644��$	��442��$	��0 3q�
�������   CornerRadius$ 10�� K� ZIndex$ 1��+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=�.���  � RowDefinitions �� $��1* ��� $��35 �.+��� 	 �� Orientation$	 Vertical=�$	��Top=�.+��� $	 
Horizontal=�$
��Left=�$��
80 10 10 5q�.+��� $��物料编号：��$��16(�$��Center=�$��Center=��� -   
 ��Name$
 MaterialsNumberTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入物料编号���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��	图号：��$��16(�$��Center=�$��Center=��� -   $
 DrawingNumTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入图号���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��物料名称：��$��16(�$��Center=�$��Center=��� -   $
 ProductNameTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入物料名称���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��	材质：��$��16(�$��Center=�$��Center=��� -   $
 MakingsTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入材质���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��材质代码：��$��16(�$��Center=�$��Center=��� -   $
 MakingsNumberTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入材质代码���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��	标准：��$��16(�$��Center=�$��Center=��� -   $
 StandardTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入标准���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��热处理代号：��$��16(�$��Center=�$��Center=��� -   $
 HeatTreatmentCodeTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入热处理代号���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��材料屈服强度：��$��16(�$��Center=�$��Center=��� -   $
 MaterialStrengthTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$! 请输入材料屈服强度���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��材料屈服强度：��$��16(�$��Center=�$��Center=��� -	   $
 CheckGroupTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入校验组别���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��热处理强度：��$��16(�$��Center=�$��Center=��� -
   $"
 HeattreatmentStrengthTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入热处理强度���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��	来源：��$��16(�$��Center=�$��Center=��� -   $
 SourceTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入来源���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��	类型：��$��16(�$��Center=�$��Center=��� -   $
 TypeTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入类型���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��	规格：��$��16(�$��Center=�$��Center=��� -   $
 SpecificationTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入规格���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��单件毛胚尺寸：��$��16(�$��Center=�$��Center=��� -   $
 BlankSizeTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$! 请输入单件毛胚尺寸���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��下料尺寸：��$��16(�$��Center=�$��Center=��� -   $
 CuttingSizeTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入下料尺寸���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��司太立及氮化：��$��16(�$��Center=�$��Center=��� -   $!
 StelliteAndNitridingTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$! 请输入司太立及氮化���� $	 
Horizontal=�$
��Left=�$��	80 5 10 5q�.+��� $��	备注：��$��16(�$��Center=�$��Center=��� -   $
 RemarksTextBox��$	��200��$��10 0 0 0q�$��Center=�$	��Top=�$
��Left=�$ 请输入备注��A� $��1�� A� Columns$ 2��.+��� -    ��Name$ OkButton��$��确定��$��60��$��30��$	��0 0q����#0A0C22
������ 
���   ��� -   $ CancelButton��$��取消��$��60��$��30��$��5 0 5 0q����#0A0C22
������ 
���   �