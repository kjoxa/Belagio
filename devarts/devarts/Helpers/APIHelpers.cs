using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace devarts.Helpers
{
    public class NBP_API_Helper
    {
        private static Logger nLog = LogManager.GetCurrentClassLogger();

        public string GetExchangeRates(string currencyCode, string startDate, string endDate)
        {
            try
            {
                var client = WebRequest.Create("http://api.nbp.pl/api/exchangerates/rates/c/" + currencyCode + "/" + startDate + "/" + endDate + "?format=json") as HttpWebRequest;
                client.ContentType = "text/json";
                client.Method = "GET";

                string response = new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd();
                return response;
            }

            catch (WebException ex)
            {

                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    switch (resp.StatusCode)
                    {
                        case HttpStatusCode.Forbidden:
                            nLog.Error("Odmowa dostępu");
                            return "Odmowa dostępu";
                        case HttpStatusCode.InternalServerError:
                            nLog.Error("Wewnętrzny błąd serwera");
                            return "Wewnętrzny błąd serwera";
                        case HttpStatusCode.RequestTimeout:
                            nLog.Error("Upłynął limit czasu żądania");
                            return "Upłynął limit czasu żądania";
                        case HttpStatusCode.Unauthorized:
                            nLog.Error("401: Brak autoryzacji");
                            return "401: Brak autoryzacji";
                        case HttpStatusCode.NotFound:
                            nLog.Error("404: Brak wyników - sprawdź wywołanie URL");
                            return "404: Brak wyników - sprawdź wywołanie URL";
                        default:
                            nLog.Error("Nieznany wyjątek: " + resp.ToString());
                            return "Nieznany wyjątek: " + resp.ToString();
                    }
                }
                else
                {
                    nLog.Error("Nieznany wyjątek: " + ex.ToString());
                    return "Nieznany wyjątek: " + ex.ToString();
                }
            }
        }

        public string GetCurrencyToday(string currencyCode)
        {
            try
            {
                var client = WebRequest.Create("http://api.nbp.pl/api/exchangerates/rates/c/" + currencyCode + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "?format=json") as HttpWebRequest;
                client.ContentType = "text/json";
                client.Method = "GET";

                string response = new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd();                
                dynamic jSONobject = JsonConvert.DeserializeObject(response);

                return jSONobject.rates[0]["ask"].ToString();
            }

            catch (WebException ex)
            {

                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    switch (resp.StatusCode)
                    {
                        case HttpStatusCode.Forbidden:
                            nLog.Error("Odmowa dostępu");
                            return "Odmowa dostępu";
                        case HttpStatusCode.InternalServerError:
                            nLog.Error("Wewnętrzny błąd serwera");
                            return "Wewnętrzny błąd serwera";
                        case HttpStatusCode.RequestTimeout:
                            nLog.Error("Upłynął limit czasu żądania");
                            return "Upłynął limit czasu żądania";
                        case HttpStatusCode.Unauthorized:
                            nLog.Error("401: Brak autoryzacji");
                            return "401: Brak autoryzacji";
                        case HttpStatusCode.NotFound:
                            nLog.Error("404: Brak wyników - sprawdź wywołanie URL");
                            return "404: Brak wyników - sprawdź wywołanie URL";
                        default:
                            nLog.Error("Nieznany wyjątek: " + resp.ToString());
                            return "Nieznany wyjątek: " + resp.ToString();
                    }
                }
                else
                {
                    nLog.Error("Nieznany wyjątek: " + ex.ToString());
                    return "Nieznany wyjątek: " + ex.ToString();
                }
            }
        }
    }
}