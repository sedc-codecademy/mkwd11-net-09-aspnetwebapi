using Notes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Service.External
{
    public interface IProfileService
    {
        Task<IEnumerable<ProfileModel>> GetProfiles(string authToken);
    }
}
