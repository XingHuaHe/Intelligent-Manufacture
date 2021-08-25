using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.DBModels;
using IntelligentManufactureApplication.Models.SocketModels;
using IntelligentManufactureApplication.Models.StaticModels;
using IntelligentManufactureApplication.Services;
using IntelligentManufactureApplication.ViewModels;
using NotificationDemoWPF;
using PC_XRF_TecsondeSync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// DNCManagment.xaml 的交互逻辑
    /// </summary>
    public partial class DNCManagment : UserControl
    {
        private double _heligh { get; set; }
        private double _width { get; set; }
        public string MasterIP { get; set; }
        List<string> RegisterNames = new List<string>();
        List<ProcessDataGrid> processItems = new List<ProcessDataGrid>();
        List<CraftDataGrid> craftItems = new List<CraftDataGrid>();
        List<NCDataGrid> nCProgramItems = new List<NCDataGrid>();
        //以下2个变量拥有标记工艺卡和NC程序是否通过审核，False表示未通过
        private bool craftFlag = false;
        private bool ncFlag = false;
        public DNCManagment(double height, double width)
        {
            InitializeComponent();

            if (height == 0 && width == 0)
            {
                this.LeftTreeView.Height = 570.0;
                _heligh = 570.0 - 4.0;
                _width = 1050 - 4.0;
            }
            else
            {
                this.LeftTreeView.Height = height;
                _heligh = height - 4.0;
                _width = width - 4.0;
                RightGrid.Height = height;
            }

            InitTreeView();
            MasterIP = "192.168.1.200";
        }
        /// <summary>
        /// 界面初始化
        /// </summary>
        private void InitTreeView()
        {
            try
            {
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
        /// (TreeView)点击触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string name = ((TreeViewItem)e.NewValue).Name.ToString();
            try
            {
                switch (name.Split('_')[0])
                {
                    case "P":
                        ProductListService productListService = new ProductListService(name);
                        this.ProductDataGrid.DataContext = productListService;

                        //清空
                        processItems.Clear();
                        this.ProcessingGrid.Items.Refresh();
                        craftItems.Clear();
                        this.CraftGrid.Items.Refresh();
                        nCProgramItems.Clear();
                        this.NCGrid.Items.Refresh();

                        this.CraftFileTextBox.Text = string.Empty;
                        this.NCFileTextBox.Text = string.Empty;

                        this.CrafFileAlert.Visibility = System.Windows.Visibility.Hidden;
                        this.CrafFileAlertTextBlock.Visibility = System.Windows.Visibility.Hidden; 
                        this.NCFileAlert.Visibility = System.Windows.Visibility.Hidden;
                        this.NCFileAlertTextBlock.Visibility = System.Windows.Visibility.Hidden;
                        this.UploadButton.IsEnabled = false;
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// (TreeView)展开按键
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
        /// <summary>
        /// 物料列表点击触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //清空
                processItems.Clear();
                craftItems.Clear();
                this.CraftGrid.Items.Refresh();
                nCProgramItems.Clear();
                this.NCGrid.Items.Refresh();

                this.CraftFileTextBox.Text = string.Empty;
                this.NCFileTextBox.Text = string.Empty;

                this.CrafFileAlert.Visibility = System.Windows.Visibility.Hidden;
                this.CrafFileAlertTextBlock.Visibility = System.Windows.Visibility.Hidden;
                this.NCFileAlert.Visibility = System.Windows.Visibility.Hidden;
                this.NCFileAlertTextBlock.Visibility = System.Windows.Visibility.Hidden;
                this.UploadButton.IsEnabled = false;

                System.Collections.IList collections = (System.Collections.IList)this.ProductDataGrid.SelectedItems;
                if (collections.Count == 0) return;
                var collection = collections.Cast<ProductListDataGrid>();
                ProductListDataGrid item = collection.ToList()[0];
                using (MonitoringContext monitoringContext1 = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext1.Processes
                                where obj.Id == item.Id
                                select obj).ToList();
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
                if (processItems.Count == 0)
                {
                    processItems.Clear();
                    //this.ProcessingGrid.ItemsSource = processItems;
                    this.ProcessingGrid.Items.Refresh();
                }
                else
                {
                    this.ProcessingGrid.ItemsSource = processItems;
                    this.ProcessingGrid.Items.Refresh();
                }
                craftItems.Clear();
                nCProgramItems.Clear();
                this.CraftGrid.Items.Refresh();
                this.NCGrid.Items.Refresh();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 工序列表点击触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessingGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //清除
                craftItems.Clear();
                nCProgramItems.Clear();

                this.CraftFileTextBox.Text = string.Empty;
                this.NCFileTextBox.Text = string.Empty;

                this.CrafFileAlert.Visibility = System.Windows.Visibility.Hidden;
                this.CrafFileAlertTextBlock.Visibility = System.Windows.Visibility.Hidden;
                this.NCFileAlert.Visibility = System.Windows.Visibility.Hidden;
                this.NCFileAlertTextBlock.Visibility = System.Windows.Visibility.Hidden;
                this.UploadButton.IsEnabled = false;

                System.Collections.IList collections = (System.Collections.IList)this.ProcessingGrid.SelectedItems;
                var collection = collections.Cast<ProcessDataGrid>();
                ProcessDataGrid item = collection.ToList()[0];

                using(MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var crafts = (from obj in monitoringContext.Crafts
                                  where obj.Id == item.Id
                                  select obj).ToList();
                    foreach (var craft in crafts)
                    {
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
                    }
                }
                this.CraftGrid.ItemsSource = craftItems;
                this.CraftGrid.Items.Refresh();

                using (MonitoringContext monitoringContext1 = new MonitoringContext())
                {
                    var ncs = (from obj in monitoringContext1.NCPrograms
                               where obj.ProcessID == item.ProcessID
                               select obj).ToList();
                    foreach (var nc in ncs)
                    {
                        NCDataGrid nCDataGrid = new NCDataGrid()
                        {
                            Id = nc.Id,
                            ProcessID = nc.ProcessID,
                            PartID = nc.PartID,

                            NCProgramID = nc.NCProgramID,
                            NCProgramName = nc.NCProgramName,
                            FileVersion = nc.FileVersion,
                            CheckState = nc.CheckState == true ? "通过" : "未通过",
                            CheckTime = nc.CheckTime,
                            CheckedUser = nc.CheckedUser,
                            ModifiedTimee = nc.ModifiedTimee,
                            ModifiedUser = nc.ModifiedUser,
                            SavePath = nc.SavePath
                        };
                        nCProgramItems.Add(nCDataGrid);
                    }
                }
                this.NCGrid.ItemsSource = nCProgramItems;
                this.NCGrid.Items.Refresh();
            }
            catch
            {

            }
        }
        private void NCGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                System.Collections.IList collections = (System.Collections.IList)this.NCGrid.SelectedItems;
                var collection = collections.Cast<NCDataGrid>();
                NCDataGrid item = collection.ToList()[0];

                this.NCFileTextBox.Text = item.NCProgramName;
                if(item.CheckState == "未通过")
                {
                    this.NCFileAlert.Visibility = System.Windows.Visibility.Visible;
                    this.NCFileAlertTextBlock.Visibility = System.Windows.Visibility.Visible;
                    craftFlag = false;
                    this.UploadButton.IsEnabled = false;
                }
                else
                {
                    this.NCFileAlert.Visibility = System.Windows.Visibility.Hidden;
                    this.NCFileAlertTextBlock.Visibility = System.Windows.Visibility.Hidden;

                    craftFlag = true;
                    if (craftFlag == true && ncFlag == true)
                    {
                        this.UploadButton.IsEnabled = true;
                    }
                    else
                    {
                        this.UploadButton.IsEnabled = false;
                    }
                }
            }
            catch
            {

            }
        }
        private void CraftGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                System.Collections.IList collections = (System.Collections.IList)this.CraftGrid.SelectedItems;
                var collection = collections.Cast<CraftDataGrid>();
                CraftDataGrid item = collection.ToList()[0];

                this.CraftFileTextBox.Text = item.CraftName;
                if (item.CheckState == "未通过")
                {
                    this.CrafFileAlert.Visibility = System.Windows.Visibility.Visible;
                    this.CrafFileAlertTextBlock.Visibility = System.Windows.Visibility.Visible;
                    ncFlag = false;
                    this.UploadButton.IsEnabled = false;
                }
                else
                {
                    this.CrafFileAlert.Visibility = System.Windows.Visibility.Hidden;
                    this.CrafFileAlertTextBlock.Visibility = System.Windows.Visibility.Hidden;

                    ncFlag = true;
                    if(ncFlag == true && craftFlag == true)
                    {
                        this.UploadButton.IsEnabled = true;
                    }
                    else
                    {
                        this.UploadButton.IsEnabled = false;
                    }
                }
            }
            catch
            {

            }
        }
        private void EditIpAdress_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ip_address.IsReadOnly = false;
            }
            catch
            {

            }
        }
        private void EditIpAdressOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ip_address.IsReadOnly = true;
            }
            catch
            {

            }
        }








        NCDataGrid _item { get; set; }
        public static List<NotificationWindow> _dialogs = new List<NotificationWindow>();
        Socket socketclient = null;
        Thread connectThread = null;
        bool isConnecting = false;
        public static MainWindow mainWindow = null;
        bool convertOK = true;
        int[] dataArr = new int[2048];//数据的数组
        string datFile = "";//打开文件的路径
        public static string workpath = "C:\\Tecsonde\\";
        //创建 1个客户端套接字 和1个负责监听服务端请求的线程  
        Thread threadclient = null;
        Thread timeThread = null;
        string ipAddress = null;
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton messageBoxButton = MessageBoxButton.OKCancel;
            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
            MessageBoxResult messageBoxResult = MessageBox.Show("是否要进行文件传输？", null, messageBoxButton, messageBoxImage);
            if (messageBoxResult == MessageBoxResult.Cancel) return;

            System.Collections.IList collections = (System.Collections.IList)this.NCGrid.SelectedItems;
            var collection = collections.Cast<NCDataGrid>();
            NCDataGrid item = collection.ToList()[0];
            _item = item;

            if (isConnecting)
            {
                //MessageBox.Show("正在连接...请稍候!");
                ShowNotify("提示:", "正在连接...请稍候!");
                return;
            }
            //连接仪器
            ipAddress = ip_address.Text.Trim();
            if (ipAddress.Equals(""))
            {
                //MessageBox.Show("请输入IP地址");
                ShowNotify("提示:", "请输入IP地址");
                return;
            }
            if (socketclient == null || !socketclient.Connected)
            {
                isConnecting = true;
                connectThread = new Thread(Connect);
                connectThread.IsBackground = true;
                connectThread.Start();
                //connect(ipAddress, port);
            }
            else
            {
                string filename = string.Empty;
                List<TNCProgram> nCPrograms = new List<TNCProgram>();
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.NCPrograms
                                where obj.NCProgramID == item.NCProgramID
                                select obj).ToList();
                    nCPrograms = linq;
                    //filename = linq[0].SavePath;
                }
                if (isSending)
                {
                    MessageBox.Show("正在发送,请稍候");
                    return;
                }
                Thread fileThread = new Thread(sendMultiFileThread);
                fileThread.IsBackground = true;
                fileThread.Start(nCPrograms);
                //string filename = @"E:\Personal Files\IntelligentManufactureSolution\files\Category_2020_08_05_07_20_46\ComponentType_2020_08_05_07_20_48\Component_2020_08_05_07_20_50\PartType_2020_08_05_07_20_53\Part_2020_08_05_07_20_55\2020_08_05_07_21_10\NC\Proc_05_08_10_23_8885\test_3.txt";
                /*if (filename != null && File.Exists(filename))
                {
                    //文件存在
                    long sendLength = 452;
                    FileInfo fileInfo = new FileInfo(filename.ToString());
                    long length = fileInfo.Length;
                    int count = (int)(length / sendLength);
                    Thread fileThread = new Thread(sendFileThread);
                    fileThread.IsBackground = true;
                    fileThread.Start(filename);
                }
                else
                {
                    MessageBox.Show("请选择文件!");
                }*/
            }

        }

        private void Btn_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            //断开仪器
            if (isConnecting)
            {
                //MessageBox.Show("正在连接...请稍候!");
                ShowNotify("提示:", "正在连接...请稍候");
                return;
            }
            StopConnetct();
            ShowNotify("提示:", "已断开连接");
        }

        private void Accomplish()
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    MessageBox.Show("上传完成");
                                });

        }

        private void sendFileThread(Object filename)
        {
            //以获取其大小
            FileInfo fileInfo = new FileInfo(filename.ToString());
            string fileName = fileInfo.Name;
            if (fileName != null)
            {
                Debug.WriteLine("fileName:" + fileName);
                string data = "WJMC=" + fileName + ";";
                FileUtils.WriteFile(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"debug.txt"), "发送文件名:" + data + "\r\n");
                byte[] datafield = System.Text.Encoding.UTF8.GetBytes(data);
                byte[] head_datafield = new byte[6 + datafield.Length + 2]; //总长度 = 头6 + 数据体 +2
                head_datafield[0] = (byte)0xF5;
                head_datafield[1] = (byte)0xA5;
                head_datafield[2] = (byte)0xA7;
                head_datafield[3] = (byte)0x01;//发送文件名称
                for (int i = 0; i < datafield.Length; i++)
                {
                    head_datafield[i + 6] = datafield[i];
                }
                CheckUtils.CalcularLength(head_datafield);
                CheckUtils.CheckSum(head_datafield, false);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < head_datafield.Length; i++)
                {
                    sb.Append(head_datafield[i].ToString("X2") + ",");
                }
                ClientSendMsg(sb.ToString());//开始发送
            }
            //socketclient.Send(a);
            //Thread.Sleep(100);
            long sendLength = 452;
            long length = fileInfo.Length;
            int count = (int)(length / sendLength);
            Debug.WriteLine("length:" + length);
            Debug.WriteLine("count:" + count);
            FileUtils.WriteFile(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "debug.txt"), "length:" + length + "\r\n");
            FileUtils.WriteFile(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "debug.txt"), "count:" + count + "\r\n");
            for (int index = 0; index <= count; index++)
            {
                //byte[] filecontent = FileUtils.ReadFileSeek(filename.ToString(), index, sendLength);
                byte[] datafield = FileUtils.ReadFileSeek(filename.ToString(), index, sendLength);
                FileUtils.WriteFile(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "debug.txt"), "datafield.Length:" + datafield.Length + "\r\n");
                /*for (int i = 0; i < datafield.Length; i++)
                {
                    Debug.WriteLine("datafield[i]:" + datafield[i]);
                }*/
                //Debug.WriteLine("datafield.Length:" + datafield.Length);
                //string data = "SAPK=" + sb.ToString() + ";";//发送APK文件
                //Debug.WriteLine("data:" + data.Length);
                //byte[] datafield = System.Text.Encoding.Default.GetBytes(data);
                byte[] head_datafield = new byte[6 + datafield.Length + 2]; //总长度 = 头6 + 数据体 +2
                head_datafield[0] = (byte)0xF5;
                head_datafield[1] = (byte)0xA5;
                head_datafield[2] = (byte)0xA7;
                head_datafield[3] = (byte)0x00;//发送APK文件
                for (int i = 0; i < datafield.Length; i++)
                {
                    head_datafield[i + 6] = datafield[i];
                }
                CheckUtils.CalcularLength(head_datafield);
                CheckUtils.CheckSum(head_datafield, false);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < head_datafield.Length; i++)
                {
                    sb.Append(head_datafield[i].ToString("X2") + ",");
                }
                //Debug.WriteLine("sb.ToString():" + sb.ToString());
                //MessageBox.Show(sb.ToString());
                if (socketclient != null && socketclient.Connected)
                {
                    while (!isSend) continue;
                    isSend = false;
                    ClientSendMsg(sb.ToString());

                    //socketclient.Send(datafield);
                    //Thread.Sleep(100);
                    //socketclient.Send(datafield);
                }
                else
                {
                    break;
                }
                //写入一条数据，调用更新主线程ui状态的委托
            }
            FileUtils.WriteFile(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "debug.txt"), "isSend:" + isSend + "\r\n");
            while (!isSend) continue;
            isSend = false;
            //Thread.Sleep(100);
            ClientSendMsg("F5,A5,A7,02,00,00,00,3B,");//发送结束
                                                      //任务完成时通知主线程作出相应的处理

            //socketclient.Send(b);

        }


        private void CheckConnect()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(5000);
                    if (socketclient != null && socketclient.Connected)
                    {
                        ClientSendMsg("F5,A5,AB,00,00,00,00,3B,");//请求电池电压值(发送心跳包)
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void ShowNotify(string title, string content)
        {
            NotifyData data = new NotifyData();
            data.Title = title;
            data.Content = content;

            NotificationWindow dialog = new NotificationWindow();//new 一个通知
            dialog.Closed += Dialog_Closed;
            dialog.TopFrom = GetTopFrom();
            _dialogs.Add(dialog);
            dialog.DataContext = data;//设置通知里要显示的数据
            dialog.Show();
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotificationWindow;
            _dialogs.Remove(closedDialog);
        }

        double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);

            while (isContinueFind)
            {
                topFrom = topFrom - 110;//此处100是NotifyWindow的高 110-100剩下的10  是通知之间的间距
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }

            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;

            return topFrom;
        }

        private void Connect()
        {
            //定义一个套接字监听  
            socketclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //获取文本框中的IP地址  
            IPAddress address = IPAddress.Parse(ipAddress);

            //将获取的IP地址和端口号绑定在网络节点上  
            IPEndPoint point = new IPEndPoint(address, 8080);

            try
            {
                //客户端套接字连接到网络节点上，用的是Connect  
                socketclient.Connect(point);
            }
            catch (Exception)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                    (Action)delegate () { ShowNotify("提示:", "连接失败,请检查IP地址是否正确"); });
                isConnecting = false;
                return;
            }
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                    (Action)delegate () { ShowNotify("提示:", "连接成功"); });
            isConnecting = false;


            string filename = string.Empty;
            List<TNCProgram> nCPrograms = new List<TNCProgram>();
            using (MonitoringContext monitoringContext = new MonitoringContext())
            {
                var linq = (from obj in monitoringContext.NCPrograms
                            where obj.NCProgramID == _item.NCProgramID
                            select obj).ToList();
                nCPrograms = linq;
                //filename = linq[0].SavePath;
                Thread fileThread = new Thread(sendMultiFileThread);
                fileThread.IsBackground = true;
                fileThread.Start(nCPrograms);
            }



            /*string filename = string.Empty;
            using (MonitoringContext monitoringContext = new MonitoringContext())
            {
                var linq = (from obj in monitoringContext.NCPrograms
                            where obj.ProcessID == _item.ProcessID
                            select obj).ToList();
                filename = linq[0].SavePath;

                Thread fileThread = new Thread(sendMultiFileThread);
                fileThread.IsBackground = true;
                fileThread.Start(linq);
            }*/


            //string filename = @"E:\Personal Files\IntelligentManufactureSolution\files\Category_2020_08_05_07_20_46\ComponentType_2020_08_05_07_20_48\Component_2020_08_05_07_20_50\PartType_2020_08_05_07_20_53\Part_2020_08_05_07_20_55\2020_08_05_07_21_10\NC\Proc_05_08_10_23_8885\test_3.txt";
            /*if (filename != null && File.Exists(filename))
            {
                //文件存在
                long sendLength = 452;
                FileInfo fileInfo = new FileInfo(filename.ToString());
                long length = fileInfo.Length;
                int count = (int)(length / sendLength);
                Thread fileThread = new Thread(sendFileThread);
                fileThread.IsBackground = true;
                fileThread.Start(filename);
            }*/
            RecvMessage();
        }

        private void sendMultiFileThread(Object obj)
        {
            isSending = true;
            List<TNCProgram> linq = (List<TNCProgram>)obj;
            FileUtils.WriteFile(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "debug.txt"), "fileName:"+linq[0].SavePath+"\r\n");
            FileUtils.WriteFile(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "debug.txt"), "file count:"+linq.Count + "\r\n");
            for (int i = 0; i < linq.Count; i++)
            {
                //string filename = file_path.Text;
                string filename = linq[i].SavePath;
                FileUtils.WriteFile(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "debug.txt"), "File.Exists(filename):" + File.Exists(filename) + "\r\n");
                if (filename != null && File.Exists(filename))
                {
                    //文件存在
                    long sendLength = 452;
                    FileInfo fileInfo = new FileInfo(filename.ToString());
                    long length = fileInfo.Length;
                    int count = (int)(length / sendLength);
                    //this.upload_progressBar.Maximum = 100;
                    //this.upload_progressBar.Value = 0;
                    //this.progressbar_status.Text = this.upload_progressBar.Value.ToString() + "%";
                    sendFileThread(filename);
                    //Thread fileThread = new Thread(sendFileThread);
                    //fileThread.IsBackground = false;
                    //fileThread.Start(filename);
                }
            }
            isSending = false;
            Accomplish();
        }

        bool isSending = false;
        private string deviceID = null;
        private bool CheckDeviceID(string strRevMsg)
        {
            string[] data = strRevMsg.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            byte[] byteData = new byte[data.Length];
            string[] stringData = new string[data.Length];
            bool ss = true;
            for (int i = 0; i < data.Length - 1; i++)
            {
                try
                {
                    byteData[i] = (byte)Convert.ToInt32(data[i], 16);
                }
                catch (Exception)
                {
                    ss = false;
                }
            }
            if (!ss) return false;
            if (CheckUtils.CheckSum(byteData, true))
            {
                string strConfig = System.Text.Encoding.ASCII.GetString(byteData);
                if (strConfig != null)
                {
                    int begin = strConfig.IndexOf("SBID");
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        string value = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                        deviceID = value;
                        if (value.Length == 24)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // 接收服务端发来信息的方法    
        private void RecvMessage()
        {
            int x = 0;
            //持续监听服务端发来的消息 
            while (true)
            {
                try
                {
                    //定义一个1M的内存缓冲区，用于临时性存储接收到的消息  
                    byte[] arrRecvmsg = new byte[1024 * 1024];

                    //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                    int length = socketclient.Receive(arrRecvmsg);

                    //将套接字获取到的字符数组转换为人可以看懂的字符串  
                    string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                    if (x == 0)
                    {
                        if (!CheckDeviceID(strRevMsg))
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "连接错误"); });
                            StopConnetct();
                            break;
                        }
                        x = 1;
                        continue;
                    }
                    try
                    {
                        DataAnalysis(strRevMsg);
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                   (Action)delegate () { ShowNotify("提示:", "连接中断"); });
                    break;
                }
            }
        }

        private void StopConnetct()
        {
            try
            {
                if (socketclient != null && socketclient.Connected)
                {
                    socketclient.Shutdown(SocketShutdown.Both);
                    socketclient.Close(100);
                }
            }
            catch (Exception)
            {
            }
        }





        //发送字符信息到服务端的方法  
        public bool ClientSendMsg(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg + "\n");
            //调用客户端套接字发送字节数组     
            if (socketclient != null && socketclient.Connected)
            {
                socketclient.Send(arrClientSendMsg);
                return true;
            }
            else
            {
                //MessageBox.Show("请先连接仪器!");
                ShowNotify("提示:", "请先连接仪器");
                return false;
            }
        }

        public static string GetTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return string.Format("{0:yyyyMMddHHmmssffff}", currentTime) + CrtAlphabet();
        }

        public static string GetCollectTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", currentTime);
        }

        private static string CrtAlphabet()
        {
            string str = "";
            Random rnd = new Random();
            for (int i = 0; i < 2; i++)
            {
                int x = rnd.Next(97, 123);
                char c = (char)x;
                str += c;
            }
            return str;
        }

        StringBuilder dataString = new StringBuilder();
        string fileName = GetTime() + ".dat";
        string resfilename = GetTime() + ".res";
        string xlsxFileName = GetTime() + ".xlsx";
        string pdfFileName = GetTime() + ".pdf";
        string startCollectTime = GetCollectTime();
        bool isSend = false;
        public static bool isCoalAshSystem = false;//是否是煤灰系统
        bool isStart = false;
        int testTime = 0;
        public bool isComplete = true;//导入导出是否完成
        bool isImportReturn = false;
        public void DataAnalysis(string msg)
        {
            msg = dataString.ToString() + msg;
            dataString.Clear();
            Debug.WriteLine("msg.Length:" + msg.Length);
            string[] data = msg.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < data.Length; i++)
            {
                if (i != data.Length - 1)
                {
                    if (data[i].Length == 24)
                    {
                        if (data[i].Equals("F5,A5,A1,31,3D,4F,4B,21,"))
                        {
                            //开始采集
                            string time = GetTime();
                            if (!isCoalAshSystem)
                            {
                                fileName = time + ".dat";
                            }
                            else
                            {
                                fileName = time + ".txt";
                            }
                            resfilename = time + ".res";
                            xlsxFileName = time + ".xlsx";
                            pdfFileName = GetTime() + ".pdf";
                            startCollectTime = GetCollectTime();
                            testTime = 0;
                            isStart = true;
                        }
                        else if (data[i].Equals("F5,A5,A1,30,3D,4F,4B,21,"))
                        {
                            //停止采集
                            isStart = false;
                            testTime = 0;
                        }
                        else if (data[i].Equals("F5,A5,A7,31,3D,4F,4B,21,"))
                        {
                            //接收成功
                            isSend = true;
                        }
                        else if (data[i].Equals("F5,A5,AD,03,3D,4F,4B,21,"))//下载成功
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "下载成功"); });
                        }
                        else if (data[i].Equals("F5,A5,AD,04,3D,4F,4B,21,"))//下载失败
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "下载失败"); });
                        }
                        else if (data[i].Equals("F5,A5,AE,02,3D,4F,4B,21,"))//导出设置完成
                        {
                            isComplete = true;
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "导出设置完成"); });
                        }
                        else if (data[i].Equals("F5,A5,AE,04,3D,4F,4B,21,"))//导出设置完成
                        {
                            isImportReturn = true;
                        }
                        dataString.Clear();
                    }
                    else if (data[i].Length == 18495)//谱图数据
                    {
                        UpdateUIThread(data[i] + "\n");
                        dataString.Clear();
                    }
                    else//参数设置等
                    {
                        ReadSetting(data[i] + "\n");
                        dataString.Clear();
                    }
                }
                else
                {
                    if (!data[i].Equals("") && msg.Substring(msg.Length - 1, 1).Equals("\n"))
                    {
                        if (data[i].Length == 24)
                        {
                            if (data[i].Equals("F5,A5,A1,31,3D,4F,4B,21,"))
                            {
                                //开始采集
                                string time = GetTime();
                                if (!isCoalAshSystem)
                                {
                                    fileName = time + ".dat";
                                }
                                else
                                {
                                    fileName = time + ".txt";
                                }
                                resfilename = time + ".res";
                                xlsxFileName = time + ".xlsx";
                                pdfFileName = GetTime() + ".pdf";
                                startCollectTime = GetCollectTime();
                                testTime = 0;
                                isStart = true;
                            }
                            else if (data[i].Equals("F5,A5,A1,30,3D,4F,4B,21,"))
                            {
                                //停止采集
                                isStart = false;
                                testTime = 0;
                            }
                            else if (data[i].Equals("F5,A5,A7,31,3D,4F,4B,21,"))
                            {
                                //接收成功
                                isSend = true;
                            }
                            else if (data[i].Equals("F5,A5,AD,03,3D,4F,4B,21,"))//下载成功
                            {
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "下载成功"); });
                            }
                            else if (data[i].Equals("F5,A5,AD,04,3D,4F,4B,21,"))//下载失败
                            {
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "下载失败"); });
                            }
                            else if (data[i].Equals("F5,A5,AE,02,3D,4F,4B,21,"))//导出设置完成
                            {
                                isComplete = true;
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "导出设置完成"); });
                            }
                            else if (data[i].Equals("F5,A5,AE,04,3D,4F,4B,21,"))//导出设置完成
                            {
                                isImportReturn = true;
                            }
                            dataString.Clear();
                        }
                        else if (data[i].Length == 18495)//谱图数据
                        {
                            UpdateUIThread(data[i] + "\n");
                            dataString.Clear();
                        }
                        else//参数设置等
                        {
                            ReadSetting(data[i] + "\n");
                            dataString.Clear();
                        }
                    }
                    else
                    {
                        dataString.Append(data[i]);
                    }
                }
            }
        }
        public string fileLocation = "C:\\Tecsonde";
        private void UpdateUIThread(object sb)
        {
            string[] data = sb.ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            byte[] byteData = new byte[data.Length];
            string[] stringData = new string[data.Length];
            bool ss = true;
            bool isTure = false;
            double[] spectrumData = new double[2048];
            for (int i = 0; i < data.Length - 1; i++)
            {
                try
                {
                    byteData[i] = (byte)Convert.ToInt32(data[i], 16);
                    //if (CaliCombSpectrumData != null && CaliCombSpectrumData.Length / 6165 == CaliCombTestCount + 1)
                    //{
                    //    CaliCombSpectrumData[i + (CaliCombTestCount * 6165)] = byteData[i];
                    //    isTure = true;
                    //}
                }
                catch (Exception)
                {
                    ss = false;
                }
            }
            if (ss)
            {
                double realtime = CheckUtils.ByteToInt(new byte[] { byteData[6150 + 0], byteData[6150 + 1], byteData[6150 + 2], byteData[6150 + 3] }) * 0.001;
                //UpdateUIDelegate((int)realtime);//更新采集进度
                /*Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    this.timeProgressBar.Value = realtime;
                                    this.progressbar_status.Text = this.timeProgressBar.Value.ToString() + "/" + this.timeProgressBar.Maximum.ToString();
                                });*/
                byte[] bs = new byte[6144];
                for (int i = 5; i < byteData.Length; i++)
                {
                    if (i - 5 < 6144)
                    {
                        bs[i - 5] = byteData[i];
                    }
                }
                double y = 0;
                double value = 0;
                StringBuilder txtData = new StringBuilder();
                int j = 0;
                for (int i = 0; i < bs.Length - 2; i += 3)
                {
                    y = CheckUtils.ByteToInt(new byte[] { bs[i + 2], bs[i + 1], bs[i] });
                    txtData.Append(y + "\r\n");
                    //if (y != 0)
                    //    y = Math.Log(y);
                    if (j < 2048)
                        spectrumData[j++] = y;
                }
                /*this.connect_status.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () {
                                    Clear_Spectrum();
                                    DataSeries ds = AddDatapoint(spectrumData, realtime + "");
                                    // 添加数据线到数据序列。 
                                    chart.Series.Add(ds);
                                });*/
                if (!isCoalAshSystem)
                {
                    if (isTure)
                    {
                        //FileUtils.SaveBinaryFile(fileLocation + "\\" + fileName, CaliCombSpectrumData);
                    }
                    else
                    {
                        FileUtils.SaveBinaryFile(fileLocation + "\\" + fileName, byteData);
                    }
                }
                else
                {
                    //煤灰系统
                    FileUtils.WriteFile(fileLocation + "\\" + fileName, txtData.ToString());
                }

            }

        }


        private void ReadSetting(object message)
        {
            string[] data = message.ToString().Split(new[] { "," }, StringSplitOptions.None);
            byte[] byteData = new byte[data.Length];
            string[] stringData = new string[data.Length];
            bool ss = true;
            int y = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                try
                {
                    if (!data[i].Equals(""))
                    {
                        byteData[y++] = (byte)Convert.ToInt32(data[i], 16);
                    }
                }
                catch (Exception)
                {
                    ss = false;
                }
            }
            if (!ss) return;
            if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA3
                        && byteData[3] == (byte)0x03)
            {
                //接收所有测试模式、定标模式、拟合系数相关参数
                //ArgumentSetting(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA3
                        && byteData[3] == (byte)0x02)
            {
                //接收定标方式参数
                //CaliwayargumentSetting(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA6
                        && byteData[3] == (byte)0x00)
            {
                //ResultFile(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA8
                       && byteData[3] == (byte)0x00)
            {
                //接收查询结果
                SearchResult(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA8
                       && byteData[3] == (byte)0x01)
            {
                //下载结果数据
                DownloadResultData(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA8
                       && byteData[3] == (byte)0x02)
            {
                //下载谱图数据
                //DownloadSpectrumData(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA8
                       && byteData[3] == (byte)0x03)
            {
                //下载所有数据
                //DownloadAllData(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA9
                       && byteData[3] == (byte)0x00)
            {
                //返回定标组合
                //Calicomb(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA9
                       && byteData[3] == (byte)0x04)
            {
                //搜索定标组合
                //searchcalicomb(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA1
                       && byteData[3] == (byte)0x01)
            {
                //定标组合检测次数
                //calicombtestcount(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAB
                      && byteData[3] == (byte)0x00)
            {
                //电池电压
                //readPower(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA1
                      && byteData[3] == (byte)0x00)
            {
                //采集状态
                //collectStatus(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAC
                      && byteData[3] == (byte)0x03)
            {
                //获取测试次数限制最大值
                //showTestCountLimitMaxValue(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAD
                      && byteData[3] == (byte)0x00)
            {
                //获取IP地址
                //showIpAddress(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAE
                      && byteData[3] == (byte)0x00)
            {
                //一键保存
                //exportSettings(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAE
                      && byteData[3] == (byte)0x01)
            {
                //一键保存
                //saveSettings(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA3
                      && byteData[3] == (byte)0x04)
            {
                //采集时间
                GetCollecttime(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x02)
            {
                //样品标签
                GetSpecimenTag(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x04)
            {
                //接收搜索样品数据
                SearchSpecimen(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x06)
            {
                //接收样品含量数据
                querySpecimenContent(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x07)
            {
                //获取样品
                GetCurrentSample(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x08)
            {
                //获取元素
                GetCurrentElement(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x0A)
            {
                //接收样品含量数据
                querySpecimenContentAndArgument(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x0C)
            {
                //获取添加样品数据
                GetAddSample(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xB2
                     && byteData[3] == (byte)0x00)
            {
                //获取指令内容
                GetCommandContent(byteData);
            }
        }

        private void GetCommandContent(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string commandContent = null;
                    int begin = strConfig.IndexOf("ZLNR");//指令内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        commandContent = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }

                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    //string oldContent = receive_data.Text;
                                    //receive_data.Text = commandContent + "\n" + oldContent;
                                });
                }
            }
        }

        private void GetAddSample(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string content = null;
                    string sampleID = null;
                    string sampleName = null;
                    int begin = strConfig.IndexOf("YPID");//样品ID
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        sampleID = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("YPMC");//样品名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        sampleName = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("WJNR");//文件内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        content = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    filename = sampleID + "(" + sampleName + ").txt";
                    if (filename != null && content != null)
                    {
                        FileUtils.WriteFile(fileLocation + "\\" + filename, content);
                    }
                    /*if (sampleLibraryOperationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    sampleLibraryOperationWindow.UpdateAddSample(sampleID, sampleName);
                                });
                    }*/
                }
            }
        }

        private void querySpecimenContentAndArgument(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string content = null;
                    int begin = strConfig.IndexOf("WJMC");//文件名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        filename = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("WJNR");//文件内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        content = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    if (filename != null && content != null)
                    {
                        FileUtils.WriteFile(fileLocation + "\\" + filename, content);
                    }
                    /*if (sampleLibraryOperationWindow != null)
                    {
                        sampleLibraryOperationWindow.DownloadSampleContentFile();
                    }*/
                }
            }
        }

        private void GetCurrentElement(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string element = null;
                    int begin = strConfig.IndexOf("XZYS");//选择元素
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        element = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    /*if (focusOnElementsWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    focusOnElementsWindow.UpdateCurrentElement(element);
                                });
                    }*/
                }
            }
        }

        private void GetCurrentSample(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string sampleID = null;
                    string sampleName = null;
                    int begin = strConfig.IndexOf("YPID");//样品ID
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        sampleID = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("YPMC");//样品名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        sampleName = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    /*if (theSampleSelectionWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    theSampleSelectionWindow.UpdateCurrentSample(sampleID, sampleName);
                                });
                    }*/
                }
            }
        }

        private void querySpecimenContent(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string content = null;
                    int begin = strConfig.IndexOf("WJMC");//文件名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        filename = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("WJNR");//文件内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        content = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    if (filename != null && content != null)
                    {
                        FileUtils.WriteFile(fileLocation + "\\" + filename, content);
                    }
                }
            }
        }

        private void SearchSpecimen(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.UTF8.GetString(data);
                if (strConfig != null)
                {
                    string specimenID = null;
                    string specimenName = null;
                    StringBuilder msb = new StringBuilder();
                    int begin = strConfig.IndexOf("YPID");//样品ID
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        specimenID = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("YPMC");//样品名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        specimenName = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    msb.Append(specimenID + "#" + specimenName + "#");
                    /*if (validationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    validationWindow.UpdateSearchSpecimenList(msb.ToString());
                                });
                    }
                    if (standardizationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    standardizationWindow.UpdateSearchSpecimenList(msb.ToString());
                                });
                    }
                    if (theSampleSelectionWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    theSampleSelectionWindow.UpdateSearchSpecimenList(msb.ToString());
                                });
                    }
                    if (sampleLibraryOperationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    sampleLibraryOperationWindow.UpdateSearchSpecimenList(msb.ToString());
                                });
                    }*/
                }
            }
        }

        private void DownloadResultData(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string content = null;
                    int begin = strConfig.IndexOf("WJMC");//文件名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        filename = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("JGNR");//结果内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        content = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    Console.WriteLine("filename:" + filename);
                    Console.WriteLine("content:" + content);
                    if (filename != null && content != null)
                    {
                        string fileName = filename.Substring(24);
                        FileUtils.WriteFile(fileLocation + "\\" + fileName, content);
                    }
                }
            }
            /*if (statisticsWindow != null)
            {
                statisticsWindow.setSend(true);
            }*/
        }

        private void SearchResult(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.UTF8.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string tag = null;
                    string time = null;
                    string person = null;
                    StringBuilder msb = new StringBuilder();
                    int begin = strConfig.IndexOf("WJMC");//文件名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        filename = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("YPBQ");//样品标签
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        tag = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("LSSJ");//历史时间
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        time = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("JCRY");//检测人员
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        person = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    msb.Append(filename + "#" + tag + "#" + time + "#" + person + "#");
                    /*if (statisticsWindow != null /*&& statisticsWindow.IsActive*//*)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    statisticsWindow.UpdateSearchList(msb.ToString());
                                });
                    }
                    if (validationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    validationWindow.UpdateSearchList(msb.ToString());
                                });
                    }
                    if (standardizationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    standardizationWindow.UpdateSearchList(msb.ToString());
                                });
                    }*/
                }
            }
        }

        public string SPECIMENTAG = null;
        private void GetSpecimenTag(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.UTF8.GetString(data);
                if (strConfig != null)
                {
                    int begin = strConfig.IndexOf("YPBQ");//样品标签
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        string count = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                        SPECIMENTAG = count;
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    //specimen_tag.Text = count;
                                });
                    }
                }
            }
        }

        string COLLECTTIME = null;
        private void GetCollecttime(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.UTF8.GetString(data);
                if (strConfig != null)
                {
                    int begin = strConfig.IndexOf("CJSJ");//采集时间
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        string count = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                        COLLECTTIME = count;
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    //collect_time.Text = COLLECTTIME;
                                });
                    }
                }
            }
        }




        /*TProcess _item { get; set; }
        public static List<NotificationWindow> _dialogs = new List<NotificationWindow>();
        Socket socketclient = null;
        Thread connectThread = null;
        bool isConnecting = false;
        public static MainWindow mainWindow = null;
        bool convertOK = true;
        int[] dataArr = new int[2048];//数据的数组
        string datFile = "";//打开文件的路径
        public static string workpath = "C:\\Tecsonde\\";
        //创建 1个客户端套接字 和1个负责监听服务端请求的线程  
        Thread threadclient = null;
        Thread timeThread = null;
        string ipAddress = null;
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collections = (System.Collections.IList)this.ProcessingGrid.SelectedItems;
            var collection = collections.Cast<TProcess>();
            TProcess item = collection.ToList()[0];
            _item = item;

            if (isConnecting)
            {
                //MessageBox.Show("正在连接...请稍候!");
                ShowNotify("提示:", "正在连接...请稍候!");
                return;
            }
            isConnecting = true;
            //连接仪器
            ipAddress = ip_address.Text.Trim();
            if (ipAddress.Equals(""))
            {
                //MessageBox.Show("请输入IP地址");
                ShowNotify("提示:", "请输入IP地址");
                return;
            }
            if (socketclient == null || !socketclient.Connected)
            {
                connectThread = new Thread(Connect);
                connectThread.IsBackground = true;
                connectThread.Start();
                //connect(ipAddress, port);
            }
            else
            {
                string filename = string.Empty;
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.NCPrograms
                                where obj.ProcessID == item.ProcessID
                                select obj).ToList();
                    filename = linq[0].SavePath;
                }
                //string filename = @"E:\Personal Files\IntelligentManufactureSolution\files\Category_2020_08_05_07_20_46\ComponentType_2020_08_05_07_20_48\Component_2020_08_05_07_20_50\PartType_2020_08_05_07_20_53\Part_2020_08_05_07_20_55\2020_08_05_07_21_10\NC\Proc_05_08_10_23_8885\test_3.txt";
                if (filename != null && File.Exists(filename))
                {
                    //文件存在
                    long sendLength = 452;
                    FileInfo fileInfo = new FileInfo(filename.ToString());
                    long length = fileInfo.Length;
                    int count = (int)(length / sendLength);
                    Thread fileThread = new Thread(sendFileThread);
                    fileThread.IsBackground = true;
                    fileThread.Start(filename);
                }
                else
                {
                    MessageBox.Show("请选择文件!");
                }
            }

        }

        private void Btn_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            //断开仪器
            if (isConnecting)
            {
                //MessageBox.Show("正在连接...请稍候!");
                ShowNotify("提示:", "正在连接...请稍候");
                return;
            }
            StopConnetct();
            ShowNotify("提示:", "已断开连接");
        }

        private void Accomplish()
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    MessageBox.Show("上传完成");
                                });

        }

        private void sendFileThread(Object filename)
        {
            //以获取其大小
            FileInfo fileInfo = new FileInfo(filename.ToString());
            string fileName = fileInfo.Name;
            if (fileName != null)
            {
                Debug.WriteLine("fileName:" + fileName);
                string data = "WJMC=" + fileName + ";";
                byte[] datafield = System.Text.Encoding.UTF8.GetBytes(data);
                byte[] head_datafield = new byte[6 + datafield.Length + 2]; //总长度 = 头6 + 数据体 +2
                head_datafield[0] = (byte)0xF5;
                head_datafield[1] = (byte)0xA5;
                head_datafield[2] = (byte)0xA7;
                head_datafield[3] = (byte)0x01;//发送文件名称
                for (int i = 0; i < datafield.Length; i++)
                {
                    head_datafield[i + 6] = datafield[i];
                }
                CheckUtils.CalcularLength(head_datafield);
                CheckUtils.CheckSum(head_datafield, false);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < head_datafield.Length; i++)
                {
                    sb.Append(head_datafield[i].ToString("X2") + ",");
                }
                ClientSendMsg(sb.ToString());//开始发送
            }
            //socketclient.Send(a);
            //Thread.Sleep(100);
            long sendLength = 452;
            long length = fileInfo.Length;
            int count = (int)(length / sendLength);
            Debug.WriteLine("length:" + length);
            Debug.WriteLine("count:" + count);
            for (int index = 0; index <= count; index++)
            {
                //byte[] filecontent = FileUtils.ReadFileSeek(filename.ToString(), index, sendLength);
                byte[] datafield = FileUtils.ReadFileSeek(filename.ToString(), index, sendLength);
                *//*for (int i = 0; i < datafield.Length; i++)
                {
                    Debug.WriteLine("datafield[i]:" + datafield[i]);
                }*//*
                //Debug.WriteLine("datafield.Length:" + datafield.Length);
                //string data = "SAPK=" + sb.ToString() + ";";//发送APK文件
                //Debug.WriteLine("data:" + data.Length);
                //byte[] datafield = System.Text.Encoding.Default.GetBytes(data);
                byte[] head_datafield = new byte[6 + datafield.Length + 2]; //总长度 = 头6 + 数据体 +2
                head_datafield[0] = (byte)0xF5;
                head_datafield[1] = (byte)0xA5;
                head_datafield[2] = (byte)0xA7;
                head_datafield[3] = (byte)0x00;//发送APK文件
                for (int i = 0; i < datafield.Length; i++)
                {
                    head_datafield[i + 6] = datafield[i];
                }
                CheckUtils.CalcularLength(head_datafield);
                CheckUtils.CheckSum(head_datafield, false);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < head_datafield.Length; i++)
                {
                    sb.Append(head_datafield[i].ToString("X2") + ",");
                }
                //Debug.WriteLine("sb.ToString():" + sb.ToString());
                //MessageBox.Show(sb.ToString());
                if (socketclient != null && socketclient.Connected)
                {
                    while (!isSend) continue;
                    isSend = false;
                    ClientSendMsg(sb.ToString());

                    //socketclient.Send(datafield);
                    //Thread.Sleep(100);
                    //socketclient.Send(datafield);
                }
                else
                {
                    break;
                }
                //写入一条数据，调用更新主线程ui状态的委托
            }
            while (!isSend) continue;
            isSend = false;
            //Thread.Sleep(100);
            ClientSendMsg("F5,A5,A7,02,00,00,00,3B,");//发送结束
            //任务完成时通知主线程作出相应的处理
            Accomplish();
            //socketclient.Send(b);

        }


        private void CheckConnect()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(5000);
                    if (socketclient != null && socketclient.Connected)
                    {
                        ClientSendMsg("F5,A5,AB,00,00,00,00,3B,");//请求电池电压值(发送心跳包)
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void ShowNotify(string title, string content)
        {
            NotifyData data = new NotifyData();
            data.Title = title;
            data.Content = content;

            NotificationWindow dialog = new NotificationWindow();//new 一个通知
            dialog.Closed += Dialog_Closed;
            dialog.TopFrom = GetTopFrom();
            _dialogs.Add(dialog);
            dialog.DataContext = data;//设置通知里要显示的数据
            dialog.Show();
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotificationWindow;
            _dialogs.Remove(closedDialog);
        }

        double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);

            while (isContinueFind)
            {
                topFrom = topFrom - 110;//此处100是NotifyWindow的高 110-100剩下的10  是通知之间的间距
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }

            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;

            return topFrom;
        }

        private void Connect()
        {
            //定义一个套接字监听  
            socketclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //获取文本框中的IP地址  
            IPAddress address = IPAddress.Parse(ipAddress);

            //将获取的IP地址和端口号绑定在网络节点上  
            IPEndPoint point = new IPEndPoint(address, 8080);

            try
            {
                //客户端套接字连接到网络节点上，用的是Connect  
                socketclient.Connect(point);
            }
            catch (Exception)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                    (Action)delegate () {  ShowNotify("提示:", "连接失败,请检查IP地址是否正确"); });
                isConnecting = false;
                return;
            }
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                    (Action)delegate () {  ShowNotify("提示:", "连接成功"); });
            isConnecting = false;

            string filename = string.Empty;
            using (MonitoringContext monitoringContext = new MonitoringContext())
            {
                var linq = (from obj in monitoringContext.NCPrograms
                            where obj.ProcessID == _item.ProcessID
                            select obj).ToList();
                filename = linq[0].SavePath;
            }
            //string filename = @"E:\Personal Files\IntelligentManufactureSolution\files\Category_2020_08_05_07_20_46\ComponentType_2020_08_05_07_20_48\Component_2020_08_05_07_20_50\PartType_2020_08_05_07_20_53\Part_2020_08_05_07_20_55\2020_08_05_07_21_10\NC\Proc_05_08_10_23_8885\test_3.txt";
            if (filename != null && File.Exists(filename))
            {
                //文件存在
                long sendLength = 452;
                FileInfo fileInfo = new FileInfo(filename.ToString());
                long length = fileInfo.Length;
                int count = (int)(length / sendLength);
                Thread fileThread = new Thread(sendFileThread);
                fileThread.IsBackground = true;
                fileThread.Start(filename);
            }
            RecvMessage();
        }
        private string deviceID = null;
        private bool CheckDeviceID(string strRevMsg)
        {
            string[] data = strRevMsg.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            byte[] byteData = new byte[data.Length];
            string[] stringData = new string[data.Length];
            bool ss = true;
            for (int i = 0; i < data.Length - 1; i++)
            {
                try
                {
                    byteData[i] = (byte)Convert.ToInt32(data[i], 16);
                }
                catch (Exception)
                {
                    ss = false;
                }
            }
            if (!ss) return false;
            if (CheckUtils.CheckSum(byteData, true))
            {
                string strConfig = System.Text.Encoding.ASCII.GetString(byteData);
                if (strConfig != null)
                {
                    int begin = strConfig.IndexOf("SBID");
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        string value = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                        deviceID = value;
                        if (value.Length == 24)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // 接收服务端发来信息的方法    
        private void RecvMessage()
        {
            int x = 0;
            //持续监听服务端发来的消息 
            while (true)
            {
                try
                {
                    //定义一个1M的内存缓冲区，用于临时性存储接收到的消息  
                    byte[] arrRecvmsg = new byte[1024 * 1024];

                    //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                    int length = socketclient.Receive(arrRecvmsg);

                    //将套接字获取到的字符数组转换为人可以看懂的字符串  
                    string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                    if (x == 0)
                    {
                        if (!CheckDeviceID(strRevMsg))
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () {  ShowNotify("提示:", "连接错误"); });
                            StopConnetct();
                            break;
                        }
                        x = 1;
                        continue;
                    }
                    try
                    {
                        DataAnalysis(strRevMsg);
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                   (Action)delegate () {  ShowNotify("提示:", "连接中断"); });
                    break;
                }
            }
        }

        private void StopConnetct()
        {
            try
            {
                if (socketclient != null && socketclient.Connected)
                {
                    socketclient.Shutdown(SocketShutdown.Both);
                    socketclient.Close(100);
                }
            }
            catch (Exception)
            {
            }
        }





        //发送字符信息到服务端的方法  
        public bool ClientSendMsg(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg + "\n");
            //调用客户端套接字发送字节数组     
            if (socketclient != null && socketclient.Connected)
            {
                socketclient.Send(arrClientSendMsg);
                return true;
            }
            else
            {
                //MessageBox.Show("请先连接仪器!");
                ShowNotify("提示:", "请先连接仪器");
                return false;
            }
        }

        public static string GetTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return string.Format("{0:yyyyMMddHHmmssffff}", currentTime) + CrtAlphabet();
        }

        public static string GetCollectTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", currentTime);
        }

        private static string CrtAlphabet()
        {
            string str = "";
            Random rnd = new Random();
            for (int i = 0; i < 2; i++)
            {
                int x = rnd.Next(97, 123);
                char c = (char)x;
                str += c;
            }
            return str;
        }

        StringBuilder dataString = new StringBuilder();
        string fileName = GetTime() + ".dat";
        string resfilename = GetTime() + ".res";
        string xlsxFileName = GetTime() + ".xlsx";
        string pdfFileName = GetTime() + ".pdf";
        string startCollectTime = GetCollectTime();
        bool isSend = false;
        public static bool isCoalAshSystem = false;//是否是煤灰系统
        bool isStart = false;
        int testTime = 0;
        public bool isComplete = true;//导入导出是否完成
        bool isImportReturn = false;
        public void DataAnalysis(string msg)
        {
            msg = dataString.ToString() + msg;
            dataString.Clear();
            Debug.WriteLine("msg.Length:" + msg.Length);
            string[] data = msg.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < data.Length; i++)
            {
                if (i != data.Length - 1)
                {
                    if (data[i].Length == 24)
                    {
                        if (data[i].Equals("F5,A5,A1,31,3D,4F,4B,21,"))
                        {
                            //开始采集
                            string time = GetTime();
                            if (!isCoalAshSystem)
                            {
                                fileName = time + ".dat";
                            }
                            else
                            {
                                fileName = time + ".txt";
                            }
                            resfilename = time + ".res";
                            xlsxFileName = time + ".xlsx";
                            pdfFileName = GetTime() + ".pdf";
                            startCollectTime = GetCollectTime();
                            testTime = 0;
                            isStart = true;
                        }
                        else if (data[i].Equals("F5,A5,A1,30,3D,4F,4B,21,"))
                        {
                            //停止采集
                            isStart = false;
                            testTime = 0;
                        }
                        else if (data[i].Equals("F5,A5,A7,31,3D,4F,4B,21,"))
                        {
                            //接收成功
                            isSend = true;
                        }
                        else if (data[i].Equals("F5,A5,AD,03,3D,4F,4B,21,"))//下载成功
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "下载成功"); });
                        }
                        else if (data[i].Equals("F5,A5,AD,04,3D,4F,4B,21,"))//下载失败
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "下载失败"); });
                        }
                        else if (data[i].Equals("F5,A5,AE,02,3D,4F,4B,21,"))//导出设置完成
                        {
                            isComplete = true;
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "导出设置完成"); });
                        }
                        else if (data[i].Equals("F5,A5,AE,04,3D,4F,4B,21,"))//导出设置完成
                        {
                            isImportReturn = true;
                        }
                        dataString.Clear();
                    }
                    else if (data[i].Length == 18495)//谱图数据
                    {
                        UpdateUIThread(data[i] + "\n");
                        dataString.Clear();
                    }
                    else//参数设置等
                    {
                        ReadSetting(data[i] + "\n");
                        dataString.Clear();
                    }
                }
                else
                {
                    if (!data[i].Equals("") && msg.Substring(msg.Length - 1, 1).Equals("\n"))
                    {
                        if (data[i].Length == 24)
                        {
                            if (data[i].Equals("F5,A5,A1,31,3D,4F,4B,21,"))
                            {
                                //开始采集
                                string time = GetTime();
                                if (!isCoalAshSystem)
                                {
                                    fileName = time + ".dat";
                                }
                                else
                                {
                                    fileName = time + ".txt";
                                }
                                resfilename = time + ".res";
                                xlsxFileName = time + ".xlsx";
                                pdfFileName = GetTime() + ".pdf";
                                startCollectTime = GetCollectTime();
                                testTime = 0;
                                isStart = true;
                            }
                            else if (data[i].Equals("F5,A5,A1,30,3D,4F,4B,21,"))
                            {
                                //停止采集
                                isStart = false;
                                testTime = 0;
                            }
                            else if (data[i].Equals("F5,A5,A7,31,3D,4F,4B,21,"))
                            {
                                //接收成功
                                isSend = true;
                            }
                            else if (data[i].Equals("F5,A5,AD,03,3D,4F,4B,21,"))//下载成功
                            {
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "下载成功"); });
                            }
                            else if (data[i].Equals("F5,A5,AD,04,3D,4F,4B,21,"))//下载失败
                            {
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "下载失败"); });
                            }
                            else if (data[i].Equals("F5,A5,AE,02,3D,4F,4B,21,"))//导出设置完成
                            {
                                isComplete = true;
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () { ShowNotify("提示:", "导出设置完成"); });
                            }
                            else if (data[i].Equals("F5,A5,AE,04,3D,4F,4B,21,"))//导出设置完成
                            {
                                isImportReturn = true;
                            }
                            dataString.Clear();
                        }
                        else if (data[i].Length == 18495)//谱图数据
                        {
                            UpdateUIThread(data[i] + "\n");
                            dataString.Clear();
                        }
                        else//参数设置等
                        {
                            ReadSetting(data[i] + "\n");
                            dataString.Clear();
                        }
                    }
                    else
                    {
                        dataString.Append(data[i]);
                    }
                }
            }
        }
        public string fileLocation = "C:\\Tecsonde";
        private void UpdateUIThread(object sb)
        {
            string[] data = sb.ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            byte[] byteData = new byte[data.Length];
            string[] stringData = new string[data.Length];
            bool ss = true;
            bool isTure = false;
            double[] spectrumData = new double[2048];
            for (int i = 0; i < data.Length - 1; i++)
            {
                try
                {
                    byteData[i] = (byte)Convert.ToInt32(data[i], 16);
                    //if (CaliCombSpectrumData != null && CaliCombSpectrumData.Length / 6165 == CaliCombTestCount + 1)
                    //{
                    //    CaliCombSpectrumData[i + (CaliCombTestCount * 6165)] = byteData[i];
                    //    isTure = true;
                    //}
                }
                catch (Exception)
                {
                    ss = false;
                }
            }
            if (ss)
            {
                double realtime = CheckUtils.ByteToInt(new byte[] { byteData[6150 + 0], byteData[6150 + 1], byteData[6150 + 2], byteData[6150 + 3] }) * 0.001;
                //UpdateUIDelegate((int)realtime);//更新采集进度
                *//*Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    this.timeProgressBar.Value = realtime;
                                    this.progressbar_status.Text = this.timeProgressBar.Value.ToString() + "/" + this.timeProgressBar.Maximum.ToString();
                                });*//*
                byte[] bs = new byte[6144];
                for (int i = 5; i < byteData.Length; i++)
                {
                    if (i - 5 < 6144)
                    {
                        bs[i - 5] = byteData[i];
                    }
                }
                double y = 0;
                double value = 0;
                StringBuilder txtData = new StringBuilder();
                int j = 0;
                for (int i = 0; i < bs.Length - 2; i += 3)
                {
                    y = CheckUtils.ByteToInt(new byte[] { bs[i + 2], bs[i + 1], bs[i] });
                    txtData.Append(y + "\r\n");
                    //if (y != 0)
                    //    y = Math.Log(y);
                    if (j < 2048)
                        spectrumData[j++] = y;
                }
                *//*this.connect_status.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate () {
                                    Clear_Spectrum();
                                    DataSeries ds = AddDatapoint(spectrumData, realtime + "");
                                    // 添加数据线到数据序列。 
                                    chart.Series.Add(ds);
                                });*//*
                if (!isCoalAshSystem)
                {
                    if (isTure)
                    {
                        //FileUtils.SaveBinaryFile(fileLocation + "\\" + fileName, CaliCombSpectrumData);
                    }
                    else
                    {
                        FileUtils.SaveBinaryFile(fileLocation + "\\" + fileName, byteData);
                    }
                }
                else
                {
                    //煤灰系统
                    FileUtils.WriteFile(fileLocation + "\\" + fileName, txtData.ToString());
                }

            }

        }


        private void ReadSetting(object message)
        {
            string[] data = message.ToString().Split(new[] { "," }, StringSplitOptions.None);
            byte[] byteData = new byte[data.Length];
            string[] stringData = new string[data.Length];
            bool ss = true;
            int y = 0;
            for (int i = 0; i < data.Length - 1; i++)
            {
                try
                {
                    if (!data[i].Equals(""))
                    {
                        byteData[y++] = (byte)Convert.ToInt32(data[i], 16);
                    }
                }
                catch (Exception)
                {
                    ss = false;
                }
            }
            if (!ss) return;
            if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA3
                        && byteData[3] == (byte)0x03)
            {
                //接收所有测试模式、定标模式、拟合系数相关参数
                //ArgumentSetting(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA3
                        && byteData[3] == (byte)0x02)
            {
                //接收定标方式参数
                //CaliwayargumentSetting(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA6
                        && byteData[3] == (byte)0x00)
            {
                //ResultFile(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA8
                       && byteData[3] == (byte)0x00)
            {
                //接收查询结果
                SearchResult(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA8
                       && byteData[3] == (byte)0x01)
            {
                //下载结果数据
                DownloadResultData(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA8
                       && byteData[3] == (byte)0x02)
            {
                //下载谱图数据
                //DownloadSpectrumData(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA8
                       && byteData[3] == (byte)0x03)
            {
                //下载所有数据
                //DownloadAllData(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA9
                       && byteData[3] == (byte)0x00)
            {
                //返回定标组合
                //Calicomb(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA9
                       && byteData[3] == (byte)0x04)
            {
                //搜索定标组合
                //searchcalicomb(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA1
                       && byteData[3] == (byte)0x01)
            {
                //定标组合检测次数
                //calicombtestcount(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAB
                      && byteData[3] == (byte)0x00)
            {
                //电池电压
                //readPower(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA1
                      && byteData[3] == (byte)0x00)
            {
                //采集状态
                //collectStatus(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAC
                      && byteData[3] == (byte)0x03)
            {
                //获取测试次数限制最大值
                //showTestCountLimitMaxValue(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAD
                      && byteData[3] == (byte)0x00)
            {
                //获取IP地址
                //showIpAddress(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAE
                      && byteData[3] == (byte)0x00)
            {
                //一键保存
                //exportSettings(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAE
                      && byteData[3] == (byte)0x01)
            {
                //一键保存
                //saveSettings(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xA3
                      && byteData[3] == (byte)0x04)
            {
                //采集时间
                GetCollecttime(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x02)
            {
                //样品标签
                GetSpecimenTag(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x04)
            {
                //接收搜索样品数据
                SearchSpecimen(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x06)
            {
                //接收样品含量数据
                querySpecimenContent(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x07)
            {
                //获取样品
                GetCurrentSample(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x08)
            {
                //获取元素
                GetCurrentElement(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x0A)
            {
                //接收样品含量数据
                querySpecimenContentAndArgument(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xAF
                      && byteData[3] == (byte)0x0C)
            {
                //获取添加样品数据
                GetAddSample(byteData);
            }
            else if (byteData[0] == (byte)0xF5 && byteData[1] == (byte)0xA5 && byteData[2] == (byte)0xB2
                     && byteData[3] == (byte)0x00)
            {
                //获取指令内容
                GetCommandContent(byteData);
            }
        }

        private void GetCommandContent(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string commandContent = null;
                    int begin = strConfig.IndexOf("ZLNR");//指令内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        commandContent = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }

                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    //string oldContent = receive_data.Text;
                                    //receive_data.Text = commandContent + "\n" + oldContent;
                                });
                }
            }
        }

        private void GetAddSample(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string content = null;
                    string sampleID = null;
                    string sampleName = null;
                    int begin = strConfig.IndexOf("YPID");//样品ID
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        sampleID = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("YPMC");//样品名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        sampleName = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("WJNR");//文件内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        content = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    filename = sampleID + "(" + sampleName + ").txt";
                    if (filename != null && content != null)
                    {
                        FileUtils.WriteFile(fileLocation + "\\" + filename, content);
                    }
                    *//*if (sampleLibraryOperationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    sampleLibraryOperationWindow.UpdateAddSample(sampleID, sampleName);
                                });
                    }*//*
                }
            }
        }

        private void querySpecimenContentAndArgument(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string content = null;
                    int begin = strConfig.IndexOf("WJMC");//文件名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        filename = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("WJNR");//文件内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        content = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    if (filename != null && content != null)
                    {
                        FileUtils.WriteFile(fileLocation + "\\" + filename, content);
                    }
                    *//*if (sampleLibraryOperationWindow != null)
                    {
                        sampleLibraryOperationWindow.DownloadSampleContentFile();
                    }*//*
                }
            }
        }

        private void GetCurrentElement(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string element = null;
                    int begin = strConfig.IndexOf("XZYS");//选择元素
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        element = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    *//*if (focusOnElementsWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    focusOnElementsWindow.UpdateCurrentElement(element);
                                });
                    }*//*
                }
            }
        }

        private void GetCurrentSample(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string sampleID = null;
                    string sampleName = null;
                    int begin = strConfig.IndexOf("YPID");//样品ID
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        sampleID = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("YPMC");//样品名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        sampleName = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    *//*if (theSampleSelectionWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    theSampleSelectionWindow.UpdateCurrentSample(sampleID, sampleName);
                                });
                    }*//*
                }
            }
        }

        private void querySpecimenContent(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string content = null;
                    int begin = strConfig.IndexOf("WJMC");//文件名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        filename = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("WJNR");//文件内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        content = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    if (filename != null && content != null)
                    {
                        FileUtils.WriteFile(fileLocation + "\\" + filename, content);
                    }
                }
            }
        }

        private void SearchSpecimen(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.UTF8.GetString(data);
                if (strConfig != null)
                {
                    string specimenID = null;
                    string specimenName = null;
                    StringBuilder msb = new StringBuilder();
                    int begin = strConfig.IndexOf("YPID");//样品ID
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        specimenID = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("YPMC");//样品名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        specimenName = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    msb.Append(specimenID + "#" + specimenName + "#");
                    *//*if (validationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    validationWindow.UpdateSearchSpecimenList(msb.ToString());
                                });
                    }
                    if (standardizationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    standardizationWindow.UpdateSearchSpecimenList(msb.ToString());
                                });
                    }
                    if (theSampleSelectionWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    theSampleSelectionWindow.UpdateSearchSpecimenList(msb.ToString());
                                });
                    }
                    if (sampleLibraryOperationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    sampleLibraryOperationWindow.UpdateSearchSpecimenList(msb.ToString());
                                });
                    }*//*
                }
            }
        }

        private void DownloadResultData(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                StringBuilder sss = new StringBuilder();
                string strConfig = System.Text.Encoding.ASCII.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string content = null;
                    int begin = strConfig.IndexOf("WJMC");//文件名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        filename = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("JGNR");//结果内容
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        content = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    Console.WriteLine("filename:" + filename);
                    Console.WriteLine("content:" + content);
                    if (filename != null && content != null)
                    {
                        string fileName = filename.Substring(24);
                        FileUtils.WriteFile(fileLocation + "\\" + fileName, content);
                    }
                }
            }
            *//*if (statisticsWindow != null)
            {
                statisticsWindow.setSend(true);
            }*//*
        }

        private void SearchResult(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.UTF8.GetString(data);
                if (strConfig != null)
                {
                    string filename = null;
                    string tag = null;
                    string time = null;
                    string person = null;
                    StringBuilder msb = new StringBuilder();
                    int begin = strConfig.IndexOf("WJMC");//文件名称
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        filename = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("YPBQ");//样品标签
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        tag = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("LSSJ");//历史时间
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        time = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    begin = strConfig.IndexOf("JCRY");//检测人员
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        person = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                    }
                    msb.Append(filename + "#" + tag + "#" + time + "#" + person + "#");
                    *//*if (statisticsWindow != null /*&& statisticsWindow.IsActive*//*)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    statisticsWindow.UpdateSearchList(msb.ToString());
                                });
                    }
                    if (validationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    validationWindow.UpdateSearchList(msb.ToString());
                                });
                    }
                    if (standardizationWindow != null)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    standardizationWindow.UpdateSearchList(msb.ToString());
                                });
                    }*//*
                }
            }
        }

        public string SPECIMENTAG = null;
        private void GetSpecimenTag(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.UTF8.GetString(data);
                if (strConfig != null)
                {
                    int begin = strConfig.IndexOf("YPBQ");//样品标签
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        string count = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                        SPECIMENTAG = count;
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    //specimen_tag.Text = count;
                                });
                    }
                }
            }
        }

        string COLLECTTIME = null;
        private void GetCollecttime(byte[] data)
        {
            if (CheckUtils.CheckSum(data, true))
            {
                string strConfig = System.Text.Encoding.UTF8.GetString(data);
                if (strConfig != null)
                {
                    int begin = strConfig.IndexOf("CJSJ");//采集时间
                    if (begin >= 0)
                    {
                        int beginOfValue = strConfig.IndexOf("=", begin);
                        int endOfValue = strConfig.IndexOf(";", begin);
                        string count = strConfig.Substring(beginOfValue + 1, endOfValue - beginOfValue - 1);
                        COLLECTTIME = count;
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                                (Action)delegate ()
                                {
                                    //collect_time.Text = COLLECTTIME;
                                });
                    }
                }
            }
        }*/

    }
}
