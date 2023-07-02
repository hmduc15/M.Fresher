using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using OfficeOpenXml;
using System.Net;
using System.Text.RegularExpressions;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    /// <summary>
    /// Implement AssetAService Interface
    /// </summary>
    public class AssetService : BaseService<Asset, AssetDto, AssetInsertDto, AssetUpdateDto>, IAssetService
    {
        private readonly IAssetRepository _assetRepository;   

        public AssetService(IAssetRepository assetRepository, IMapper mapper) : base(assetRepository, mapper)
        {
            _assetRepository = assetRepository;

        }

        /// <summary>
        /// Function Get List Asset Pagging 
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
        public Task<object> GetPagging(int pageSize, int pageNumber, string searchInput, string m_DepartmentName, string m_CategoryName)
        {
            var respone = _assetRepository.GetPagging(pageSize,pageNumber,searchInput,m_DepartmentName,m_CategoryName);

            return respone;
        }


        /// <summary>
        /// Funtion Export Excel
        /// </summary>
        /// <returns>package Stream</returns>
        /// Author: HMDUC (29/06/2023)
       public async Task<Stream> ExportExcel()
        {
            var stream = await _assetRepository.GetListExport();

            return stream;
        }


        /// <summary>
        /// Funtion get Entity Code of new Enity 
        /// </summary>
        /// <returns>AssetCode</returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var code = await _assetRepository.GetNewCodeAsync();


            var prefixCode = Regex.Replace(code, @"\d", "");
            var suffixCode = Regex.Replace(code, @"[^0-9]", "");
            var assetCode = GenerateCode(prefixCode, suffixCode);

            var asset = await _assetRepository.GetByCodeAsync(assetCode);

            while (asset != null)
            {
                assetCode = GenerateCode(prefixCode, suffixCode);
                break;
            }

            return assetCode;
        }


        /// <summary>
        /// Funtion Generate new AssetCode
        /// </summary>
        /// <param name="prefixCode"></param>
        /// <param name="suffixCode"></param>
        /// <returns>AssetCode</returns>
        private string GenerateCode(string prefixCode, string suffixCode)
        {
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

 
    }

}
