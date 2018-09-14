using Sy.Base;
using Sy.Module.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Module.Service
{
    public class Demo : ServiceBase,IDemo
    {
        public void TT()
        {
            Log.Info("你好");
        }
    }
}
