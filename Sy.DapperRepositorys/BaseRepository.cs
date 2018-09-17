
using Dapper;
using Sy.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sy.DapperRepositorys
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseRepository<T,TKey> : IBaseRepositoryDapper<T, TKey> ,IDisposable//当类被释放时，自动关闭连接
        where T :IEntity<int>
    {
        private IDbConnection _connection;

        public IDbConnection _DbConnection
        {
            get { return this._DbConnection; }
        }

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public virtual bool Add(T model)
        {
            int? result;
            result = _connection.Insert<T>(model);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据ID删除一条数据
        /// </summary>
        public virtual bool Delete(TKey id)
        {
            int? result = _connection.Delete<T>(id);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual bool DeleteList(string strWhere, object parameters)
        {
            int? result = _connection.DeleteList<T>(strWhere, parameters);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public virtual bool Update(T model)
        {
            int? result = _connection.Update<T>(model);
         
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据ID获取实体对象
        /// </summary>
        public virtual T GetModel(TKey id)
        {
            return _connection.Get<T>(id);
        }

        /// <summary>
        /// 根据条件获取实体对象集合
        /// </summary>
        public virtual IEnumerable<T> GetModelList(string strWhere, object parameters)
        {
            return _connection.GetList<T>(strWhere, parameters);
        }
        /// <summary>
        /// 根据条件分页获取实体对象集合
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="rowsNum"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderBy"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetListPage(int pageNum, int rowsNum, string strWhere, string orderBy, object parameters)
        {
            return _connection.GetListPaged<T>(pageNum, rowsNum, strWhere, orderBy, parameters); ;
        }

        public void Dispose()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception)
            {
            }
        }


        #endregion
    }
}
