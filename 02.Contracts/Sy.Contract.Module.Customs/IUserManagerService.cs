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
   public interface IUserManagerService:IServices, IServiceCURDDapper<User,int,InDto_AddUser,int?,InDto_AddUser>
    {
        string aa();
    }
}
