using System.Collections.Generic;
using System.Net;

namespace api.Domain.VM.Shared
{
    public class BaseResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        public T Data { get; set; }
    }
}
