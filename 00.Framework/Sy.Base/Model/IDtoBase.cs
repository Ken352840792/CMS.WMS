﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Base
{
   public interface IDtoBase<TKey>
    {
        TKey Id { get; set; }
    }
}
