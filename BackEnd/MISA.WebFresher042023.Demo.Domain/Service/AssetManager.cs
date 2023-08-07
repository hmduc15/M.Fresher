using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Enum;
using MISA.WebFresher042023.Demo.Domain.Interface.Manager;
using MISA.WebFresher042023.Demo.Domain.Interface.Repository;
using MISA.WebFresher042023.Demo.Domain.MISAException;
using MISA.WebFresher042023.Demo.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Domain.Service
{
    public class AssetManager : IAssetManager
    {
        #region Field
        private readonly IAssetRepository _assetRepository;
        #endregion

        #region Constructor
        public AssetManager(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm check trùng mã tài sản
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <param name="id">Id tài sản</param>
        /// <param name="isInsert">Method là Insert hay Update</param>
        /// <exception cref="ValidateException"></exception>
        /// Author: HMDUC (20/07/2023)
        public void CheckAssetExistByCode(string? code, Guid? id = null, bool isInsert = true)
        {
            bool isExistAssetCode;
            var errorData = new Dictionary<string, string>();

            if (isInsert)
            {
                isExistAssetCode = _assetRepository.CheckExistAssetCode(code);

            }
            else
            {
                isExistAssetCode = _assetRepository.CheckExistAssetCode(code, id);
            }

            if (isExistAssetCode)
            {
                errorData.Add("AssetCode", ResourceVN.Error_Dupli_Code);
                throw new ValidateException(errorData, (int)MISACode.Validate);
            }
        }

        /// <summary>
        /// Hàm check tỷ lệ hao mòn và hao mòn năm
        /// </summary>
        /// <param name="depreciationRate">Tỷ lệ hao mòn</param>
        /// <param name="depreciationYear">Hao mòn năm</param>
        /// <param name="cost">Nguyên giá</param>
        /// <param name="lifeTime">Số năm sử dụng</param>
        /// <exception cref="ValidateException"></exception>
        /// Author: HMDUC (20/07/2023)
        public void CheckDepreciation(float depreciationRate, decimal depreciationYear, decimal cost, int lifeTime)
        {
            var errorData = new Dictionary<string, string>();
            if (depreciationYear > cost)
            {
                errorData.Add("DepreciationYear", ResourceVN.Error_DepreciationYear);
            }

            if (depreciationRate != (float)Math.Round(1f / lifeTime, 4))
            {
                errorData.Add("DepreciationRate", ResourceVN.Error_DepreciationRate);
            }

            if (errorData.Count > 0)
            {
                throw new ValidateException(errorData, (int)MISACode.Validate);
            }
        }


        /// <summary>
        /// Hàm check Ngày mua và Ngày bắt đầu sử dụng của tài sản
        /// </summary>
        /// <param name="purchaseDate">Ngày mua</param>
        /// <param name="productionYear">Ngày bắt đầu sử dụng</param>
        /// <exception cref="ValidateException"></exception>
        /// Author: HMDUC (20/07/2023)
        public void CheckTime(DateTime? purchaseDate, DateTime? productionYear)
        {
            var errorData = new Dictionary<string, string>();

            if (purchaseDate > DateTime.Now)
            {
                errorData.Add("PurchaseDate", ResourceVN.Error_PurchaseDate);
            }

            if (productionYear > DateTime.Now)
            {
                errorData.Add("ProductionYear", ResourceVN.Error_ProductionYear);
            }

            if (purchaseDate > productionYear)
            {
                errorData.Add("ProductionAndPurchaseDate", ResourceVN.Error_PurchaseDateAndProductionYear);
            }

            if (errorData.Count > 0)
            {
                throw new ValidateException(errorData, (int)MISACode.Validate);
            }
        }
        #endregion
    }
}
