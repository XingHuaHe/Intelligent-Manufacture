   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  \ XIntelligentManufactureApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Fclr-namespace:IntelligentManufactureApplication.Views.UserControlViews8IntelligentManufactureApplication.Views.UserControlViews g8clr-namespace:IntelligentManufactureApplication.Services*IntelligentManufactureApplication.Services M   GIntelligentManufactureApplication.Views.UserControlViews.FilesManagment   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  3system&clr-namespace:System;assembly=mscorlib  RlocalFclr-namespace:IntelligentManufactureApplication.Views.UserControlViews  Fservice8clr-namespace:IntelligentManufactureApplication.Services    � 	Resources   �� MergedDictionaries �� Source �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius	 � ColumnDefinitions
 �� TextAlignment ��Name
 � Tag !� Visual F�Name �� Triggers P� Property P� Value �� 
TargetName��  �� $j dpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.DialogHost.xaml?�%�  '��       �� " ��.���  � �� �� " ��.R��� $ layout��
�������$��1,1,1,0q�#��z��$ 0��$��	-2,0,0,-1q�._�� $��20��	 �� �� $��25 �.+��� $
 Center=�#��z��$��0��$��4,0,3,0q�$��Center=�$��Center=��� $ TabControlDeletedButton��-   $��X��$��1��$��8��$��8��$��4,1,3,2q�# z��$��Center=���Z� ��Z� ���� .R�� .+��� ��!�  F� $ btnPath��+����        р�� ��� �    ��� р��     	
�������$��1�� P�  �@J�IsMouseOver . . ��� $ btnPath�� � ��
 �  �� P�  �@��
IsSelected . . ���  � ��
 ������� $ layout�� � ��$ 	-2,0,0,-1q�P�  �@��
IsSelected .  . ���  � ��
 ��ح�.��� 
�������	 �� $	��210 ��� $��1* �.+�� .+�(  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Standard=�$	��0 0q�   CornerRadius$ 2�� K� ZIndex$ 1��$��5 10 5 5q�+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=�.���  � RowDefinitions �� $��30 ��� $
��Auto ��� $��1* �.+���  �� Orientation$ 
Horizontal=�$��Right=�$��Center=�.+�)  #MaterialDesignThemes.Wpf.DialogHost  -     Name$ LTreeViewNewButton��$
��Left=�$��Center=�   DialogContent �� $��16q�.+��� Add a new file.�� -    ��Name$ NewNameTextBox��$��0 8 0 0q�$��Stretch=��� $ 
Horizontal=�$��Right=�.+���    MaterialDesignFlatButton#��[   �� 	IsDefault . $��0 8 8 0q�$2��,materialDesign:DialogHost.CloseDialogCommand������True��.��ACCEPT�� #��[   �� IsCancel . $��0 8 8 0q�$2��,materialDesign:DialogHost.CloseDialogCommand������	False��.��CANCEL.���� -   $ NewFolderButton��$��30��$��30�� / +MaterialDesignFloatingActionMiniLightButton#��[  � ToolTip$ New Fold��
���   �   OpenDialogCommand#��Z ��Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon      Kind$  
FolderPlus=�$��21��$��19���� -   $ RefreshButton��$��30��$��30��#��[ $ Refresh��
���   ���Z� ��Z� .��  $  Refresh=�$��21��$��19���� $��1���� -   ! ��Name$! LeftScrollViewer��$��2��$	��200��$
��Auto=�$
��Auto=�" �� PanningMode$
" Both=�.��R� -   # R�Name$# LeftTreeView��$	��200��
�������$ � ContextMenu$ �� .@�m� -   % m�Name$% RenameMenuItem��$��	重命名���� m� -	   $% DeleteMenuItem��$��删除���� m� -
   $% UnfoldMenuItem��$��展开全部��.@�Q� -   & Q�Name$& LeftTreeViewItem��$��商品类别��' Q� 
IsExpanded' . � $��1��.+�  -   (  Name$( RightColorZone��$ Standard=�$��0 0 0 0q�$ 2��$ 1��$��1 2 2 2q�$ Depth2=�.���� -   ) ��Name$) FilesTabControl��$
��Left=�$	��Top=�$y�Hidden=�