using Sy.Core;
using Sy.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Base
{
    public class ServiceBase
    {
        public ILog Log { get; set; }

        public IIocResolver IResolver { get; set; }
    }
}
