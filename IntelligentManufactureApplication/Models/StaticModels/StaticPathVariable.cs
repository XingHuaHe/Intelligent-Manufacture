using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.StaticModels
{
    public static class StaticPathVariable
    {
        /// <summary>
        /// 程序启动路径
        /// </summary>
        public static string StartUpPath { get; set; }
        /// <summary>
        /// 文件保存根目录
        /// </summary>
        public static string FileSaveRootPath { get; set; }
        /// <summary>
        /// 文件打开对话框路径(默认与StartUpPath一致)
        /// </summary>
        public static string FileDialogRootPath { get; set; }
    }
}
