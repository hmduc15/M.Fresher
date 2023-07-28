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
    public class ReceiptController : BaseController<ReceiptDto, ReceiptInsertDto, ReceiptUpdateDto>
    {
        private IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService) : base(receiptService)
        {
            _receiptService = receiptService;
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
            if(assetReceipt == null)
            {
                return StatusCode(400);
            }
            else
            {
                return Ok(assetReceipt);
            }
        }

    }
}
