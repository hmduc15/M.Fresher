using AutoMapper;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application.Mapper
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<Receipt,ReceiptDto>();
            CreateMap<ReceiptInsertDto, Receipt>();
            CreateMap<ReceiptUpdateDto, Receipt>();
        }
    }
}
