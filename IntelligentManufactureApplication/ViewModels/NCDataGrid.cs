using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.ViewModels
{
    public class NCDataGrid
    {
        public string Id { get; set; }
        public string ProcessID { get; set; }
        public string PartID { get; set; }                  //零件ID（5级）


        public string NCProgramID { get; set; }             //NC程序ID
        public string NCProgramName { get; set; }           //NC程序名称
        public string FileVersion { set; get; }             //文件版本
        public string CheckState { get; set; }              //审核状态
        public string CheckTime { get; set; }               //审核时间
        public string CheckedUser { get; set; }             //审核人
        public string ModifiedTimee { get; set; }           //修改时间(上一次)
        public string ModifiedUser { get; set; }            //上传者（修改者）
        public string SavePath { get; set; }                //文件保存路径
    }
}
