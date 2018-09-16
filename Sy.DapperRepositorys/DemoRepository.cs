using Sy.Base;
using Sy.Module.Model;
using Sy.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.DapperRepositorys
{
    public class DemoRepository : BaseRepository<User,int>
    {
        public DemoRepository() :
            base(ConfigHelp.GetSqlConnection())
        {

        }
    }
}
