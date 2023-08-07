using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Application;
using MySqlConnector;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<CategoryDto, CategoryInsertDto, CategoryUpdateDto, CategoryTranferDto>
    {
        #region Field
        private readonly ICategoryService _categoryService;
        #endregion

        #region Constructor
        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
            _categoryService = categoryService;
        } 
        #endregion
    }
}
