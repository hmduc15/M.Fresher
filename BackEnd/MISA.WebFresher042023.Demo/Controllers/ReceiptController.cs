using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Application.Service;
using MISA.WebFresher042023.Demo.Domain.Entity;

namespace MISA.WebFresher042023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReceiptController : BaseController<ReceiptDto, ReceiptInsertDto, ReceiptUpdateDto, ReceiptTranferDto>
    {
        private IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService) : base(receiptService)
        {
            _receiptService = receiptService;
        }


        /// <summary>
        /// Api lấy ra mã chứng từ mới nhất phục vụ thêm mới chứng từ
        /// </summary>
        /// <returns>Mã chứng từ</returns>
        /// Author: HMDUC (27/07/2023)
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCode()
        {
            var newReceiptCode = await _receiptService.GetNewCodeAsync();

            return Ok(newReceiptCode);

        }

        /// <summary>
        /// Api phân trang và tìm kiếm danh sách chứng từ
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchInput"></param>
        /// Author: HMDUC (27/07/2023)
        [HttpGet("PaggingList")]
        public async Task<object> GetPagging(int pageSize, int pageNumber, string searchInput)

        {
            var assetPaggingList = await _receiptService.GetPagging(pageSize, pageNumber, searchInput);

            if (assetPaggingList == null)
            {
                return StatusCode(400);
            }
            else
            {
                return Ok(assetPaggingList);
            }
        }

        /// <summary>
        /// Api phân trang danh sách tài sản  tìm theo mã chứng từ 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// Author: HMDUC (27/07/2023)
        [HttpGet("PaggingListAsset")]
        public async Task<object> GetAssetByReceiptIdPagging(Guid id, int pageSize, int pageNumber)
        {
            var assetReceipt = await _receiptService.GetAssetByReceiptIdPagging(id, pageSize, pageNumber);
            if (assetReceipt == null)
            {
                return StatusCode(400);
            }
            else
            {
                return Ok(assetReceipt);
            }
        }

        /// <summary>
        /// Api thêm mới chứng từ 
        /// </summary>
        /// <param name="receiptData">Thông tin chứng từ và ds tài sản</param>
        /// <returns>
        ///  400: Thất bại
        ///  201: Thêm mới thành công
        /// </returns>
        /// Author: HMDUC (27/07/2023)
        [HttpPost("InsertReceipt")]
        public async Task<IActionResult> InsertReceiptAndAsset([FromBody] ReceiptDataInsert receiptData)
        {
            var rowEffect = await _receiptService.InsertReceiptAndAssetAsync(receiptData.receiptInsertDto, receiptData.receiptAssets, receiptData.members);

            if (rowEffect == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created, rowEffect);
            }

        }

        /// <summary>
        /// Api cập nhật chứng từ
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("UpdateReceipt")]
        public async Task<IActionResult> UpdateReceiptAndAsset([FromBody] ReceiptUpdateData receiptData)
        {
            var rowEffect = await _receiptService.UpdateReceiptAndAssetAsync(receiptData.receiptUpdateDto, receiptData.receiptAssets, receiptData.members, receiptData.listDelete);

            if (rowEffect == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, rowEffect);
            }
        }

        /// <summary>
        /// Hàm xóa  một chứng từ theo Id
        /// </summary>
        /// <param name="receiptId">Id của chứng từ</param>
        /// Author: HMDUC (03/08/2023)
        [HttpDelete("ReceiptId/{receiptId}")]
        public async Task<IActionResult> DeleteOnlyReceipt(Guid receiptId)
        {
            var rowEffect = await _receiptService.DeleteOnlyReceiptAsync(receiptId);

            if (rowEffect > 0)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }

        /// <summary>
        /// Api check  Reference chứng từ phục vụ nghiệp vụ xóa
        /// </summary>
        /// <param name="id">Id của chứng từ</param>
        /// <returns>Obj chứa chứng từ phát sinh khác với id của Chứng từ truyền vào</returns>
        /// Author: HMDUC (03/08/2023)
        [HttpGet("CheckReference")]
        public async Task<object> CheckReference(Guid id)
        {
            var result = await _receiptService.CheckReference(id);

            return result;
        }

        /// <summary>
        ///  Hàm check nghiệp vụ phục xóa tài sản ở FORM EDIT
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="receiptId"></param>
        /// <returns>Obj</returns>
        /// Author: HMDUC (06/08/2023)
        [HttpGet("CheckExistReceipt")]
        public async Task<object> CheckExistReceipt(Guid assetId, Guid receiptId)
        {
            var result = await _receiptService.CheckExistReceiptOfAsset(assetId, receiptId);

            return result;
        }

        [HttpPost("CheckDeleteListReceipt")]
        public async Task<object> CheckDeleteListReceipt(List<Guid> ids)
        {
            var result = await _receiptService.CheckDeleteListReceipt(ids);

            return Ok(result);
        }


        /// <summary>
        /// Class chứa Id chứng từ và list ds của chứng từ
        /// </summary>
        public class ReceiptDeleteData
        {
            public Guid Id { get; set; }
            public List<Asset> listAsset { get; set; }
        }

        public class ReceiptUpdateData
        {
            public ReceiptUpdateDto receiptUpdateDto { get; set; }
            public List<ReceiptAsset> receiptAssets { get; set; }
            public List<MemberReceipt> members { get; set; }
            public List<ReceiptAsset> listDelete { get; set; }
        }

        /// <summary>
        /// Class chứa thông tin chứng từ insert và ds tài sản của chứng từ
        /// </summary>
        public class ReceiptDataInsert
        {
            public ReceiptInsertDto receiptInsertDto { get; set; }
            public List<ReceiptAsset> receiptAssets { get; set; }

            public List<MemberReceipt> members { get; set; }
        }


    }
}
