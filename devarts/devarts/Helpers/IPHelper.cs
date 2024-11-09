using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace devarts.Helpers
{
    public class IPHelper
    {
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