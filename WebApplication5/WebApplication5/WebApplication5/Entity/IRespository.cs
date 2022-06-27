using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Entity
{
    public interface IGenericRepository<T> where T : class
    {
        // IQueryable<T> GetAll();
        Task<T> GetByIdAsync(long? id);
        Task<T> InsertAsync(T obj);
        Task<List<T>> RawQueryAsync(string query);
        Task<T> UpdateAsync(T obj);
        Task<bool> RemoveAsync(T id);
        IQueryable<T> Table {get;}

        IQueryable<T> TrackAll {get;}
        Task SaveAsync();
    }
}