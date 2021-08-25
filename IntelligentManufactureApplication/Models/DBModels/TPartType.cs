using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.DBModels
{
    /// <summary>
    /// 4级
    /// </summary>
    public class TPartType
    {
        [Key]
        public string PartTypeID { get; set; }
        public string PartTypeName { get; set; }
        public string ParentID { get; set; }
        public bool ChildenID { get; set; }
        public string SavePath { get; set; }            //文件保存路径
    }
}
