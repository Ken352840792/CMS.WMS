using EntityFramework.Extensions;
using Sy.Core;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;

namespace Sy.Repository
{
    public class EFDbContext : DbContext
    {
        #region 模型


        #endregion
        public  IIocResolver IResolver { get; set; }
        //public IDataLogCache DataLogCache { get; set; }
        //public Sy.Data.Interface.ISystemConfig SystemConfig { get; set; }
        //public ILog Log { get; set; }

        public EFDbContext(IIocResolver IResolver) : base(GetConnectionString())
        {
            this.IResolver = IResolver;
            Database.SetInitializer<EFDbContext>(null);
            this.Configuration.ProxyCreationEnabled = false;
           
        }
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //反射配置
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith("_Config"))
                                .Where(type => type.BaseType != null && type.BaseType.GetGenericTypeDefinition() == typeof(BaseConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);

        }

        /// <summary>
        /// 批量删除（与上下文不保持同一个事务）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> where) where T : class
        {
            return base.Set<T>().Where(where).Delete();
        }

        /// <summary>
        /// 批量插入(与上下文不保持同一个事务)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Update<T>(System.Linq.Expressions.Expression<Func<T, T>> where) where T : class
        {
            return base.Set<T>().Update(where);
        }


        /// <summary>
        /// 提交修改包含日志检测
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                //记录日志
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
               //记录错误日志
            }
            return 0;
        }
        /// <summary>
        /// 提交修改不检测日志
        /// </summary>
        /// <returns></returns>
        public int SaveChangesWithoutLog()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                //记录错误日志
            }
            return 0;
        }

        /// <summary>
        /// 是否开启导航属性
        /// </summary>
        public void OpenProxyCreation(bool isOpen)
        {
            try
            {
                base.Configuration.ProxyCreationEnabled = isOpen;
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
