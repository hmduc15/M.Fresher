using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.CategoryDto;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MySqlConnector;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<CategoryDto, CategoryInsertDto, CategoryUpdateDto>
    {
        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryService categoryService) : base(categoryService) 
        {
            _categoryService = categoryService;
        }
    }
}
