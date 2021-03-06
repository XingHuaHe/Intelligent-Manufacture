using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.ViewModels
{
    public class ProcessDataGrid
    {
        public string Id { get; set; }                      //对应ProductList
        public string ProcessID { get; set; }               //
        public string ProcessNumber { get; set; }           //工序号
        public string ProcessName { get; set; }             //工序名称
        public string Applicability { get; set; }           //适用机床
        public string Hours { get; set; }                   //工时
        public string UnitPrice { get; set; }               //单价
        public string SavePath { get; set; }                //保存路径
    }
}
