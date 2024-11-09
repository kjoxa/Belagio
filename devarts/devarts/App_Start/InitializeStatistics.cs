using devarts.Helpers;
using devarts.Models;
using devarts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace App_Start
{
    public class InitializeStatistics
    {
        private AdminRepository _statisticRepo;
        private string createDate;

        public InitializeStatistics()
        {
            _statisticRepo = new AdminRepository();
            createDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm"); ;
        }

        public void AddUser(string ip, int deviceType)
        {
            var _stat = new Statistic();

            _stat.DateTime = createDate;
            _stat.IpAddress = ip;
            _stat.Name = GetComputerName(ip);
            _stat.DeviceType = deviceType;

            var lastRecord = _statisticRepo.GetTheSameRecord(_stat.DateTime, ip);

            // jeśli ostatni rekord nie jest pusty - to znaczy jeśli baza danych nie jest przed chwilą wyczyszczona
            if (lastRecord == null)
            {
                try
                {
                    var geoLoc = new IpStackGeoLocalization();
                    _stat.Country = geoLoc.GetUserLocationDetailsyByIp(ip).Country;
                    _stat.Continent = geoLoc.GetUserLocationDetailsyByIp(ip).Continent;
                    _stat.GeoLong = geoLoc.GetUserLocationDetailsyByIp(ip).Longitude;
                    _stat.GeoLat = geoLoc.GetUserLocationDetailsyByIp(ip).Latitude;
                }
                catch
                {
                    _stat.Country = "N/A";
                    _stat.Continent = "N/A";
                    _stat.GeoLong = "N/A";
                    _stat.GeoLat = "N/A";
                }
                // sprawdzamy czy dodawany rekord nie jest powtórzeniem requestu
                _statisticRepo.Add(_stat);
                _statisticRepo.SaveChanges();
            }

        }

        public void AddTrace(string ip, string url, int deviceType)
        {

            var _tracing = new Tracing();

            _tracing.DateTime = createDate;
            _tracing.HostAndIp = GetComputerName(ip) + " (" + ip + ")";
            _tracing.Url = url;
            _tracing.DeviceType = deviceType;

            _statisticRepo.AddTrace(_tracing);
            _statisticRepo.SaveChanges();

        }

        public string GetComputerName(string clientIP)
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(clientIP);
                return hostEntry.HostName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}