using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.ViewModels
{
    public class DrawingDataGrid
    {
        public string Id { get; set; }
        public string PartID { get; set; }              //零件ID（5级）

        public string DrawingID { get; set; }           //图纸ID
        public string DrawingName { get; set; }         //图纸名称
        public string FileVersion { set; get; }         //文件版本
        public string CheckState { get; set; }          //审核状态
        public string CheckTime { get; set; }           //审核时间
        public string CheckedUser { get; set; }         //审核人
        public string ModifiedTimee { get; set; }       //修改时间(上一次)
        public string ModifiedUser { get; set; }        //上传者（修改者）
        public string SavePath { get; set; }            //文件保存路径
    }
}
