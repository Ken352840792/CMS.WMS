using Sy.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Repository
{
    /// <summary>
    /// <system.data>
    //  <DbProviderFactories>
    //    <remove invariant="System.Data.SQLite" />
    //    <add name="SQLite Data Provider" invariant="System.Data.SQLite" description=".Net Framework Data Provider for SQLite" type="System.Data.SQLite.SQLiteFactory, System.Data.SQLite, Version=1.0.81.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139" />
    //  </DbProviderFactories>
    //</system.data>
    //<connectionStrings>
    //  <!--<add name="ApplicationServices" connectionString="data source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true" providerName="System.Data.SqlClient"/>-->
    //  <add name="MSIDB" connectionString="Data Source=|DataDirectory|\sqlitedb.db3;Pooling=true;FailIfMissing=false" providerName="System.Data.SQLite" />
    //</connectionStrings>
    /// </summary>
    public class BaseRepostitory : IBaseRepositoryOldAdoNet
    {
        private readonly static Hashtable CachedFactories = Hashtable.Synchronized(new Hashtable());

        private readonly DbProviderFactory _factory = null;
        private readonly string _connectionString = null;
        private DbConnection _connection = null;
        private DbTransaction _transaction;
        private bool _connectionIsOpen = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConfigName">database config name</param>
        public BaseRepostitory(string dbConfigName)
        {
            var setting = ConfigurationManager.ConnectionStrings[dbConfigName];

            if (setting == null) throw new ArgumentException("dbConfigName");

            _factory = GetFactory(setting.ProviderName);
            _connectionString = setting.ConnectionString;
        }

        ~BaseRepostitory()
        {
            Close();
        }

        #region private properties/methods
        private DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = _factory.CreateConnection();
                    _connection.ConnectionString = _connectionString;
                }

                return _connection;
            }
        }

        private static DbProviderFactory GetFactory(string providerName)
        {
            DbProviderFactory factory = CachedFactories[providerName] as DbProviderFactory;
            if (factory == null)
            {
                lock (CachedFactories.SyncRoot)
                {
                    factory = DbProviderFactories.GetFactory(providerName);
                    CachedFactories.Add(providerName, factory);
                }
            }

            return factory;
        }
        #endregion

        #region public methods
        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                _connectionIsOpen = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            if (_connectionIsOpen && _transaction == null)
            {
                if (Connection != null && Connection.State != ConnectionState.Closed)
                {
                    _connectionIsOpen = false;
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                Open();
                _transaction = Connection.BeginTransaction();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }

            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }

            Close();
        }
        #endregion

        #region CreateCommand
        /// <summary>
        /// create new sql command
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParams"></param>
        /// <param name="mustCloseConnection"></param>
        /// <returns></returns>
        private DbCommand CreateCommand(CommandType commandType, string commandText,
            IEnumerable<DbParameter> commandParams, out bool mustCloseConnection)
        {
            if (Connection == null) throw new ArgumentNullException("_connection");

            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            // If the provided Connection is not open, we will open it
            if (Connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                Connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            DbCommand command = Connection.CreateCommand();

            command.CommandText = commandText;

            if (_transaction != null)
            {
                if (_transaction.Connection == null)
                {
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }
                command.Transaction = _transaction;
            }

            command.CommandType = commandType;

            if (commandParams != null)
            {
                foreach (DbParameter p in commandParams)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }

            return command;
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(commandType, commandText, null);
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, IEnumerable<DbParameter> cmdParams)
        {
            bool mustCloseConnection = false;
            DbCommand cmd = CreateCommand(commandType, commandText, cmdParams, out mustCloseConnection);

            int ret = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();

            if (mustCloseConnection)
            {
                Connection.Close();
            }

            return ret;
        }
        #endregion

        #region ExecuteDataset
        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText)
        {
            return ExecuteDataset(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="spName">store procedure name</param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string spName, IEnumerable<DbParameter> cmdParams)
        {
            return ExecuteDataset(CommandType.StoredProcedure, spName, cmdParams);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return ExecuteDataset(commandType, commandText, null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText, IEnumerable<DbParameter> cmdParams)
        {
            bool mustCloseConnection = false;
            DbCommand cmd = CreateCommand(commandType, commandText, cmdParams, out mustCloseConnection);

            using (DbDataAdapter adapter = _factory.CreateDataAdapter())
            {
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                cmd.Parameters.Clear();

                if (mustCloseConnection)
                {
                    Connection.Close();
                }

                return ds;
            }
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(commandType, commandText, null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(CommandType commandType, string commandText, IEnumerable<DbParameter> cmdParams)
        {
            bool mustCloseConnection = false;
            DbCommand cmd = CreateCommand(commandType, commandText, cmdParams, out mustCloseConnection);

            DbDataReader reader = null;

            if (mustCloseConnection)
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                reader = cmd.ExecuteReader();
            }

            // cmd.Parameters.Clear();

            return reader;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return ExecuteScalar(commandType, commandText, null);
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText, IEnumerable<DbParameter> cmdParams)
        {
            bool mustCloseConnection = false;
            DbCommand cmd = CreateCommand(commandType, commandText, cmdParams, out mustCloseConnection);

            object ret = cmd.ExecuteScalar();

            cmd.Parameters.Clear();

            if (mustCloseConnection)
            {
                Connection.Close();
            }

            return ret;
        }
        #endregion

        #region create parameter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public DbParameter MakeInParam(string paramName, DbType dbType, object paramValue)
        {
            return MakeParam(paramName, dbType, 0, ParameterDirection.Input, paramValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public DbParameter MakeInParam(string paramName, DbType dbType, int size, object paramValue)
        {
            return MakeParam(paramName, dbType, size, ParameterDirection.Input, paramValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public DbParameter MakeOutParam(string paramName, DbType dbType)
        {
            return MakeParam(paramName, dbType, 0, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public DbParameter MakeOutParam(string paramName, DbType dbType, int size)
        {
            return MakeParam(paramName, dbType, size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public DbParameter MakeParam(string paramName, DbType dbType, Int32 size, ParameterDirection direction, object paramValue)
        {
            DbParameter param = _factory.CreateParameter();
            param.ParameterName = paramName;
            param.DbType = dbType;
            param.Size = size;
            param.Direction = direction;

            if (!(direction == ParameterDirection.Output && paramValue == null))
            {
                param.Value = paramValue;
            }

            return param;
        }
        #endregion
    }
}


