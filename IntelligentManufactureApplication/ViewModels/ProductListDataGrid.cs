using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.ViewModels
{
    public class ProductListDataGrid : INotifyPropertyChanged
    {
        private bool _isSelected;
        private string _id;
        private string _materialsNumber;            //物料编号
        private string _drawingID;                  //图号.
        private string _productName;                //名称（产品名称）
        private string _makings;                    //材料
        private string _makingsNumber;              //材料代码
        private string _standard;                   //标准
        private string _heatTreatmentCode;          //热处理代号
        private string _materialStrength;           //材料屈服强度
        private string _checkGroup;                 //校验组别
        private string _heattreatmentStrength;      //热处理强度
        private string _source;                     //来源
        private string _type;                       //类型
        private string _specification;              //规格
        private string _blankSize;                  //单件毛胚尺寸
        private string _cuttingSize;                //下料尺寸
        private string _stelliteAndNitriding;       //司太立及氮化
        private string _remarks;                    //备注
        private string _savePath;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }
        public string SavePath
        {
            get => _savePath;
            set
            {
                if (_savePath == value) return;
                _savePath = value;
            }
        }
        public string Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }
        public string MaterialsNumber
        {
            get => _materialsNumber;
            set
            {
                if (_materialsNumber == value) return;
                _materialsNumber = value;
                OnPropertyChanged();
            }
        }
        public string DrawingID
        {
            get => _drawingID;
            set
            {
                if (_drawingID == value) return;
                _drawingID = value;
                OnPropertyChanged();
            }
        }
        public string ProductName
        {
            get => _productName;
            set
            {
                if (_productName == value) return;
                _productName = value;
                OnPropertyChanged();
            }
        }
        public string Makings
        {
            get => _makings;
            set
            {
                if (_makings == value) return;
                _makings = value;
                OnPropertyChanged();
            }
        }
        public string MakingsNumber
        {
            get => _makingsNumber;
            set
            {
                if (_makingsNumber == value) return;
                _makingsNumber = value;
                OnPropertyChanged();
            }
        }
        public string Standard
        {
            get => _standard;
            set
            {
                if (_standard == value) return;
                _standard = value;
                OnPropertyChanged();
            }
        }
        public string HeatTreatmentCode
        {
            get => _heatTreatmentCode;
            set
            {
                if (_heatTreatmentCode == value) return;
                _heatTreatmentCode = value;
                OnPropertyChanged();
            }
        }
        public string MaterialStrength
        {
            get => _materialStrength;
            set
            {
                if (_materialStrength == value) return;
                _materialStrength = value;
                OnPropertyChanged();
            }
        }
        public string CheckGroup
        {
            get => _checkGroup;
            set
            {
                if (_checkGroup == value) return;
                _checkGroup = value;
                OnPropertyChanged();
            }
        }
        public string HeattreatmentStrength
        {
            get => _heattreatmentStrength;
            set
            {
                if (_heattreatmentStrength == value) return;
                _heattreatmentStrength = value;
                OnPropertyChanged();
            }
        }
        public string Source
        {
            get => _source;
            set
            {
                if (_source == value) return;
                _source = value;
                OnPropertyChanged();
            }
        }
        public string Type
        {
            get => _type;
            set
            {
                if (_type == value) return;
                _type = value;
                OnPropertyChanged();
            }
        }
        public string Specification
        {
            get => _specification;
            set
            {
                if (_specification == value) return;
                _specification = value;
                OnPropertyChanged();
            }
        }
        public string BlankSize
        {
            get => _blankSize;
            set
            {
                if (_blankSize == value) return;
                _blankSize = value;
                OnPropertyChanged();
            }
        }
        public string CuttingSize
        {
            get => _cuttingSize;
            set
            {
                if (_cuttingSize == value) return;
                _cuttingSize = value;
                OnPropertyChanged();
            }
        }
        public string StelliteAndNitriding
        {
            get => _stelliteAndNitriding;
            set
            {
                if (_stelliteAndNitriding == value) return;
                _stelliteAndNitriding = value;
                OnPropertyChanged();
            }
        }
        public string Remarks
        {
            get => _remarks;
            set
            {
                if (_remarks == value) return;
                _remarks = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
