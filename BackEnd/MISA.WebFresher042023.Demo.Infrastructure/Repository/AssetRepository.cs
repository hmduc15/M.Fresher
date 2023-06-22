using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entity;
using MySqlConnector;
using Dapper;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Resources;
using MISA.WebFresher042023.Demo.Core.Dto;
using System.Data;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    /// <summary>
    /// Implement Interfacse Asset Repository
    /// </summary>
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(IConfiguration configuration) : base(configuration)
        {

        }


        /// <summary>
        /// Funtion get EntityCode of new Entity 
        /// </summary>
        /// <returns>AssetCode</returns>
        /// Author: HMDUC (19/06/2023)

        public async Task<string> GetNewCodeAsync()
        {
            //Get tableName Entity
            var tableName = typeof(Asset).Name;

            //connect mysql
            var sqlConnection = new MySqlConnection(_connectionString);
            var sqlCommand = $"CALL Proc_{tableName}_GetNew";

            //open connection
            await sqlConnection.OpenAsync();

            var newCode = await sqlConnection.QueryAsync<string>(sql: sqlCommand);

            //close connection
            await sqlConnection.CloseAsync();


            return string.Join(",", newCode);

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
        public async Task<object> GetPagging(int pageSize, int pageNumber, string? searchInput, string? m_DepartmentName, string? m_CategoryName)
        {
            var sqlConnection = new MySqlConnection(_connectionString);
            var sqlCommand = "Proc_Asset_Pagging";

            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@searchInput", searchInput);
            parameters.Add("@m_DepartmentName", m_DepartmentName);
            parameters.Add("@m_CategoryName", m_CategoryName);
            parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@totalRow", dbType:DbType.Int32, direction:ParameterDirection.Output);

            var Assets = await sqlConnection.QueryAsync<Asset>(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);
            int totalRowTable = parameters.Get<int>("totalRecord");
            int totalRow = parameters.Get<int>("totalRow");


            var response = new
            {
                data = Assets,
                totalRecord= totalRowTable,
                totalRow = totalRow,
            };

            return response;
        }

    }
}
