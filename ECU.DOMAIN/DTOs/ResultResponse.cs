using ECU.DOMAIN.DTOs.Autentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.DOMAIN.DTOs
{
    public class ResultResponse<T>
    {
        public ResultResponse() { }

        public ResultResponse(bool TieneError, string Mensaje)
        {
            this.Mensaje = Mensaje;
            this.TieneError = TieneError;
        }

        public bool TieneError { get; set; }
        public T? Result { get; set; } = default(T?);
        public string Mensaje { get; set; } = string.Empty;

        public ResultResponse<T> Failed(string message) => new ResultResponse<T>(true, message);
        public ResultResponse<T> Successful(string message) => new ResultResponse<T>(false, message);
    }
}
