using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Interface.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Api Assset 
    /// Author: HMDUC (11/06/2023)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetsController : BaseController<AssetDto, AssetInsertDto, AssetUpdateDto>
    {

        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService) : base(assetService)
        {
            _assetService = assetService;
        }


        /// <summary>
        /// API lấy ra mã tài sản mới
        /// </summary>
        /// <returns>Mã tài sản </returns>
        /// Author: HMDUC (19/06/2023)
        #region GetNewCode
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCodeAsync()
        {
            var newAssetCode = await _assetService.GetNewCodeAsync();

            return Ok(newAssetCode);
        } 
        #endregion



        /// <summary>
        ///  API phân trang
        /// </summary>
        /// <param name="pageSize">Số dòng hiển thị</param>
        /// <param name="pageNumber">Số trang</param>
        /// <returns>
        ///   Object chứa DS tài sản 
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region GetPaging
        [HttpGet("PaggingList")]
        public async Task<object> GetPagging(int pageSize, int pageNumber, string searchInput, string m_DepartmentName, string m_CategoryName)
        {
            var assetPaggingList = await _assetService.GetPagging(pageSize, pageNumber, searchInput, m_DepartmentName, m_CategoryName);

            if (assetPaggingList == null)
            {
                return StatusCode(400);
            }
            else
            {
                return Ok(assetPaggingList);
            }
        }
        #endregion

        /// <summary>
        /// API tải xuống file Excel
        /// </summary>
        /// <returns> File excel</returns>
        /// Author: HMDUC (29/06/2023)
        #region GetListExport
        [HttpGet("Export")]
        public async Task<IActionResult> GetListExport()
        {
            var stream = await _assetService.ExportExcel();
            string excelName = $"AssetList_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        } 
        #endregion


    }
}
