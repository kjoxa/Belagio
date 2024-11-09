using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devarts.Repositories
{
    interface IFinanceRepository
    {
        IQueryable<Finance> GetFinances();
        Finance GetFinanceById(int id);

        IQueryable<FinanceCathegoryName> GetFinanceCathegoryNames();
        FinanceCathegoryName GetFinanceCathegoryNameById(int id);

        IQueryable<FinanceFavouritesName> GetFinanceFavouritesNames();
        FinanceFavouritesName GetFinanceFavouritesNameById(int id);

        void SaveChanges();
    }
}
