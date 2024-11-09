using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Repositories
{
    public class AssistantRepository : IAssistantRepository
    {
        KennelDbContext _db;

        public AssistantRepository()
        {
            _db = new KennelDbContext();
        }

        /* Cieczki i mioty
        ----------------------------------------------------------------------- */

        public Reproduction GetReproductionById(int id)
        {
            return _db.Reproduction.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// Pobranie ostatniego wpisu suki z cieczką na podstawie nazwy
        /// </summary>
        /// <param name="dogName"></param>
        /// <returns></returns>
        public Reproduction GetReproductionByLastDogName(string dogName)
        {
            return _db.Reproduction.FirstOrDefault(r => r.DogName.ToLower() == dogName.ToLower());
        }

        public IQueryable<Reproduction> GetReproductionList()
        {
            return _db.Reproduction;//.OrderByDescending(r => r.Id);
        }
        
        public void Add(Reproduction reproduction)
        {
            _db.Reproduction.Add(reproduction);
        }

        public void Remove(Reproduction reproduction)
        {
            _db.Reproduction.Remove(reproduction);
        }

        /* Rezerwacje
        ----------------------------------------------------------------------- */

        public Reservation GetReservationById(int id)
        {
            return _db.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<Reservation> GetReservationList()
        {
            return _db.Reservations;//.OrderByDescending(r => r.Id);
        }

        public void AddReservation(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
        }

        public void RemoveReservation(Reservation reservation)
        {
            _db.Reservations.Remove(reservation);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        
         /* Badania i szczepienia
        ----------------------------------------------------------------------- */

        public HealthAndVaccinations GetHealthAndVaccinationsById(int id)
        {
            return _db.HealthAndVaccinations.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<HealthAndVaccinations> GetHealthAndVaccinationsList()
        {
            return _db.HealthAndVaccinations;//.OrderByDescending(r => r.Id);
        }

        public void AddHealthAndVaccinations(HealthAndVaccinations heav)
        {
            _db.HealthAndVaccinations.Add(heav);
        }

        public void RemoveHealthAndVaccinations(HealthAndVaccinations heav)
        {
            _db.HealthAndVaccinations.Remove(heav);
        }     
    }
}