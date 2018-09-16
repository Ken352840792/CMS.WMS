using Sy.Base;
using Sy.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Core
{
    public abstract class ServiceCurdDapperBase<TEntity, TKeyType, TIndto,TIndtoKey, TOutdto> : ServiceBase, IServiceCURDDapper<TEntity, TKeyType, TIndto, TIndtoKey, TOutdto>
        where TEntity : IEntity<TKeyType>
        where TIndto:IDtoBase<TIndtoKey>
        where TOutdto:new()

    {
        public IBaseRepositoryDapper<TEntity, TKeyType> _repositorys { get; set; }

        public bool CreateOrUpdate(TIndto indto)
        {
            var entity = AutoMapper.Mapper.Map<TEntity>(indto);
            if (indto.Id == null)
            {
                //Add
                return _repositorys.Add(entity);
            }
            else
            {
                //update
                return _repositorys.Update(entity);
            }
        }

        public bool Delete(TIndtoKey key)
        {
            return _repositorys.Delete(key.CastTo<TKeyType>());
        }

        public int DeleteList(List<TIndtoKey> listKey)
        {
            var count = listKey.Count;
            var restultCount = 0;
            listKey.ForEach((key) =>
            {
                restultCount += Delete(key) ? 1 : 0;
            });
            return restultCount;
        }

        public List<TOutdto> GetAllList(PageModel pageModel)
        {
            var entity = _repositorys.GetModelList(GetWhereStr(pageModel), null);
            return AutoMapper.Mapper.Map<List<TOutdto>>(entity);
        }
        private string GetWhereStr(PageModel pageModel)
        {
            StringBuilder where = new StringBuilder();
            if (pageModel.Search.Count > 0)
            {
                foreach (var item in pageModel.Search)
                {
                    switch (item.Operation)
                    {
                        case "=":
                            {
                                where = where.Append($" And [{item.Col}] = '{item.Val}'");
                            }
                            break;
                        case "like":
                            {
                                where = where.Append($" And [{item.Col}] like '%{item.Val}%'");
                            }
                            break;
                        case ">=":
                            {
                                where = where.Append($" And [{item.Col}] >= '%{item.Val}%'");
                            }
                            break;
                        case "<=":
                            {
                                where = where.Append($" And [{item.Col}] >= '%{item.Val}%'");
                            }
                            break;
                    }
                }
            }
            return where.Length > 0 ? where.ToString() : "";
        }
        public List<TOutdto> GetPageList(PageModel pageModel, out int AllCount)
        {
            var entity = _repositorys.GetListPage(pageModel.PageIndex, pageModel.PageSize, GetWhereStr(pageModel), pageModel.Sort, null);
            AllCount = GetAllList(pageModel).Count;
            return AutoMapper.Mapper.Map<List<TOutdto>>(entity);
        }
    }
}
