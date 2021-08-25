using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.StaticModels;
using IntelligentManufactureApplication.ViewModels;
using Microsoft.Win32;
using MoonPdfLib;
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

namespace IntelligentManufactureApplication.Views.DialogViews
{
    /// <summary>
    /// DrawingPreviewDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DrawingPreviewDialog : Window
    {
        private bool _isLoaded = false;
        public DrawingDataGrid _item { get; set; }
        public DrawingPreviewDialog(DrawingDataGrid item)
        {
            InitializeComponent();

            _item = item;
            Init();
        }
        private void Init()
        {
            try
            {
                MoonPdfPanel moonPdfPanel = new MoonPdfPanel();
                /*moonPdfPanel.ViewType = "SinglePage";
                moonPdfPanel.PageRowDisplay = "ContinuousPageRows";
                moonPdfPanel.PageMargin = "0,2,4,2";*/
                moonPdfPanel.AllowDrop = true;
                moonPdfPanel.OpenFile(_item.SavePath);
                this.ShowBorder.Child = moonPdfPanel;

                this.RegisterName("moonPdfPanel", moonPdfPanel);
                _isLoaded = true;
            }
            catch (Exception)
            {
                _isLoaded = false;
            }
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                MoonPdfPanel moonPdfPanel = this.FindName("moonPdfPanel") as MoonPdfPanel;
                moonPdfPanel.ZoomIn();
            }
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                MoonPdfPanel moonPdfPanel = this.FindName("moonPdfPanel") as MoonPdfPanel;
                moonPdfPanel.ZoomOut();
            }
        }

        private void NormalButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                MoonPdfPanel moonPdfPanel = this.FindName("moonPdfPanel") as MoonPdfPanel;
                moonPdfPanel.Zoom(1.0);
            }
        }

        private void FitToHeightButton_Click(object sender, RoutedEventArgs e)
        {
            MoonPdfPanel moonPdfPanel = this.FindName("moonPdfPanel") as MoonPdfPanel;
            moonPdfPanel.ZoomToHeight();
        }

        private void SinglePageButton_Click(object sender, RoutedEventArgs e)
        {
            MoonPdfPanel moonPdfPanel = this.FindName("moonPdfPanel") as MoonPdfPanel;
            moonPdfPanel.ViewType = MoonPdfLib.ViewType.SinglePage;
        }

        private void FacingButton_Click(object sender, RoutedEventArgs e)
        {
            MoonPdfPanel moonPdfPanel = this.FindName("moonPdfPanel") as MoonPdfPanel;
            moonPdfPanel.ViewType = MoonPdfLib.ViewType.Facing;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void ReviewButton_Click(object sender, RoutedEventArgs e)
        {
            int row = 0;
            using(MonitoringContext monitoringContext = new MonitoringContext())
            {
                var linq = (from obj in monitoringContext.Drawings
                            where obj.DrawingID == _item.DrawingID
                            select obj).SingleOrDefault();
                linq.CheckState = true;
                linq.CheckedUser = StaticUserInfo.UserName;
                linq.CheckTime = System.DateTime.Now.ToString("yyyy_MM_dd_hh");
                row = monitoringContext.SaveChanges();
            }
            if (row > 0)
            {
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                MessageBoxResult messageBoxResult = MessageBox.Show("通过审核！", null, messageBoxButton, messageBoxImage);
                this.Close();
            }
        }
    }
}
