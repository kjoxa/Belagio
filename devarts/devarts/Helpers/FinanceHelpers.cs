using devarts.Models;
using devarts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Helpers
{
    public class FinanceBarModel
    {       
        public string[] monthsNames = new string[] {"Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"};
        public string monthsNamesJS = "Styczeń,Luty,Marzec,Kwiecień,Maj,Czerwiec,Lipiec,Sierpień,Wrzesień,Październik,Listopad,Grudzień";
        public int[] monthsIncome = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] monthsExpense = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public decimal sumExpense = 0;
        public decimal sumIncome = 0;        
    }
    
    public class FinancePieModel
    {
        // tu powinno być z bazy danych
        public string[] cathegoryNames = new string[] { "Odżywianie", "Leczenie", "Profilaktyka", "Krycia", "Sprzedaż", "Akcesoria", "Wystawy", "Sport", "Inne" };
        public int[] sumCathegories = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }
    
    public class FinanceCharts
    {
        public FinanceRepository _financeRepo;
        public FinanceCharts()
        {
            _financeRepo = new FinanceRepository();
        }

        /// WYKRES SŁUPKOWY (BAR) ORAZ LINIOWY (LINE CHART)
        public FinanceBarModel YearFinanceBar(int year)
        {
            FinanceBarModel financeBar = new FinanceBarModel();
            var financeList = _financeRepo.GetFinances().AsEnumerable().Where(fin => fin.DateTo.Year == year);
            for (int i = 1; i <= 13; i++)
            {
                financeBar.sumExpense = financeList.Where(fin => (fin.DateTo.Month == (i) && fin.IsExpense == true)).Select(fin => fin.Amount).Sum();
                financeBar.sumIncome = financeList.Where(fin => (fin.DateTo.Month == (i) && fin.IsExpense == false)).Select(fin => fin.Amount).Sum();

                financeBar.monthsExpense[i-1] = Convert.ToInt32(financeBar.sumExpense);
                financeBar.monthsIncome[i-1] = Convert.ToInt32(financeBar.sumIncome);
            }

            return financeBar;
        }

        public FinancePieModel YearFinancePie(int year)
        {
            FinancePieModel financePie = new FinancePieModel();

            var financeList = _financeRepo.GetFinances().AsEnumerable().Where(fin => fin.DateTo.Year == year);
            for (int i = 0; i <= 8; i++)
            {
                financePie.sumCathegories[i] = Convert.ToInt32(financeList.Where(fin => (fin.Cathegory == financePie.cathegoryNames[i])).Select(fin => fin.Amount).Sum());
            }

            return financePie;
        }
    }

}