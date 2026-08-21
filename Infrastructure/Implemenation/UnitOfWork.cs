using Application.Contracts;
using Application.Contracts.Repos;
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

        public UnitOfWork(Appcontext context,IComplaint complaint, IComplaintCategory ComplaintCategory)
        {
            _context = context;
            this.Complaints = complaint;
            this.ComplaintCategory = ComplaintCategory;
        }

        public IComplaint Complaints { get; }
        public IComplaintCategory ComplaintCategory { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}