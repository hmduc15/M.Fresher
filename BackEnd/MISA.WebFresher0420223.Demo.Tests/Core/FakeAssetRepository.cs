using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher0420223.Demo.Tests.Core
{
    public class FakeAssetRepository : IAssetRepository
    {

        public Guid ActualId { get; set; }
        public Task<int> DeleteEntityAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteEntityMulAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<Asset>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Asset> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Asset> GetByIdAsync(Guid id)
        {
            ActualId = id;

            var asset = new Asset()
            {
                AssetId = id
            };

            return Task.FromResult(asset);
        }

        public Task<string> GetNewCodeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<object> GetPagging(int pageSize, int pageNumber, string searchInput, string m_DepartmentName, string m_CategoryName)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Asset entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Asset entity)
        {
            throw new NotImplementedException();
        }
    }
}
