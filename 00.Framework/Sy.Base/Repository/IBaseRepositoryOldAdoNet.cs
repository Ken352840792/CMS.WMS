using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Base
{
    public interface IBaseRepositoryOldAdoNet
    {
        void Open();
        void Close();
        void BeginTransaction();
        void Rollback();
        void Commit();
         int ExecuteNonQuery(string commandText);
         int ExecuteNonQuery(CommandType commandType, string commandText);
         int ExecuteNonQuery(CommandType commandType, string commandText, IEnumerable<DbParameter> cmdParams);
         DataSet ExecuteDataset(string commandText);
         DataSet ExecuteDataset(string spName, IEnumerable<DbParameter> cmdParams);
         DataSet ExecuteDataset(CommandType commandType, string commandText);
         DataSet ExecuteDataset(CommandType commandType, string commandText, IEnumerable<DbParameter> cmdParams);
         DbDataReader ExecuteReader(string commandText);
         DbDataReader ExecuteReader(CommandType commandType, string commandText);
         DbDataReader ExecuteReader(CommandType commandType, string commandText, IEnumerable<DbParameter> cmdParams);
         object ExecuteScalar(string commandText);
         object ExecuteScalar(CommandType commandType, string commandText);
         object ExecuteScalar(CommandType commandType, string commandText, IEnumerable<DbParameter> cmdParams);
         DbParameter MakeInParam(string paramName, DbType dbType, object paramValue);
         DbParameter MakeInParam(string paramName, DbType dbType, int size, object paramValue);
         DbParameter MakeOutParam(string paramName, DbType dbType);
         DbParameter MakeOutParam(string paramName, DbType dbType, int size);
         DbParameter MakeParam(string paramName, DbType dbType, Int32 size, ParameterDirection direction, object paramValue);


    }
}
