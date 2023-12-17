using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.toolkit
{
    public static class Exceptions
    {
        public static string BuildMessage(Exception ex)
        {

            String message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            if (message.Contains("Violation of UNIQUE KEY"))
            {
                message = "Llave duplicada";
            }
            return message;
        }
    }
}
