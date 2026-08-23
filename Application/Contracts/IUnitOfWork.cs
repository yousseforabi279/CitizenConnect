using Application.Contracts.Repos;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUnitOfWork
    {
        ICitizinRequierment CitizinRequierment { get; }
        IComplaintDepartment ComplaintDepartment { get; }
        Task<int> SaveChangesAsync();
    }
}
