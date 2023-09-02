using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.BLL.Models
{
    public class CreateProfileModel
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int YearOfBirth { get; set; }

        public string? ImageUrl { get; set; } = string.Empty;
    }
}
