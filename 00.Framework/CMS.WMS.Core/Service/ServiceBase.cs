using Sy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Core
{
    public abstract class ServiceBase : IServices
    {
        public ILog _log { get; set; }

        public IIocResolver _iocResolver { get; set; }
        public IBaseRepositoryOldAdoNet _baseRepositoryOldAdoNet { get; set; }
    }
}
