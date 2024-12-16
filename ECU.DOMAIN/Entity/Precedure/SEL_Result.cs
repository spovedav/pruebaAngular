using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.DOMAIN.Entity.Precedure
{
    public class SEL_Result
    {
        public SEL_Result()
        {
            
        }

        public SEL_Result(bool Error, string Mensaje)
        {
             this.Error = Error;
            this.Mensaje = Mensaje;
        }

        public bool Error { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
