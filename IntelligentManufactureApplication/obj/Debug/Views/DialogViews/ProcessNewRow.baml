   M S B A M L   `   `   `  ����\  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:IntelligentManufactureApplication.Views.DialogViews3IntelligentManufactureApplication.Views.DialogViews  5   	   G    AIntelligentManufactureApplication.Views.DialogViews.ProcessNewRow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   MlocalAclr-namespace:IntelligentManufactureApplication.Views.DialogViews  5   	     � Title$  ProcessNewRow��5	   	   $	��400��6   $	��350��6,    � WindowStyle$
 None=�68    � WindowStartupLocation$ CenterScreen=�6K    � 
ResizeMode$ NoResize=�6p   .��5      � .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Standard=�6#   $��0 0 0 10q�64   $	��394��6G   $	��342��6T   $	��0 3q�6`   
�������   CornerRadius$ 10��6�    K� ZIndex$ 1��6�   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=�6�   .��5      �  � RowDefinitions 5      �� 5      $��1* �6$   �� 5      $��35 �6$   .+�5      �� 	 �� Orientation$	 Vertical=�6   $	��Top=�64   .+�5      �� $	 
Horizontal=�6!   $
��Left=�6:   $��	40 20 0 0q�6U   .+�5      �� $��工序号：��6$   $��Center=�60   �� 5      -   5        
 ��Name$
 ProcessIDTextBox��5   "   $	��200��6<   $��10 0 0 0q�6H   $��Center=�6Z   $	��Top=�6w   $
��Left=�6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	工序号��6�   5      �� 5      $	 
Horizontal=�6!   $
��Left=�6:   $��	40 20 0 0q�6U   .+�5      �� $��工序名称：��6$   $��Center=�61   �� 5      -   5        $
 ProcessNameTextBox��5   "   $	��200��6>   $��10 0 0 0q�6J   $��Center=�6\   $	��Top=�6y   $
��Left=�6�   $ 工序名称��6�   5      �� 5      $	 
Horizontal=�6!   $
��Left=�6:   $��	40 20 0 0q�6U   .+�5      �� $��适用机床：��6$   $��Center=�61   �� 5      -   5        $
 ApplicabilityTextBox��5   "   $	��200��6@   $��10 0 0 0q�6L   $��Center=�6^   $	��Top=�6{   $
��Left=�6�   $ 适用机床��6�   5      �� 5       $	 
Horizontal=�6!   $
��Left=�6:   $��	40 20 0 0q�6U   .+�5!      �� $��	工时：��6$   $��Center=�6/   �� 5"      -   5        $
 HoursTextBox��5"   "   $	��200��68   $��10 0 0 0q�6D   $��Center=�6V   $	��Top=�6s   $
��Left=�6�   $ 工时��6�   5#      �� 5$      $	 
Horizontal=�6!   $
��Left=�6:   $��	40 20 0 0q�6U   .+�5%      �� $��工时单价：��6$   $��Center=�61   �� 5&      -   5        $
 UnitPriceTextBox��5&   "   $	��200��6<   $��10 20 10 10q�6H   $��Center=�6]   $	��Top=�6z   $
��Left=�6�   $ 工时单价��6�   5'      5(      A� 5*      $��1��6    A� Columns$ 2��6+   .+�5+      �� -   5         ��Name$ CancelButton��5+      $��取消��63   $��60��6@   $��30��6K   $	��0 0q�6W   ���#0A0C22
������ 
���   �6�   �� 5,      -   5        $ OkButton��5,      $��确定��6/   $��60��6<   $��30��6G   $	��0 0q�6S   ���#0A0C22
������ 
���   �6�   5-      5.      5/      51      52      