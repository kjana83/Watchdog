
using BusinessFacade.Models;

namespace BusinessFacade.Interface
{
    /// <summary>
    ///  Service Tester interface
    /// </summary>
    public interface IServiceTester
    {
        /// <summary>
        /// Tests the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        ServiceResultsDto Test(ServiceDto service);
    }
}
