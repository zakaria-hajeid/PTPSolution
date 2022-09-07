using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Proxies.Proxies
{
    public class ResultEntity<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int? StatusCode { get; set; }
        public T Payload { get; set; }

    }
}
