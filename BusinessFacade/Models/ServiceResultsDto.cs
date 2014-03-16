using System;

namespace BusinessFacade.Models
{
    /// <summary>
    /// Service results dto
    /// </summary>
    public class ServiceResultsDto
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public string Status { get; set; }

        public string Response { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime Date { get; set; }
    }
}