using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Domain.Enum;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntityDto, TEntityInsertDto, TEntityUpdateDto> : ControllerBase
    {

        #region Field
        protected readonly IBaseService<TEntityDto, TEntityInsertDto, TEntityUpdateDto> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntityDto, TEntityInsertDto, TEntityUpdateDto> baseService)
        {

            _baseService = baseService;
        } 
        #endregion

        /// <summary>
        ///  API lấy ra tất các entity
        /// </summary>
        /// <returns>
        /// 200 - List Entity
        /// </returns> 
        /// Author: HMDUC (19/06/2023)
        #region GetAll 
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entityDto = await _baseService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, entityDto);
        }
        #endregion


        /// <summary>
        ///  API lấy ra entity theo ID
        /// </summary>
        /// <param name="id"> Id Entity</param>
        /// <returns>
        /// 200 - success
        /// </returns>
        /// Author: HMDUC (12/06/2023)
        #region GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var entityDto = await _baseService.GetByIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, entityDto);
        }
        #endregion

        /// <summary>
        ///  API thêm mới Entity
        /// </summary>
        /// <param name="TEntityInsertDto">entityInsertDto</param>
        /// <returns>
        /// 400 - error
        /// 201 - success
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region AddEntity
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
        #endregion


        /// <summary>
        ///  API update Entity
        /// </summary>
        /// <param name="TEntityUpdateDto">entityUpdateDto</param>
        /// <returns>
        ///  200 - update success
        ///  400 - error validate
        /// </returns>
        /// Author: HMDUC (13/06/2023)
        #region UpdateEntity
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
        #endregion

        /// <summary>
        ///  API xóa Entity theo Id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>
        ///  204 - success
        ///  400 - bad request
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region DeleteEntity
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
        #endregion

        /// <summary>
        ///  API xóa nhiều Entity theo List Id
        /// </summary>
        /// <param name="ids">Danh sách Id</param>
        /// <returns>
        ///  204 - success
        ///  400 - bad request
        /// </returns>
        /// Author: HMDUC (12/06/2023)
        #region DeleteMultiple
        [HttpDelete("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple(List<Guid> ids)
        {
            var rowsAffected = await _baseService.DeleteMulAsync(ids);

            if (rowsAffected > 0)
            {
                return StatusCode(StatusCodes.Status204NoContent);
                
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        } 
        #endregion

    }
}
