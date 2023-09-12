using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Models
{
    public class CreatePostModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public int UserId { get; set; }
    }
}
