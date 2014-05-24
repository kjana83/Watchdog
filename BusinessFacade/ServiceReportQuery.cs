using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BusinessFacade.Interface;
using BusinessFacade.Models;
using ServiceStack.Redis;

namespace BusinessFacade
{
    /// <summary>
    /// Service report query
    /// </summary>
    public class ServiceReportQuery : IQueryFor<ServiceReportDto,IEnumerable<ServiceResultsDto>>
    {
        /// <summary>
        /// Executes the query with.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public IEnumerable<ServiceResultsDto> ExecuteQueryWith(ServiceReportDto input)
        {
            using (var redisClient = new RedisClient())
            {
                redisClient.ChangeDb(Int32.Parse(ConfigurationManager.AppSettings["DBNAME"]));
                var serviceClient = redisClient.As<ServiceResultsDto>();
                IEnumerable<ServiceResultsDto> lists = serviceClient.Lists[GeneralConstants.SERVICE_RESULTS].ToList();
                if (input.ServiceId!=0)
                    lists= lists.Where(p=>p.ServiceId==input.ServiceId);
                if (input.FromDate.HasValue && input.ToDate.HasValue)
                {
                    lists = lists.Where(p => input.FromDate.Value <= p.Date && 
                        input.ToDate.Value.AddDays(1) >= p.Date);
                }
                else if (input.FromDate.HasValue)
                {
                    lists =
                        lists.Where(
                            p =>
                                p.Date.Day == input.FromDate.Value.Day &&
                                p.Date.Month == input.FromDate.Value.Month &&
                                p.Date.Year == input.FromDate.Value.Year);
                }

                return lists.OrderByDescending(p=>p.Date);
            }
        }
    }
}
