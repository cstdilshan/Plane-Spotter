using PlaneSpotters.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.Core.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterUser(RegisterViewModel model);
        Task<List<RegisterViewModel>> GetAll();
    }
}
