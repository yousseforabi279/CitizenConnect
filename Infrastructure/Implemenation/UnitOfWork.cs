using Application.Contracts;
using Infrastructure.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Appcontext _context;

        public UnitOfWork(Appcontext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}