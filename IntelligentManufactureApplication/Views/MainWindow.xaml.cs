using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.DBModels;
using IntelligentManufactureApplication.Models.LoginModels;
using IntelligentManufactureApplication.Models.StaticModels;
using IntelligentManufactureApplication.Views.UserControlViews;
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
using System.Windows.Shapes;

namespace IntelligentManufactureApplication.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 窗口缩小放大（True表示放大,False表示缩小）
        /// </summary>
        private static bool WindowRestore { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            WindowRestore = false;//缩小
            Init();
            FilesManagment filesManagment = new FilesManagment(this.ActualHeight, this.ActualWidth);
            this.StackPanelRight.Children.Add(filesManagment);
        }
        /// <summary>
        /// 界面初始化
        /// </summary>
        private void Init()
        {
            this.UserNameTextBlock.Text = StaticUserInfo.UserName;
            try
            {
                //头像初始化.
                if (StaticUserInfo.HeadPortrait != null)
                {
                    this.HeadPortrait.Source = new BitmapImage(new Uri(StaticUserInfo.HeadPortrait, UriKind.Absolute));
                }
                else
                {
                    this.HeadPortrait.Source = new BitmapImage(new Uri("../Images/img.jpg", UriKind.Relative));
                }

                switch (StaticUserInfo.Level)
                {
                    case "11":
                        //测试码
                        this.LeftStackPanel.Children.Remove(this.MonitoringAnalysis);
                        this.LeftStackPanel.Children.Remove(this.EquipmentManagement);
                        this.LeftStackPanel.Children.Remove(this.BasicInfoExpander);
                        this.ManufacturingStackPanel.Children.Remove(this.flag1);
                        this.ManufacturingStackPanel.Children.Remove(this.flag2);

                        break;
                    case "01":
                        //管理部门
                        break;
                    case "02":
                        //市场部门
                        break;
                    case "03":
                        //质量部门
                        break;
                    case "04":
                        //技术部门
                        this.LeftStackPanel.Children.Remove(this.BasicInfoExpander);
                        break;
                    case "10":
                        //经理/董事长
                        break;
                }
            }
            catch (Exception ex)
            {
                //ex.StackTrace;
            }
        }
        /// <summary>
        /// 窗口移动.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowsMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    DragMove();
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 最小化（按键）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 窗口放大缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WindowRestore == false)
                {
                    //原来是缩小，现在放大.
                    WindowState = WindowState.Maximized;
                    WindowRestore = true;
                    this.LeftScrollViewer.Height = this.ActualHeight - 30.0 - 50.0;

                    this.StackPanelRight.Children.Clear();
                    FilesManagment filesManagment = new FilesManagment(this.ActualHeight - 80.0, this.ActualWidth - 200.0);
                    this.StackPanelRight.Children.Add(filesManagment);
                }
                else
                {
                    //原来放大，现在缩小(还原).
                    WindowState = WindowState.Normal;
                    this.LeftScrollViewer.Height = this.ActualHeight - 30.0 - 50.0;
                    WindowRestore = false;

                    this.StackPanelRight.Children.Clear();
                    FilesManagment filesManagment = new FilesManagment(this.ActualHeight - 80.0, this.ActualWidth - 200.0);
                    this.StackPanelRight.Children.Add(filesManagment);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 关闭窗口（按键）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MonitoringContext _monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in _monitoringContext.Users
                                where obj.UserID == StaticUserInfo.UserID
                                select obj).SingleOrDefault();
                    if (linq != null)
                    {
                        linq.OnlineState = false;
                        _monitoringContext.SaveChanges();
                    }
                    this.Close();
                }
            }
            catch
            {
                using (MonitoringContext _monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in _monitoringContext.Users
                                where obj.UserID == StaticUserInfo.UserID
                                select obj).SingleOrDefault();
                    linq.OnlineState = false;
                    _monitoringContext.SaveChanges();
                }
                this.Close();
            }
        }
        /// <summary>
        /// 注销登录（按键）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MonitoringContext _monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in _monitoringContext.Users
                                where obj.UserID == StaticUserInfo.UserID
                                select obj).SingleOrDefault();
                    if (linq != null)
                    {
                        linq.OnlineState = false;
                        int row = _monitoringContext.SaveChanges();
                        if (row > 0)
                        {
                            LoginWindow login = new LoginWindow();
                            this.Close();
                            login.Show();
                        }
                        else
                        {
                            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                            MessageBoxResult messageBoxResult = MessageBox.Show("注销错误！", null, messageBoxButton, messageBoxImage);
                        }
                    }
                    else
                    {
                        MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                        MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                        MessageBoxResult messageBoxResult = MessageBox.Show("注销错误！", null, messageBoxButton, messageBoxImage);
                    }
                }
            }
            catch (Exception ex)
            {
                LoginWindow login = new LoginWindow();
                this.Close();
                login.Show();
            }
        }

        private void MonitoringStackPanl_Click(object sender, RoutedEventArgs e)
        {
            RadioButton button = e.OriginalSource as RadioButton;
            this.StackPanelRight.Children.Clear();
            if (button.Content.ToString().Equals("生产状态"))
            {
                ManufacturingState manufacturingState = new ManufacturingState();
                this.StackPanelRight.Children.Add(manufacturingState);
            }
        }

        private void CommodityStackPanl_Click(object sender, RoutedEventArgs e)
        {
            RadioButton button = e.OriginalSource as RadioButton;
            this.StackPanelRight.Children.Clear();
            if (button.Content.ToString().Equals("程序管理"))
            {
                FilesManagment filesManagment = new FilesManagment(this.ActualHeight - 80.0, this.ActualWidth - 200.0);
                this.StackPanelRight.Children.Add(filesManagment);
            }
        }

        private void ManufacturingStackPanel_Click(object sender, RoutedEventArgs e)
        {
            RadioButton button = e.OriginalSource as RadioButton;
            this.StackPanelRight.Children.Clear();
            if (button.Content.ToString().Equals("DNC程序管理"))
            {

                DNCManagment dNCManagment = new DNCManagment(this.ActualHeight - 80.0, this.ActualWidth - 200.0);
                this.StackPanelRight.Children.Add(dNCManagment);
            }
        }
    }
}
