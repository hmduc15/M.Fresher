using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Resources;
using System.Net;

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
        /// Funtion get Entity Code of new Enity 
        /// </summary>
        /// <returns>AssetCode</returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var code = await _assetRepository.GetNewCodeAsync();

            return code;
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
    }
}
