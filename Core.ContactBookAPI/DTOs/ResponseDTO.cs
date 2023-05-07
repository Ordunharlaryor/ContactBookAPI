using Azure.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ContactBook.Core.DTOs
{
    public class ResponseDto<T>
    {
        public  int StatusCode {get; set; }
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public T? Data { get; set; } 
       

        public static ResponseDto<T> Fail(IEnumerable<string> errors, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new ResponseDto<T>
            {
                Status = false,
                Errors = errors,
                StatusCode = statusCode


            };
           
        }
        public static ResponseDto<T> Success(T data, string succssMessage = "", int statusCode = (int)HttpStatusCode.OK)
        {
          return new ResponseDto<T> 
          { 
              Status = true, 
              Message = succssMessage, 
              Data = data,
              StatusCode = statusCode 
          };
        }
    }
}
