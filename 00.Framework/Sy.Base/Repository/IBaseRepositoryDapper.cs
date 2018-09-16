using Sy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Base
{
    public interface IBaseRepositoryDapper<T,TKey> : IScopeDependency
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(T model);
        /// <summary>
        /// 根据ID删除一条数据
        /// </summary>
        bool Delete(TKey Id);
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool DeleteList(string strWhere, object parameters);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(T model);
        /// <summary>
        /// 根据ID获取实体对象
        /// </summary>
        T GetModel(TKey Id);
        ///// <summary>
        ///// 根据条件获取实体对象
        ///// </summary>
        //T GetModel(string strWhere, object parameters);
        /// <summary>
        /// 根据条件获取实体对象集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> GetModelList(string strWhere, object parameters);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageNum">页码</param>
        /// <param name="rowsNum">每页行数</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="orderBy">Orde by排序</param>
        /// <param name="parameters">parameters参数</param>
        /// <returns></returns>
        IEnumerable<T> GetListPage(int pageNum, int rowsNum, string strWhere, string orderBy, object parameters);
        #endregion
    }
}
