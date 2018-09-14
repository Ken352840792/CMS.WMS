using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sy.WebApi.Controllers
{
    public class DefaultController : ApiController
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetMsg()
        {
            //Log.Info("你好");
            return "aaa";
        }
    }
}
