using Sy.Base;
using Sy.DapperRepositorys;
using Sy.Module.DTOModel;
using Sy.Module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Module.Contract
{
   public interface IUserManagerService: IServices, IServiceCURD<BaseRepository<User, int>, User, int, InDto_AddUser, InDto_AddUser>
    {
        
    }
}
