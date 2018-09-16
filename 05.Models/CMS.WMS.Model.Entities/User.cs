using Sy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Module.Model
{
   public class User:EntityBase<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
