using Dapper;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(IUnitOfWork unitOfWork) :base(unitOfWork) { }


        /// <summary>
        /// Hàm phân trang và tìm kiếm chứng từ
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchInput"></param>
        /// Author: HMDUC (27/07/2023)
        public async Task<object> GetPagging(int pageSize, int pageNumber, string? searchInput)
        {
            //Sql command
            var sqlCommand  = "Proc_Receipt_Pagging";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@searchInput", searchInput);
            parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _uow.Connection.QueryMultipleAsync(sqlCommand, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            //Get assetPagging
            var receiptPagging = await result.ReadAsync<Receipt>();

            //Get assetAll
            var receiptAll = await result.ReadAsync<Receipt>();

            int totalRowTable = parameters.Get<int>("totalRecord");
            int totalRow = parameters.Get<int>("totalRow");

            var response = new
            {
                data = receiptPagging.ToList(),
                totalRecord = totalRowTable,
                totalRow = totalRow,
                dataSumaryAll = receiptAll.ToList(),
            };
            return response;

        }

        /// <summary>
        ///  Hàm lấy tất cả các tài sản theo mã chứng từ
        /// </summary>
        /// <param name="id">ID của chứng từ</param>
        /// Author: HMDUC (27/07/2023)
        public async Task<object> GetAssetByReceiptIdPagging(Guid receiptId, int pageSize, int pageNumber)
        {
            //Sql command
            var sqlCommand = "Proc_ReceiptAsset_GetAsset";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@receiptId", receiptId);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var assetTranfers = await _uow.Connection.QueryAsync<AssetTranferDto>(sql: sqlCommand, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            //Get total Row
            int totalRow = parameters.Get<int>("totalRow");


            var response = new
            {
                data = assetTranfers.ToList(),
                totalRow = totalRow,
            };

            return response;
        }

    }
}
