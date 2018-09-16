using Sy.Base;
using Sy.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Core
{
    public class Mapping
    {
        public static void RegisterMappings()
        {
            //获取所有IProfile实现类4
            var assembly = new DirectoryAssemblyFinder().FindAll().FirstOrDefault(a => a.FullName.Contains("Sy.Module.Mapper.Customs"));
            var allType = assembly.GetTypes().Where(a => typeof(IProfile).IsAssignableFrom(a));
            //var allType =
            //Assembly
            //   .GetEntryAssembly()//获取默认程序集
            //   .GetReferencedAssemblies()//获取所有引用程序集
            //   .Select(Assembly.Load)
            //   .SelectMany(y => y.DefinedTypes)
            //   .Where(type => typeof(IProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));

            foreach (var typeInfo in allType)
            {

                //注册映射
                AutoMapper.Mapper.Initialize(y =>
                {
                    y.AddProfiles(typeInfo); // Initialise each Profile classe
                    });

            }
        }
    }
}
