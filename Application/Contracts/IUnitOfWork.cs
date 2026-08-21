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
        IComplaint Complaints { get; }
        IComplaintCategory ComplaintCategory { get; }


        Task<int> SaveChangesAsync();
    }
}
