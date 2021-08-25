using IntelligentManufactureApplication.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentManufactureApplication.DBContext
{
    public class MonitoringContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConfigurationManager.ConnectionStrings["Monitoring"].ConnectionString
            optionsBuilder.UseNpgsql("Server = 127.0.0.1; Database = Monitoring; Port = 5432; User ID = postgres; Password = postgresql123",
                options => options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null));
        }

        public DbSet<TUser> Users { get; set; }                         //用户信息表
        public DbSet<TCategory> Categories { get; set; }                //类别表（1级）
        public DbSet<TComponentType> ComponentTypes { get; set; }       //部件类型表（2级）
        public DbSet<TComponent> Components { get; set; }               //部件表（3级）
        public DbSet<TPartType> PartTypes { get; set; }                 //零件类型表（4级）
        public DbSet<TPart> Parts { get; set; }                         //零件表（5级）
        public DbSet<TProductList> ProductLists { get; set; }           //产品表
        public DbSet<TDrawing> Drawings { get; set; }                   //图纸
        public DbSet<TCraft> Crafts { get; set; }                       //工艺卡
        public DbSet<TNCProgram> NCPrograms { get; set; }              //NC程序
        public DbSet<TProcess> Processes { get; set; }                  //工序
    }
}
