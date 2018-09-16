using Sy.Core;
using Sy.DapperRepositorys;
using Sy.Module.Contract;
using Sy.Module.DTOModel;
using Sy.Module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Module.Service
{
    
    public class UserManagerService : ServiceBase<BaseRepository<User,int>, User,int, InDto_AddUser, InDto_AddUser>, IUserManagerService
    {
       
    }
}
