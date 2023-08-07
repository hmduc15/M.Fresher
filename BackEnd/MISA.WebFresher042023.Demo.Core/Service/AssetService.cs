using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Enum;
using MISA.WebFresher042023.Demo.Domain.Interface.Manager;
using MISA.WebFresher042023.Demo.Domain.Interface.Repository;
using MISA.WebFresher042023.Demo.Domain.MISAException;
using MISA.WebFresher042023.Demo.Domain.Resources;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace MISA.WebFresher042023.Demo.Application.Service
{
    public class AssetService : BaseService<Asset, AssetDto, AssetInsertDto, AssetUpdateDto, AssetTranferDto>, IAssetService
    {
        #region Field
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetManager _assetManager;
        #endregion

        #region Constructor
        public AssetService(IAssetRepository assetRepository, IMapper mapper, IAssetManager assetManager, IUnitOfWork unitOfWork) : base(assetRepository, mapper, unitOfWork)
        {
            _assetRepository = assetRepository;
            _assetManager = assetManager;
        }
        #endregion


        /// <summary>
        /// Hàm check ds tài sản có chứng từ hay không
        /// </summary>
        /// <param name="ids">danh sách tài sản</param>
        /// <returns>Danh sách chứng từ của tài sản nếu có</returns>
        /// Author: HMDUC (05/08/2023)
        public async Task<object> CheckExistReceipt(List<Guid> ids)
        {
            var result = await _assetRepository.GetReceiptByAssetId(ids);

            //Obj  respone trả về cho client
            var response = new Dictionary<Guid, List<ReceiptAssetCommon>>();

            foreach(var item in result)
            {
                if (!response.ContainsKey(item.AssetId))
                {
                    response[item.AssetId] = new List<ReceiptAssetCommon>();
                }
                response[item.AssetId].Add(item);
            }


            return response;

        }


        /// <summary>
        /// Hàm check null
        /// </summary>
        /// <param name="errData">List lỗi</param>  
        /// <param name="value">Giá trị cần check</param>
        /// <param name="entityField">Tên field cần check</param>
        /// <param name="errMessage">Message lỗi</param>
        /// Author: HMDUC (03/07/2023)
        #region CheckNull
        protected override void CheckNull(Dictionary<string, string> errData, string? value, string entityField, string errMessage)
        {

            if (string.IsNullOrEmpty(value))
            {
                errData.Add(entityField, errMessage);
            }
        }
        #endregion

        /// <summary>
        /// Hàm check length
        /// </summary>
        /// <param name="errData">List lỗi</param>
        /// <param name="value">Giá trị cần check</param>
        /// <param name="entityField">Tên field cần check</param>
        /// <param name="errorMessage">Message lỗi</param>
        /// <param name="maxLength">Độ dài thỏa mãn</param>
        /// Author: HMDUC (03/07/2023)
        #region CheckLength
        protected override void CheckLength(Dictionary<string, string> errData, string? value, string entityField, string errorMessage, int maxLength)
        {
            if (value?.Length > maxLength)
            {
                errData.Add(entityField, errorMessage);
            }
        }
        #endregion

        /// <summary>
        /// Hàm validate nghiệp vụ
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="isInsert"></param>
        protected override void ValidateBusiness(Asset asset, bool isInsert = true)
        {

            // Check trùng mã tài sản
            if (isInsert)
            {
                _assetManager.CheckAssetExistByCode(asset.AssetCode);
            }
            else
            {
                _assetManager.CheckAssetExistByCode(asset.AssetCode, asset.AssetId, false);
            }

            // Check ngày mua và ngày bắt đầu sử dụng
            _assetManager.CheckTime(asset.PurchaseDate, asset.ProductionYear);

            //Check tỷ lệ hao mòn và hao mòn năm
            _assetManager.CheckDepreciation(asset.DepreciationRate, asset.DepreciationYear, asset.Cost, asset.LifeTime);

        }

        /// <summary>
        /// Hàm phân trang và tìm kiếm 
        /// </summary>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageNumber">pageNumer</param>
        /// <param name="searchInput">search</param>
        /// <param name="m_DepartmentName">DepartmentName</param>
        /// <param name="m_CategoryName">CategoryName</param>
        /// <returns>
        ///  Object {ListAsset, totalPage}
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region GePagging
        public async Task<object> GetPagging(int pageSize, int pageNumber, string searchInput, string m_DepartmentName, string m_CategoryName)
        {

            var response = await _assetRepository.GetPagging(pageSize, pageNumber, searchInput, m_DepartmentName, m_CategoryName);

            //Parse response to JSON
            JObject jObject = JObject.FromObject(response);
            var data = jObject["data"]?.ToObject<IEnumerable<Asset>>();
            var dataSumaryAll = jObject["dataSumaryAll"]?.ToObject<IEnumerable<Asset>>();
            var totalRecord = jObject["totalRecord"]?.Value<int>();
            var totalRow = jObject["totalRow"]?.Value<int>();

            decimal totalCost = 0;
            float totalDepreciation = 0;
            decimal toltalResidualPrice = 0;

            //Calculate Depreciation and Residual
            CalculateDepreciationAndResidual(data);

            CalculateDepreciationAndResidual(dataSumaryAll);

            //Calculate total Cost, Depreciation,Residual
            foreach (var asset in dataSumaryAll)
            {
                totalCost += (decimal)asset.Cost;
                totalDepreciation += (float)asset.DepreciationAmount;
                toltalResidualPrice += (decimal)asset.ResidualPrice;
            }

            var result = new
            {
                data = data,
                totalRecord = totalRecord,
                totalRow = totalRow,
                summaryData = new
                {
                    total_quantity = dataSumaryAll.Sum(asset => asset.Quantity),
                    total_cost = totalCost,
                    total_depreciation = totalDepreciation,
                    total_residual_price = toltalResidualPrice,

                }
            };

            return result;
        }
        #endregion

        /// <summary>
        /// Hàm phân trang + tìm kiếm tài sản phục vụ thêm tài sản vào chứng từ
        /// </summary>
        /// <param name="ids">Danh sach Id tài sản cần loại bỏ</param>
        /// <param name="pageSize">Số dòng hiển thị</param>
        /// <param name="pageNumber">Số trang</param>
        /// Author: HMDUC (28/07/2023)
        public async Task<object> GetPaggingAssetChose(List<Guid> ids, int pageSize, int pageNumber)
        {
            var respone = await _assetRepository.GetPaggingAssetChose(ids, pageSize, pageNumber);

            //Parse response to JSON
            JObject jObject = JObject.FromObject(respone);
            var data = jObject["data"]?.ToObject<IEnumerable<Asset>>();
            var dataSumaryAll = jObject["dataSumaryAll"]?.ToObject<IEnumerable<Asset>>();
            var totalRow = jObject["totalRow"]?.Value<int>();

            decimal totalCost = 0;
            float totalDepreciation = 0;
            decimal toltalResidualPrice = 0;

            //Calculate Depreciation and Residual
            CalculateDepreciationAndResidual(data);

            CalculateDepreciationAndResidual(dataSumaryAll);

            //Calculate total Cost, Depreciation,Residual
            foreach (var asset in dataSumaryAll)
            {
                totalCost += (decimal)asset.Cost;
                totalDepreciation += (float)asset.DepreciationAmount;
                toltalResidualPrice += (decimal)asset.ResidualPrice;
            }

            var result = new
            {
                data = data,
                totalRow = totalRow,
                summaryData = new
                {
                    total_quantity = dataSumaryAll.Sum(asset => asset.Quantity),
                    total_cost = totalCost,
                    total_depreciation = totalDepreciation,
                    total_residual_price = toltalResidualPrice,

                }
            };

            return result;
        }

        /// <summary>
        ///  Hàm tính giá trị còn lại , giá trị hao mòn năm và tỉ lệ hao mòn
        /// </summary>
        /// <param name="assets">Tài sản cần tính</param>
        /// Author: HMDUC (19/06/2023)
        #region Calculate
        private void CalculateDepreciationAndResidual(IEnumerable<Asset> assets)
        {
            foreach (var asset in assets)
            {
                //Giá trị hao mòn năm = 
                asset.DepreciationYear = Math.Round(decimal.Round((decimal)((1 / (float)asset.LifeTime) * (float)asset.Cost), 3));

                //Hao mòn lũy kế 
                asset.DepreciationAmount = (decimal)((float)asset.DepreciationYear * (float)(DateTime.Now.Year - asset.TrackedYear));
            }
        }
        #endregion


        /// <summary>
        /// Hàm export Excel
        /// </summary>
        /// <returns>Đôi tượng file stream</returns>
        /// Author: HMDUC (29/06/2023)
        #region ExportExcel
        public async Task<Stream> ExportExcel()
        {
            var stream = await _assetRepository.GetListExport();

            return stream;
        }
        #endregion


        /// <summary>
        /// Hàm lấy ra mã tài sản mới nhất
        /// </summary>
        /// <returns>AssetCode</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetNewCodeAsync
        public async Task<string> GetNewCodeAsync()
        {
            var code = await _assetRepository.GetNewCodeAsync();

            var newAssetCode = "";
            var maxAssetCode = code;

            Asset? asset = null;

            //Auto Generate AssetCode Max 
            do
            {
                newAssetCode = GenerateCode(maxAssetCode);
                asset = await _assetRepository.GetByCodeAsync(newAssetCode);
                maxAssetCode = newAssetCode;
            } while (asset != null);

            return maxAssetCode;
        }
        #endregion


        /// <summary>
        /// hàm generate mã tài sản mới nhất
        /// </summary>
        /// <returns>AssetCode</returns>
        #region GenerateCode
        private string GenerateCode(string code)
        {

            var prefixCode = Regex.Replace(code, @"\d", "");
            var suffixCode = Regex.Replace(code, @"[^0-9]", "");

            if (!string.IsNullOrEmpty(suffixCode))
            {
                var numberSuffix = (int.Parse(suffixCode) + 1).ToString();
                var preZero = "";

                for (int i = 0; i < suffixCode.Length - numberSuffix.Length; i++)
                {
                    preZero = preZero + "0";
                }
                return prefixCode + preZero + numberSuffix;

            }
            else
            {
                int suffixTemp = 1;
                return prefixCode + suffixTemp.ToString();
            }
        }

        /// <summary>
        /// Hàm lấy tất cả các tài sản theo mã chứng từ
        /// </summary>
        /// <param name="receiptId">Mã chứng từ</param>
        /// <returns>
        /// Danh sách tài sản
        /// </returns>
        /// Author: HMDUC (28/07/2023)
        public async Task<List<AssetTranferDto>> GetAssetAllByReceipId(Guid receiptId)
        {
            var response = await _assetRepository.GetAssetAllByReceipId(receiptId);

            var result = _mapper.Map<List<AssetTranferDto>>(response);

            return result;
        }





        #endregion




    }

}
