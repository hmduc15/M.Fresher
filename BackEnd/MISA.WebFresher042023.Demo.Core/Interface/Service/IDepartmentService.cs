using MISA.WebFresher042023.Demo.Core.Dto.DepartmentDto;
using MISA.WebFresher042023.Demo.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Interface Department Service
    /// </summary>
    /// Author: HMDUC (17/06/2023)
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentInsertDto, DepartmentUpdateDto>
    {
        
    }
}
