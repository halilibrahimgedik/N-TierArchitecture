using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ResponseDTOs
{
    public class NoResponseDto
    {
        //// istek yapılan endpoint'den zaten durum kodu gelicek. Bu yüzden int StatusCode'un geri dönülmesine gerek yok.
        //// Sadece kod içinde lazım olacak bize.
        //[JsonIgnore]
        //public int StatusCode { get; set; }

        //public List<string> Errors { get; set; }


        //// Başarılı Durum Metotları
        //public static NoResponseDto Success(int statusCode)
        //{
        //    return new NoResponseDto { StatusCode = statusCode };
        //}

        //// Başarısız Durum Metotları
        //public static NoResponseDto Fail(int statusCode, List<string> errors)
        //{
        //    return new NoResponseDto { StatusCode = statusCode, Errors = errors };
        //}

        //public static NoResponseDto Fail(int statusCode, string error)
        //{
        //    return new NoResponseDto { StatusCode = statusCode, Errors = new List<string> { error } };
        //}
    }
}
