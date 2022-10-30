using Application.Repositories.EntityFramework;
using Domain.Entities;
using Persistence.Repositories.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.EntityFramework
{
    public class DoctorRepository : EfRepositoryBase<Doctor, BaseDbContext>, IDoctorRepository
    {
        public DoctorRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
