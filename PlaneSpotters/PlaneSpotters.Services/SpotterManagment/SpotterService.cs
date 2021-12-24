using AutoMapper;
using PlaneSpotters.Core.Entities;
using PlaneSpotters.Core.Interfaces;
using PlaneSpotters.Core.Model;
using PlaneSpotters.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.Services.SpotterManagment
{
    public class SpotterService : ISpotterService
    {
        private IBaseRepository<PlaneSpotter> _planeSpotter { get; set; }
        private IMapper _mapper { get; set; }
        private IUnitOfWork _uow { get; set; }
        public SpotterService(IBaseRepository<PlaneSpotter> planeSpotter, IMapper mapper, IUnitOfWork uow)
        {
            this._planeSpotter = planeSpotter;
            this._mapper = mapper;
            this._uow = uow;
        }
        public async Task<bool> Create(SpotterViewModel model)
        {
            try
            {
                var spotter = _mapper.Map<PlaneSpotter>(model);
                var result = await _planeSpotter.CreateAsync(spotter).ConfigureAwait(true);
                await _uow.Commit().ConfigureAwait(true);
                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }
        public async Task<List<SpotterViewModel>> GetAll()
        {
            try
            {
                var result = _planeSpotter.GetAll();
                var spotters = _mapper.Map<IList<SpotterViewModel>>(result);
                return spotters.ToList();
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<SpotterViewModel> GetById(int id)
        {
            try
            {
                var result = await _planeSpotter.FindByConditionAsync(x=> x.Id ==id).ConfigureAwait(true);
                var spotter = _mapper.Map<SpotterViewModel>(result);
                return spotter;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<bool> Update(SpotterViewModel model)
        {
            try
            {
                var spotter = await _planeSpotter.FindByConditionAsync(x => x.Id == model.Id).ConfigureAwait(true);
                spotter.Make = model.Make;
                spotter.Model = model.Model;
                spotter.Registration = model.Registration;
                spotter.Location = model.Location;
                spotter.DateTime = model.DateTime;
                var result = await _planeSpotter.UpdateAsync(spotter).ConfigureAwait(true);

                await _uow.Commit().ConfigureAwait(true);
                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }
    }
}
