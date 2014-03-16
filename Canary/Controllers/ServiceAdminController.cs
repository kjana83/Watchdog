using BusinessFacade;
using BusinessFacade.Interface;
using BusinessFacade.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Canary.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceAdminController : ApiController
    {
        private readonly IQueryFor<EmptyParameter, IEnumerable<ServiceDto>> serviceAdmin;
        private readonly ICommandHandler<ServiceDto> serviceCommandHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAdminController"/> class.
        /// </summary>
        public ServiceAdminController()
        {
            serviceAdmin = new ServiceQuery();
            serviceCommandHandler = new ServiceCommandHandler();
        }

        /// <summary>
        /// Posts the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]ServiceDto service)
        {
            this.serviceCommandHandler.Execute(service);
            return this.Request.CreateResponse(HttpStatusCode.Created);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK,
                this.serviceAdmin.ExecuteQueryWith(new EmptyParameter()));
        }
    }
}