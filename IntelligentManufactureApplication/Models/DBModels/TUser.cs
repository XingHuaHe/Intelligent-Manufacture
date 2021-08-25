using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.Models.DBModels
{
    public class TUser
    {
        [Key]
        [MaxLength(8)]
        public int UserID { get; set; }          //用户账号
        [MaxLength(20)]
        public string UserPasswd { get; set; }      //密码
        public string UserName { get; set; }        //用户名
        public string HeadPortrait { get; set; }    //用户头像
        public string Position { get; set; }        //岗位
        public string Level { get; set; }           //用户等级(权限)
        public bool OnlineState { set; get; }       //状态（登录）
    }
}
