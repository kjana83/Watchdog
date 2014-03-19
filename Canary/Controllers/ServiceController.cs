using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessFacade;
using BusinessFacade.Interface;
using BusinessFacade.Models;

namespace Canary.Controllers
{
    /// <summary>
    /// Service controller
    /// </summary>
    public class ServiceController : ApiController
    {
        /// <summary>
        /// The service tester
        /// </summary>
        private IServiceTester serviceTester;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceController"/> class.
        /// </summary>
        public ServiceController()
        {
            serviceTester = new ServiceTester();
        }

        /// <summary>
        /// Posts the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] ServiceDto service)
        {
            var serviceResults = serviceTester.Test(service);
            return this.Request.CreateResponse(HttpStatusCode.OK, serviceResults);
        }
    }
}
