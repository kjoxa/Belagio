using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devarts.Repositories
{
    interface IAdminRepository : IRepository<Statistic>
    {
        IQueryable<Statistic> GetStatisticList();        
    }
}
