using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AD.Data.Interfaces
{
   public interface IRepository<T> where T:class
    {
        Task Create(T obj);
        Task<List<T>> GetElements();
        void Delete(T obj);
        void Update(T obj);
        void Detach(T entry);
    }
}
