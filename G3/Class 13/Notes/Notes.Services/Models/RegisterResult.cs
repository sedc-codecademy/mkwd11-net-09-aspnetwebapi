using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Models
{
    public class RegisterResult
    {
        public string Message { get; set; } = string.Empty;

        public bool IsSuccess => string.IsNullOrEmpty(Message);
    }
}
