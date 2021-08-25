using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.DBModels
{
    /// <summary>
    /// 1级
    /// </summary>
    public class TCategory
    {
        [Key]
        public string CategoryID { set; get; }          //类别ID（Key）,唯一标识
        public string CategoryName { get; set; }        //类别名称
        public bool ChildenID { get; set; }             //标记是否存在子目录/文件，true表示存在，false表示不存在.
        public string SavePath { get; set; }            //文件保存路径
    }
}
