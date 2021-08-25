using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.DBModels;
using IntelligentManufactureApplication.Models.StaticModels;
using IntelligentManufactureApplication.ViewModels;
using IntelligentManufactureApplication.Views.DialogViews;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// FilesDetail.xaml 的交互逻辑
    /// </summary>
    public partial class FilesDetail : UserControl
    {
        /// <summary>
        /// Id 对应ProductList
        /// </summary>
        private string _id { get; set; }
        public string DrawingPath { get; set; }
        public string CraftPath { get; set; }
        public string NCPath { get; set; }
        //public string currentProcPath { get; set; }//当前选择的工序存储路径
        List<ProductListDataGrid> materialItems = new List<ProductListDataGrid>();
        List<DrawingDataGrid> drawingItems = new List<DrawingDataGrid>();
        List<CraftDataGrid> craftItems = new List<CraftDataGrid>();
        List<ProcessDataGrid> processItems = new List<ProcessDataGrid>();
        List<NCDataGrid> ncProgramItems = new List<NCDataGrid>();
        public FilesDetail(ProductListDataGrid item, double heligh, double width)
        {
            InitializeComponent();

            InitData(item);
            _id = item.Id;
            DrawingPath = System.IO.Path.Combine(item.SavePath, "Drawings");
            CraftPath = System.IO.Path.Combine(item.SavePath, "Crafts");
            NCPath = System.IO.Path.Combine(item.SavePath, "NC");
            this.OutGrid.Width = width;
            this.OutGrid.Height = heligh;
            //this.ProcessingGrid.Height = heligh - 100.0 - 40.0;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="item"></param>
        public void InitData(ProductListDataGrid item)
        {
            //初始化物料表
            materialItems.Add(item);
            this.ProductListDataGrid.ItemsSource = materialItems;
            //初始化图表
            try
            {
                using(MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.Drawings
                                where obj.Id == item.Id
                                select obj).ToList();
                    if (linq != null)
                    {
                        foreach (var it in linq)
                        {
                            DrawingDataGrid drawingDataGrid = new DrawingDataGrid()
                            {
                                Id = it.Id,
                                PartID = it.PartID,

                                DrawingID = it.DrawingID,
                                DrawingName = it.DrawingName,
                                FileVersion = it.FileVersion,
                                CheckState = it.CheckState == true ? "通过" : "未通过",
                                CheckTime = it.CheckTime,
                                CheckedUser = it.CheckedUser,
                                ModifiedTimee = it.ModifiedTimee,
                                ModifiedUser = it.ModifiedUser,
                                SavePath = it.SavePath
                            };
                            drawingItems.Add(drawingDataGrid);
                        }
                    }
                }
                this.DrawingGrid.ItemsSource = drawingItems;
            }
            catch
            {

            }
            //初始化工艺卡表
            try
            {
                using(MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.Crafts
                                where obj.Id == item.Id
                                select obj).ToList();
                    if (linq != null)
                    {
                        foreach (var it in linq)
                        {
                            CraftDataGrid craftDataGrid = new CraftDataGrid()
                            {
                                Id = it.Id,
                                PartID = it.PartID,

                                CraftID = it.CraftID,
                                CraftName = it.CraftName,
                                FileVersion = it.FileVersion,
                                CheckState = it.CheckState == true ? "通过" : "未通过",
                                CheckTime = it.CheckTime,
                                CheckedUser = it.CheckedUser,
                                ModifiedTimee = it.ModifiedTimee,
                                ModifiedUser = it.ModifiedUser,
                                SavePath = it.SavePath
                            };
                            craftItems.Add(craftDataGrid);
                        }
                    }
                }
                this.CraftGrid.ItemsSource = craftItems;
            }
            catch
            {
                
            }
            //初始化工序表
            try
            {
                using (MonitoringContext monitoringContext1 = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext1.Processes
                                where obj.Id == item.Id
                                select obj).ToList();
                    if (linq != null)
                    {
                        foreach (var ll in linq)
                        {
                            ProcessDataGrid processDataGrid = new ProcessDataGrid()
                            {
                                Id = ll.Id,
                                ProcessID = ll.ProcessID,
                                ProcessNumber = ll.ProcessNumber,
                                ProcessName = ll.ProcessName,
                                Applicability = ll.Applicability,
                                Hours = ll.Hours,
                                UnitPrice = ll.UnitPrice,
                                SavePath = ll.SavePath
                            };
                            processItems.Add(processDataGrid);
                        }
                    }
                }
                this.ProcessingGrid.ItemsSource = processItems;
            }
            catch
            {

            }
        }
        /// <summary>
        /// 图纸预览按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingPreviewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Collections.IList collections = (System.Collections.IList)this.DrawingGrid.SelectedItems;
                if (collections.Count == 0) return;
                var collection = collections.Cast<DrawingDataGrid>();
                DrawingDataGrid item = collection.ToList()[0];

                DrawingPreviewDialog drawingPreviewDialog = new DrawingPreviewDialog(item);
                drawingPreviewDialog.ShowDialog();

                //更新数据
                drawingItems.Remove(item);
                DrawingDataGrid drawingDataGrid = new DrawingDataGrid();
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.Drawings
                                where obj.DrawingID == item.DrawingID
                                select obj).SingleOrDefault();
                    drawingDataGrid.Id = linq.Id;
                    drawingDataGrid.PartID = linq.PartID;

                    drawingDataGrid.DrawingID = linq.DrawingID;
                    drawingDataGrid.DrawingName = linq.DrawingName;
                    drawingDataGrid.FileVersion = linq.FileVersion;
                    drawingDataGrid.CheckState = linq.CheckState == true ? "通过" : "未通过";
                    drawingDataGrid.CheckTime = linq.CheckTime;
                    drawingDataGrid.CheckedUser = linq.CheckedUser;
                    drawingDataGrid.ModifiedTimee = linq.ModifiedTimee;
                    drawingDataGrid.ModifiedUser = linq.ModifiedUser;
                    drawingDataGrid.SavePath = linq.SavePath;
                }
                drawingItems.Add(drawingDataGrid);
                this.DrawingGrid.Items.Refresh();
            }
            catch
            {

            }
            
        }
        /// <summary>
        /// 图纸导入按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.FileName = "Drawing";
                dialog.DefaultExt = ".pdf";
                dialog.InitialDirectory = StaticPathVariable.StartUpPath;
                dialog.Filter = "Default documents (*.pdf,*.jpg)|*.pdf;*.jpg";
                dialog.Multiselect = false;
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    string filename = dialog.FileName;
                    string[] strArray = filename.Split('\\');
                    string drawName = strArray[strArray.Length - 1];
                    //上传到数据库
                    int row = 1;
                    TDrawing drawing = new TDrawing()
                    {
                        Id = _id,
                        CategoryID = string.Empty,
                        ComponentTypeID = string.Empty,
                        ComponentID = string.Empty,
                        PartTypeID = string.Empty,
                        PartID = string.Empty,

                        DrawingID = string.Format("Draw_{0}", System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_ffff")),
                        DrawingName = drawName.Split('.')[0],
                        FileVersion = string.Empty,
                        CheckState = false,
                        CheckTime = string.Empty,
                        CheckedUser = string.Empty,
                        ModifiedTimee = System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm"),
                        ModifiedUser = StaticUserInfo.UserName,
                        SavePath = System.IO.Path.Combine(DrawingPath, drawName)
                    };
                    using (MonitoringContext monitoringContext = new MonitoringContext())
                    {
                        monitoringContext.Drawings.Add(drawing);
                        row = monitoringContext.SaveChanges();
                    }
                    if (row > 0)
                    {
                        //刷新表格
                        DrawingDataGrid drawingDataGrid = new DrawingDataGrid()
                        {
                            Id = drawing.Id,
                            PartID = drawing.PartID,

                            DrawingID = drawing.DrawingID,
                            DrawingName = drawing.DrawingName,
                            FileVersion = drawing.FileVersion,
                            CheckState = drawing.CheckState == false ? "未通过" : "通过",
                            CheckTime = drawing.CheckTime,
                            CheckedUser = drawing.CheckedUser,
                            ModifiedTimee = drawing.ModifiedTimee,
                            ModifiedUser = drawing.ModifiedUser,
                            SavePath = drawing.SavePath
                        };
                        drawingItems.Add(drawingDataGrid);
                        this.DrawingGrid.ItemsSource = drawingItems;
                        this.DrawingGrid.Items.Refresh();

                        //新建文件夹，复杂文件.
                        bool flag = CopyFile(filename, DrawingPath, drawing.SavePath);
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourseFiles">源文件夹</param>
        /// <param name="destFolder">目标文件夹</param>
        /// <param name="destFile">目标文件</param>
        /// <returns></returns>
        public bool CopyFile(string sourseFiles, string destFolder, string destFile)
        {
            try
            {
                if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                System.IO.File.Copy(sourseFiles, destFile, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 图纸删除按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingDeletedButton_Click(object sender, RoutedEventArgs e)
        {
            
            System.Collections.IList collections = (System.Collections.IList)this.DrawingGrid.SelectedItems;
            var collection = collections.Cast<DrawingDataGrid>();
            List<DrawingDataGrid> items = collection.ToList();
            foreach (var item in items)
            {
                //删除数据库
                int row = 0;
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.Drawings
                                where obj.DrawingID == item.DrawingID
                                select obj).SingleOrDefault();
                    monitoringContext.Drawings.Remove(linq);
                    row = monitoringContext.SaveChanges();
                }
                //删除目录下文件
                if (row > 0 && System.IO.File.Exists(item.SavePath))
                {
                    System.IO.File.Delete(item.SavePath);
                }
                //刷新界面
                drawingItems.Remove(item);
                this.DrawingGrid.Items.Refresh();
            }
        }
        /// <summary>
        /// 工艺卡导入按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CraftImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.FileName = "Craft";
                dialog.DefaultExt = ".pdf";
                dialog.InitialDirectory = StaticPathVariable.StartUpPath;
                dialog.Filter = "Default documents (*.pdf,*.jpg)|*.pdf;*.jpg";
                dialog.Multiselect = false;
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    string filename = dialog.FileName;
                    string[] strArray = filename.Split('\\');
                    string craftName = strArray[strArray.Length - 1];
                    //上传到数据库
                    int row = 1;
                    TCraft craft = new TCraft()
                    {
                        Id = _id,
                        CategoryID = string.Empty,
                        ComponentTypeID = string.Empty,
                        ComponentID = string.Empty,
                        PartTypeID = string.Empty,
                        PartID = string.Empty,

                        CraftID = string.Format("Craft_{0}", System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_ffff")),
                        CraftName = craftName.Split('.')[0],
                        FileVersion = string.Empty,
                        CheckState = false,
                        CheckTime = string.Empty,
                        CheckedUser = string.Empty,
                        ModifiedTimee = System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm"),
                        ModifiedUser = StaticUserInfo.UserName,
                        SavePath = System.IO.Path.Combine(CraftPath, craftName)
                    };
                    using (MonitoringContext monitoringContext = new MonitoringContext())
                    {
                        monitoringContext.Crafts.Add(craft);
                        row = monitoringContext.SaveChanges();
                    }
                    if (row > 0)
                    {
                        //刷新表格
                        CraftDataGrid craftDataGrid = new CraftDataGrid()
                        {
                            Id = craft.Id,
                            PartID = craft.PartID,

                            CraftID = craft.CraftID,
                            CraftName = craft.CraftName,
                            FileVersion = craft.FileVersion,
                            CheckState = craft.CheckState == false ? "未通过" : "通过",
                            CheckTime = craft.CheckTime,
                            CheckedUser = craft.CheckedUser,
                            ModifiedTimee = craft.ModifiedTimee,
                            ModifiedUser = craft.ModifiedUser,
                            SavePath = craft.SavePath
                        };
                        craftItems.Add(craftDataGrid);
                        this.CraftGrid.ItemsSource = craftItems;
                        this.CraftGrid.Items.Refresh();

                        //新建文件夹，复杂文件.
                        bool flag = CopyFile(filename, CraftPath, craft.SavePath);
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 工艺卡删除按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CraftDeletedButton_Click(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collections = (System.Collections.IList)this.CraftGrid.SelectedItems;
            var collection = collections.Cast<CraftDataGrid>();
            List<CraftDataGrid> items = collection.ToList();
            foreach (var item in items)
            {
                //删除数据库
                int row = 0;
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.Crafts
                                where obj.CraftID == item.CraftID
                                select obj).SingleOrDefault();
                    monitoringContext.Crafts.Remove(linq);
                    row = monitoringContext.SaveChanges();
                }
                //删除目录下文件
                if (row > 0 && System.IO.File.Exists(item.SavePath))
                {
                    System.IO.File.Delete(item.SavePath);
                }
                //刷新界面
                craftItems.Remove(item);
                this.CraftGrid.Items.Refresh();
            }
        }
        /// <summary>
        /// 工艺卡预览按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CraftPreviewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Collections.IList collections = (System.Collections.IList)this.CraftGrid.SelectedItems;
                if (collections.Count == 0) return;
                var collection = collections.Cast<CraftDataGrid>();
                CraftDataGrid item = collection.ToList()[0];

                CraftPreviewDialog drawingPreviewDialog = new CraftPreviewDialog(item);
                drawingPreviewDialog.ShowDialog();

                //更新
                craftItems.Remove(item);
                CraftDataGrid craftDataGrid = new CraftDataGrid();
                using(MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.Crafts
                                where obj.CraftID == item.CraftID
                                select obj).SingleOrDefault();
                    craftDataGrid.Id = linq.Id;
                    craftDataGrid.PartID = linq.PartID;

                    craftDataGrid.CraftID = linq.CraftID;
                    craftDataGrid.CraftName = linq.CraftName;
                    craftDataGrid.FileVersion = linq.FileVersion;
                    craftDataGrid.CheckState = linq.CheckState == true ? "通过" : "未通过";
                    craftDataGrid.CheckTime = linq.CheckTime;
                    craftDataGrid.CheckedUser = linq.CheckedUser;
                    craftDataGrid.ModifiedTimee = linq.ModifiedTimee;
                    craftDataGrid.ModifiedUser = linq.ModifiedUser;
                    craftDataGrid.SavePath = linq.SavePath;
                }
                craftItems.Add(craftDataGrid);
                this.CraftGrid.Items.Refresh();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 工序删除按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletedProcessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Collections.IList collections = (System.Collections.IList)this.ProcessingGrid.SelectedItems;
                var collection = collections.Cast<ProcessDataGrid>();
                List<ProcessDataGrid> items = collection.ToList();
                foreach (var item in items)
                {
                    //删除数据库数据
                    using (MonitoringContext monitoringContext = new MonitoringContext())
                    {
                        //删除NC程序
                        var linq = (from obj in monitoringContext.NCPrograms
                                    where obj.ProcessID == item.ProcessID
                                    select obj).ToList();
                        foreach (var ll in linq)
                        {
                            monitoringContext.Remove(ll);
                        }
                        monitoringContext.SaveChanges();
                    }
                    ncProgramItems.Clear();
                    //删除工序
                    using (MonitoringContext monitoringContext1 = new MonitoringContext())
                    {
                        var linq = (from obj in monitoringContext1.Processes
                                    where obj.ProcessID == item.ProcessID
                                    select obj).SingleOrDefault();
                        if (linq != null)
                        {
                            monitoringContext1.Processes.Remove(linq);
                            monitoringContext1.SaveChanges();
                        }
                    }
                    processItems.Remove(item);
                    this.ProcessingGrid.Items.Refresh();
                    //删除文件夹
                    if (System.IO.Directory.Exists(item.SavePath))
                    {
                        System.IO.Directory.Delete(item.SavePath, true);
                    }

                    this.DNCDataGrid.ItemsSource = null;
                    this.DNCDataGrid.Items.Refresh();
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 工序添加按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProcessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.IO.Directory.Exists(NCPath))
                {
                    System.IO.Directory.CreateDirectory(NCPath);
                }
                //写入数据库
                ProcessNewRow processNewRow = new ProcessNewRow(NCPath, _id);
                processNewRow.ShowDialog();
                //刷新显示
                ProcessDataGrid processDataGrid = processNewRow.GetItem();
                if (processDataGrid.ProcessID == null || processDataGrid.ProcessID == string.Empty) return;

                processItems.Add(processDataGrid);
                this.ProcessingGrid.Items.Refresh();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 工序列表点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessingGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //清除表格.
                ncProgramItems.Clear();

                System.Collections.IList collections = (System.Collections.IList)this.ProcessingGrid.SelectedItems;
                var collection = collections.Cast<ProcessDataGrid>();
                ProcessDataGrid item = collection.ToList()[0];

                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.NCPrograms
                                where obj.ProcessID == item.ProcessID
                                select obj).ToList();
                    foreach (var ll in linq)
                    {
                        NCDataGrid nCDataGrid = new NCDataGrid()
                        {
                            Id = ll.Id,
                            ProcessID = ll.ProcessID,
                            PartID = ll.PartID,

                            NCProgramID = ll.NCProgramID,
                            NCProgramName = ll.NCProgramName,
                            FileVersion = ll.FileVersion,
                            CheckState = ll.CheckState == true ? "通过" : "未通过",
                            CheckTime = ll.CheckTime,
                            CheckedUser = ll.CheckedUser,
                            ModifiedTimee = ll.ModifiedTimee,
                            ModifiedUser = ll.ModifiedUser,
                            SavePath = ll.SavePath
                        };
                        ncProgramItems.Add(nCDataGrid);
                    }
                }
                this.DNCDataGrid.ItemsSource = ncProgramItems;
                this.DNCDataGrid.Items.Refresh();
            }
            catch
            {

            }
        }
        /// <summary>
        /// NC程序删除按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletedNCButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Collections.IList collections = (System.Collections.IList)this.DNCDataGrid.SelectedItems;
                var collection = collections.Cast<NCDataGrid>();
                NCDataGrid item = collection.ToList()[0];

                //删除数据库文件
                int row = 0;
                string filepath = string.Empty;
                using(MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.NCPrograms
                                where obj.NCProgramID == item.NCProgramID
                                select obj).SingleOrDefault();
                    monitoringContext.NCPrograms.Remove(linq);
                    row = monitoringContext.SaveChanges();
                    filepath = linq.SavePath;
                    foreach (var ncProgramItem in ncProgramItems)
                    {
                        if (ncProgramItem.NCProgramID == linq.NCProgramID)
                        {
                            ncProgramItems.Remove(ncProgramItem);
                            break;
                        }
                    }
                    //ncProgramItems.Remove(linq);
                    this.DNCDataGrid.Items.Refresh();
                }
                //删除目录下文件
                if(row > 0)
                {
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// NC程序添加按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNCButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (currentProcPath == null) return;
                System.Collections.IList collections = (System.Collections.IList)this.ProcessingGrid.SelectedItems;
                var collection = collections.Cast<ProcessDataGrid>();
                ProcessDataGrid item = collection.ToList()[0];
                if (item == null) return;

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.FileName = "DNC";
                dialog.DefaultExt = ".NC";
                dialog.InitialDirectory = @"E:\Personal Files\测试文件\客服";
                dialog.Filter = "Text documents All Files|*.NC*";
                dialog.Multiselect = false;
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    string filename = dialog.FileName;
                    string[] strArray = filename.Split('\\');
                    string ncName = strArray[strArray.Length - 1];
                    //上传到数据库
                    int row = 1;
                    TNCProgram nCProgram = new TNCProgram()
                    {
                        Id = _id,
                        ProcessID = item.ProcessID,
                        CategoryID = string.Empty,
                        ComponentTypeID = string.Empty,
                        ComponentID = string.Empty,
                        PartTypeID = string.Empty,
                        PartID = string.Empty,

                        NCProgramID = string.Format("NC_{0}", System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_ffff")),
                        NCProgramName = ncName.Split('.')[0],
                        FileVersion = string.Empty,
                        CheckState = false,
                        CheckTime = string.Empty,
                        CheckedUser = string.Empty,
                        ModifiedTimee = System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm"),
                        ModifiedUser = StaticUserInfo.UserName,
                        SavePath = System.IO.Path.Combine(item.SavePath, ncName)
                    };
                    using (MonitoringContext monitoringContext = new MonitoringContext())
                    {
                        monitoringContext.NCPrograms.Add(nCProgram);
                        row = monitoringContext.SaveChanges();
                    }
                    if (row > 0)
                    {
                        NCDataGrid nCDataGrid = new NCDataGrid()
                        {
                            Id = nCProgram.Id,
                            PartID = nCProgram.PartID,
                            ProcessID = nCProgram.ProcessID,

                            NCProgramID = nCProgram.NCProgramID,
                            NCProgramName = nCProgram.NCProgramName,
                            FileVersion = nCProgram.FileVersion,
                            CheckState = nCProgram.CheckState == false ? "未通过" : "通过",
                            CheckTime = nCProgram.CheckTime,
                            CheckedUser = nCProgram.CheckedUser,
                            ModifiedTimee = nCProgram.ModifiedTimee,
                            ModifiedUser = nCProgram.ModifiedUser,
                            SavePath = nCProgram.SavePath
                        };
                        ncProgramItems.Add(nCDataGrid);
                        this.DNCDataGrid.ItemsSource = ncProgramItems;
                        this.DNCDataGrid.Items.Refresh();

                        //新建文件夹，复制文件.
                        bool flag = CopyFile(filename, item.SavePath, nCProgram.SavePath);
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// NC程序预览按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewNCButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Collections.IList collections = (System.Collections.IList)this.DNCDataGrid.SelectedItems;
                if (collections.Count == 0) return;
                var collection = collections.Cast<NCDataGrid>();
                NCDataGrid item = collection.ToList()[0];

                NCPreviewDialog nCPreviewDialog = new NCPreviewDialog(item);
                nCPreviewDialog.ShowDialog();

                //更新数据
                ncProgramItems.Remove(item);
                NCDataGrid nCDataGrid = new NCDataGrid();
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.NCPrograms
                                where obj.NCProgramID == item.NCProgramID
                                select obj).SingleOrDefault();
                    nCDataGrid.Id = linq.Id;
                    nCDataGrid.ProcessID = linq.ProcessID;
                    nCDataGrid.PartID = linq.PartID;

                    nCDataGrid.NCProgramID = linq.NCProgramID;
                    nCDataGrid.NCProgramName = linq.NCProgramName;
                    nCDataGrid.FileVersion = linq.FileVersion;
                    nCDataGrid.CheckState = linq.CheckState == true ? "通过" : "未通过";
                    nCDataGrid.CheckTime = linq.CheckTime;
                    nCDataGrid.CheckedUser = linq.CheckedUser;
                    nCDataGrid.ModifiedTimee = linq.ModifiedTimee;
                    nCDataGrid.ModifiedUser = linq.ModifiedUser;
                    nCDataGrid.SavePath = linq.SavePath;
                }
                ncProgramItems.Add(nCDataGrid);

                this.DNCDataGrid.Items.Refresh();
            }
            catch
            {

            }
            
        }
    }
}
