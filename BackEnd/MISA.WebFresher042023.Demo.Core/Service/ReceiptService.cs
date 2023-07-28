using AutoMapper;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application.Service
{
    public class ReceiptService : BaseService<Receipt,ReceiptDto,ReceiptInsertDto,ReceiptUpdateDto>, IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;

        public ReceiptService(IReceiptRepository receiptRepository, IMapper mapper) : base(receiptRepository, mapper)
        {
            _receiptRepository = receiptRepository;
        }


        /// <summary>
        /// Hàm phân trang danh sách tài sản theo mã chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<object> GetAssetByReceiptIdPagging(Guid id, int pageSize, int pageNumber)
        {
            var response = await _receiptRepository.GetAssetByReceiptIdPagging(id, pageSize, pageNumber);

            JObject jObject = JObject.FromObject(response);
            var data = jObject["data"]?.ToObject<IEnumerable<AssetTranferDto>>();
            var totalRow = jObject["totalRow"]?.Value<int>();

            //Calculate Depreciation and Residual
         

            var result = new
            {
                data = data,
                totalRow = totalRow,
            };

            return result;

        }

        /// <summary>
        ///  Hàm tính giá trị còn lại , giá trị hao mòn năm và tỉ lệ hao mòn
        /// </summary>
        /// <param name="assets">Tài sản cần tính</param>
        /// Author: HMDUC (19/06/2023)
        #region Calculate
        private void CalculateDepreciationAndResidual(IEnumerable<AssetTranferDto> assetTranferDtos)
        {
            foreach (var asset in assetTranferDtos)
            {
                //Giá trị hao mòn năm = 
                    asset.DepreciationYear = Math.Round(decimal.Round((decimal)(1 / (float)asset.LifeTime * (float)asset.Cost), 3));

                    //Hao mòn lũy kế 
                    asset.DepreciationAmount = (decimal)((float)asset.DepreciationYear * (float)(DateTime.Now.Year - asset.TrackedYear));

                    //Giá trị còn lại
                    asset.ResidualPrice = (decimal)((float)asset.Quantity * (float)asset.Cost - (float)asset.DepreciationAmount);
            }
        }

        #endregion
        /// <summary>
        /// Hàm phân trang và tìm kiếm chứng từ điều chuyển
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        public async Task<object> GetPagging(int pageSize, int pageNumber, string? searchInput)
        {
            var response =  await _receiptRepository.GetPagging(pageSize, pageNumber, searchInput);
            //Parse response to JSON
            JObject jObject = JObject.FromObject(response);
            var data = jObject["data"]?.ToObject<IEnumerable<Receipt>>();
            var dataSumaryAll = jObject["dataSumaryAll"]?.ToObject<IEnumerable<Receipt>>();
            var totalRecord = jObject["totalRecord"]?.Value<int>();
            var totalRow = jObject["totalRow"]?.Value<int>();

            decimal totalCost = 0;

            foreach (var receipt in dataSumaryAll)
            {

                totalCost += (decimal)receipt.OrgPrice;
            }

            var result = new
            {
                data = data,
                totalRecord = totalRecord,
                totalRow = totalRow,
                dataSumaryAll=dataSumaryAll,
            };

            return result;

        }

      
    }
}
