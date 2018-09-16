using Sy.Base;
using Sy.Core;
using System.Data.Entity.ModelConfiguration;

namespace Sy.Repository
{
    public abstract class BaseConfiguration<T> : EntityTypeConfiguration<T> where T : class, IEntity<int>, new()
    {
        public abstract void Init();

        public BaseConfiguration()
        {
            Init();
        }

    }
}
