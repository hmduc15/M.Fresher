using MISA.WebFresher042023.Demo.Core.Dto.CategoryDto;
using MISA.WebFresher042023.Demo.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Service
{
    /// <summary>
    /// Interface Category Service
    /// </summary>
    /// Author: HMDUC (17/06/2023)
    public interface ICategoryService : IBaseService<CategoryDto,CategoryInsertDto,CategoryUpdateDto>
    {
   
    }
}
