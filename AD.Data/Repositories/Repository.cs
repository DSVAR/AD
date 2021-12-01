using AD.Data.Entities;
using AD.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AD.Data.Repositories
{
    class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _application;
        private readonly DbSet<T> _dbSet;
        private readonly IUnitOfWork _IUOW;
        public Repository(ApplicationContext application, IUnitOfWork IUOW)
        {
            _application = application;
            _dbSet = application.Set<T>();
            _IUOW = IUOW;
        }
        public async Task Create(T obj)
        {
           await _application.AddAsync(obj);
        }

        public  void Delete(T obj)
        {
             _application.Remove(obj);
        }

   
        public async Task<List<T>> GetElements()
        {
          return  await _dbSet.ToListAsync();
        }

        public void Update(T obj)
        {
            _application.Update(obj);
        }

        public void Detach(T entry)
        {
            _IUOW._context.Entry(entry).State = EntityState.Detached;
        }

    }
}
