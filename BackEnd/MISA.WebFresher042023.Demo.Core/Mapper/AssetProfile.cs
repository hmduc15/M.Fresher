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
    /// <summary>
    /// Kế thừa Profile của Mapper
    /// </summary>
    public class AssetProfile : Profile
    {
        /// <summary>
        /// Tạo các hàm mapper theo form mode
        /// </summary>
        public AssetProfile() 
        {
            CreateMap<Asset, AssetDto>();
            CreateMap<AssetInsertDto, Asset>();
            CreateMap<AssetUpdateDto, Asset>();
        }   
    }
}
