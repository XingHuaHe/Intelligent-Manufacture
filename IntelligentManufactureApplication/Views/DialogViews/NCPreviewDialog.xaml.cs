using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.StaticModels;
using IntelligentManufactureApplication.ViewModels;
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
using System.Windows.Shapes;

namespace IntelligentManufactureApplication.Views.DialogViews
{
    /// <summary>
    /// NCPreviewDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NCPreviewDialog : Window
    {
        public NCDataGrid _item { get; set; }
        public NCPreviewDialog(NCDataGrid item)
        {
            InitializeComponent();

            _item = item;
            Init(item);
        }
        private void Init(NCDataGrid item)
        {
            try
            {
                //清空显示界面.
                this.EditRichTextBox.Document.Blocks.Clear();
                this.FileNameTextBlock.Text = string.Empty;

                this.FileNameTextBlock.Text = "文件名称：" + item.NCProgramName;

                using (StreamReader streamReader = File.OpenText(item.SavePath))
                {
                    var text = streamReader.ReadToEnd();
                    Run run = new Run(text);
                    Paragraph paragraph = new Paragraph();
                    paragraph.Inlines.Add(run);
                    this.EditRichTextBox.Document.Blocks.Add(paragraph);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 窗口移动
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
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 取消按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 通过审核按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = 0;
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.NCPrograms
                                where obj.NCProgramID == _item.NCProgramID
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
            catch
            {

            }
        }
    }
}
