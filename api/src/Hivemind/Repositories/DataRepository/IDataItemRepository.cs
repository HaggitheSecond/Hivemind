using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hivemind.Data;

namespace Hivemind.Repositories.DataRepository
{
    public interface IDataItemRepository<T> where T : BaseEntity
    {
        Task Add(T item, CancellationToken token = default(CancellationToken));
        Task Remove(int id, CancellationToken token = default(CancellationToken));
        Task Update(T item, CancellationToken token = default(CancellationToken));
        Task<T> FindByID(int id, CancellationToken token = default(CancellationToken));
        Task<IEnumerable<T>> FindAll(CancellationToken token = default(CancellationToken));
    }
}