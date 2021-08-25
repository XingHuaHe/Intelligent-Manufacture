using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.DBModels;
using IntelligentManufactureApplication.ViewModels;
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
    /// ProcessNewRow.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessNewRow : Window
    {
        public string _NCPath { get; set; }
        public string _id { get; set; }
        ProcessDataGrid processItem = new ProcessDataGrid();
        public ProcessNewRow(string NCPath, string id)
        {
            InitializeComponent();
            _NCPath = NCPath;
            _id = id;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //创建文件夹
                string processID = string.Format("Proc_{0}", System.DateTime.Now.ToString("dd_hh_mm_ss_ffff"));
                string savePath = System.IO.Path.Combine(_NCPath, processID);
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                //写入数据库
                TProcess process = new TProcess()
                {
                    Id = _id,
                    ProcessID = processID,
                    ProcessNumber = this.ProcessIDTextBox.Text,
                    ProcessName = this.ProcessNameTextBox.Text,
                    Applicability = this.ApplicabilityTextBox.Text,
                    Hours = this.HoursTextBox.Text,
                    UnitPrice = this.UnitPriceTextBox.Text,
                    SavePath = savePath
                };
                int row = 0;
                using(MonitoringContext monitoringContext = new MonitoringContext())
                {
                    monitoringContext.Processes.Add(process);
                    row = monitoringContext.SaveChanges();
                }
                //添加显示条目
                if (row > 0)
                {
                    ProcessDataGrid processDataGrid = new ProcessDataGrid()
                    {
                        Id = process.Id,
                        ProcessID = process.ProcessID,
                        ProcessNumber = process.ProcessNumber,
                        ProcessName = process.ProcessName,
                        Applicability = process.Applicability,
                        Hours = process.Hours,
                        UnitPrice = process.UnitPrice,
                        SavePath = process.SavePath
                    };
                    processItem = processDataGrid;
                }
                this.Close();
            }
            catch
            {

            }
            
        }

        public ProcessDataGrid GetItem()
        {
            return processItem;
        }
    }
}
