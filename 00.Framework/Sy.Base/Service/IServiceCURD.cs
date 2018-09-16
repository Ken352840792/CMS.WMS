using Sy.Base;
using Sy.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Base
{
    public interface IServiceCURD<TRepositorys, TEntity, TKeyType, TIndto, TOutdto>
        where TRepositorys: IBaseRepository<TEntity,TKeyType>
        where TEntity : IEntity<TKeyType>
    {
        bool CreateOrUpdate(TIndto indto);
        bool Delete(TKeyType key);
        int DeleteList(List<TKeyType> listKey);
        List<TOutdto> GetPageList(PageModel pageModel,out int AllCount);
        List<TOutdto> GetAllList(PageModel pageModel);
    }
}
