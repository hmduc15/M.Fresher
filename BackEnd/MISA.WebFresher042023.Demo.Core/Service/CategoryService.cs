using AutoMapper;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Application.Service;
using MISA.WebFresher042023.Demo.Domain.Interface;

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
    public class CategoryService : BaseService<Category, CategoryDto, CategoryInsertDto, CategoryUpdateDto>, ICategoryService
    {
        #region Field
        private readonly ICategoryRepository _categoryRepository;
        #endregion

        #region Constructor
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
        } 
        #endregion


    }
}
