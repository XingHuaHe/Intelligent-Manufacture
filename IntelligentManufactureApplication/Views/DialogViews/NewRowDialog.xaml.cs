using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.Models.DBModels;
using IntelligentManufactureApplication.Services;
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
    /// NewRowDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewRowDialog : Window
    {
        ProductListService _productListService = new ProductListService();
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
            }
        }
        public NewRowDialog(string name , ProductListService productListService)
        {
            InitializeComponent();
            Id = name;
            _productListService = productListService;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filepath = string.Empty;
                string productListId = System.DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.Parts
                                where obj.PartID == _id
                                select obj).SingleOrDefault();
                    filepath = System.IO.Path.Combine(linq.SavePath, productListId);
                }
                if (!System.IO.Directory.Exists(filepath))
                {
                    //创建文件夹
                    System.IO.Directory.CreateDirectory(filepath);
                }
                TProductList productList = new TProductList()
                {
                    PartID = _id,
                    Id = productListId,
                    MaterialsNumber = this.MaterialsNumberTextBox.Text.Trim(),
                    DrawingID = this.DrawingNumTextBox.Text.Trim(),
                    ProductName = this.ProductNameTextBox.Text,
                    Makings = this.MakingsTextBox.Text.Trim(),
                    MakingsNumber = this.MakingsNumberTextBox.Text.Trim(),
                    Standard = this.StandardTextBox.Text.Trim(),
                    HeatTreatmentCode = this.HeatTreatmentCodeTextBox.Text.Trim(),
                    MaterialStrength = this.MaterialStrengthTextBox.Text.Trim(),
                    CheckGroup = this.CheckGroupTextBox.Text.Trim(),
                    HeattreatmentStrength = this.HeattreatmentStrengthTextBox.Text.Trim(),
                    Source = this.SourceTextBox.Text.Trim(),
                    Type = this.TypeTextBox.Text.Trim(),
                    Specification = this.SpecificationTextBox.Text.Trim(),
                    BlankSize = this.BlankSizeTextBox.Text.Trim(),
                    CuttingSize = this.CuttingSizeTextBox.Text.Trim(),
                    StelliteAndNitriding = this.StelliteAndNitridingTextBox.Text.Trim(),
                    Remarks = this.RemarksTextBox.Text.Trim(),
                    SavePath = filepath
                };
                int row = 0;
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    monitoringContext.ProductLists.Add(productList);
                    row = monitoringContext.SaveChanges();
                }
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
                    HeatTreatmentCode = productList.HeatTreatmentCode,
                    MaterialStrength = productList.MaterialStrength,
                    CheckGroup = productList.CheckGroup,
                    HeattreatmentStrength = productList.HeattreatmentStrength,
                    Source = productList.Source,
                    Type = productList.Type,
                    Specification = productList.Specification,
                    BlankSize = productList.BlankSize,
                    CuttingSize = productList.CuttingSize,
                    StelliteAndNitriding = productList.StelliteAndNitriding,
                    Remarks = productList.Remarks,
                    SavePath = filepath
                };
                bool flag = _productListService.AddItem(productListDataGrid);

                if (row > 0 && flag == true)
                {
                    MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                    MessageBoxImage messageBoxImage = MessageBoxImage.Information;
                    MessageBoxResult messageBoxResult = MessageBox.Show("OK!", null, messageBoxButton, messageBoxImage);
                }
                this.Close();
            }
            catch
            {

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
