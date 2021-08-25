using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.DBModels
{
    /// <summary>
    /// 5级
    /// </summary>
    public class TPart
    {
        [Key]
        public string PartID { get; set; }              //零件ID（Key）,唯一表示
        public string PartName { get; set; }            //零件名称
        public string ParentID { get; set; }            //父目录ID
        public bool ChildenID { get; set; }             //标记是否存在子目录/文件，true表示存在，false表示不存在.
        public string SavePath { get; set; }            //文件保存路径
    }
}
