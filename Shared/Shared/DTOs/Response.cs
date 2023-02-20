using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class Response<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }

        public static Response<T> Success(T data, int statuscode)
        {
            return new Response<T> { Data = data, StatusCode = statuscode ,IsSuccessful=true};
        }

        public static Response<T> Success(int statuscode)
        {
            return new Response<T> { Data = default(T), StatusCode = statuscode, IsSuccessful = true };
        }
        public static Response<T> Fail(List<string> errors, int statuscode)
        {
           return new Response<T>{ Errors = errors, StatusCode = statuscode, IsSuccessful = false };
        }
        public static Response<T> Fail(string errors, int statuscode)
        {
            return new Response<T> { Errors = new List<string> { errors }, StatusCode = statuscode, IsSuccessful = false };
        }
    }
}
