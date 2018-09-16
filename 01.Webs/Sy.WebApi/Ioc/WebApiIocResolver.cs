using Autofac;
using Sy.Base;
using Sy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sy.WebApi
{

    public class WebApiIocResolver : IIocResolver

    {
        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return GlobalConfiguration.Configuration.DependencyResolver.GetService(type);
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> Resolves<T>()
        {
            return Resolves(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// 获取指定类型的所有实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<object> Resolves(Type type)
        {
            return GlobalConfiguration.Configuration.DependencyResolver.GetServices(type);
        }


        public T Resolve<T>(params KeyValuePair<string, object>[] args)

        {
            var res = GlobalConfiguration.Configuration.DependencyResolver as Autofac.Integration.WebApi.AutofacWebApiDependencyResolver;
            if (args.Length > 0)
            {
                List<NamedParameter> paras = new List<NamedParameter>();
                foreach (var r in args)
                {
                    paras.Add(new NamedParameter(r.Key, r.Value));
                }
                return res.Container.Resolve<T>(paras.ToArray());
            }
            else
            {
                return res.Container.Resolve<T>();
            }
        }


        public object Resolve(string serviceName, Type serviceType, params KeyValuePair<string, object>[] args)

        {
            var res = GlobalConfiguration.Configuration.DependencyResolver as Autofac.Integration.WebApi.AutofacWebApiDependencyResolver;
            if (args.Length > 0)
            {
                List<NamedParameter> paras = new List<NamedParameter>();
                foreach (var r in args)
                {
                    paras.Add(new NamedParameter(r.Key, r.Value));
                }
                return res.Container.ResolveNamed(serviceName, serviceType, paras.ToArray());
            }
            else
            {
                return res.Container.ResolveNamed(serviceName, serviceType);
            }
        }
    }
}
