using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Sy.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //Mapper.Initialize(t => t.AddProfiles(new Sy.Framework.Tool.Reflection.DirectoryAssemblyFinder().FindAll()));
            IocConfig.InitIoc();
        }
    }
}
