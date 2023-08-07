using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Domain.Enum;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using MISA.WebFresher042023.Demo.Application.Service;

namespace MISA.WebFresher042023.Demo.Controllers
{
    /// <summary>
    /// Api Assset 
    /// Author: HMDUC (11/06/2023)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetsController : BaseController<AssetDto, AssetInsertDto, AssetUpdateDto, AssetTranferDto>
    {

        #region Field
        private readonly IAssetService _assetService;

        #endregion

        #region Constructor
        public AssetsController(IAssetService assetService) : base(assetService)
        {
            _assetService = assetService;
        }
        #endregion


        /// <summary>
        /// Hàm check xem tài sản có chứng từ không theo Id phục vụ nghiệp vụ xóa
        /// </summary>
        /// <param name="ids">Danh sách chứng từ cần xóa</param>
        /// <returns>Danh sách chứng từ</returns>
        /// Author: HMDUC (05/08/2023)
        [HttpPost("CheckExistReceipt")]
        public async Task<IActionResult> CheckExistReceipt(List<Guid> ids)
        {
            var result = await _assetService.CheckExistReceipt(ids);

            return Ok(result);
        }


        /// <summary>
        /// Hàm lấy ra tất cả tài sản theo mã chứng từ
        /// </summary>
        /// <param name="receiptId">Id của chứng từ</param>
        /// <returns>Danh sách tài sản</returns>
        [HttpGet("Receipt/{receiptId}")]
        public async Task<IActionResult> GetAssetByReceiptId(Guid receiptId)
        {
            var result = await _assetService.GetAssetAllByReceipId(receiptId);

            return Ok(result);
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
                return StatusCode((int)MISACode.BadResquest);
            }
            else
            {
                return Ok(assetPaggingList);
            }
        }
        #endregion

        /// <summary>
        /// Hàm phân trang + tìm kiếm tài sản phục vụ thêm tài sản vào chứng từ
        /// </summary>
        /// <param name="ids">Danh sach Id tài sản cần loại bỏ</param>
        /// <param name="pageSize">Số dòng hiển thị</param>
        /// <param name="pageNumber">Số trang</param>
        /// Author: HMDUC (28/07/2023)
        [HttpPost("PaggingAssetChose")]
        public async Task<object> GetPaggingAssetChose([FromBody] DataBodyPost data )
        {
        
            var assetChose  = await _assetService.GetPaggingAssetChose(data.ids, data.pageSize, data.pageNumber);

            if (assetChose == null)
            {
                return StatusCode((int)MISACode.BadResquest);
            }
            else
            {
                return Ok(assetChose);
            }

        }


        public class DataBodyPost
        {
           
            public List<Guid> ids { get; set; }
            public int pageSize { get; set; }
            public int pageNumber { get; set; }

        }

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
