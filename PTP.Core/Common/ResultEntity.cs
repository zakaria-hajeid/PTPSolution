using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PTP.Core.Common
{
    public class ResultEntity<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int? StatusCode { get; set; }
         public T Payload { get; set; }
    
  
    public ResultEntity(bool isSuccess, string message, T payload, int? statusCode = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
        Payload = payload;
    }

        public static ResultEntity<T> Succeeded(string msg, T payload, int? statusCode = null)
        {
            return new ResultEntity<T>(true, msg, payload, statusCode ?? (int)HttpStatusCode.OK);
        }


        public static ResultEntity<T> Failed(string msg, T payload, int? statusCode = null)
        {
            return new ResultEntity<T>(false, msg, payload, statusCode);
        }

    }
}
