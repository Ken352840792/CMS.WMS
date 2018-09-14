using Sy.Module.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sy.WebApi.Controllers
{
    /// <summary>
    /// 测试分类
    /// </summary>
    public class DemoController :  BaseApi
    {
        public IDemo demo { get; set; }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetMsg()
        {
            Log.Info("你好");
            return "aaa";
        }
        [HttpGet]
        public string GetMsg2()
        {
            demo.TT();
            return "aaa";
        }
    }
}
