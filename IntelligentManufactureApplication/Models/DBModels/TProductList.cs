using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.DBModels
{
    public class TProductList
    {
        public string PartID { get; set; }                      //父节点（对应Treeview的控件Name）
        [Key]
        public string Id { get; set; }                          //
        public string MaterialsNumber { get; set; }             //物料编号
        public string DrawingID { get; set; }                   //图号.
        public string ProductName { get; set; }                 //名称（产品名称）
        public string Makings { get; set; }                     //材料
        public string MakingsNumber { get; set; }               //材料代码
        public string Standard { get; set; }                    //标准
        public string HeatTreatmentCode { get; set; }              //热处理代号
        public string MaterialStrength { get; set; }            //材料屈服强度
        public string CheckGroup { get; set; }                  //校验组别
        public string HeattreatmentStrength { get; set; }       //热处理强度
        public string Source { get; set; }                      //来源
        public string Type { get; set; }                        //类型
        public string Specification { get; set; }               //规格
        public string BlankSize { get; set; }                   //单件毛胚尺寸
        public string CuttingSize { get; set; }                 //下料尺寸
        public string StelliteAndNitriding { get; set; }        //司太立及氮化
        public string Remarks { get; set; }                     //备注
        public string SavePath { get; set; }                    //文件保存路径
    }
}
