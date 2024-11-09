using System;
using System.Collections.Generic;
using Chart.Mvc.ComplexChart;
using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;

namespace devarts
{
    public class FakeComplexChartData
    {
        private KennelRepository _kennelRepo;

        public FakeComplexChartData()
        {
            _kennelRepo = new KennelRepository();
        }

        public List<string> Labels
        {
            get
            {
                var allDogsNames = _kennelRepo.GetAllDogs();
                List<string> result = new List<string>();
                foreach (var pos in allDogsNames)
                {
                    if (pos.DogName.ToLower().Contains("cyre") == false)
                    {
                        result.Add(pos.DogName);
                    }                    
                }
                return result;
                //return string.Join(",", allDogsNames.ToString()); //allDogsNames.ToString();
                //return new[]
                //           {
                //               "Mini",
                //               "Lea",
                //               "Cuki",
                //               "Nana",
                //               "Skazka",
                //               "Frups",
                //               "Xulia"
                //           };
            }
        }

        public IEnumerable<ComplexDataset> Datasets
        {
            get
            {
                var allDogsNames = _kennelRepo.GetAllDogs();
                var years = new List<double>();
                var maxyears = new List<double>();
                AgeHelper howOld = new AgeHelper();
                foreach (var pos in allDogsNames)
                {
                    years.Add(Convert.ToDouble(howOld.HowManyYears(pos.BornDate)));
                    maxyears.Add(Convert.ToDouble(15));
                }

                return new List<ComplexDataset>
                           {
                               new ComplexDataset
                                   {
                                       Data = years,//new List<double> { 65, 59, 80, 81, 56, 55, 40 },
                                       Label = "My First dataset",
                                       FillColor = "green",
                                       StrokeColor = "rgba(151,187,205,1)",
                                       PointColor = "rgba(151,187,205,1)",
                                       PointStrokeColor = "#fff",
                                       PointHighlightFill = "#fff",
                                       PointHighlightStroke = "rgba(151,187,205,1)",              
                                       },
                                   //new ComplexDataset
                                   //    {
                                   //        Data = maxyears,//new List<double> { 28, 48, 40, 19, 86, 27, 90 },
                                   //        Label = "My Second dataset",
                                   //        FillColor = "red",
                                   //        StrokeColor = "rgba(151,187,205,1)",
                                   //        PointColor = "rgba(151,187,205,1)",
                                   //        PointStrokeColor = "#fff",
                                   //        PointHighlightFill = "#fff",
                                   //        PointHighlightStroke = "rgba(151,187,205,1)",
                                   //    }
                               };
            }
        }
    }
}
