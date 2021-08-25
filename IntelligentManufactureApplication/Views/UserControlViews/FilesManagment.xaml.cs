using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.DBModels;
using IntelligentManufactureApplication.Models.StaticModels;
using IntelligentManufactureApplication.Services;
using IntelligentManufactureApplication.Views.DialogViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IntelligentManufactureApplication.Views.UserControlViews
{
    /// <summary>
    /// FilesManagment.xaml 的交互逻辑
    /// </summary>
    public partial class FilesManagment : UserControl
    {
        List<string> RegisterNames = new List<string>();
        List<string> TabContRegisterNames = new List<string>();
        public double _heligh { get; set; }
        public double _width { get; set; }

        public FilesManagment(double height, double width)
        {
            InitializeComponent();

            if (height == 0 && width == 0)
            {
                this.LeftTreeView.Height = 570.0 - 19.0 - 30.0;
                _heligh = 570.0 - 4.0;
                _width = 1050 - 200.0 - 210.0 - 3.0;
                this.RightColorZone.Height = _heligh;
                this.RightColorZone.Width = _width;
            }
            else
            {
                this.LeftTreeView.Height = height - 19.0 - 30.0;
                _heligh = height - 4.0;
                _width = width - 210.0 - 3.0;
                this.RightColorZone.Height = _heligh;
                this.RightColorZone.Width = _width;
            }
            InitTreeView();
        }
        /// <summary>
        /// (LeftTreeView)初始化
        /// </summary>
        private void InitTreeView()
        {
            try
            {
                //this.LeftTreeViewItem.Items.Clear();
                if (RegisterNames.Count > 0)
                {
                    //检查是否存在已注册名，如果有，则移除.
                    foreach (var item in RegisterNames)
                    {
                        this.LeftTreeViewItem.UnregisterName(item);
                    }
                }
                using (MonitoringContext _monitoringContext = new MonitoringContext())
                {
                    //遍历类型表（1级）.
                    var categories = (from obj in _monitoringContext.Categories
                                      select obj).ToList();
                    foreach (var category in categories)
                    {
                        TreeViewItem categoryItem = new TreeViewItem();
                        categoryItem.Header = category.CategoryName;
                        categoryItem.Name = category.CategoryID;
                        this.LeftTreeViewItem.RegisterName(category.CategoryID, categoryItem);//注册在LeftTreeViewItem下
                        RegisterNames.Add(category.CategoryID);
                        this.LeftTreeViewItem.Items.Add(categoryItem);

                        if (category.ChildenID != false)
                        {
                            //遍历部件类型表（2级）.
                            var componentTypes = (from obj in _monitoringContext.ComponentTypes
                                                  where obj.ParentID == category.CategoryID
                                                  select obj).ToList();
                            foreach (var componentType in componentTypes)
                            {
                                TreeViewItem componentTypeItem = new TreeViewItem();
                                componentTypeItem.Header = componentType.ComponentTypeName;
                                componentTypeItem.Name = componentType.ComponentTypeID;
                                this.LeftTreeViewItem.RegisterName(componentType.ComponentTypeID, componentTypeItem);//注册在LeftTreeViewItem下
                                RegisterNames.Add(componentType.ComponentTypeID);
                                categoryItem.Items.Add(componentTypeItem);

                                if (componentType.ChildenID != false)
                                {
                                    //遍历部件表（3级）
                                    var components = (from obj in _monitoringContext.Components
                                                      where obj.ParentID == componentType.ComponentTypeID
                                                      select obj).ToList();
                                    foreach (var component in components)
                                    {
                                        TreeViewItem componentItem = new TreeViewItem();
                                        componentItem.Header = component.ComponentName;
                                        componentItem.Name = component.ComponentID;
                                        this.LeftTreeViewItem.RegisterName(component.ComponentID, componentItem);//注册在treeViewItem1下
                                        RegisterNames.Add(component.ComponentID);
                                        componentTypeItem.Items.Add(componentItem);

                                        if (component.ChildenID != false)
                                        {
                                            //遍历零件类型表（4级）
                                            var partTypes = (from obj in _monitoringContext.PartTypes
                                                             where obj.ParentID == component.ComponentID
                                                             select obj).ToList();
                                            foreach (var partType in partTypes)
                                            {
                                                TreeViewItem partTypeItem = new TreeViewItem();
                                                partTypeItem.Header = partType.PartTypeName;
                                                partTypeItem.Name = partType.PartTypeID;
                                                this.LeftTreeViewItem.RegisterName(partType.PartTypeID, partTypeItem);//注册在treeViewItem1下
                                                RegisterNames.Add(partType.PartTypeID);
                                                componentItem.Items.Add(partTypeItem);

                                                if (partType.ChildenID != false)
                                                {
                                                    var parts = (from obj in _monitoringContext.Parts
                                                                 where obj.ParentID == partType.PartTypeID
                                                                 select obj).ToList();
                                                    foreach (var part in parts)
                                                    {
                                                        TreeViewItem partItem = new TreeViewItem();
                                                        partItem.Header = part.PartName;
                                                        partItem.Name = part.PartID;
                                                        this.LeftTreeViewItem.RegisterName(part.PartID, partItem);//注册在treeViewItem1下
                                                        RegisterNames.Add(part.PartID);
                                                        partTypeItem.Items.Add(partItem);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// (LeftTreeView)新建文件夹按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void LTreeViewNewButton_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            //you can cancel the dialog close:
            //eventArgs.Cancel();
            if (Equals(eventArgs.Parameter, true) && !string.IsNullOrWhiteSpace(this.NewNameTextBox.Text))
            {
                //Accept and the name of input is not null.
                try
                {
                    TreeViewItem treeViewItem = this.LeftTreeView.SelectedItem as TreeViewItem;
                    if (treeViewItem != null)
                    {
                        int row = 0;
                        string filePath = string.Empty;
                        switch (treeViewItem.Name.Split('_')[0])
                        {
                            case "LeftTreeViewItem":
                                //新建1级文件.
                                string categoryId = string.Format("Cg_{0}", System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));
                                filePath = System.IO.Path.Combine(StaticPathVariable.FileSaveRootPath, categoryId);
                                if (!System.IO.Directory.Exists(filePath))
                                {
                                    //创建文件夹
                                    System.IO.Directory.CreateDirectory(filePath);
                                }
                                TCategory category = new TCategory()
                                {
                                    CategoryID = categoryId,
                                    CategoryName = this.NewNameTextBox.Text,
                                    ChildenID = false,
                                    SavePath = filePath
                                };
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    _monitoringContext.Categories.Add(category);
                                    row = _monitoringContext.SaveChanges();
                                }
                                if (row > 0)
                                {
                                    TreeViewItem treeViewItem1 = new TreeViewItem();
                                    treeViewItem1.Header = category.CategoryName;
                                    treeViewItem1.Name = category.CategoryID;
                                    this.LeftTreeViewItem.RegisterName(category.CategoryID, treeViewItem1);
                                    RegisterNames.Add(category.CategoryID);
                                    treeViewItem.Items.Add(treeViewItem1);
                                }
                                break;
                            case "Cg":
                                //新建2级文件.
                                string componentTypeId = string.Format("CT_{0}", System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    var linq = (from obj in _monitoringContext.Categories
                                                where obj.CategoryID == treeViewItem.Name
                                                select obj).SingleOrDefault();
                                    //父节点记录存在子节点.
                                    linq.ChildenID = true;
                                    _monitoringContext.SaveChanges();
                                    filePath = System.IO.Path.Combine(linq.SavePath, componentTypeId);
                                }
                                if (!System.IO.Directory.Exists(filePath))
                                {
                                    //不存在则创建文件夹
                                    System.IO.Directory.CreateDirectory(filePath);
                                }
                                TComponentType componentType = new TComponentType()
                                {
                                    ComponentTypeID = componentTypeId,
                                    ComponentTypeName = this.NewNameTextBox.Text,
                                    ParentID = treeViewItem.Name,
                                    ChildenID = false,
                                    SavePath = filePath
                                };
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    _monitoringContext.ComponentTypes.Add(componentType);
                                    row = _monitoringContext.SaveChanges();
                                }
                                if (row > 0)
                                {
                                    TreeViewItem treeViewItem1 = new TreeViewItem();
                                    treeViewItem1.Header = componentType.ComponentTypeName;
                                    treeViewItem1.Name = componentType.ComponentTypeID;
                                    this.LeftTreeViewItem.RegisterName(componentType.ComponentTypeID, treeViewItem1);
                                    RegisterNames.Add(componentType.ComponentTypeID);
                                    treeViewItem.Items.Add(treeViewItem1);
                                    treeViewItem.IsExpanded = true;
                                }
                                break;
                            case "CT":
                                //新建3级文件.
                                string componentId = string.Format("C_{0}", System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    var linq = (from obj in _monitoringContext.ComponentTypes
                                                where obj.ComponentTypeID == treeViewItem.Name
                                                select obj).SingleOrDefault();
                                    linq.ChildenID = true;
                                    _monitoringContext.SaveChanges();
                                    filePath = System.IO.Path.Combine(linq.SavePath, componentId);
                                }
                                if (!System.IO.Directory.Exists(filePath))
                                {
                                    //不存在则创建文件夹
                                    System.IO.Directory.CreateDirectory(filePath);
                                }
                                TComponent component = new TComponent()
                                {
                                    ComponentID = componentId,
                                    ComponentName = this.NewNameTextBox.Text,
                                    ParentID = treeViewItem.Name,
                                    ChildenID = false,
                                    SavePath = filePath
                                };
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    _monitoringContext.Components.Add(component);
                                    row = _monitoringContext.SaveChanges();
                                }
                                if (row > 0)
                                {
                                    TreeViewItem treeViewItem1 = new TreeViewItem();
                                    treeViewItem1.Header = component.ComponentName;
                                    treeViewItem1.Name = component.ComponentID;
                                    this.LeftTreeViewItem.RegisterName(component.ComponentID, treeViewItem1);
                                    RegisterNames.Add(component.ComponentID);
                                    treeViewItem.Items.Add(treeViewItem1);
                                    treeViewItem.IsExpanded = true;
                                }
                                break;
                            case "C":
                                //新建4级文件.
                                string partTypeId = string.Format("PT_{0}", System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    var linq = (from obj in _monitoringContext.Components
                                                where obj.ComponentID == treeViewItem.Name
                                                select obj).SingleOrDefault();
                                    linq.ChildenID = true;
                                    _monitoringContext.SaveChanges();
                                    filePath = System.IO.Path.Combine(linq.SavePath, partTypeId);
                                }
                                if (!System.IO.Directory.Exists(filePath))
                                {
                                    //不存在则创建文件夹
                                    System.IO.Directory.CreateDirectory(filePath);
                                }
                                TPartType partType = new TPartType()
                                {
                                    PartTypeID = partTypeId,
                                    PartTypeName = this.NewNameTextBox.Text,
                                    ParentID = treeViewItem.Name,
                                    ChildenID = false,
                                    SavePath = filePath
                                };
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    _monitoringContext.PartTypes.Add(partType);
                                    row = _monitoringContext.SaveChanges();
                                }
                                if (row > 0)
                                {
                                    TreeViewItem treeViewItem1 = new TreeViewItem();
                                    treeViewItem1.Header = partType.PartTypeName;
                                    treeViewItem1.Name = partType.PartTypeID;
                                    this.LeftTreeViewItem.RegisterName(partType.PartTypeID, treeViewItem1);
                                    RegisterNames.Add(partType.PartTypeID);
                                    treeViewItem.Items.Add(treeViewItem1);
                                    treeViewItem.IsExpanded = true;
                                }
                                break;
                            case "PT":
                                string partId = string.Format("P_{0}", System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    var linq = (from obj in _monitoringContext.PartTypes
                                                where obj.PartTypeID == treeViewItem.Name
                                                select obj).SingleOrDefault();
                                    linq.ChildenID = true;
                                    row = _monitoringContext.SaveChanges();
                                    filePath = System.IO.Path.Combine(linq.SavePath, partId);
                                }
                                if (!System.IO.Directory.Exists(filePath))
                                {
                                    //不存在则创建文件夹
                                    System.IO.Directory.CreateDirectory(filePath);
                                }
                                TPart part = new TPart()
                                {
                                    PartID = partId,
                                    PartName = this.NewNameTextBox.Text,
                                    ParentID = treeViewItem.Name,
                                    ChildenID = false,
                                    SavePath = filePath
                                };
                                using (MonitoringContext _monitoringContext = new MonitoringContext())
                                {
                                    _monitoringContext.Parts.Add(part);
                                    row = _monitoringContext.SaveChanges();
                                }
                                if (row > 0)
                                {
                                    TreeViewItem treeViewItem1 = new TreeViewItem();
                                    treeViewItem1.Header = part.PartName;
                                    treeViewItem1.Name = part.PartID;
                                    this.LeftTreeViewItem.RegisterName(part.PartID, treeViewItem1);
                                    RegisterNames.Add(part.PartID);
                                    treeViewItem.Items.Add(treeViewItem1);
                                    treeViewItem.IsExpanded = true;
                                }
                                break;
                            case "P":

                                break;
                        }
                    }
                }
                catch
                {

                }
                return;
            }
            else
            {
                //Cancell
                return;
            }
        }
        /// <summary>
        /// (LeftTreeView)刷新按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            this.LeftTreeViewItem.Items.Clear();
            foreach (var item in RegisterNames)
            {
                this.LeftTreeViewItem.UnregisterName(item);
            }
            RegisterNames.Clear();
            InitTreeView();
        }
        /// <summary>
        /// (LeftTreeView)选择触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string name = ((TreeViewItem)e.NewValue).Name.ToString();
            string header = ((TreeViewItem)e.NewValue).Header.ToString();
            try
            {
                switch (name.Split('_')[0])
                {
                    case "P":
                        this.NewFolderButton.IsEnabled = false;
                        this.FilesTabControl.Visibility = Visibility.Visible;

                        if (TabContRegisterNames.Count > 0)
                        {
                            foreach (var item in TabContRegisterNames)
                            {
                                this.FilesTabControl.UnregisterName(item);
                            }
                            TabContRegisterNames.Clear();
                            this.FilesTabControl.Items.Clear();
                        }

                        TabItem tabItem = new TabItem();
                        tabItem.Header = header;
                        tabItem.IsSelected = true;
                        FilesDataGrid filesDataGrid = new FilesDataGrid(name, _heligh, _width, this.FilesTabControl);
                        tabItem.Content = filesDataGrid;
                        this.FilesTabControl.Items.Add(tabItem);
                        this.FilesTabControl.RegisterName("filesmanagement", tabItem);
                        TabContRegisterNames.Add("filesmanagement");
                        break;
                    default:
                        this.NewFolderButton.IsEnabled = true;
                        break;
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// (LeftTreeView)重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem treeViewItem = this.LeftTreeView.SelectedItem as TreeViewItem;
                RenameTreeView renameTreeView = new RenameTreeView(treeViewItem.Name, treeViewItem.Header.ToString());
                renameTreeView.ShowDialog();

                string newHeader = renameTreeView.GetNewName();
                if (newHeader != string.Empty)
                {
                    TreeViewItem treeViewItem1 = this.LeftTreeViewItem.FindName(treeViewItem.Name) as TreeViewItem;
                    treeViewItem1.Header = newHeader;
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// (LeftTreeView)删除按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem treeViewItem = this.LeftTreeView.SelectedItem as TreeViewItem;
                if (treeViewItem != null)
                {
                    switch (treeViewItem.Name.Split('_')[0])
                    {
                        case "Cg":
                            using (MonitoringContext _monitoringContext = new MonitoringContext())
                            {
                                //1级节点
                                var category = (from obj in _monitoringContext.Categories
                                                where obj.CategoryID == treeViewItem.Name
                                                select obj).SingleOrDefault();
                                //2级节点
                                var compoentTypes = (from obj in _monitoringContext.ComponentTypes
                                                     where obj.ParentID == category.CategoryID
                                                     select obj).ToList();
                                foreach (var compoentType in compoentTypes)
                                {
                                    //3级节点
                                    var compoents = (from obj in _monitoringContext.Components
                                                     where obj.ParentID == compoentType.ComponentTypeID
                                                     select obj).ToList();
                                    foreach (var compoent in compoents)
                                    {
                                        //4级节点
                                        var partTypes = (from obj in _monitoringContext.PartTypes
                                                         where obj.ParentID == compoent.ComponentID
                                                         select obj).ToList();
                                        foreach (var partType in partTypes)
                                        {
                                            //5级节点
                                            var parts = (from obj in _monitoringContext.Parts
                                                         where obj.ParentID == partType.PartTypeID
                                                         select obj).ToList();
                                            foreach (var part in parts)
                                            {
                                                var products = (from obj in _monitoringContext.ProductLists
                                                                where obj.PartID == part.PartID
                                                                select obj).ToList();
                                                foreach (var product in products)
                                                {
                                                    //删除工序
                                                    var processes = (from obj in _monitoringContext.Processes
                                                                     where obj.Id == product.Id
                                                                     select obj).ToList();
                                                    foreach (var process in processes)
                                                    {
                                                        _monitoringContext.Processes.Remove(process);
                                                    }
                                                    //删除工艺卡
                                                    var crafts = (from obj in _monitoringContext.Crafts
                                                                  where obj.Id == product.Id
                                                                  select obj).ToList();
                                                    foreach (var item in crafts)
                                                    {
                                                        _monitoringContext.Crafts.Remove(item);
                                                    }
                                                    //删除图纸
                                                    var drawings = (from obj in _monitoringContext.Drawings
                                                                    where obj.Id == product.Id
                                                                    select obj).ToList();
                                                    foreach (var item in drawings)
                                                    {
                                                        _monitoringContext.Drawings.Remove(item);
                                                    }
                                                    //删除NC程序
                                                    var ncP = (from obj in _monitoringContext.NCPrograms
                                                               where obj.Id == product.Id
                                                               select obj).ToList();
                                                    foreach (var item in ncP)
                                                    {
                                                        _monitoringContext.NCPrograms.Remove(item);
                                                    }
                                                    //删除product list.
                                                    _monitoringContext.ProductLists.Remove(product);
                                                }
                                                //TreeViewItem partItem = this.LeftTreeViewItem.FindName(part.PartID) as TreeViewItem;
                                                this.LeftTreeViewItem.UnregisterName(part.PartID);
                                                RegisterNames.Remove(part.PartID);
                                                _monitoringContext.Parts.Remove(part);

                                            }
                                            this.LeftTreeViewItem.UnregisterName(partType.PartTypeID);
                                            RegisterNames.Remove(partType.PartTypeID);
                                            _monitoringContext.PartTypes.Remove(partType);
                                        }
                                        this.LeftTreeViewItem.UnregisterName(compoent.ComponentID);
                                        RegisterNames.Remove(compoent.ComponentID);
                                        _monitoringContext.Components.Remove(compoent);
                                    }
                                    this.LeftTreeViewItem.UnregisterName(compoentType.ComponentTypeID);
                                    RegisterNames.Remove(compoentType.ComponentTypeID);
                                    _monitoringContext.ComponentTypes.Remove(compoentType);
                                }

                                TreeViewItem categoryItem = this.LeftTreeViewItem.FindName(category.CategoryID) as TreeViewItem;
                                categoryItem.Items.Clear();
                                this.LeftTreeViewItem.Items.Remove(categoryItem);
                                this.LeftTreeViewItem.UnregisterName(category.CategoryID);
                                RegisterNames.Remove(category.CategoryID);
                                _monitoringContext.Categories.Remove(category);
                                int row = _monitoringContext.SaveChanges();
                                if (row > 0)
                                {
                                    ProductListService productListService = new ProductListService();
                                    this.DataContext = productListService;

                                    //删除文件夹
                                    DirectoryInfo directoryInfo = new DirectoryInfo(category.SavePath);
                                    directoryInfo.Delete(true);
                                }
                            }
                            break;
                        case "CT":
                            using (MonitoringContext _monitoringContext = new MonitoringContext())
                            {
                                //2级节点
                                var compoentType = (from obj in _monitoringContext.ComponentTypes
                                                    where obj.ComponentTypeID == treeViewItem.Name
                                                    select obj).SingleOrDefault();
                                //3级节点
                                var compoents = (from obj in _monitoringContext.Components
                                                 where obj.ParentID == compoentType.ComponentTypeID
                                                 select obj).ToList();
                                foreach (var compoent in compoents)
                                {
                                    //4级节点
                                    var partTypes = (from obj in _monitoringContext.PartTypes
                                                     where obj.ParentID == compoent.ComponentID
                                                     select obj).ToList();
                                    foreach (var partType in partTypes)
                                    {
                                        //5级节点
                                        var parts = (from obj in _monitoringContext.Parts
                                                     where obj.ParentID == partType.PartTypeID
                                                     select obj).ToList();
                                        foreach (var part in parts)
                                        {
                                            var products = (from obj in _monitoringContext.ProductLists
                                                            where obj.PartID == part.PartID
                                                            select obj).ToList();
                                            foreach (var product in products)
                                            {
                                                //删除工序
                                                var processes = (from obj in _monitoringContext.Processes
                                                                 where obj.Id == product.Id
                                                                 select obj).ToList();
                                                foreach (var process in processes)
                                                {
                                                    _monitoringContext.Processes.Remove(process);
                                                }
                                                //删除工艺卡
                                                var crafts = (from obj in _monitoringContext.Crafts
                                                              where obj.Id == product.Id
                                                              select obj).ToList();
                                                foreach (var item in crafts)
                                                {
                                                    _monitoringContext.Crafts.Remove(item);
                                                }
                                                //删除图纸
                                                var drawings = (from obj in _monitoringContext.Drawings
                                                                where obj.Id == product.Id
                                                                select obj).ToList();
                                                foreach (var item in drawings)
                                                {
                                                    _monitoringContext.Drawings.Remove(item);
                                                }
                                                //删除NC程序
                                                var ncP = (from obj in _monitoringContext.NCPrograms
                                                           where obj.Id == product.Id
                                                           select obj).ToList();
                                                foreach (var item in ncP)
                                                {
                                                    _monitoringContext.NCPrograms.Remove(item);
                                                }
                                                //删除product list.
                                                _monitoringContext.ProductLists.Remove(product);
                                            }
                                            //TreeViewItem partItem = this.LeftTreeViewItem.FindName(part.PartID) as TreeViewItem;
                                            this.LeftTreeViewItem.UnregisterName(part.PartID);
                                            RegisterNames.Remove(part.PartID);
                                            _monitoringContext.Parts.Remove(part);

                                        }
                                        this.LeftTreeViewItem.UnregisterName(partType.PartTypeID);
                                        RegisterNames.Remove(partType.PartTypeID);
                                        _monitoringContext.PartTypes.Remove(partType);
                                    }
                                    this.LeftTreeViewItem.UnregisterName(compoent.ComponentID);
                                    RegisterNames.Remove(compoent.ComponentID);
                                    _monitoringContext.Components.Remove(compoent);
                                }
                                TreeViewItem categoryItem = this.LeftTreeViewItem.FindName(compoentType.ParentID) as TreeViewItem;
                                TreeViewItem compoentTypeItem = this.LeftTreeViewItem.FindName(compoentType.ComponentTypeID) as TreeViewItem;
                                compoentTypeItem.Items.Clear();
                                categoryItem.Items.Remove(compoentTypeItem);
                                this.LeftTreeViewItem.UnregisterName(compoentType.ComponentTypeID);
                                RegisterNames.Remove(compoentType.ComponentTypeID);
                                _monitoringContext.ComponentTypes.Remove(compoentType);

                                //父节点是否存在子节点改为False.
                                var category = (from obj in _monitoringContext.Categories
                                                where obj.CategoryID == compoentType.ParentID
                                                select obj).SingleOrDefault();
                                category.ChildenID = false;

                                int row = _monitoringContext.SaveChanges();
                                if (row > 0)
                                {
                                    ProductListService productListService = new ProductListService();
                                    this.DataContext = productListService;

                                    //删除文件夹
                                    DirectoryInfo directoryInfo = new DirectoryInfo(compoentType.SavePath);
                                    directoryInfo.Delete(true);
                                }
                            }
                            break;
                        case "C":
                            using (MonitoringContext _monitoringContext = new MonitoringContext())
                            {

                                //3级节点
                                var compoent = (from obj in _monitoringContext.Components
                                                where obj.ComponentID == treeViewItem.Name
                                                select obj).SingleOrDefault();
                                //4级节点
                                var partTypes = (from obj in _monitoringContext.PartTypes
                                                 where obj.ParentID == compoent.ComponentID
                                                 select obj).ToList();
                                foreach (var partType in partTypes)
                                {
                                    //5级节点
                                    var parts = (from obj in _monitoringContext.Parts
                                                 where obj.ParentID == partType.PartTypeID
                                                 select obj).ToList();
                                    foreach (var part in parts)
                                    {
                                        var products = (from obj in _monitoringContext.ProductLists
                                                        where obj.PartID == part.PartID
                                                        select obj).ToList();
                                        foreach (var product in products)
                                        {
                                            //删除工序
                                            var processes = (from obj in _monitoringContext.Processes
                                                             where obj.Id == product.Id
                                                             select obj).ToList();
                                            foreach (var process in processes)
                                            {
                                                _monitoringContext.Processes.Remove(process);
                                            }
                                            //删除工艺卡
                                            var crafts = (from obj in _monitoringContext.Crafts
                                                          where obj.Id == product.Id
                                                          select obj).ToList();
                                            foreach (var item in crafts)
                                            {
                                                _monitoringContext.Crafts.Remove(item);
                                            }
                                            //删除图纸
                                            var drawings = (from obj in _monitoringContext.Drawings
                                                            where obj.Id == product.Id
                                                            select obj).ToList();
                                            foreach (var item in drawings)
                                            {
                                                _monitoringContext.Drawings.Remove(item);
                                            }
                                            //删除NC程序
                                            var ncP = (from obj in _monitoringContext.NCPrograms
                                                       where obj.Id == product.Id
                                                       select obj).ToList();
                                            foreach (var item in ncP)
                                            {
                                                _monitoringContext.NCPrograms.Remove(item);
                                            }
                                            //删除product list.
                                            _monitoringContext.ProductLists.Remove(product);
                                        }
                                        //TreeViewItem partItem = this.LeftTreeViewItem.FindName(part.PartID) as TreeViewItem;
                                        this.LeftTreeViewItem.UnregisterName(part.PartID);
                                        RegisterNames.Remove(part.PartID);
                                        _monitoringContext.Parts.Remove(part);

                                    }
                                    this.LeftTreeViewItem.UnregisterName(partType.PartTypeID);
                                    RegisterNames.Remove(partType.PartTypeID);
                                    _monitoringContext.PartTypes.Remove(partType);
                                }
                                TreeViewItem compoentTypeItem = this.LeftTreeViewItem.FindName(compoent.ParentID) as TreeViewItem;
                                TreeViewItem compoentItem = this.LeftTreeViewItem.FindName(compoent.ComponentID) as TreeViewItem;
                                compoentItem.Items.Clear();
                                compoentTypeItem.Items.Remove(compoentItem);
                                this.LeftTreeViewItem.UnregisterName(compoent.ComponentID);
                                RegisterNames.Remove(compoent.ComponentID);
                                _monitoringContext.Components.Remove(compoent);

                                //父节点是否存在子节点改为False.
                                var componentType = (from obj in _monitoringContext.ComponentTypes
                                                     where obj.ComponentTypeID == compoent.ParentID
                                                     select obj).SingleOrDefault();
                                componentType.ChildenID = false;

                                int row = _monitoringContext.SaveChanges();
                                if (row > 0)
                                {
                                    ProductListService productListService = new ProductListService();
                                    this.DataContext = productListService;

                                    //删除文件夹
                                    DirectoryInfo directoryInfo = new DirectoryInfo(compoent.SavePath);
                                    directoryInfo.Delete(true);
                                }
                            }
                            break;
                        case "PT":
                            using (MonitoringContext _monitoringContext = new MonitoringContext())
                            {
                                //4级节点
                                var partType = (from obj in _monitoringContext.PartTypes
                                                where obj.PartTypeID == treeViewItem.Name
                                                select obj).SingleOrDefault();
                                //5级节点
                                var parts = (from obj in _monitoringContext.Parts
                                             where obj.ParentID == partType.PartTypeID
                                             select obj).ToList();
                                foreach (var part in parts)
                                {
                                    var products = (from obj in _monitoringContext.ProductLists
                                                    where obj.PartID == part.PartID
                                                    select obj).ToList();
                                    foreach (var product in products)
                                    {
                                        //删除工序
                                        var processes = (from obj in _monitoringContext.Processes
                                                         where obj.Id == product.Id
                                                         select obj).ToList();
                                        foreach (var process in processes)
                                        {
                                            _monitoringContext.Processes.Remove(process);
                                        }
                                        //删除工艺卡
                                        var crafts = (from obj in _monitoringContext.Crafts
                                                      where obj.Id == product.Id
                                                      select obj).ToList();
                                        foreach (var item in crafts)
                                        {
                                            _monitoringContext.Crafts.Remove(item);
                                        }
                                        //删除图纸
                                        var drawings = (from obj in _monitoringContext.Drawings
                                                        where obj.Id == product.Id
                                                        select obj).ToList();
                                        foreach (var item in drawings)
                                        {
                                            _monitoringContext.Drawings.Remove(item);
                                        }
                                        //删除NC程序
                                        var ncP = (from obj in _monitoringContext.NCPrograms
                                                   where obj.Id == product.Id
                                                   select obj).ToList();
                                        foreach (var item in ncP)
                                        {
                                            _monitoringContext.NCPrograms.Remove(item);
                                        }
                                        //删除product list.
                                        _monitoringContext.ProductLists.Remove(product);
                                    }
                                    //TreeViewItem partItem = this.LeftTreeViewItem.FindName(part.PartID) as TreeViewItem;
                                    this.LeftTreeViewItem.UnregisterName(part.PartID);
                                    RegisterNames.Remove(part.PartID);
                                    _monitoringContext.Parts.Remove(part);

                                }
                                TreeViewItem compoentItem = this.LeftTreeViewItem.FindName(partType.ParentID) as TreeViewItem;
                                TreeViewItem partTypeItem = this.LeftTreeViewItem.FindName(partType.PartTypeID) as TreeViewItem;
                                partTypeItem.Items.Clear();
                                compoentItem.Items.Remove(partTypeItem);
                                this.LeftTreeViewItem.UnregisterName(partType.PartTypeID);
                                RegisterNames.Remove(partType.PartTypeID);
                                _monitoringContext.PartTypes.Remove(partType);

                                //父节点是否存在子节点改为False.
                                var compoent = (from obj in _monitoringContext.Components
                                                where obj.ComponentID == partType.ParentID
                                                select obj).SingleOrDefault();
                                compoent.ChildenID = false;

                                int row = _monitoringContext.SaveChanges();
                                if (row > 0)
                                {
                                    ProductListService productListService = new ProductListService();
                                    this.DataContext = productListService;

                                    //删除文件夹
                                    DirectoryInfo directoryInfo = new DirectoryInfo(partType.SavePath);
                                    directoryInfo.Delete(true);
                                }
                            }
                            break;
                        case "P":
                            using (MonitoringContext monitoringContext = new MonitoringContext())
                            {
                                var part = (from obj in monitoringContext.Parts
                                            where obj.PartID == treeViewItem.Name
                                            select obj).SingleOrDefault();
                                var products = (from obj in monitoringContext.ProductLists
                                                where obj.PartID == part.PartID
                                                select obj).ToList();
                                if (products != null)
                                {
                                    foreach (var product in products)
                                    {
                                        //删除工序
                                        var processes = (from obj in monitoringContext.Processes
                                                         where obj.Id == product.Id
                                                         select obj).ToList();
                                        foreach (var process in processes)
                                        {
                                            monitoringContext.Processes.Remove(process);
                                        }
                                        //删除工艺卡
                                        var crafts = (from obj in monitoringContext.Crafts
                                                      where obj.Id == product.Id
                                                      select obj).ToList();
                                        if (crafts != null)
                                        {
                                            foreach (var item in crafts)
                                            {
                                                monitoringContext.Crafts.Remove(item);
                                            }
                                        }
                                        //删除图纸
                                        var drawings = (from obj in monitoringContext.Drawings
                                                        where obj.Id == product.Id
                                                        select obj).ToList();
                                        if (drawings != null)
                                        {
                                            foreach (var item in drawings)
                                            {
                                                monitoringContext.Drawings.Remove(item);
                                            }
                                        }
                                        //删除NC程序
                                        var ncP = (from obj in monitoringContext.NCPrograms
                                                   where obj.Id == product.Id
                                                   select obj).ToList();
                                        if (ncP != null)
                                        {
                                            foreach (var item in ncP)
                                            {
                                                monitoringContext.NCPrograms.Remove(item);
                                            }
                                        }
                                        //删除product list.
                                        monitoringContext.ProductLists.Remove(product);
                                    }
                                }
                                TreeViewItem partTypeItem = this.LeftTreeViewItem.FindName(part.ParentID) as TreeViewItem;
                                TreeViewItem partItem = this.LeftTreeViewItem.FindName(part.PartID) as TreeViewItem;
                                this.LeftTreeViewItem.UnregisterName(part.PartID);
                                RegisterNames.Remove(part.PartID);
                                partTypeItem.Items.Remove(partItem);
                                monitoringContext.Parts.Remove(part);

                                //父节点是否存在子节点改为False.
                                var partType = (from obj in monitoringContext.PartTypes
                                                where obj.PartTypeID == part.ParentID
                                                select obj).SingleOrDefault();
                                partType.ChildenID = false;

                                int row = monitoringContext.SaveChanges();
                                if (row > 0)
                                {
                                    ProductListService productListService = new ProductListService();
                                    this.DataContext = productListService;

                                    //删除文件夹
                                    DirectoryInfo directoryInfo = new DirectoryInfo(part.SavePath);
                                    directoryInfo.Delete(true);
                                }

                            }
                            break;
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// (LeftTreeView)显示全部按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnfoldMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem treeViewItem = this.LeftTreeView.SelectedItem as TreeViewItem;
                if (treeViewItem != null)
                {
                    treeViewItem.IsExpanded = true;
                    Unfold(treeViewItem);
                }
            }
            catch
            {

            }
        }
        private void Unfold(TreeViewItem treeViewItem)
        {
            if (treeViewItem.Items.Count > 0)
            {
                foreach (TreeViewItem item in treeViewItem.Items)
                {
                    item.IsExpanded = true;
                    Unfold(item);
                }
            }
        }

        private void TabControlDeletedButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string header = btn.Tag.ToString();
            foreach (TabItem item in this.FilesTabControl.Items)
            {
                if (item.Header.ToString() == header)
                {
                    this.FilesTabControl.Items.Remove(item);
                    //TabContRegisterNames.Remove()
                    break;
                }
            }
        }

    }
}
