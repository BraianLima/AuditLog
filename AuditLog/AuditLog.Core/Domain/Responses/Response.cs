using System.Net;

namespace AuditLog.Core.Domain.Responses
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
