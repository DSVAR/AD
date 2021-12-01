using AD.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AD.Data.Interfaces
{
   public interface IUnitOfWork
    {
        ApplicationContext _context { get; }

        Task save();
        Task Dispose();
    }
}
