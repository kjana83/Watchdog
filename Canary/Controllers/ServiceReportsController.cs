using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessFacade;
using BusinessFacade.Interface;
using BusinessFacade.Models;

namespace Canary.Controllers
{
    /// <summary>
    /// Service reports controller
    /// </summary>
    public class ServiceReportsController : ApiController
    {
        /// <summary>
        /// The service report query
        /// </summary>
        private IQueryFor<ServiceReportDto, IEnumerable<ServiceResultsDto>> serviceReportQuery;

        /// <summary>
        /// The service admin
        /// </summary>
        private readonly IQueryFor<EmptyParameter, IEnumerable<ServiceDto>> serviceAdmin;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceReportsController"/> class.
        /// </summary>
        public ServiceReportsController()
        {
            serviceReportQuery = new ServiceReportQuery();
            this.serviceAdmin = new ServiceQuery();
        }

        // GET api/<controller>
        /// <summary>
        /// Gets the specified service report.
        /// </summary>
        /// <param name="serviceReport">The service report.</param>
        /// <returns></returns>
        public HttpResponseMessage Get([FromUri]ServiceReportDto serviceReport)
        {
            var result = serviceReportQuery.ExecuteQueryWith(serviceReport);
            var services = this.serviceAdmin.ExecuteQueryWith(new EmptyParameter());

            result.ToList().ForEach(p =>
            {
                var service = services.FirstOrDefault(ser => ser.Id == p.ServiceId);
                p.Name = service.Name;
                p.Url = service.Url;
            });
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}