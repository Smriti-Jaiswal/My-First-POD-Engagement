using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace api.Entity
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DBContext _context;
        private DbSet<T> table;

        

        public GenericRepository(DBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        // public IQueryable<T> GetAll()
        // {
        //     return table.AsNoTracking();
        // }

        public async Task<List<T>> RawQueryAsync(string query)
        {
            return await table.FromSqlRaw(query).ToListAsync();
        }

        public async Task<T> GetByIdAsync(long? id)
        {
            return await table.FindAsync(id);
        }

        public async Task<T> InsertAsync(T obj)  
        {
            await table.AddAsync(obj);
            await SaveAsync();
            return obj;
        }

      
        public async Task<T> UpdateAsync(T obj)
        {
            table.Update(obj);
            await SaveAsync();
            return obj;
        }

        public async Task<bool> RemoveAsync(T obj)
        {
            table.Remove(obj);
            await SaveAsync();
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        IQueryable<T> IGenericRepository<T>.Table => table;

        IQueryable<T> IGenericRepository<T>.TrackAll => table.AsNoTracking();
    }
}