using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private KennelDbContext _db;

        public FinanceRepository()
        {
            _db = new KennelDbContext();
        }

        /** 
         * DAO Finanse
        */
        public IQueryable<Finance> GetFinances()
        {
            return _db.Finances.OrderByDescending(fin => fin.CreateDate);
        }

        public Finance GetFinanceById(int id)
        {
            return _db.Finances.FirstOrDefault(fin => fin.Id == id);
        }

        public void AddFinance(Finance fin)
        {
            _db.Finances.Add(fin);
        }

        public void RemoveFinance(Finance fin)
        {
            _db.Finances.Remove(fin);
        }

        /** 
         * DAO Kategorii finansów 
         */
        public IQueryable<FinanceCathegoryName> GetFinanceCathegoryNames()
        {
            return _db.FinanceCathegoryNames.OrderByDescending(fcn => fcn.CreateDate);
        }

        public FinanceCathegoryName GetFinanceCathegoryNameById(int id)
        {
            return _db.FinanceCathegoryNames.FirstOrDefault(fcn => fcn.Id == id);
        }

        public void AddFinanceCathegoryName(FinanceCathegoryName fcn)
        {
            _db.FinanceCathegoryNames.Add(fcn);
        }

        public void RemoveFinanceCathegoryName(FinanceCathegoryName fin)
        {
            _db.FinanceCathegoryNames.Remove(fin);
        }

        /** 
         * DAO Zapamiętanych pozycji finansów 
         */
        public IQueryable<FinanceFavouritesName> GetFinanceFavouritesNames()
        {
            return _db.FinanceFavouritesNames.OrderByDescending(ffn => ffn.CreateDate);
        }

        public FinanceFavouritesName GetFinanceFavouritesNameById(int id)
        {
            return _db.FinanceFavouritesNames.FirstOrDefault(ffn => ffn.Id == id);
        }

        public void AddFinanceFavouriteName(FinanceFavouritesName ffn)
        {
            _db.FinanceFavouritesNames.Add(ffn);
        }

        public void RemoveFinanceFavouriteName(FinanceFavouritesName ffn)
        {
            _db.FinanceFavouritesNames.Remove(ffn);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}