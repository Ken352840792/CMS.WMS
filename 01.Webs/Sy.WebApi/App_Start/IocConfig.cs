using Autofac;
using Autofac.Integration.WebApi;
using Sy.Base;
using Sy.Util;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Sy.WebApi
{
    /// <summary>
    /// IOC配置类
    /// </summary>
    public class IocConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void InitIoc()
        {
            var assemblies = new DirectoryAssemblyFinder().FindAll();
            var builder = new ContainerBuilder();
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;
            Type PerLifetimeScopeType = typeof(IScopeDependency);
            Type PerDependencyType = typeof(ITransientDependency);
            Type SingleInstanceType = typeof(ISingletonDependency);

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(type => PerLifetimeScopeType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf()   //自身服务，用于没有接口的类
                .AsImplementedInterfaces() //接口服务
                .PropertiesAutowired()  //属性注入
                .InstancePerLifetimeScope(); //PerLifetimeScope

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                      .Where(type => PerDependencyType.IsAssignableFrom(type) && !type.IsAbstract)
                      .AsSelf()   //自身服务，用于没有接口的类
                      .AsImplementedInterfaces() //接口服务
                      .PropertiesAutowired()  //属性注入
                      .InstancePerDependency();    //PerDependency

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                      .Where(type => SingleInstanceType.IsAssignableFrom(type) && !type.IsAbstract)
                      .AsSelf()   //自身服务，用于没有接口的类
                      .AsImplementedInterfaces() //接口服务
                      .PropertiesAutowired()  //属性注入
                      .SingleInstance();    //SingleInstance


            builder.RegisterType<WebApiIocResolver>().As<IIocResolver>().AsSelf()   //自身服务，用于没有接口的类
                      .AsImplementedInterfaces() //接口服务
                      .PropertiesAutowired()  //属性注入;
                      .SingleInstance();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(config);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
