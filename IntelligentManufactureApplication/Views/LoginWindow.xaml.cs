using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.DBModels;
using IntelligentManufactureApplication.Models.LoginModels;
using IntelligentManufactureApplication.Models.StaticModels;
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
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 生成文件存储目录
        /// </summary>
        public void Init()
        {
            string rootpath = AppDomain.CurrentDomain.BaseDirectory;
            string[] strArray = rootpath.Split('\\');
            string path = strArray[0] + "\\";
            for (int i = 1; i < strArray.Length-4; i++)
            {
                path = System.IO.Path.Combine(path, strArray[i]);
            }
            StaticPathVariable.StartUpPath = path;
            StaticPathVariable.FileDialogRootPath = path;
            string filename = "files";
            StaticPathVariable.FileSaveRootPath = System.IO.Path.Combine(StaticPathVariable.StartUpPath, filename);
            if (!System.IO.Directory.Exists(StaticPathVariable.FileSaveRootPath))
            {
                System.IO.Directory.CreateDirectory(StaticPathVariable.FileSaveRootPath);
            }

        }

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

        private void Min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (this.userName.Text == string.Empty || this.userPassword.ToString() == string.Empty)
            {
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Warning;
                MessageBoxResult messageBoxResult = MessageBox.Show("输入信息不能为空！", null, messageBoxButton, messageBoxImage);
            }
            else
            {
                UserLogin userLogin = new UserLogin
                {
                    UserID = this.userName.Text,
                    UserPasswd = this.userPassword.Password
                };
                try
                {
                    using (MonitoringContext _monitoringContext = new MonitoringContext())
                    {
                        var linq = (from obj in _monitoringContext.Users
                                    where obj.UserID == int.Parse(userLogin.UserID)
                                    select obj).SingleOrDefault();
                        if (linq != null)
                        {
                            if (linq.OnlineState == true)
                            {
                                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                                MessageBoxResult messageBoxResult = MessageBox.Show("账户已经在线！", null, messageBoxButton, messageBoxImage);
                            }
                            else if (linq.UserPasswd == userLogin.UserPasswd)
                            {
                                StaticUserInfo.UserID = linq.UserID;
                                StaticUserInfo.UserName = linq.UserName;
                                StaticUserInfo.HeadPortrait = linq.HeadPortrait;
                                StaticUserInfo.Position = linq.Position;
                                StaticUserInfo.Level = linq.Level;

                                /*linq.OnlineState = true;
                                int row = _monitoringContext.SaveChanges();*/
                                int row = 1;
                                if (row > 0)
                                {
                                    MainWindow mainWindow = new MainWindow();
                                    this.Close();
                                    mainWindow.Show();
                                }
                            }
                            else
                            {
                                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                                MessageBoxResult messageBoxResult = MessageBox.Show("密码错误！", null, messageBoxButton, messageBoxImage);
                            }

                        }
                        else
                        {
                            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                            MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                            MessageBoxResult messageBoxResult = MessageBox.Show("账户不存在！", null, messageBoxButton, messageBoxImage);
                        }
                    }
                }
                catch
                {
                    using (MonitoringContext _monitoringContext = new MonitoringContext())
                    {
                        var linq = (from obj in _monitoringContext.Users
                                    where obj.UserID == int.Parse(userLogin.UserID)
                                    select obj).SingleOrDefault();
                        linq.OnlineState = false;
                        _monitoringContext.SaveChanges();
                    }
                }
            }
        }
    }
}
