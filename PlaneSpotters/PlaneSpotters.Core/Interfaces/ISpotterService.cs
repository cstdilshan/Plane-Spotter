using PlaneSpotters.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.Core.Interfaces
{
    public interface ISpotterService
    {
        Task<bool> Create(SpotterViewModel model);
        Task<List<SpotterViewModel>> GetAll();
        Task<SpotterViewModel> GetById(int id);
        Task<bool> Update(SpotterViewModel model);
    }
}
