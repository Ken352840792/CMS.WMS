using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Core
{
    public static class IocContainer
    {
        public static IocServiceProvider ServiceProvider { get; set; }
        public static void SetIoc(IocServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
