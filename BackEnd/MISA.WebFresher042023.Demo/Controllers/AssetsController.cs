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
        /// Function get EntityCode of new Entity
        /// </summary>
        /// <returns>EntityCode</returns>
        /// Author: HMDUC (19/06/2023)
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCodeAsync()
        {
            var newAssetCode = await _assetService.GetNewCodeAsync();

            return Ok(newAssetCode);
        }


        /// <summary>
        ///  Funtion Get list Asset Pagging
        /// </summary>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageNumber">pageNumber</param>
        /// <returns>
        ///   {Assets, totalRecord}
        /// </returns>
        /// Author: HMDUC (19/06/2023)

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
    }
}
