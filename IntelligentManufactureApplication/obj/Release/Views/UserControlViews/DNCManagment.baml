   M S B A M L   `   `   `  ���� \  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:IntelligentManufactureApplication.Services*IntelligentManufactureApplication.Services  �Fclr-namespace:IntelligentManufactureApplication.Views.UserControlViews8IntelligentManufactureApplication.Views.UserControlViews  K    EIntelligentManufactureApplication.Views.UserControlViews.DNCManagment   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Fservice8clr-namespace:IntelligentManufactureApplication.Services  RlocalFclr-namespace:IntelligentManufactureApplication.Views.UserControlViews  .��� 
�������  � ColumnDefinitions  �� $	��210 ��� $��1* �.+��� -    ��Name$ LeftScrollViewer��$	��200��$
��Auto=�$
��Auto=� �� PanningMode$
 Both=�.��R� -    R�Name$ LeftTreeView��$	��200��
������� � ContextMenu �� .@�m� -    m�Name$ UnfoldMenuItem��$��展开全部��.@�Q� -    Q�Name$ LeftTreeViewItem��$��商品类别�� Q� 
IsExpanded . � -    �Name$ 	RightGrid��$��1��	 � RowDefinitions	 �� $��1* ��� $
��1.5* ��� $	��150 �.+��� $��物料单号��$��0 2 0 0q���Z� .��&   System.Windows.Controls.DataGrid  -   
  Name$
 ProductDataGrid��$��
-2 0 -2 -2q�   SelectionUnit$ FullRow=�   SelectionMode$ Single=�   HeadersVisibility$	 All=�$��1q�   CanUserAddRows .     AutoGenerateColumns .     
IsReadOnly .    GridLinesVisibility$ 
Horizontal=�   CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  $   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$
 Auto=����� *Items1+ � 	Resources =   7IntelligentManufactureApplication.Services.BindingProxy   DataContextProxy   Data��%   &	           ��    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 物料编号��1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ MaterialsNumber� �� ValidationRules G   AIntelligentManufactureApplication.Services.NotEmptyValidationRule    $ 名称�� �� $ ProductName�     $ 备注�� �� $ Remarks�   � $��1��  �� $��1* ��� $	��450 �.+��� $��工序列表��$��0 0 0 0q���Z� .��  -   $
 ProcessingGrid��$��1��$��
-2 0 -2 -2q�$ FullRow=�$ Single=�$	 All=�$��1q� .   .   . $ 
Horizontal=� .   .   .  $
 Auto=�$
 Auto=� ��%   &	           ��  1  +MaterialDesignThemes.Wpf.DataGridTextColumn  $ 	工序号�� ) %MaterialDesignDataGridTextColumnStyle   ElementStyle# [  5 1MaterialDesignDataGridTextColumnPopupEditingStyle   EditingElementStyle# [  �� $ ProcessNumber�     $ 工序名称��# [ # [  �� $ ProcessName�     $ 适用机床��# [ # [  �� $ Applicability�     $ 	工时(h)��# [ # [  �� $ Hours�     $ 工时单价��# [ # [  �� $ 	UnitPrice�     $ 状态��# [ # [  �� $ 	UnitPrice�     $ 传输时间��# [ # [  �� $ 	UnitPrice�     $ 机床编号��# [ # [  �� $ 	UnitPrice�     $ 程序文件��# [ # [  �� $ 	UnitPrice�     $ 	版本号��# [ # [  �� $ 	UnitPrice�   � $��1��	 �� $��1* ��� $��1* �.+��� $��工艺卡（待发送）��$	��0 0q���Z� .��  -   $
 	CraftGrid��$��1��$��
-2 0 -2 -2q�$ FullRow=�$ Single=�$	 All=�$��1q� .   .   . $ 
Horizontal=� .   .   .  $
 Auto=�$
 Auto=� ��%   &	           ��    $ 工艺卡名称��# [ # [  �� $ 	CraftName�     $ 审核状态��# [ # [  �� $ 
CheckState�     $ 审核时间��# [ # [  �� $ 	CheckTime�     $ 状态��# [ # [  �� $ 
CheckState�     $ 传输时间��# [ # [  �� $ ModifiedTimee�   �� $��1��$��NC程序（待发送）��$	��0 0q���Z� .��  -	   $
 NCGrid��$��1��$��
-2 0 -2 -2q�$ FullRow=�$ Single=�$	 All=�$��1q� .   .   . $ 
Horizontal=� .   .   .  $
 Auto=�$
 Auto=� ��%   &	           ��    $ 程序名称��# [ # [  �� $ NCProgramName�     $ 审核状态��# [ # [  �� $ 
CheckState�     $ 审核时间��# [ # [  �� $ 	CheckTime�     $ 状态��# [ # [  �� $ 
CheckState�     $ 传输时间��# [ # [  �� $ ModifiedTimee�   � $��2��  �� $	��450 ��� $��1* �.+��� $��主控配置��$��0 0 0 2q���Z� .����   �� Orientation$  Vertical=�$	��Top=�$��	10 0 10 0q�.+��� $  
Horizontal=�$
��Left=�.+��� $��主控IP地址:��$��0��! �� TextAlignment$! Right=�$��Center=�$��20(��� -
   " ��Name$" 
ip_address��$��1��$��192.168.1.201��$��Center=�# �� TextAlignment$# Center=�$��20(�$ �� 
IsReadOnly$ . $��10 0 5 0q�$
��Auto�����#000000% �� 
CaretBrush% �#000000�� -   & ��Name$& EditIpAdress��$��修改��$��60��$��25��$��10 0 5 0q����#0A0C22
������ 
���   ��� -   $& EditIpAdressOK��$��确认��$��60��$��25��$��10 0 5 0q����#0A0C22
������ 
���   ��� $  
Horizontal=�$
��Left=�$
��0 10q�.+��� $��连接测试:��$��0��$! Right=�$��Center=�$��20(��� -   $& ConnectIpAdressOK��$��连接��$��60��$��25��$��30 0 0 0q����#0A0C22
������ 
���   ��� -   $& UnConnectIpAdress��$��断开��$��60��$��25��$��30 0 0 0q����#0A0C22
������ 
���   ��� $  
Horizontal=�$
��Left=�$	��0 0q�.+��� $��测试结果:��$��0��$! Right=�$��Center=�$��20(�$��0 0 0 0q��� -   $" ConnectResult��$ . $�� ��$��0��$# Right=�$��Center=�$��16(�$��	 30 0 0 0q�$��80���� $��1��$��传输控制��$��0 0 0 2q���Z� .���� $  Vertical=�$	��Top=�$��
10 0 10 10q�.+��� $  
Horizontal=�$
��Left=�$��0 0 0 10q�.+��� $��待传输工艺卡：��$��0��$
! Left=�$��Center=�$��20(��� -   $" CraftFileTextBox��$	��200���� -   $& CrafFileAlert��{�. $y�Hidden=�
������ $��30��$��30�� . *MaterialDesignFloatingActionMiniDarkButton#��[ ��Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon  '   Kind$' Alert=�$��25��$��25��
���  ���� -   ( ��Name$( CrafFileAlertTextBlock��$y�Hidden=�$��该文件未通过审核��
���  ��$��Center=��� $  
Horizontal=�$
��Left=�$��0 0 0 10q�.+��� $��待传输NC程序：��$��0��$
! Left=�$��Center=�$��20(��� -   $" NCFileTextBox��$	��200���� -   $& NCFileAlert��{�. $y�Hidden=�
������ $��30��$��30��#��[ ��Z� ��Z� .��  $' Alert=�$��25��$��25��
���  ���� -   $( NCFileAlertTextBlock��$y�Hidden=�$��该文件未通过审核��
���  ��$��Center=��� $  
Horizontal=�$��Right=�.+��� -   $& UploadButton��{�.  $��传输��$��60��$��30��$��5 0 50 0q����#0A0C22
������ 
���   ��� -   $& CancelButton��$��取消��$��60��$��30��$��5 0 5 0q����#0A0C22
������ 
���   �