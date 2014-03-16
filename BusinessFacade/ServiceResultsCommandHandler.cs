using System;
using System.Configuration;
using BusinessFacade.Interface;
using BusinessFacade.Models;
using ServiceStack.Redis;

namespace BusinessFacade
{
    /// <summary>
    /// To save results
    /// </summary>
    public class ServiceResultsCommandHandler : ICommandHandler<ServiceResultsDto>
    {
        /// <summary>
        /// Executes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public void Execute(ServiceResultsDto input)
        {
            using (var client = new RedisClient())
            {
                client.ChangeDb(Int32.Parse(ConfigurationManager.AppSettings["DBNAME"]));
                client.Increment(GeneralConstants.SERVICE_RESULTS_ID, 1);
                input.Id = client.Get<int>(GeneralConstants.SERVICE_RESULTS_ID);
                var clientService = client.As<ServiceResultsDto>();
                clientService.Lists[GeneralConstants.SERVICE_RESULTS].Add(input);
            }
        }
    }
}