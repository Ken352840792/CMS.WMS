using Sy.Base;
using Sy.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Base
{
    public interface IServiceCURDDapper<TEntity, TKeyType, TIndto,TIndtoKey,TOutdto>:IScopeDependency
        where TEntity : IEntity<TKeyType>
    {
        IBaseRepositoryDapper<TEntity, TKeyType> _repositorys { get; set; }
        bool CreateOrUpdate(TIndto indto);
        bool Delete(TIndtoKey key);
        int DeleteList(List<TIndtoKey> listKey);
        List<TOutdto> GetPageList(PageModel pageModel,out int AllCount);
        List<TOutdto> GetAllList(PageModel pageModel);
    }
}
