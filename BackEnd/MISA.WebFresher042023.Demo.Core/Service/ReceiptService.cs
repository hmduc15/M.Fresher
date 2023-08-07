using AutoMapper;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Enum;
using MISA.WebFresher042023.Demo.Domain.Interface.Manager;
using MISA.WebFresher042023.Demo.Domain.Interface.Repository;
using MISA.WebFresher042023.Demo.Domain.MISAException;
using MISA.WebFresher042023.Demo.Domain.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Application.Service
{
    public class ReceiptService : BaseService<Receipt, ReceiptDto, ReceiptInsertDto, ReceiptUpdateDto, ReceiptTranferDto>, IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IReceiptManager _receiptManager;


        public ReceiptService(IReceiptRepository receiptRepository, IReceiptManager receiptManager, IMapper mapper, IUnitOfWork unitOfWork) : base(receiptRepository, mapper, unitOfWork)
        {
            _receiptRepository = receiptRepository;
            _receiptManager = receiptManager;
        }


        /// <summary>
        /// Hàm số 1 chứng từ
        /// </summary>
        /// <param name="id">Id chứng từ cần xóa</param>
        /// <returns>
        ///  số dòng bị ảnh hưởng
        /// </returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<int> DeleteOnlyReceiptAsync(Guid id)
        {

            var receipt = await _receiptRepository.GetByIdAsync(id);

            if (receipt == null)
            {
                throw new NotFoundException(ResourceVN.Err_NotExit_Receipt, (int)MISACode.Validate);
            }

            var result = await _receiptRepository.DeleteOnlyReceiptAsync(id);

            return result;
        }

        /// <summary>
        ///  Hàm check nghiệp vụ phục xóa tài sản ở FORM EDIT
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="receiptId"></param>
        /// <returns>Obj</returns>
        /// Author: HMDUC (06/08/2023)
        public async Task<object> CheckExistReceiptOfAsset(Guid assetId, Guid receiptId)
        {
            var respone = new Dictionary<string, List<ReceiptAssetCommon>>();

            //ds những chứng từ có tài sản là assetId và khác với chứng từ có receiptId
            var listReceiptOfAsset = await _receiptRepository.GetAllReceiptByAssetId(assetId, receiptId);

            var currentReceipt = await _receiptRepository.GetByIdAsync(receiptId);

            var listReceiptResponse = new List<ReceiptAssetCommon>();
            var objFirst = listReceiptOfAsset.FirstOrDefault();


            foreach (var item in listReceiptOfAsset)
            {
                if (item.CreatedDate > currentReceipt.CreatedDate)
                {
                    listReceiptResponse.Add(item);
                }
            }
            if (listReceiptResponse.Count > 0)
            {
                respone.Add($"{objFirst.AssetCode}", listReceiptResponse);
            }


            return respone;

        }

        /// <summary>
        /// Hàm check phát sinh mã chứng từ
        /// </summary>
        /// <param name="receiptId">Mã chứng từ cần check</param>
        /// <returns>
        ///  false:  k có phát sinh được xóa
        ///  true: có phát sinh, k được xóa
        /// </returns>
        public async Task<Object> CheckReference(Guid receiptId)
        {
            bool isError = false;

            var respone = new Dictionary<string, List<ReceiptAssetCommon>>();

            //Lấy ra danh sách tài sản của chứng từ
            var listAssetReceipt = await _receiptRepository.GetAssetByReceiptId(receiptId);

            // danh sách Id của tài sản
            var listAssetId = listAssetReceipt.Select(a => a.AssetId).ToList();

            //Lấy ra danh sách chứng từ phát sinh của tài sản
            var listReceiptAccured = await _receiptRepository.GetReceiptAccured(listAssetId, receiptId);

            // Lấy ds Id của chứng từ phát sinh của tài sản
            var listReceiptAcceuredId = new List<Guid>();

            // Ds tài sản của chứng từ phát sinh
            var listAssetByReceipAccuredId = new List<Asset>();
            listReceiptAcceuredId.Add(receiptId);

            // Lấy ra chứng từ hiện tại
            var receipt = await _receiptRepository.GetByIdAsync(receiptId);

            var listReceiptAfter = new List<Receipt>();

            if (listReceiptAccured.Count == 0)
            {
                List<ReceiptAssetCommon> empty = new List<ReceiptAssetCommon>();
                respone.Add($"{receiptId}", empty);
            }
            else
            {
                // Check những chứng từ có createdDate lớn hơn chứng từ cần  check từ thêm vào mảng để lấy ra danh sach tài sản
                foreach (var item in listReceiptAccured)
                {
                    if (item.CreatedDate > receipt.CreatedDate)
                    {
                        listReceiptAcceuredId.Add(item.ReceiptId);
                    }
                }

                // Lấy ra tất cả các tài sản có trong chứng từ phát sinh và chứng từ cần xóa
                var result = await _receiptRepository.GetListAssetCommonReceipId(listReceiptAcceuredId);

                var listAssetNoReceiptCurrent = new List<ReceiptAssetCommon>();
                var listReceiptAssetResponse = new List<ReceiptAssetCommon>();

                //Lấy ra các tài sản phat sinh chứng từ có ReceiptID khác với ID chứng từ cần xóa
                foreach (var item in result)
                {
                    if (item.ReceiptId != receipt.ReceiptId)
                    {
                        listAssetNoReceiptCurrent.Add(item);
                    }
                }

                //Lấy ra chứng từ k phai chứng từ hiện tại
                foreach (var item in listAssetNoReceiptCurrent)
                {
                    foreach (var asset in listAssetReceipt)
                    {
                        if (item.AssetId == asset.AssetId)
                        {

                            listReceiptAssetResponse.Add(item);
                        }
                    }
                }


                respone.Add($"{receipt.ReceiptId}", listReceiptAssetResponse);
            }
            return respone;
        }

        public async Task<Object> CheckDeleteListReceipt(List<Guid> ids)
        {
            var respone = new Dictionary<string, List<ReceiptAssetCommon>>();

            // Danh sách tài sản theo danh Id
            var listAssetByListId = await _receiptRepository.GetListAssetByListReceiptId(ids);

            // Danh sách tài sản khác với List Id
            var listAssetByNotListId = await _receiptRepository.GetListAssetByNotListReceiptId(ids);

            List<ReceiptAssetCommon> listCommons = listAssetByNotListId
                 .Where(item1 => listAssetByListId.Any(item2 => item2.AssetId == item1.AssetId))
                 .ToList();
            

            List<ReceiptAssetCommon> receiptAssetCommons = new List<ReceiptAssetCommon>();

            foreach (var item in listCommons)
            {
                foreach (var asset in listAssetByListId)
                {
                    if (item.AssetId == asset.AssetId && item.CreatedDate > asset.CreatedDate)
                    {
                        receiptAssetCommons.Add(item);
                    }
                }
            }

            if (receiptAssetCommons.Count > 0)
            {
                var objFirst = receiptAssetCommons.FirstOrDefault();
                respone.Add($"{objFirst.AssetCode}", receiptAssetCommons);
            }


            return respone;
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
            var response = await _receiptRepository.GetPagging(pageSize, pageNumber, searchInput);
            //Parse response to JSON
            JObject jObject = JObject.FromObject(response);
            var data = jObject["data"]?.ToObject<IEnumerable<Receipt>>();
            var dataSumaryAll = jObject["dataSumaryAll"]?.ToObject<IEnumerable<Receipt>>();
            var totalRecord = jObject["totalRecord"]?.Value<int>();
            var totalRow = jObject["totalRow"]?.Value<int>();

            decimal totalCost = 0;
            decimal toltalResidualPrice = 0;

            foreach (var receipt in dataSumaryAll)
            {
                toltalResidualPrice += (decimal)receipt.ResidualPrice;
                totalCost += (decimal)receipt.OrgPrice;
            }

            var result = new
            {
                data = data,
                totalRecord = totalRecord,
                totalRow = totalRow,
                summaryData = new
                {
                    total_cost = totalCost,
                    total_residual_price = toltalResidualPrice,
                }
            };

            return result;

        }

        /// <summary>
        /// Hàm lấy ra mã chứng từ mới nhất phục vụ thêm mới chứng từ
        /// </summary>
        /// <returns>Mã chứng từ</returns>
        ///  Author: HMDUC (27/07/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var code = await _receiptRepository.GetNewCodeAsync();

            var newReceiptCode = "";
            var maxReceiptCode = code;

            Receipt? receipt = null;

            do
            {
                newReceiptCode = GenerateCode(maxReceiptCode);
                receipt = await _receiptRepository.GetByCodeAsync(newReceiptCode);
                maxReceiptCode = newReceiptCode;
            } while (receipt != null);

            return newReceiptCode;

        }


        /// <summary>
        /// Hàm generatde mã chứng từ mới nhất 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
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
        /// Hàm thêm mơi chứng từ
        /// </summary>
        /// <param name="receipt">Chứng từ</param>
        /// <param name="listAsset">Danh sách tài sản có trong chứng từ</param>
        ///  Author: HMDUC (27/07/2023)
        public async Task<int> InsertReceiptAndAssetAsync(ReceiptInsertDto receiptInsertDto, List<ReceiptAsset> listAsset, List<MemberReceipt> memberReceipts)
        {
            var receipt = _mapper.Map<Receipt>(receiptInsertDto);

            //Check Validate data input
            ValidateData(receipt);

            //Check Validate business
            ValidateBusiness(receipt);
            receipt.CreatedDate = DateTime.Now;
            receipt.ModifiedDate = DateTime.Now;


            var rowEffect = await _receiptRepository.InsertReceiptAndAssetAsync(receipt, listAsset, memberReceipts);

            return rowEffect;
        }

        /// <summary>
        /// Hàm cập nhật lại chứng từ
        /// </summary>
        /// <param name="receipt">chứng từ cần cập nhật</param>
        /// <param name="receiptAssets"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// Author: HMDUC (03/08/2023)
        public async Task<int> UpdateReceiptAndAssetAsync(ReceiptUpdateDto receiptUpdateDto, List<ReceiptAsset> listAsset, List<MemberReceipt> listMembers, List<ReceiptAsset> listDelete)
        {
            var receipt = _mapper.Map<Receipt>(receiptUpdateDto);
            var createdDateOld = receiptUpdateDto.CreatedDate;

            //Check Validate data Input
            ValidateData(receipt);

            //Check Validate business
            ValidateBusiness(receipt, false);

            receipt.ModifiedDate = DateTime.Now;
            receipt.CreatedDate = createdDateOld;

            var rowEffect = await _receiptRepository.UpdateReceiptAndAssetAsync(receipt, listAsset, listMembers, listDelete);

            return rowEffect;
        }

        /// <summary>
        /// Hàm validate business
        /// </summary>
        /// <param name="receipt">Chứng từ cần check</param>
        /// <param name="isInsert">
        /// True: Method Insert
        /// False: Method Update
        /// </param>
        /// Author: HMDUC (27/07/2023)
        protected override void ValidateBusiness(Receipt receipt, bool isInsert = true)
        {
            //Check trùng
            if (isInsert)
            {
                _receiptManager.CheckReceiptExistBydCode(receipt.ReceiptCode);
            }
            else
            {
                _receiptManager.CheckReceiptExistBydCode(receipt.ReceiptCode, receipt.ReceiptId, false);
            }

            //Check ngày chứng từ và ngày điều chuyển chứng từ
            _receiptManager.CheckTime(receipt.ReceiptDate, receipt.TranferDate);
        }

        /// <summary>
        /// Hàm validate data input
        /// </summary>
        /// <param name="receiptInsertDto">Chứng từ</param>
        /// Author: HMDUC (31/07/2023)
        private void ValidateData(Receipt receipt)
        {
            var errorData = new Dictionary<string, string>();

            CheckNull(errorData, receipt.ReceiptCode, "ReceiptCode", ResourceVN.Empty_ReceiptCode);
            CheckNull(errorData, receipt.TranferDate.ToString(), "ReceiptCode", ResourceVN.Empty_TranferDate);
            CheckNull(errorData, receipt.ReceiptDate.ToString(), "ReceiptCode", ResourceVN.Empty_ReceiptDate);
            CheckLength(errorData, receipt.ReceiptCode, "ReceiptCode", ResourceVN.Err_Length_ReceiptCode, 20);

            if (errorData.Count > 0)
            {
                throw new ValidateException(errorData, (int)MISACode.Validate);
            }
        }

        /// <summary>
        ///  Hàm check null
        /// </summary>
        /// <param name="errData">List lỗi </param>
        /// <param name="value">value cần check</param>
        /// <param name="entityField">Tên của trường cần check</param>
        /// <param name="errMessage">Msg lỗi</param>
        /// Author: HMDUC (27/07/2023)
        protected override void CheckNull(Dictionary<string, string> errData, string? value, string entityField, string errMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                errData.Add(entityField, errMessage);
            }
        }

        /// <summary>
        ///  Hàm check length
        /// </summary>
        /// <param name="errData">List lỗi</param>
        /// <param name="value">Value cần check</param>
        /// <param name="entityField">Tên trường cần check</param>
        /// <param name="errorMessage">Msg lỗi</param>
        /// <param name="maxLength">Độ dài tối đa</param>
        /// Author: HMDUC (31/07/2023)
        protected override void CheckLength(Dictionary<string, string> errData, string? value, string entityField, string errorMessage, int maxLength)
        {
            if (value?.Length > maxLength)
            {
                errData.Add(entityField, errorMessage);
            }

        }




    }
}
