   M S B A M L   `   `   `  ����\  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:IntelligentManufactureApplication.Services*IntelligentManufactureApplication.Services  5      �Fclr-namespace:IntelligentManufactureApplication.Views.UserControlViews8IntelligentManufactureApplication.Views.UserControlViews  5      K    EIntelligentManufactureApplication.Views.UserControlViews.DNCManagment   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Fservice8clr-namespace:IntelligentManufactureApplication.Services  5      RlocalFclr-namespace:IntelligentManufactureApplication.Views.UserControlViews  5      .��5      � 
�������  � ColumnDefinitions  5   
   �� 5      $	��210 �6   �� 5      $��1* �6   .+�5   
   �� -   5         ��Name$ LeftScrollViewer��5      $	��200��61   $
��Auto=�6=   $
��Auto=�6`    �� PanningMode$
 Both=�6�   .��5      R� -   5         R�Name$ LeftTreeView��5      $	��200��6-   
������� � ContextMenu 5      �� 5      .@�5      m� -   5         m�Name$ UnfoldMenuItem��5   #   $��展开全部��6;   6I   5      .@�5      Q� -   5         Q�Name$ LeftTreeViewItem��5      $��商品类别��69    Q� 
IsExpanded . 6G   5      5      � 5   
   -   5         �Name$ 	RightGrid��5      $��1��6   	 � RowDefinitions	 5      �� 5      $��1* �6    �� 5       $
��1.5* �6    �� 5!      $	��150 �6    .+�5$      �� $��物料单号��6   $��0 2 0 0q�66   ��6%   Z� .��5%      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 ProductDataGrid��5%      $��
-2 0 -2 -2q�64      SelectionUnit$ FullRow=�5&         SelectionMode$ Single=�67      HeadersVisibility$	 All=�6N   $��1q�6g      CanUserAddRows .     AutoGenerateColumns .     
IsReadOnly .    GridLinesVisibility$ 
Horizontal=�5(         CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  $   HorizontalScrollBarVisibility$
 Auto=�5*      "   VerticalScrollBarVisibility$
 Auto=�6E   ��5%   H   �� *Items1+ � 	Resources 5,      =   7IntelligentManufactureApplication.Services.BindingProxy   DataContextProxy   Data��%&   &	          5-       6H   �� 5.         Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$ 物料编号��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 52      �� 53   "    �� Path$ MaterialsNumber�6*    �� ValidationRules 54   &   G   AIntelligentManufactureApplication.Services.NotEmptyValidationRule  55   *   57   #   59        5;      $ 名称��6-    5<      �� 5=   "   $ ProductName�6*    5>   &     5?   *   5A   #   5C        5E      $ 备注��6-    5F      �� 5G   "   $ Remarks�6*    5H   &     5I   *   5K   #   5M      5O      5P      � 5R      $��1��6     5S      �� 5T      $��1* �6'   �� 5U      $	��450 �6'   .+�5X      �� $��工序列表��6   $��0 0 0 0q�6:   ��6)   Z� .��5Y        -   5        $
 ProcessingGrid��5Y   ,   $��1��6   $��
-2 0 -2 -2q�6D   $ FullRow=�5Z      $ Single=�67   $	 All=�6N   $��1q�6g    .   .   . $ 
Horizontal=�5\       .   .   .  $
 Auto=�5^      $
 Auto=�6E    5_      ��%&   &	          5`       6L   �� 5a       5c      1  +MaterialDesignThemes.Wpf.DataGridTextColumn  5d      $ 	工序号��6@    ) %MaterialDesignDataGridTextColumnStyle   ElementStyle# [ 6M    5 1MaterialDesignDataGridTextColumnPopupEditingStyle   EditingElementStyle# [ 6�    5e   "   �� 5f   &   $ ProcessNumber�6.    5g   *     5h   .   5j   '   5l        5n      $ 工序名称��6@   # [ 6N   # [ 6�    5o   "   �� 5p   &   $ ProcessName�6.    5q   *     5r   .   5t   '   5v        5x      $ 适用机床��6@   # [ 6N   # [ 6�    5y   "   �� 5z   &   $ Applicability�6.    5{   *     5|   .   5~   '   5�        5�      $ 	工时(h)��6@   # [ 6O   # [ 6�    5�   "   �� 5�   &   $ Hours�6.    5�   *     5�   .   5�   '   5�        5�      $ 工时单价��6@   # [ 6N   # [ 6�    5�   "   �� 5�   &   $ 	UnitPrice�6.    5�   *     5�   .   5�   '   5�        5�      $ 状态��6@   # [ 6L   # [ 6�    5�   "   �� 5�   &   $ 	UnitPrice�6.    5�   *     5�   .   5�   '   5�        5�      $ 传输时间��6@   # [ 6N   # [ 6�    5�   "   �� 5�   &   $ 	UnitPrice�6.    5�   *     5�   .   5�   '   5�        5�      $ 机床编号��6@   # [ 6N   # [ 6�    5�   "   �� 5�   &   $ 	UnitPrice�6.    5�   *     5�   .   5�   '   5�        5�      $ 程序文件��6@   # [ 6N   # [ 6�    5�   "   �� 5�   &   $ 	UnitPrice�6.    5�   *     5�   .   5�   '   5�        5�      $ 	版本号��6@   # [ 6M   # [ 6�    5�   "   �� 5�   &   $ 	UnitPrice�6.    5�   *     5�   .   5�   '   5�      5�      5�      � 5�      $��1��6   	 5�      �� 5�      $��1* �6(   �� 5�      $��1* �6(   .+�5�      �� $��工艺卡（待发送）��6   $	��0 0q�6B   ��61   Z� .��5�        -   5        $
 	CraftGrid��5�   0   $��1��6#   $��
-2 0 -2 -2q�6C   $ FullRow=�5�      $ Single=�67   $	 All=�6N   $��1q�6g    .   .   . $ 
Horizontal=�5�       .   .   .  $
 Auto=�5�      $
 Auto=�6E    5�      ��%&   &	          5�   "    6P   �� 5�       5�        5�   "   $ 工艺卡名称��6D   # [ 6S   # [ 6�    5�   &   �� 5�   *   $ 	CraftName�62    5�   .     5�   2   5�   +   5�   #     5�   "   $ 审核状态��6D   # [ 6R   # [ 6�    5�   &   �� 5�   *   $ 
CheckState�62    5�   .     5�   2   5�   +   5�   #     5�   "   $ 审核时间��6D   # [ 6R   # [ 6�    5�   &   �� 5�   *   $ 	CheckTime�62    5�   .     5�   2   5�   +   5�   #     5�   "   $ 状态��6D   # [ 6P   # [ 6�    5�   &   �� 5�   *   $ 
CheckState�62    5�   .     5�   2   5  +   5  #     5  "   $ 传输时间��6D   # [ 6R   # [ 6�    5  &   �� 5  *   $ ModifiedTimee�62    5  .     5	  2   5  +   5  #   5     5     �� 5     $��1��6   $��NC程序（待发送）��6,   $	��0 0q�6P   ��6?   Z� .��5       -	   5        $
 NCGrid��5  0   $��1��6#   $��
-2 0 -2 -2q�6@   $ FullRow=�5     $ Single=�67   $	 All=�6N   $��1q�6g    .   .   . $ 
Horizontal=�5      .   .   .  $
 Auto=�5     $
 Auto=�6E    5     ��%&   &	          5  "    6P   �� 5      5       5  "   $ 程序名称��6D   # [ 6R   # [ 6�    5   &   �� 5!  *   $ NCProgramName�62    5"  .     5#  2   5%  +   5'  #     5)  "   $ 审核状态��6D   # [ 6R   # [ 6�    5*  &   �� 5+  *   $ 
CheckState�62    5,  .     5-  2   5/  +   51  #     53  "   $ 审核时间��6D   # [ 6R   # [ 6�    54  &   �� 55  *   $ 	CheckTime�62    56  .     57  2   59  +   5;  #     5=  "   $ 状态��6D   # [ 6P   # [ 6�    5>  &   �� 5?  *   $ 
CheckState�62    5@  .     5A  2   5C  +   5E  #     5G  "   $ 传输时间��6D   # [ 6R   # [ 6�    5H  &   �� 5I  *   $ ModifiedTimee�62    5J  .     5K  2   5M  +   5O  #   5R     5S     5T     5U     � 5W     $��2��6     5X     �� 5Y     $	��450 �6'   �� 5Z     $��1* �6'   .+�5]     �� $��主控配置��6   $��0 0 0 2q�6:   ��6)   Z� .��5^     ��   �� Orientation$  Vertical=�6!   $	��Top=�68   $��	10 0 10 0q�6P   .+�5_     �� $  
Horizontal=�6%   $
��Left=�6>   .+�5`     �� $��主控IP地址:��6(   $��0��67   ! �� TextAlignment$! Right=�6D   $��Center=�6Z   $��20(�6u   �� 5a     -
   5        " ��Name$" 
ip_address��5a  6   $��1��6&   $��192.168.1.201��6J   $��Center=�6_   # �� TextAlignment$# Center=�6z   $��20(�6�   $ �� 
IsReadOnly$ . $��10 0 5 0q�6�   $
��Auto��6�   ���#000000% �� 
CaretBrush% �#0000006�   �� 5b     -   5        & ��Name$& EditIpAdress��5b  %   $��修改��6;   $��60��6H   $��25��6S   $��10 0 5 0q�6_   ���#0A0C22
������ 
���   �6�   �� 5c     -   5        $& EditIpAdressOK��5c  %   $��确认��6=   $��60��6J   $��25��6U   $��10 0 5 0q�6a   ���#0A0C22
������ 
���   �6�   5d     �� 5e     $  
Horizontal=�6%   $
��Left=�6>   $
��0 10q�6Y   .+�5f     �� $��连接测试:��6(   $��0��65   $! Right=�6B   $��Center=�6X   $��20(�6s   �� 5g     -   5        $& ConnectIpAdressOK��5g  %   $��连接��6@   $��60��6M   $��25��6X   $��30 0 0 0q�6d   ���#0A0C22
������ 
���   �6�   �� 5h     -   5        $& UnConnectIpAdress��5h  %   $��断开��6@   $��60��6M   $��25��6X   $��30 0 0 0q�6d   ���#0A0C22
������ 
���   �6�   5i     �� 5j     $  
Horizontal=�6%   $
��Left=�6>   $	��0 0q�6Y   .+�5k     �� $��测试结果:��6(   $��0��65   $! Right=�6B   $��Center=�6X   $��20(�6s   $��0 0 0 0q�6�   �� 5l     -   5        $" ConnectResult��5l  &   $ . $�� ��6O   $��0��6W   $# Right=�6d   $��Center=�6z   $��16(�6�   $��	 30 0 0 0q�6�   $��80��6�   5m     5n     5o     �� 5q     $��1��6   $��传输控制��6+   $��0 0 0 2q�6J   ��69   Z� .��5r     �� $  Vertical=�6!   $	��Top=�68   $��
10 0 10 10q�6P   .+�5s     �� $  
Horizontal=�6%   $
��Left=�6>   $��0 0 0 10q�6Y   .+�5t     �� $��待传输工艺卡：��6(   $��0��67   $
! Left=�6D   $��Center=�6Y   $��20(�6t   �� 5u     -   5        $" CraftFileTextBox��5u  &   $	��200��6A   �� 5v     -   5        $& CrafFileAlert��5v  %   {�. $y�Hidden=�6M   
������ $��30��6�   $��30��6�    . *MaterialDesignFloatingActionMiniDarkButton#��[ 6�   ��6a   Z� ��6w   Z� .��5w  "   '  !MaterialDesignThemes.Wpf.PackIcon  '   Kind$' Alert=�6:   $��25��6G   $��25��6R   
���  ��6^   5x     �� 5y     -   5        ( ��Name$( CrafFileAlertTextBlock��5y  (   $y�Hidden=�6H   $��该文件未通过审核��6\   
���  ��$��Center=�6}   5z     �� 5|     $  
Horizontal=�6%   $
��Left=�6>   $��0 0 0 10q�6Y   .+�5}     �� $��待传输NC程序：��6(   $��0��68   $
! Left=�6E   $��Center=�6Z   $��20(�6u   �� 5~     -   5        $" NCFileTextBox��5~  &   $	��200��6=   �� 5     -   5        $& NCFileAlert��5  %   {�. $y�Hidden=�6K   
������ $��30��6�   $��30��6�   #��[ 6�   ��6_   Z� ��6u   Z� .��5�  "     $' Alert=�6:   $��25��6G   $��25��6R   
���  ��6^   5�     �� 5�     -   5        $( NCFileAlertTextBlock��5�  (   $y�Hidden=�6F   $��该文件未通过审核��6Z   
���  ��$��Center=�6{   5�     �� 5�     $  
Horizontal=�6%   $��Right=�6>   .+�5�     �� -   5        $& UploadButton��5�  %   {�.  $��传输��6M   $��60��6Z   $��30��6e   $��5 0 50 0q�6q   ���#0A0C22
������ 
���   �6�   �� 5�     -   5        $& CancelButton��5�  %   $��取消��6;   $��60��6H   $��30��6S   $��5 0 5 0q�6_   ���#0A0C22
������ 
���   �6�   5�     5�     5�     5�     5�     5�     5�     