using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.CategoryDto;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    /// <summary>
    /// Implement Interface Category Service
    /// </summary>
    public class CategoryService : BaseService<Category,CategoryDto,CategoryInsertDto,CategoryUpdateDto>, ICategoryService
    {
         private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// use Interface Repository
        /// </summary>
        /// <param name="categoryRepository"></param>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper) 
        {
            _categoryRepository = categoryRepository;
        }

     
    }
}
