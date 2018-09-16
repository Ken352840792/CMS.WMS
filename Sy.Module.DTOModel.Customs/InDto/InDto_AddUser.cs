using Sy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Module.DTOModel
{
    public class InDto_AddUser: IDtoBase<int?>
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
        public string Address { get; set; }
        public int Sex { get; set; }
    }

}
