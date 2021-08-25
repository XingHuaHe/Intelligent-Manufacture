using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.DBModels
{
    public class TDrawing
    {
        public string Id { get; set; }
        public string CategoryID { get; set; }          //所属类别ID（1级）
        public string ComponentTypeID { get; set; }     //加工类型（2级）
        public string ComponentID { get; set; }         //所属部件ID（3级）
        public string PartTypeID { get; set; }          //所属零件类型（4级）
        public string PartID { get; set; }              //零件ID（5级）
        [Key]
        public string DrawingID { get; set; }           //图纸ID
        public string DrawingName { get; set; }         //图纸名称
        public string FileVersion { set; get; }         //文件版本
        public bool CheckState { get; set; }            //审核状态
        public string CheckTime { get; set; }           //审核时间
        public string CheckedUser { get; set; }         //审核人
        public string ModifiedTimee { get; set; }       //修改时间(上一次)
        public string ModifiedUser { get; set; }        //上传者（修改者）
        public string SavePath { get; set; }            //文件保存路径
    }
}
