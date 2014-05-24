using System;

namespace BusinessFacade.Models
{
    /// <summary>
    /// Service report model
    /// </summary>
    public class ServiceReportDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int ServiceId { get; set; }
    }
}
