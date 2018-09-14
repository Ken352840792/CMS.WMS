
using Sy.Core;
using Sy.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sy.WebApi
{
    public class BaseApi : System.Web.Http.ApiController
    {
        /// <summary>
        /// 日志接口
        /// </summary>
        public ILog Log { get; set; }
        /// <summary>
        /// IOC接口
        /// </summary>
        public IIocResolver IResolver { get; set; }



    }
}