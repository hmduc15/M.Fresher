using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class AssetProfile : Profile
    {
        public AssetProfile() 
        {
            CreateMap<Asset, AssetDto>();
            CreateMap<AssetInsertDto, Asset>();
            CreateMap<AssetUpdateDto, Asset>();
        }   
    }
}
