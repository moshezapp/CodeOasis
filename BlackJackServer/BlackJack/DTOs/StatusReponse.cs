using BlackJack.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DTOs
{
    public class StatusReponse
    {
        public int StatusCode { get; set; }
        public string StatusDesc { get; set; }

        public StatusReponse(StatusCodesEnum statusCode)
        {
            StatusCode = (int)statusCode;
            StatusDesc = statusCode.ToString();
        }
    }
}
