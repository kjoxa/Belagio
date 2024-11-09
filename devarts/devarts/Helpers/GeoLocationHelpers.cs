using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace devarts.Helpers
{
    public class IpStack
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("continent_name")]
        public string Continent { get; set; }

        [JsonProperty("country_name")]
        public string Country { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }
    }

    public class IpStackGeoLocalization
    {
        public IpStack GetUserLocationDetailsyByIp(string ip)
        {
            var ipInfo = new IpStack();
            try
            {
                // jeśli nie uruchamiamy weba lokalnie, to...
                if (!ip.Contains("::1"))
                {
                    var info = new WebClient().DownloadString("http://api.ipstack.com/" + ip + "?access_key=41b994140f071ca73a45c7db0eddb40c");
                    ipInfo = JsonConvert.DeserializeObject<IpStack>(info);
                }
                // w przeciwnym wypadku
                else
                {
                    // podstawiamy wartości do modelu
                    var info = "{'ip':'localhost','type':'ipv4','continent_code':'EU','continent_name':'Europe','country_code':'PL','country_name':'Poland - WebDev'," +
                        "'region_code':null,'region_name':null,'city':null,'zip':null,'latitude':0,'longitude':0," +
                        "'location':{" +
                        "'geoname_id':null,'capital':'Paris','languages':" +
                        "[" +
                            "{'code':'fr','name':'French','native':'Fran\u00e7ais'}" +
                        "]" +
                        ",'country_flag':'http:\\/\\/assets.ipstack.com\\/flags\\/fr.svg','country_flag_emoji':'\ud83c\uddeb\ud83c\uddf7','country_flag_emoji_unicode':'U+1F1EB U+1F1F7','calling_code':'33','is_eu':true}}"; ;
                    ipInfo = JsonConvert.DeserializeObject<IpStack>(info);
                }
            }
            catch (Exception ex)
            {
                //Exception Handling
            }

            return ipInfo;
        }

        public string getIpInformation(string ipAddress)
        {
            try
            {
                //var client = WebRequest.Create("http://api.ipstack.com/" + ipAddress + "?access_key=41b994140f071ca73a45c7db0eddb40c") as HttpWebRequest;
                //client.ContentType = "text/json";
                //client.Method = "POST";

                //string response = new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd();
                //return response;

                return "{'ip':'54.36.148.251','type':'ipv4','continent_code':'EU','continent_name':'Europe','country_code':'FR','country_name':'France'," +
                        "'region_code':null,'region_name':null,'city':null,'zip':null,'latitude':48.8582,'longitude':2.3387000000000002," +
                        "'location':{" +
                        "'geoname_id':null,'capital':'Paris','languages':" +
                        "[" +
                            "{'code':'fr','name':'French','native':'Fran\u00e7ais'}" +
                        "]" +
                        ",'country_flag':'http:\\/\\/assets.ipstack.com\\/flags\\/fr.svg','country_flag_emoji':'\ud83c\uddeb\ud83c\uddf7','country_flag_emoji_unicode':'U+1F1EB U+1F1F7','calling_code':'33','is_eu':true}}";
            }

            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        /*
          https://ipstack.com/plan
          https://status.ipstack.com/

          http://api.ipstack.com/IP/?acces_key=ACCES_KEY
          https://api.ipstack.com/54.36.148.251?access_key=41b994140f071ca73a45c7db0eddb40c

          
          -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

          {"ip":"54.36.148.251","type":"ipv4","continent_code":"EU",
          "continent_name":"Europe","country_code":"FR","country_name":"France",
          "region_code":null,"region_name":null,"city":null,"zip":null,"latitude":48.8582,
          "longitude":2.3387000000000002,
          "location":{"geoname_id":null,"capital":"Paris","languages":
             [
               {"code":"fr","name":"French","native":"Fran\u00e7ais"}
             ]
          ,"country_flag":"http:\/\/assets.ipstack.com\/flags\/fr.svg","country_flag_emoji":"\ud83c\uddeb\ud83c\uddf7","country_flag_emoji_unicode":"U+1F1EB U+1F1F7","calling_code":"33","is_eu":true}}

         -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
         */
    }
}