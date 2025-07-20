using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Errors
{
    public class ErrorResponce
    {
        public ErrorResponce(string status, string message)
        {
            Status = status;
            Message = message;
        }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
