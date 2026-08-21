using Application.Contracts.Repos;
using Domain;
using Infrastructure.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implemenation
{
    public class CompliantRepo:GenericRepository<Complaint>,IComplaint
    {
        public CompliantRepo(Appcontext context):base(context)
        {
        }
    }

}

