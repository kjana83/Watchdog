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
            IServiceTester serviceTester = new ServiceTester();
            return serviceTester.Test(service);
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