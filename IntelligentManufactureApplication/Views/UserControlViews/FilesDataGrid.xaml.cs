using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.DBModels;
using IntelligentManufactureApplication.Services;
using IntelligentManufactureApplication.ViewModels;
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
    /// FilesDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class FilesDataGrid : UserControl
    {
        ProductListService productListService = new ProductListService();
        private string _treeViewName { get; set; }
        TabControl _tabControl { get; set; }
        private double _heligh { get; set; }
        private double _width { get; set; }
        public FilesDataGrid(string name, double heligh, double width, TabControl tabControl)
        {
            InitializeComponent();
            _tabControl = tabControl;
            _treeViewName = name;
            _heligh = heligh - 25.0;
            _width = width;
            this.ProductDataGrid.Height = _heligh - 30.0;
            this.ProductDataGrid.Width = _width;
            

            /*ProductListService productListService = new ProductListService(name);*/
            productListService.InitData(name);
            this.DataContext = productListService;
        }
        /*private void NewItemDialog_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, true) && !string.IsNullOrWhiteSpace(this.NewMaterialNumbTextBox.Text))
            {
                try
                {
                    //TreeViewItem treeViewItem = this.LeftTreeView.SelectedItem as TreeViewItem;
                    if (_treeViewName == string.Empty) return;
                    string name = _treeViewName;
                    if (name.Split('_')[0] != "Part")
                    {
                        return;
                    }
                    //写入数据库
                    TProductList productList = new TProductList()
                    {
                        PartID = name,
                        Id = System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"),
                        MaterialsNumber = this.NewMaterialNumbTextBox.Text,
                        DrawingID = string.Empty,
                        ProductName = string.Empty,
                        Makings = string.Empty,
                        MakingsNumber = string.Empty,
                        Standard = string.Empty,
                        HeatTreatmentCode = 0,
                        MaterialStrength = string.Empty,
                        CheckGroup = string.Empty,
                        HeattreatmentStrength = string.Empty,
                        Source = string.Empty,
                        Type = string.Empty,
                        Specification = string.Empty,
                        BlankSize = string.Empty,
                        CuttingSize = string.Empty,
                        StelliteAndNitriding = string.Empty,
                        Remarks = string.Empty
                    };
                    int row = 0;
                    using (MonitoringContext monitoringContext = new MonitoringContext())
                    {
                        monitoringContext.ProductLists.Add(productList);
                        row = monitoringContext.SaveChanges();
                    }

                    //添加数据到界面
                    ProductListDataGrid productListDataGrid = new ProductListDataGrid()
                    {
                        IsSelected = false,
                        Id = productList.Id,
                        MaterialsNumber = productList.MaterialsNumber,
                        DrawingID = productList.DrawingID,
                        ProductName = productList.ProductName,
                        Makings = productList.Makings,
                        MakingsNumber = productList.MakingsNumber,
                        Standard = productList.Standard,
                        HeatTreatmentCode = productList.HeatTreatmentCode.ToString(),
                        MaterialStrength = productList.MaterialStrength,
                        CheckGroup = productList.CheckGroup,
                        HeattreatmentStrength = productList.HeattreatmentStrength,
                        Source = productList.Source,
                        Type = productList.Type,
                        Specification = productList.Specification,
                        BlankSize = productList.BlankSize,
                        CuttingSize = productList.CuttingSize,
                        StelliteAndNitriding = productList.StelliteAndNitriding,
                        Remarks = productList.Remarks
                    };
                    bool flag = productListService.AddItem(productListDataGrid);

                    if (row > 0 && flag == true)
                    {
                        MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                        MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                        MessageBoxResult messageBoxResult = MessageBox.Show("新建成功！", null, messageBoxButton, messageBoxImage);
                    }
                }
                catch
                {

                }

            }
        }*/
        /// <summary>
        /// 打开（右键）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Collections.IList collections = (System.Collections.IList)this.ProductDataGrid.SelectedItems;
                var collection = collections.Cast<ProductListDataGrid>();
                List<ProductListDataGrid> items = collection.ToList();

                foreach (var item in items)
                {
                    TabItem tabItem = new TabItem();
                    tabItem.Header = item.ProductName;
                    tabItem.IsSelected = true;
                    FilesDetail filesDetail = new FilesDetail(item, _heligh, _width);
                    tabItem.Content = filesDetail;
                    _tabControl.Items.Add(tabItem);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 添加（右键）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_treeViewName == string.Empty) return;
                string name = _treeViewName;
                if (name.Split('_')[0] != "P")
                {
                    return;
                }
                NewRowDialog newRowDialog = new NewRowDialog(name, productListService);
                newRowDialog.ShowDialog();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 删除（右键）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_treeViewName == string.Empty) return;

                System.Collections.IList collections = (System.Collections.IList)this.ProductDataGrid.SelectedItems;
                var collection = collections.Cast<ProductListDataGrid>();
                List<ProductListDataGrid> items = collection.ToList();

                foreach (var item in items)
                {
                    //通过每行对应的ID号进行删除
                    int row = 0;
                    using (MonitoringContext monitoringContext = new MonitoringContext())
                    {
                        //删除工艺卡
                        var crafts = (from obj in monitoringContext.Crafts
                                      where obj.Id == item.Id
                                      select obj).ToList();
                        foreach (var craft in crafts)
                        {
                            monitoringContext.Crafts.Remove(craft);
                        }
                        //删除图纸
                        var drawings = (from obj in monitoringContext.Drawings
                                        where obj.Id == item.Id
                                        select obj).ToList();
                        foreach (var drawing in drawings)
                        {
                            monitoringContext.Drawings.Remove(drawing);
                        }
                        //删除工序
                        var processes = (from obj in monitoringContext.Processes
                                         where obj.Id == item.Id
                                         select obj).ToList();
                        foreach (var process in processes)
                        {
                            monitoringContext.Processes.Remove(process);
                        }
                        //删除NC程序
                        var ncP = (from obj in monitoringContext.NCPrograms
                                   where obj.Id == item.Id
                                   select obj).ToList();
                        foreach (var nc in ncP)
                        {
                            monitoringContext.NCPrograms.Remove(nc);
                        }
                        //删除product list.
                        var progs = (from obj in monitoringContext.ProductLists
                                     where obj.Id == item.Id
                                     select obj).ToList();
                        foreach (var pro in progs)
                        {
                            monitoringContext.ProductLists.Remove(pro);
                        }
                        row = monitoringContext.SaveChanges();
                    }
                    if (row >= 1)
                    {
                        if (System.IO.Directory.Exists(item.SavePath))
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(item.SavePath);
                            directoryInfo.Delete(true);
                        }
                        productListService.DeletedItem(item.Id);
                    }
                    
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 删除按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_treeViewName == string.Empty) return;

                System.Collections.IList collections = (System.Collections.IList)this.ProductDataGrid.SelectedItems;
                var collection = collections.Cast<ProductListDataGrid>();
                List<ProductListDataGrid> items = collection.ToList();

                foreach (var item in items)
                {
                    //通过每行对应的ID号进行删除
                    int row = 0;
                    using (MonitoringContext monitoringContext = new MonitoringContext())
                    {
                        //删除工艺卡
                        var crafts = (from obj in monitoringContext.Crafts
                                      where obj.Id == item.Id
                                      select obj).ToList();
                        foreach (var craft in crafts)
                        {
                            monitoringContext.Crafts.Remove(craft);
                        }
                        //删除图纸
                        var drawings = (from obj in monitoringContext.Drawings
                                        where obj.Id == item.Id
                                        select obj).ToList();
                        foreach (var drawing in drawings)
                        {
                            monitoringContext.Drawings.Remove(drawing);
                        }
                        //删除工序
                        var processes = (from obj in monitoringContext.Processes
                                         where obj.Id == item.Id
                                         select obj).ToList();
                        foreach (var process in processes)
                        {
                            monitoringContext.Processes.Remove(process);
                        }
                        //删除NC程序
                        var ncP = (from obj in monitoringContext.NCPrograms
                                   where obj.Id == item.Id
                                   select obj).ToList();
                        foreach (var nc in ncP)
                        {
                            monitoringContext.NCPrograms.Remove(nc);
                        }
                        //删除productList.
                        var progs = (from obj in monitoringContext.ProductLists
                                     where obj.Id == item.Id
                                     select obj).ToList();
                        foreach (var pro in progs)
                        {
                            monitoringContext.ProductLists.Remove(pro);
                        }
                        row = monitoringContext.SaveChanges();
                    }
                    if (row >= 1)
                    {
                        if (System.IO.Directory.Exists(item.SavePath))
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(item.SavePath);
                            directoryInfo.Delete(true);
                        }

                        productListService.DeletedItem(item.Id);
                    }

                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 添加按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_treeViewName == string.Empty) return;
                string name = _treeViewName;
                if (name.Split('_')[0] != "P")
                {
                    return;
                }
                NewRowDialog newRowDialog = new NewRowDialog(name, productListService);
                newRowDialog.ShowDialog();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 导入按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportedButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
