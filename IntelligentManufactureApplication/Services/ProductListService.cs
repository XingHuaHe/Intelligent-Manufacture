using IntelligentManufactureApplication.DBContext;
using IntelligentManufactureApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Services
{
    public class ProductListService : INotifyPropertyChanged
    {
        public ObservableCollection<ProductListDataGrid> Items1 { get; set; }

        public ProductListService(string name)
        {
            Items1 = CreateData(name);
        }
        public ProductListService()
        {
            Items1 = null;
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="name"></param>
        public void InitData(string name)
        {
            Items1 = CreateData(name);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public void DeletedItem(string id)
        {
            try
            {
                if (Items1.Count > 0)
                {
                    var item = (from obj in Items1
                                where obj.Id == id
                                select obj).SingleOrDefault();
                    if (item != null)
                    {
                        Items1.Remove(item);
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="id"></param>
        public bool AddItem(ProductListDataGrid item)
        {
            Items1.Add(item);
            return true;
        }

        public bool? IsAllItems1Selected
        {
            get
            {
                if (Items1 != null)
                {
                    var selected = Items1.Select(item => item.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                else
                {
                    return null;
                }

            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items1);
                    OnPropertyChanged();
                }
            }
        }
        private static void SelectAll(bool select, IEnumerable<ProductListDataGrid> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        public ObservableCollection<ProductListDataGrid> CreateData(string name)
        {
            ObservableCollection<ProductListDataGrid> Items = new ObservableCollection<ProductListDataGrid>();
            try
            {
                using (MonitoringContext monitoringContext = new MonitoringContext())
                {
                    var linq = (from obj in monitoringContext.ProductLists
                                where obj.PartID == name
                                select obj).ToList();
                    if (linq.Count > 0)
                    {
                        foreach (var item in linq)
                        {
                            ProductListDataGrid productListDataGrid = new ProductListDataGrid()
                            {
                                IsSelected = false,
                                Id = item.Id,
                                MaterialsNumber = item.MaterialsNumber,
                                DrawingID = item.DrawingID,
                                ProductName = item.ProductName,
                                Makings = item.Makings,
                                MakingsNumber = item.MakingsNumber,
                                Standard = item.Standard,
                                HeatTreatmentCode = item.HeatTreatmentCode.ToString(),
                                MaterialStrength = item.MaterialStrength,
                                CheckGroup = item.CheckGroup,
                                HeattreatmentStrength = item.HeattreatmentStrength,
                                Source = item.Source,
                                Type = item.Type,
                                Specification = item.Specification,
                                BlankSize = item.BlankSize,
                                CuttingSize = item.CuttingSize,
                                StelliteAndNitriding = item.StelliteAndNitriding,
                                Remarks = item.Remarks,
                                SavePath = item.SavePath
                            };
                            Items.Add(productListDataGrid);
                        }
                    }
                }
            }
            catch
            {

            }

            return Items;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
