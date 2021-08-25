using IntelligentManufactureApplication.DBContext;
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
    /// RenameTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class RenameTreeView : Window
    {
        private string _name { get; set; }
        private string _header { get; set; }
        private string _newheader { get; set; }
        public RenameTreeView(string name, string header)
        {
            InitializeComponent();

            _name = name;
            _header = header.Trim();
            this.RenameText.Text = header.Trim();
            _newheader = string.Empty;
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(this.RenameText.Text.Trim() == _header)
                {
                    _newheader = _header;
                }
                else
                {
                    _newheader = this.RenameText.Text.Trim();
                    int row = 0;
                    switch (_name.Split('_')[0])
                    {
                        case "Cg":
                            using(MonitoringContext monitoringContext = new MonitoringContext())
                            {
                                var linq = (from obj in monitoringContext.Categories
                                            where obj.CategoryID == _name
                                            select obj).SingleOrDefault();
                                linq.CategoryName = _newheader;
                                row = monitoringContext.SaveChanges();
                            }
                            if (row > 0)
                            {
                                this.Close();
                            }
                            break;
                        case "CT":
                            using(MonitoringContext monitoringContext = new MonitoringContext())
                            {
                                var linq = (from obj in monitoringContext.ComponentTypes
                                            where obj.ComponentTypeID == _name
                                            select obj).SingleOrDefault();
                                linq.ComponentTypeName = _newheader;
                                row = monitoringContext.SaveChanges();
                            }
                            if (row > 0)
                            {
                                this.Close();
                            }
                            break;
                        case "C":
                            using (MonitoringContext monitoringContext = new MonitoringContext())
                            {
                                var linq = (from obj in monitoringContext.Components
                                            where obj.ComponentID == _name
                                            select obj).SingleOrDefault();
                                linq.ComponentName = _newheader;
                                row = monitoringContext.SaveChanges();
                            }
                            if (row > 0)
                            {
                                this.Close();
                            }
                            break;
                        case "PT":
                            using (MonitoringContext monitoringContext = new MonitoringContext())
                            {
                                var linq = (from obj in monitoringContext.PartTypes
                                            where obj.PartTypeID == _name
                                            select obj).SingleOrDefault();
                                linq.PartTypeName = _newheader;
                                row = monitoringContext.SaveChanges();
                            }
                            if (row > 0)
                            {
                                this.Close();
                            }
                            break;
                        case "P":
                            using (MonitoringContext monitoringContext = new MonitoringContext())
                            {
                                var linq = (from obj in monitoringContext.Parts
                                            where obj.PartID == _name
                                            select obj).SingleOrDefault();
                                linq.PartName = _newheader;
                                row = monitoringContext.SaveChanges();
                            }
                            if (row > 0)
                            {
                                this.Close();
                            }
                            break;
                    }
                }
            }
            catch
            {

            }
        }

        public string GetNewName()
        {
            if (_newheader != string.Empty)
            {
                return _newheader;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
