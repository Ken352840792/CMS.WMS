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
using Sy.Base;

namespace Sy.Module.Service
{

    public class UserManagerService :  ServiceCurdDapperBase<User,int,InDto_AddUser,int?,InDto_AddUser>, IUserManagerService
    {
       

        public string aa()
        {
            return "123";
        }
    }
}
