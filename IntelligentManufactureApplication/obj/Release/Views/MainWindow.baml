   M S B A M L   `   `   `  ���� \  XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:IntelligentManufactureApplication.Views'IntelligentManufactureApplication.Views  8    2IntelligentManufactureApplication.Views.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Alocal5clr-namespace:IntelligentManufactureApplication.Views    � Title$  
MainWindow��$	��650��$
��1050�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � 	Resources    	leftImage �� ImageSource  leftImageSub  topImage  ToggleButtonStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName  RadioMiniButtonStyle �Name ��Name �� ContentStringFormat �� ContentStringFormat �� StrokeDashArray  ExpanderStyle d�Name d� 	IsChecked �� Mode �� RelativeSource��%8  &	        &	 #     &	 I     &	 k     7   &	 ~    7  &	 �    7  �� $ ../Images/leftImage.png���� $  ../Images/leftImageSub.png���� $ ../Images/topImage.png���� " d�.���  �@�FocusVisualStyle Z� ��  � ��$ 80����  � ��$ Right=���  � ��$ Center=���  � ��$ 10 10q���  � �� �� "	 d�.R�� .+��� $
 border2��#��z��#��z��#��z��#��z��._��� #��z��#��z����. # z #��z�� P�  � �� . . ���  � �� �	#FFd2e7f4P�  �@d�	IsChecked$
 trueY�. ��� $ border2��8    � ��P�  �@d�	IsChecked$ falseY�. ��� $ border2�� � �� �#191E36P�  � {� .  . ���  � ��
 ������� " 	�.���  � ��$ 5 5 1 5q���  � ��$ 22(���  � �� �#4B8EC4��  � ��$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 0q���  � �� �#101F3F��  � ��$ 50����  � ��$ Right=���  � �� �� "	 	�.R�� $ templateRoot��
������  . .+��� $
 border2���� $ contentPresenter��#��z��#��z��# z |�.  #��z��#��z����. # z #��z�� P�  � �� . . ���  �@�FocusVisualStyle �� .���  � �� �� .R��� $��14,0,0,0q� . #��� ��$��1��$	 1 2R���  � ��$ 4,-1,0,0q�P�  Z�  �@d�	IsCheckedP�  �@d�	IsChecked$
 trueY�. ���  � ��
 ������� $ border2��8    � ��P�  �@d�	IsChecked$ falseY�. ��� $ border2�� � �� �#101F3F��  � �� �#4B8EC4�� " 9�.���  � ��# � ����  � ��$ Stretch=���  � ��$ Stretch=���  � ��
 ���� ��  � ��$ 1q���  � �� �� "	 9�.R�]� .+�d� $ 
HeaderSite��#��z��#��z��#��z��$	��Top=�#��z��#��z��#��z��#��z��#��z��#��z��#��z��$��1q�$��0��$��0��#��z��8��  #��z�� �� *
IsExpanded+$ TwoWay=� �� *TemplatedParent+�� $ 
ExpandSite��$
��Left=�|�.  #��z��#��z��$y�	Collapsed=�#��z�� P�  �@9�
IsExpanded . . ��� $ 
ExpandSite�� � y�$ Visible=�.���  � RowDefinitions �� $��30 ��� $��50 ��� $��1* �.+�� -   $��30�����#121F40.+���  �� Orientation$ 
Horizontal=�$��Right=�$��Center=�$
��10,0q�.+��� -    ��Name$ 	MinButton�� / +MaterialDesignFloatingActionMiniLightButton#��[ $��30��$��30��
���������Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ WindowMinimize=��� -   $ RestoreButton��#��[ $��30��$��30��
���������Z� ��Z� .��  $ WindowRestore=��� -   $ CloseButton��#��[ $��30��$��30��
���������Z� ��Z� .��  $ Close=�� $��1��$
��Auto�����#121F40 � ColumnDefinitions �� $	��250 ��� �� $	��400 �.+��� $��1 1 1 0q����#121F40�� $ 
Horizontal=�$��Center=�.+��� $��70��$��50��
���������Z� ��Z� .��  $ Monitor=�$��40��$��40���� $��智能生产系统软件��$��20(�$��Center=�
�������$��Light"�$
��15 0q��� $��2��$ 
Horizontal=�$��Right=�.+��� -    ��Name$ HeadPortrait��$��40��$��40��$
��Fill=�$��2 0 2 0q� J� Clip @�   @� RadiusX$  20Q�! @� RadiusY$! 20Q�" @� Center$" 20,20&��� -   # ��Name$# UserNameTextBlock��$��	未登录��$��20(�$��Center=�
�������$��Light"�$
��15 0q�%  MaterialDesignThemes.Wpf.Badged  $   CornerRadius$$ 5��$��Center=�  ControlzEx.BadgedEx%   Badge% 0  *MaterialDesignThemes.Wpf.PackIconExtension  *Alert+.���� $��2 0 2 0q���Z� ��Z� .��  $
 Bell=�$��20��$��20���� $��2 0 2 0q���Z� ��Z� .��  $ EmailOutline=�$��20��$��20���� $��2 0 2 0q���Z� ��Z� .��  $ MessageProcessingOutline=�$��20��$��20��'  !MaterialDesignThemes.Wpf.PopupBox  $��Center=�&   PlacementMode$& BottomAndAlignRightEdges=�
�������$��2 0 20 0q�'   	StaysOpen' .  (   PopupContent.( �� .+��� $ 
Horizontal=�$��Center=�$��10q�.+��� $��Light��$��0 0 10 0q�d� -   $ DarkModeToggleButton���� $
��Dark��$��10 0 0 0q��� �� $��账户信息��$��Center=��� $��密码修改��$��Center=��� �� -   $ LogoutButton��$��注销��$��Center=�� $��2�����#0A0C22 �� $	��200 ��� $��1* �.+��� $��1q����#121F40���� $ ../images/leftback.png���� -	   ) ��Name$) LeftScrollViewer��$	��200��$	��570��$��Visible=�$
��Auto=�* �� PanningMode$
* Both=�.���� -
   + ��Name$+ LeftStackPanel��$ Vertical=�$	��Top=�.+�9� -   , 9�Name$, MonitoringAnalysis��#���  $	��180������ $ 
Horizontal=�$��Center=�$��Bottom=�$��80��$	��160��.+��� $��Center=�
�������$��40��$��40��$
��10 0q�  $ MonitorCellphone=�$��40��$��40���� $��15 0 0 0q�$��Center=�.+��� $��监控分析��$��20(�
����������� -   $+ MonitoringStackPanl��.+�	� #��[ $��40��$	��160��$��Center=�$	��5 5q�$��生产状态��
�������$��18(�	� #��[ $��40��$	��160��$��Center=�$	��5 5q�$��效率视图��
�������$��18(�	� #��[ $��40��$	��160��$��Center=�$	��5 5q�$��能耗视图��
�������$��18(�9� -   $, EquipmentManagement��#���  $	��180��$��0 1 0 0q����� $ 
Horizontal=�$��Center=�$��Center=�$��80��$	��160��.+��� $��Center=�
�������$��40��$��40��$
��10 0q�  $ DesktopMacDashboard=�$��40��$��40���� $��15 0 0 0q�$��Center=�.+��� $��设备管理��$��20(�
�������.���� -   $+ EquipmentStackPanl��.+�	� #��[ $��40��$	��160��$��Center=�$
�� 5 5q�$��添加/删除设备��
�������$��18(�	� #��[ $��40��$	��160��$��Center=�$
�� 5 5q�$��设备分组��
�������$��18(�	� #��[ $��40��$	��160��$��Center=�$
�� 5 5q�$��操作日记��
�������$��18(�9� #���  $	��180��$��0 1 0 0q����� $ 
Horizontal=�$��Center=�$��Center=�$��80��$	��160��.+��� $��Center=�
�������$��40��$��40��$
��10 0q�  $ FileDocument=�$��40��$��40���� $��15 0 0 0q�$��Center=�.+��� $��商品管理��$��20(�
�������.���� -   $+ CommodityStackPanl��.+�	� #��[ $��40��$	��160��$��Center=�$
�� 5 5q�$��程序管理��
�������$��18(�9� #���  $	��180��$��0 1 0 0q����� $ 
Horizontal=�$��Center=�$��Center=�$��80��$	��160��.+��� $��Center=�
�������$��40��$��40��$
��10 0q�  $ Tools=�$��40��$��40���� $��15 0 0 0q�$��Center=�.+��� $��生产制造��$��20(�
�������.���� -   $+ ManufacturingStackPanel��.+�	� -   - 	�Name$- flag1��#��[ $��40��$	��160��$	��5 5q�$��Center=�$��生产管理��
�������$��18(�	� -   $- flag2��#��[ $��40��$	��160��$	��5 5q�$��Center=�$��APS自动排产��
�������$��18(�	� #��[ $��40��$	��160��$	��5 5q�$��Center=�$��DNC程序管理��
�������$��18(�9� -   $, BasicInfoExpander��#���  $	��180��$��0 1 0 0q����� $ 
Horizontal=�$��Center=�$��Center=�$��80��$	��160��.+��� $��Center=�
�������$��40��$��40��$
��10 0q�  $ Firebase=�$��40��$��40���� $��15 0 0 0q�$��Center=�.+��� $��基础资料��$��20(�
�������.���� -   $+ BasicInformation��.+�	� #��[ $��40��$	��160��$	��5 5q�$��Center=�$��产品信息��
�������$��18(�	� #��[ $��40��$	��160��$	��5 5q�$��Center=�$��人员管理��
�������$��18(��� -   $+ StackPanelRight��$��1��