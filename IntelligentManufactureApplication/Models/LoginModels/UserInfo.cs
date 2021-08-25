using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.LoginModels
{
    public class UserInfo
    {
        public int UserID { get; set; }          //用户账号
        public string UserName { get; set; }        //用户名
        public string HeadPortrait { get; set; }    //用户头像
        public string Position { get; set; }        //岗位
        public string Level { get; set; }           //用户等级(权限)
    }
}
