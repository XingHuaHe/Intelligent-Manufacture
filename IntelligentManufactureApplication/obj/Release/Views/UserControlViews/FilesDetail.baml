   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  \ XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Fclr-namespace:IntelligentManufactureApplication.Views.UserControlViews8IntelligentManufactureApplication.Views.UserControlViews g8clr-namespace:IntelligentManufactureApplication.Services*IntelligentManufactureApplication.Services J   DIntelligentManufactureApplication.Views.UserControlViews.FilesDetail   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  3system&clr-namespace:System;assembly=mscorlib  RlocalFclr-namespace:IntelligentManufactureApplication.Views.UserControlViews  Fservice8clr-namespace:IntelligentManufactureApplication.Services  .��� -     �Name$  OutGrid��
������� � RowDefinitions �� $	��100 ��� $��1* �.+�&   System.Windows.Controls.DataGrid  -     Name$ ProductListDataGrid��$	��2 2q�   SelectionUnit$ FullRow=�   SelectionMode$ Single=�   HeadersVisibility$	 All=�$��1q�   CanUserAddRows .     AutoGenerateColumns .     
IsReadOnly . 	   GridLinesVisibility$	 
Horizontal=�
   CanUserReorderColumns
 .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns .  $   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$ Hidden=� � 	Resources =  7IntelligentManufactureApplication.Services.BindingProxy   DataContextProxy   Data��%   &	           ��  � ContextMenu �� .@�m� -    m�Name$ PreviewMenuItem��$��预览��   Columns 1  +MaterialDesignThemes.Wpf.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 物料编号��   
IsReadOnly .  ) %MaterialDesignDataGridTextColumnStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [  5 1MaterialDesignDataGridTextColumnPopupEditingStyle   EditingElementStyle# [    Binding ��  �� Path$ MaterialsNumber� �� ValidationRules G  AIntelligentManufactureApplication.Services.NotEmptyValidationRule    $ 图号��# [ # [  �� $ 	DrawingID�     $ 名称��# [ # [  �� $ ProductName�     $ 材质��# [ # [  �� $ Makings�     $ 材质代码��# [ # [  �� $ MakingsNumber�     $ 标准��# [ # [  �� $ Standard�     $ 热处理代号��# [ # [  �� $ HeatTreatmentCode�     $ 材料屈服强度��# [ # [  �� $ MaterialStrength�     $ 检验组别��# [ # [  �� $ 
CheckGroup�     $ 热处理硬度��# [ # [  �� $ HeattreatmentStrength�     $ 来源��# [ # [  �� $ Source�     $ 类型��# [ # [  �� $
 Type�     $ 规格��# [ # [  �� $ Specification�     $ 单件毛胚尺寸��# [ # [  �� $ 	BlankSize�     $ 下斜尺寸��# [ # [  �� $ CuttingSize�     $ 司太立及氮化��# [ # [  �� $ StelliteAndNitriding�     $ 备注��# [ # [  �� $ Remarks�   � $��1�� � ColumnDefinitions �� $	��400 ��� $��1* �.+��  �� $��1* ��� $��30 �.+�  -   $ ProcessingGrid��$	��2 2q�$ FullRow=�$ Single=�$	 All=�$��1q� .   .   . $	 
Horizontal=�
 .   .   .  $
 Auto=�$
 Auto=� ��%   &	           ��    $ 	工序号��# [ # [  �� $ ProcessNumber�     $ 工序名称��# [ # [  �� $ ProcessName�     $ 适用机床��# [ # [  �� $ Applicability�     $ 	工时(h)��# [ # [  �� $ Hours�     $ 工时单价��# [ # [  �� $ 	UnitPrice�   �� $��1�� �� Orientation$ 
Horizontal=�$��Right=�.+��� -    ��Name$ DeletedProcessButton��$��删除��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   ��� -   $ AddProcessButton��$��添加��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   �� $��1�� �� $	��340 ��� $��1* ��� $��30 �.+��� $ Vertical=�$	��Top=�$��0 2 2 0q�.+�  -   $ DrawingGrid��$	��140��$ FullRow=�$ Single=�$	 All=�$��1q� .   .   . $	 
Horizontal=�
 .   .   .  $
 Auto=�$
 Auto=� ��%   &	           ��    $ 图纸名称��# [ # [  �� $ DrawingName�     $ 	版本号��# [ # [  �� $ FileVersion�     $ 审核状态��# [ # [  �� $ 
CheckState�     $ 	审核人��# [ # [  �� $ CheckedUser�     $ 审核时间��# [ # [  �� $ 	CheckTime�     $ 	创建人��# [ # [  �� $ ModifiedUser�     $ 创建时间��# [ # [  �� $ ModifiedTimee�   �� $ 
Horizontal=�$��Right=�$��0 2 0 2q�.+��� -   $ DrawingPreviewButton��$��预览��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   ��� -	   $ DrawingDeletedButton��$��删除��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   ��� -
   $ DrawingImportButton��$��导入��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   �  -   $ 	CraftGrid��$	��140��$ FullRow=�$ Single=�$	 All=�$��1q� .   .   . $	 
Horizontal=�
 .   .   .  $
 Auto=�$
 Auto=� ��%   &	           ��    $ 工艺卡名称��# [ # [  �� $ 	CraftName�     $ 	版本号��# [ # [  �� $ FileVersion�     $ 审核状态��# [ # [  �� $ 
CheckState�     $ 	审核人��# [ # [  �� $ CheckedUser�     $ 审核时间��# [ # [  �� $ 	CheckTime�     $ 	创建人��# [ # [  �� $ ModifiedUser�     $ 创建时间��# [ # [  �� $ ModifiedTimee�   �� $ 
Horizontal=�$��Right=�$��0 2 0 0q�.+��� -   $ CraftPreviewButton��$��预览��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   ��� -   $ CraftDeletedButton��$��删除��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   ��� -   $ CraftImportButton��$��导入��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   �  -   $ DNCDataGrid��$��1��$��0 0 0 2q�$ FullRow=�$ Single=�$	 All=�$��1q� .   .   . $	 
Horizontal=�
 .   .   .  $
 Auto=�$
 Auto=� ��%   &	           ��    $ 程序名称��# [ # [  �� $ NCProgramName�     $ 	版本号��# [ # [  �� $ FileVersion�     $ 审核状态��# [ # [  �� $ 
CheckState�     $ 	审核人��# [ # [  �� $ CheckedUser�     $ 审核时间��# [ # [  �� $ 	CheckTime�     $ 	创建人��# [ # [  �� $ ModifiedUser�     $ 创建时间��# [ # [  �� $ ModifiedTimee�   �� $��2��$ 
Horizontal=�$��Right=�.+��� -   $ PreviewNCButton��$��预览��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   ��� -   $ DeletedNCButton��$��删除��$��60��$��25��$��5 0 5 0q����#0A0C22
������ 
���   ��� -   $ AddNCButton��$��导入��$��60��$��25��$��5 0 7 0q����#0A0C22
������ 
���   �