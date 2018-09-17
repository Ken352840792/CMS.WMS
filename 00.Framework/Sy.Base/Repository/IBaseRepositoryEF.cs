using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Base
{
    public interface IBaseRepositoryEF<T, TKey> : IScopeDependency
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(T model);
        /// <summary>
        /// 根据ID删除一条数据
        /// </summary>
        bool Delete(TKey id);
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool DeleteList(List<TKey> ids);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(T model);
        /// <summary>
        /// 根据ID获取实体对象
        /// </summary>
        T GetModel(TKey Id);
        /// <summary>
        /// 根据条件获取实体对象集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> GetModelList();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        IEnumerable<T> GetListPage(int pageIndex, int pageSize);
        #endregion
    }
}
