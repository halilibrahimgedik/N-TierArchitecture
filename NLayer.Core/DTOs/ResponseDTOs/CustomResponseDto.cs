using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ResponseDTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        // istek yapılan endpoint'den zaten durum kodu gelicek. Bu yüzden int StatusCode'un geri dönülmesine gerek yok.
        // Sadece kod içinde lazım olacak bize.
        [JsonIgnore]
        public int StatusCode { get; set; }

        public List<string> Errors { get; set; }


        // Başarılı Durum Metotları
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data };
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }


        // Başarısız Durum Metotları
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
