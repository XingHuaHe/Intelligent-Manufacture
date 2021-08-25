using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.StaticModels
{
    /// <summary>
    /// 保存用户信息（登录传进）
    /// </summary>
    public static class StaticUserInfo
    {
        static StaticUserInfo()
        {

        }
        public static int UserID { get; set; }          //用户账号
        public static string UserName { get; set; }        //用户名
        public static string HeadPortrait { get; set; }    //用户头像
        public static string Position { get; set; }        //岗位
        public static string Level { get; set; }           //用户等级(权限)
    }
}
