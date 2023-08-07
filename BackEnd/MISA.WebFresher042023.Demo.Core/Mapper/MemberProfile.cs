using AutoMapper;
using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application.Mapper
{
    public class MemberProfile : Profile
    {
        public MemberProfile() 
        {
            CreateMap<MemberReceipt, MemberDto>();
            CreateMap<MemberInsertDto, MemberReceipt>();
            CreateMap<MemberUpdateDto, MemberReceipt>();
           
        }
    }
}
