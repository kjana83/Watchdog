using BusinessFacade;
using BusinessFacade.Interface;
using BusinessFacade.Models;
using System;
using System.Net;
using System.Text;

namespace Sentry
{
    /// <summary>
    /// To monitor the service
    /// </summary>
    public static class ServiceMonitor
    {
        /// <summary>
        /// Invokes the service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static ServiceResultsDto InvokeService(ServiceDto service)
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
                var content = responseStream.Read(result, 0, (int)response.ContentLength);
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

        /// <summary>
        /// Saves the results.
        /// </summary>
        /// <param name="serviceResults">The service results.</param>
        public static void SaveResults(ServiceResultsDto serviceResults)
        {
            ICommandHandler<ServiceResultsDto> serviceResultsCommandHandler = new ServiceResultsCommandHandler();

            serviceResultsCommandHandler.Execute(serviceResults);
        }
    }
}