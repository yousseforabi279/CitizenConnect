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

        public UnitOfWork(Appcontext context, ICitizinRequierment CitizinRequierment, IComplaintDepartment ComplaintDepartment)
        {
            _context = context;
            this.CitizinRequierment = CitizinRequierment;
            this.ComplaintDepartment = ComplaintDepartment;
        }

        public ICitizinRequierment CitizinRequierment { get; }
        public IComplaintDepartment ComplaintDepartment { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}