using System.Net;

namespace ProjectAPI.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object? Result { get; set; }
    }
}
