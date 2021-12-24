using AutoMapper;
using PlaneSpotters.Core.Entities;
using PlaneSpotters.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegisterViewModel>()
                .ReverseMap();

            CreateMap<PlaneSpotter, SpotterViewModel>()
                .ReverseMap();
        }
    }
}
