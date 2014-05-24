using System;
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
    /// Service results controller
    /// </summary>
    public class ServiceResultsController : ApiController
    {
        /// <summary>
        /// The service results query
        /// </summary>
        private IQueryFor<EmptyParameter, IEnumerable<ServiceResultsDto>> serviceResultsQuery;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResultsController"/> class.
        /// </summary>
        public ServiceResultsController()
        {
            this.serviceResultsQuery = new ServiceResultsQuery();
        }

        /// <summary>
        /// Gets the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var serviceResults = this.serviceResultsQuery.ExecuteQueryWith((EmptyParameter) null);
            
            return this.Request.CreateResponse(HttpStatusCode.OK, serviceResults);
        }
    }
}
