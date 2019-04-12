using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment.Data
{
    public class HouseContext : DbContext
    {
        private readonly static string Connection_String = "name=OAConnecting";

        public HouseContext()
            : base(Connection_String)
        {
            try
            {
                this.Configuration.ProxyCreationEnabled = false;//禁用代理，提高序列化速度
            }
            finally
            {
                this.Configuration.ProxyCreationEnabled = true;
            }
        }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<HouseInfo> HouseInfo { get; set; }
        public DbSet<AreaInfo> AreaInfo { get; set; }
        public DbSet<TenantsInfo> TenantsInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除复数表名的契约
        }

        public virtual void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
    }
}