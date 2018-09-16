using Sy.Core;
using Sy.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Sy.WebApi
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ErrorAttribute'
    public class ErrorAttribute : ExceptionFilterAttribute
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ErrorAttribute'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ErrorAttribute.log'
        public  ILog log { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ErrorAttribute.log'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ErrorAttribute.OnException(HttpActionExecutedContext)'
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ErrorAttribute.OnException(HttpActionExecutedContext)'
        {
           var response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content =new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject((new  ApiResult() { Status=0, ErrorCode="服务崩啦，异常信息："+ actionExecutedContext.Exception.Message+" 详情查看日志" })),System.Text.Encoding.UTF8, "application/json");
            actionExecutedContext.Response = response;
            log.Error(actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}