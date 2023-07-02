using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Interface.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntityDto, TEntityInsertDto, TEntityUpdateDto> : ControllerBase
    {

        protected readonly IBaseService<TEntityDto, TEntityInsertDto, TEntityUpdateDto> _baseService;


        public BaseController(IBaseService<TEntityDto, TEntityInsertDto, TEntityUpdateDto> baseService)
        {

            _baseService = baseService;
        }

        /// <summary>
        /// Get List Entity 
        /// </summary>
        /// <returns>
        /// 200 - List Entity
        /// 204 - No data
        /// </returns> 
        /// Author: HMDUC (19/06/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entityDto = await _baseService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, entityDto);
        }


        /// <summary>
        ///  Function Get Entity by Code
        /// </summary>
        /// <param name="Entity">id</param>
        /// <returns>
        /// 201 - success
        /// 204 - no data
        /// </returns>
        /// Author: HMDUC (12/06/2023)

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var entityDto = await _baseService.GetByIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, entityDto);
        }

        /// <summary>
        ///  Function Add new Entity
        /// </summary>
        /// <param name="EntityInsertDto">EntityInsertDto</param>
        /// <returns>
        /// 400 - error
        /// 201 - success
        /// </returns>
        /// Author: HMDUC (19/06/2023)

        [HttpPost]
        public async Task<IActionResult> AddAsync(TEntityInsertDto entityInsertDto)
        {
            var rowsAffected = await _baseService.InsertAsync(entityInsertDto);

            if (rowsAffected == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created, rowsAffected);
            }
        }


        /// <summary>
        ///  Function update Entity
        /// </summary>
        /// <param name="EntityUpdateDto">EntityUpdateDto</param>
        /// <returns>
        ///  200 - update success
        ///  400 - error validate
        /// </returns>
        /// Author: HMDUC (13/06/2023)

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(TEntityUpdateDto entityUpdateDto)
        {
            var rowAffected = await _baseService.UpdateAsync(entityUpdateDto);

            if (rowAffected == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK);
            }
        }

        /// <summary>
        ///  Funtion Delete Entity By Id
        /// </summary>
        /// <param name="EntityId">Entity Id</param>
        /// <returns>
        ///  204 - success
        ///  400 - bad request
        /// </returns>
        /// Author: HMDUC (19/06/2023)

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var rowAffect = await _baseService.DeleteAsync(id);
            if (rowAffect > 0)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }

        /// <summary>
        ///  Funtion Delete multiple Entity By Id
        /// </summary>
        /// <param name="ids">Ids</param>
        /// <returns>
        ///  204 - success
        ///  400 - bad request
        /// </returns>
        /// Author: HMDUC (12/06/2023)
        [HttpDelete("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple(List<Guid> ids)
        {
            var rowsAffected = await _baseService.DeleteMulAsync(ids);

            if (rowsAffected < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

    }
}
