using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace devarts.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private SiteDbContext _db;

        public AdminRepository()
        {
            _db = new SiteDbContext();
        }        

        /// ------------------------------ TRACING DATABASE ----------------------------------
        public List<Tracing> GetTracingList()
        {
            return _db.Tracing.OrderByDescending(t => t.Id).ToList();
        }

        /// ------------------------------ STATISTICS DATABASE ----------------------------------
        public IQueryable<Statistic> GetStatisticList()
        {
            return _db.Statistics.OrderBy(u => u.Id);
        }

        public Statistic GetTheSameRecord(string date, string ip)
        {
            return _db.Statistics.Where(u => u.DateTime == date && u.IpAddress == ip).FirstOrDefault();
        }

        public int GetCountRecords()
        {
            return _db.Statistics.Count();
        }

        public bool DeleteAllPositionsFromStatistics()
        {
            try
            {
                var itemsToDelete = _db.Statistics.Where(a => a.Id != 0);

                if (itemsToDelete.Any())
                {
                    _db.Statistics.RemoveRange(itemsToDelete);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAllPositionsFromTracing()
        {
            try
            {
                var itemsToDelete = _db.Tracing.Where(a => a.Id != 0);

                if (itemsToDelete.Any())
                {
                    _db.Tracing.RemoveRange(itemsToDelete);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void Add(Statistic stat)
        {
            _db.Statistics.Add(stat);
        }

        public void Delete(Statistic stat)
        {
            _db.Statistics.Remove(stat);
            _db.SaveChanges();
        }

        public void AddTrace(Tracing stat)
        {
            _db.Tracing.Add(stat);
        }

        public void DeleteTrace(Tracing stat)
        {
            _db.Tracing.Remove(stat);
            _db.SaveChanges();
        }        

        /// ------------------------------ DOGS LOCATION DATABASE ----------------------------------

        public DogsLocation GetLocationForDogByDogId(int dogsByLittersId)
        {
            return _db.DogsLocation.FirstOrDefault(x => x.LitterId == dogsByLittersId);
        }

        public List<DogsLocation> GetAllDogsLocations()
        {
            return _db.DogsLocation.OrderByDescending(x => x.Id).ToList();
        }

        public void AddDogsLocation(DogsLocation stat)
        {
            _db.DogsLocation.Add(stat);
        }

        public void DeleteDogsLocation(DogsLocation stat)
        {
            _db.DogsLocation.Remove(stat);
            _db.SaveChanges();
        }

        /// ------------------------------ NEWSLETTER DATABASE ----------------------------------
        public Subscriber GetSubscriberById(int id)
        {
            return _db.Subscriber.FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<Newsletter> GetAllNewsletters()
        {
            return _db.Newsletter.OrderBy(u => u.Id);
        }

        public void AddNewsletter(Newsletter stat)
        {
            _db.Newsletter.Add(stat);
        }

        public void DeleteNewsletter(Newsletter stat)
        {
            _db.Newsletter.Remove(stat);
            _db.SaveChanges();
        }

        /// ------------------------------ SUBSCRIBER DATABASE ----------------------------------

        public IQueryable<Subscriber> GetAllSubscribers()
        {
            return _db.Subscriber.OrderBy(u => u.Id);
        }

        public void AddSubscriber(Subscriber stat)
        {
            _db.Subscriber.Add(stat);
        }

        public void DeleteSubscriber(Subscriber stat)
        {
            _db.Subscriber.Remove(stat);
            _db.SaveChanges();
        }

        /// ------------------------------ END SUBSCRIBER DATABASE ----------------------------------

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}