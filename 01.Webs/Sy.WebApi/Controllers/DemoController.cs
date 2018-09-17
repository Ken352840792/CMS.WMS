using Sy.Core;
using Sy.Module.Contract;
using Sy.Module.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sy.Util;

namespace Sy.WebApi.Controllers
{
    /// <summary>
    /// 测试分类
    /// </summary>
    public class DemoController : BaseApi
    {
        public IDemo demo { get; set; }
        public IUserManagerService _userManagerService { get; set; }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetMsg()
        {
            var result = _userManagerService.CreateOrUpdate(new InDto_AddUser() { Address = "湖北", Age = 10, Name = "Ken",  });
            var results = _userManagerService.CreateOrUpdate(new InDto_AddUser() { Address = "湖北武汉", Age = 10, Name = "Ken",  Id = 1 });
            var resultsss = _userManagerService.GetAllList(new PageModel() { PageIndex = 1, PageSize = 10 });
            var count = 0;
            var resultssss = _userManagerService.GetPageList(new PageModel() { PageIndex = 1, PageSize = 10 }, out count);
            var ff = _userManagerService.Delete(1);
            var fd = _userManagerService.DeleteList(new List<int?>(){ 1,2});

            Log.Info("你好");
            return "aaa";
        }
        [HttpGet]
        public string GetMsg2()
        {
            //demo.TT();
            return "aaa";
        }
    }
}
