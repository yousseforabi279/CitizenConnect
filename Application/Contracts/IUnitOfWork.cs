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
        //IGenericRepository<ComplaintResolution> ComplaintResolutions { get; }

        Task<int> SaveChangesAsync();
    }
}
