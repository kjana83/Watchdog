namespace BusinessFacade.Models
{
    public class ServiceDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SoapAction { get; set; }

        public string Url { get; set; }

        public string Request { get; set; }

        public string Keyword { get; set; }

        public string ContentType { get; set; }

        public string Method { get; set; }
    }
}