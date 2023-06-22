using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.DepartmentDto;
using MISA.WebFresher042023.Demo.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Mapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentInsertDto, DepartmentDto>();
            CreateMap<DepartmentUpdateDto, DepartmentDto>();
        }
    }
}
