using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devarts.Repositories
{
    interface IAssistantRepository
    {
        /* Cieczki i mioty
        ----------------------------------------------------------------------- */
        Reproduction GetReproductionById(int id);
        IQueryable<Reproduction> GetReproductionList();

        /* Rezerwacje
       ----------------------------------------------------------------------- */
        Reservation GetReservationById(int id);
        IQueryable<Reservation> GetReservationList();

        void SaveChanges();
    }
}
