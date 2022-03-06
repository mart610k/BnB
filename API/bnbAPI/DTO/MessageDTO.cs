using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class MessageDTO
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public DateTime Date { get; set; }

        public MessageDTO()
        {
            Date = DateTime.Now;
        }

        public MessageDTO(string message, int statusCode) : this()
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}