using System;
using System.Net;
using System.Text;
using BusinessFacade.Interface;
using BusinessFacade.Models;

namespace BusinessFacade
{
    /// <summary>
    /// Service tester
    /// </summary>
    public class ServiceTester : IServiceTester
    {
        public ServiceResultsDto Test(ServiceDto service)
        {
            var serviceResults = new ServiceResultsDto
            {
                ServiceId = service.Id,
                Date = DateTime.Now,
                Status = "Amber"
            };

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(service.Url);
                request.ContentType = service.ContentType;
                request.Method = service.Method;

                if (service.Method == "Post")
                {
                    var dataStream = request.GetRequestStream();
                    var byteArray = Encoding.UTF8.GetBytes(service.Request);
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                var requestTimeSpan = DateTime.Now;
                var response = (HttpWebResponse)request.GetResponse();
                var responseTimeSpan = DateTime.Now;
                serviceResults.Duration = responseTimeSpan - requestTimeSpan;

                var responseStream = response.GetResponseStream();
                var result = new byte[response.ContentLength];
                responseStream.Read(result, 0, (int)response.ContentLength);
                var resultString = Encoding.UTF8.GetString(result);
                if (resultString.Contains(service.Keyword))
                {
                    serviceResults.Status = "Green";
                }

                serviceResults.Response = resultString;
            }
            catch (Exception exception)
            {
                serviceResults.Status = "Red";
                serviceResults.Response = exception.Message;
            }

            return serviceResults;
        }
    }
}
