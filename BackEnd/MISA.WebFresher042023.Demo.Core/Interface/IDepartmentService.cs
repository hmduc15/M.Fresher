using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application.Interface
{
    /// <summary>
    /// Interface Department Service
    /// </summary>
    /// Author: HMDUC (17/06/2023)
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentInsertDto, DepartmentUpdateDto>
    {

    }
}
