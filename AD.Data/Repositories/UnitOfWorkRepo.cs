using AD.Data.Entities;
using AD.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AD.Data.Repositories
{
    public class UnitOfWorkRepo : IUnitOfWork
    {
        public ApplicationContext _context { get; }

        public UnitOfWorkRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task  save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
