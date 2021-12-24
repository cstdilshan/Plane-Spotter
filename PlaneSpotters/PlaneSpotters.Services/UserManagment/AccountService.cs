using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlaneSpotters.Core.Entities;
using PlaneSpotters.Core.Interfaces;
using PlaneSpotters.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.Services.UserManagment
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected IMapper _mapper { get; set; }
        public AccountService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._mapper = mapper;
        }

        public async Task<bool> RegisterUser(RegisterViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait(true);
                if (user is null)
                {
                    var appUser = _mapper.Map<ApplicationUser>(model);
                    var result = await _userManager.CreateAsync(appUser, model.Password).ConfigureAwait(true);

                    return result.Succeeded;
                }
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        public async Task<List<RegisterViewModel>> GetAll()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync().ConfigureAwait(true);
                var userDtos = _mapper.Map<IList<RegisterViewModel>>(users);
                return userDtos.ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<RegisterViewModel>();
        }

    }
}
