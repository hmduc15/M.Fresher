using Dapper;
using Microsoft.VisualBasic;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MISA.WebFresher042023.Demo.Domain.Interface.Repository;
using Newtonsoft.Json;
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
        #region Contructor
        public ReceiptRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Method
        /// <summary>
        /// Hàm check trùng mã chứng từ 
        /// </summary>
        /// <param name="receiptCode">Mã chứng từ cần check</param>
        /// <param name="receiptId">Id của chứng từ cần check</param>
        /// <returns>
        /// true: Trùng 
        /// false: Không trùng
        /// </returns>
        /// Author: HMDUC (27/07/2023)
        public bool CheckIsDuplicate(string? receiptCode, Guid? receiptId = null)
        {
            var sqlCommand = "Proc_Receipt_CheckDuplicate";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@M_ReceiptId", receiptId);
            parameters.Add("@M_ReceiptCode", receiptCode);

            var result = _uow.Connection.QueryFirstOrDefault<bool>(sqlCommand, parameters,commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result;
        }

        /// <summary>
        /// Hàm check phát sinh chứng từ phục vụ xóa
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns></returns>
        public bool CheckIsAccrued(Guid? receiptId)
        {
            var sqlCommand = "Proc_ReceiptAsset_CheckExist";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@M_receiptId", receiptId);

            var result = _uow.Connection.QueryFirstOrDefault<bool>(sqlCommand,parameters,commandType: CommandType.StoredProcedure,transaction: _uow.Transaction);
         
            return result;
        }

        /// <summary>
        /// Hàm lấy ra mã chứng từ mới nhất phục vụ thêm mới chứng từ
        /// </summary>
        /// <returns>Mã chứng từ</returns>
        ///  Author: HMDUC (27/07/2023)
        public async Task<string> GetNewCodeAsync()
        {

            //Get tableName Entity
            var tableName = typeof(Receipt).Name;

            //Sql command line
            var sqlCommand = $"CALL Proc_{tableName}_GetNew";

            var newCode = await _uow.Connection.QueryAsync<string>(sql: sqlCommand, transaction: _uow.Transaction);

            return string.Join(",", newCode);
        }

      
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
            var sqlCommand = "Proc_Receipt_Pagging";

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
        /// Hàm lấy ra tài sản có ReceiptId nằm trong tất cả các id của list Ids
        /// </summary>
        /// <param name="ids">Danh sách Id</param>
        /// <returns>
        /// List danh sách tài sản chung
        /// </returns>
        ///  Author: HMDUC (04/08/2023)
        public async Task<List<ReceiptAssetCommon>> GetListAssetCommonReceipId(List<Guid> ids)
        {
            var sqlCommand = "Proc_Asset_GetComonReceiptId";
            DynamicParameters parameters = new DynamicParameters();
            var paramIds = string.Join(",", ids);
            parameters.Add("@receiptIdsList", paramIds);

            var result = await _uow.Connection.QueryAsync<ReceiptAssetCommon>(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result.ToList();
        }

        /// <summary>
        /// Hàm lấy tất cả tài sản có trong chừng từ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Asset>> GetAssetByReceiptId(Guid id)
        {
            var sqlCommand = "SELECT ra.AssetId,a.AssetName FROM receipt_asset ra LEFT JOIN asset a ON " +
                 "ra.AssetId = a.AssetId" +
                " WHERE ra.ReceiptId = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var result = await _uow.Connection.QueryAsync<Asset>(sqlCommand,parameters,transaction: _uow.Transaction);

            return result.ToList();
        }

        /// <summary>
        ///  Hàm phân trang tài sản theo mã chứng từ
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

            var listAssetTranfer = await _uow.Connection.QueryAsync<AssetTranferDto>(sql: sqlCommand, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            //Get total Row
            int totalRow = parameters.Get<int>("totalRow");

            var response = new
            {
                data = listAssetTranfer.ToList(),
                totalRow = totalRow,
            };

            return response;
        }

        /// <summary>
        /// Hàm lấy chứng từ theo AssetId và khác ID chứng từ truyền vào phục vụ nghiệp vụ xóa tài sản ở Form Edit
        /// </summary>
        /// <param name="assetId">Id Tài sản</param>
        /// <param name="receiptId">Id chứng từ</param>
        /// <returns>Danh sách chứng từ</returns>
        /// Author: HMDUC (06/08/2023)
        public async Task<List<ReceiptAssetCommon>> GetAllReceiptByAssetId(Guid assetId, Guid receiptId)
        {
            var sqlCommand = "Proc_Receipt_GetByAsset";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@assetId", assetId);
            parameters.Add("@receiptId", receiptId);

            var result = await _uow.Connection.QueryAsync<ReceiptAssetCommon>(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return result.ToList();

        }


        /// <summary>
        /// Hàm thêm mơi chứng từ
        /// </summary>
        /// <param name="receipt">Chứng từ</param>
        /// <param name="listAsset">Danh sách tài sản có trong chứng từ</param>
        ///  Author: HMDUC (27/07/2023)
        public async Task<int> InsertReceiptAndAssetAsync(Receipt receipt, List<ReceiptAsset> listAsset, List<MemberReceipt> listMembers)
        {
            int rowEffected = 0;
            try
            {
                await _uow.BeginTrasactionAsync();
                DynamicParameters parameters = new DynamicParameters();

                var sql = "";
                parameters.Add("@receiptId", Guid.NewGuid());
                parameters.Add("@receiptAssetId", Guid.NewGuid());
            
                //Lấy ra các trường có trong Receipt
                var colReceipt = receipt.GetType().GetProperties()
                                     .Where(prop => prop.Name != "ReceiptId"
                                     && prop.Name != "OrgPrice" && prop.Name != "ResidualPrice");
                 
                // tạo sql thêm chứng từ
                sql += "INSERT INTO receipt (ReceiptId, ";
                sql += string.Join(", ", colReceipt.Select(prop => prop.Name));
                sql += $") VALUES (@receiptId, ";
                sql += string.Join(", ", colReceipt.Select(prop => $"@{prop.Name}"));
                sql += "); ";

                foreach (var col in colReceipt)
                {
                    parameters.Add($"{col.Name}", col.GetValue(receipt));
                   
                }
    
                var index = 0;

                if (listMembers.Count > 0)
                {
                    foreach (var member in listMembers)
                    {
                        var colMember = member.GetType().GetProperties()
                                   .Where(prop => prop.GetValue(member) != null);

                        sql += $"INSERT INTO member (";
                        sql += string.Join(", ", colMember.Select(prop => prop.Name));
                        sql += $",ReceiptId) VALUES (";
                        sql += string.Join(", ", colMember.Select(prop => $"@{prop.Name}_{index}"));
                        sql += $", @receiptId); ";

                        foreach (var prop in colMember)
                        {
                            parameters.Add($"{prop.Name}_{index}", prop.GetValue(member));
                        }
                        parameters.Add($"MemberId_{index}", Guid.NewGuid());

                        index++;
                    }
                }


                //tạo sql thêm danh sách tài sản
                foreach (var asset in listAsset)
                {
                    //Lấy ra các trường của asset
                    var colAsset = asset.GetType().GetProperties()
                        .Where(prop => prop.GetValue(asset) != null && prop.Name != "ReceiptId" && prop.Name != "DepartmentId");
                  
                    sql += $"INSERT INTO receipt_asset (ReceiptAssetId,ReceiptId, ";
                    sql += string.Join(", ", colAsset.Select(prop => prop.Name));
                    sql += $") VALUES (@ReceiptAssetId_{index},@receiptId, ";
                    sql += string.Join(", ", colAsset.Select(prop => $"@{prop.Name}_{index}"));
                    sql += ");";

                    foreach (var prop in colAsset)
                    {
                        parameters.Add($"{prop.Name}_{index}", prop.GetValue(asset));
                    }

                    parameters.Add($"@ReceiptAssetId_{index}", Guid.NewGuid());

                    //tạo sql update lại phòng ban
                    sql += $"UPDATE asset a SET a.DepartmentId = ";
                    sql += $"(SELECT d.DepartmentId FROM department d where d.DepartmentId = @DepartmentReceiptId_{index}),";
                    sql += $"a.ModifiedDate = '{DateTime.Now.ToString("yyyy-MM-dd")}' ";
                    sql += $"WHERE a.AssetId = @AssetId_{index} ;";

                    index++;
                }
                rowEffected = await _uow.Connection.ExecuteAsync(sql, parameters,transaction: _uow.Transaction);

              await  _uow.CommitAsync();

            }catch(Exception ex)
            {
                await _uow.RollBackAsync();
            }

            return rowEffected;

        }


        /// <summary>
        /// Hàm cập nhật lại chứng từ
        /// </summary>
        /// <param name="receipt">chứng từ cần cập nhật</param>
        /// <param name="receiptAssets"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// Author: HMDUC (03/08/2023)
       public async Task<int> UpdateReceiptAndAssetAsync(Receipt receipt, List<ReceiptAsset> receiptAssets, List<MemberReceipt> listMembers, List<ReceiptAsset> listDelete)
        {
            int rowEffected = 0;

            try
            {
                await _uow.BeginTrasactionAsync();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@receiptId", receipt.ReceiptId);
                parameters.Add("@newReceipId", Guid.NewGuid());
                parameters.Add("@receiptAssetId", Guid.NewGuid());


                var sql = "";

                //Lấy ra các trường có trong Receipt
                var colReceipt = receipt.GetType().GetProperties()
                                     .Where(prop => prop.Name != "ReceiptId"
                                     && prop.Name != "OrgPrice" && prop.Name != "ResidualPrice");

                // tạo sql thêm chứng từ
                sql += "INSERT INTO receipt (ReceiptId, ";
                sql += string.Join(", ", colReceipt.Select(prop => prop.Name));
                sql += $") VALUES (@newReceipId, ";
                sql += string.Join(", ", colReceipt.Select(prop => $"@{prop.Name}"));
                sql += "); ";

                foreach (var col in colReceipt)
                {
                    parameters.Add($"{col.Name}", col.GetValue(receipt));
                }

                var index = 0;

                //tạo sql thêm danh sách ban giao nhận 
                if (listMembers.Count > 0)
                {
                    foreach (var member in listMembers)
                    {
                        //Lấy ra các trường của ban giao nhận 
                        var colMember = member.GetType().GetProperties()
                                   .Where(prop => prop.GetValue(member) != null  );

                        sql += $"INSERT INTO member (";
                        sql += string.Join(", ", colMember.Select(prop => prop.Name));
                        sql += $",ReceiptId) VALUES (";
                        sql += string.Join(", ", colMember.Select(prop => $"@{prop.Name}_{index}"));
                        sql += $", @newReceipId); ";

                        foreach (var prop in colMember)
                        {
                            parameters.Add($"{prop.Name}_{index}", prop.GetValue(member));
                        }
                        parameters.Add($"MemberId_{index}", Guid.NewGuid());

                        index++;
                    }
                }

                //tạo sql update lại phòng ban cho tài sản bị xóa khỏi chứng từ
                if(listDelete.Count > 0)
                {
                    foreach(var asset in listDelete)
                    {
                        sql += $"UPDATE asset a SET a.DepartmentId = ";
                        sql += $"(SELECT ra.OldDepartmentId FROM receipt_asset ra WHERE ra.AssetId = @assetId_{index} and ra.ReceiptId = @receiptId)";
                        sql += $"WHERE a.AssetId = @assetId_{index} ;";

                        parameters.Add($"@assetId_{index}", asset.AssetId);

                        index++;
                    }
                }

       
                //tạo sql thêm danh sách tài sản
                foreach (var asset in receiptAssets)
                {
                    //Lấy ra các trường của asset
                    var colAsset = asset.GetType().GetProperties()
                        .Where(prop => prop.GetValue(asset) != null && prop.Name != "ReceiptId" && prop.Name != "DepartmentId");


                    sql += $"INSERT INTO receipt_asset (ReceiptAssetId,ReceiptId, ";
                    sql += string.Join(", ", colAsset.Select(prop => prop.Name));
                    sql += $") VALUES (@ReceiptAssetId_{index},@newReceipId, ";
                    sql += string.Join(", ", colAsset.Select(prop => $"@{prop.Name}_{index}"));
                    sql += ");";

                    foreach (var prop in colAsset)
                    {
                        parameters.Add($"{prop.Name}_{index}", prop.GetValue(asset));
                    }

                    parameters.Add($"@ReceiptAssetId_{index}", Guid.NewGuid());

                    sql += $"UPDATE asset a SET a.DepartmentId = ";
                    sql += $"(SELECT d.DepartmentId FROM department d where d.DepartmentId = @DepartmentReceiptId_{index}),";
                    sql += $"a.ModifiedDate = '{DateTime.Now.ToString("yyyy-MM-dd")}' ";
                    sql += $"WHERE a.AssetId = @AssetId_{index} ;";

                    index++;
                }

                sql += $"DELETE FROM receipt r WHERE r.ReceiptId = @receiptId;";

                rowEffected = await _uow.Connection.ExecuteAsync(sql, parameters, transaction: _uow.Transaction);

                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollBackAsync();
            }

            return rowEffected;
        }

        /// <summary>
        ///  Hàm lấy ra chứng từ phát sinh theo Id
        /// </summary>
        /// <param name="assetId">Danh sách id tài sản của chứng từ</param>
        /// <param name="receiptId">ID chứng từ</param>
        /// <returns>Danh sách chứng từ phát sinh</returns>
        /// Author: HMDUC (27/07/2023)
        public async Task<List<Receipt>> GetReceiptAccured(List<Guid> ids,Guid receiptId)
        {
            var sqlCommand = "Proc_Receipt_GetAccuredAll";

            DynamicParameters parameters = new DynamicParameters();
            var paramString = JsonConvert.SerializeObject(ids);
            parameters.Add("@ids", paramString);
            parameters.Add("@receiptId", receiptId);

            var result = await _uow.Connection.QueryAsync<Receipt>(sqlCommand, parameters,commandType: System.Data.CommandType.StoredProcedure,transaction: _uow.Transaction);

            return result.ToList();
        }


        /// <summary>
        /// Hàm xóa chứng từ
        /// </summary>
        /// <param name="listAsset"></param>
        /// <param name="id"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        ///  Author: HMDUC (27/07/2023)
        public async Task<int> DeleteOnlyReceiptAsync(Guid id) 
        {
            int rowEffected = 0;

            try
            {
              await  _uow.BeginTrasactionAsync();

                var listAssetReceipt = await this.GetAssetByReceiptId(id);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var sql = "";


                var index  = 0;
                foreach (var asset in listAssetReceipt)
                {
                    sql += $"UPDATE asset a SET a.DepartmentId = ";
                    sql += $"(SELECT ra.OldDepartmentId FROM receipt_asset ra WHERE ra.AssetId = @assetId_{index} and ra.ReceiptId = @id)";
                    sql += $"WHERE a.AssetId = @assetId_{index} ;";
        
                    parameters.Add($"@assetId_{index}", asset.AssetId);

                    index++;
                }

         
                sql += $"DELETE FROM receipt WHERE receiptId = @id ;";

                rowEffected = await _uow.Connection.ExecuteAsync(sql, parameters, transaction: _uow.Transaction);

                await _uow.CommitAsync();

            }catch(Exception ex)
            {
                await _uow.RollBackAsync();
            }

            return rowEffected;
        }

        /// <summary>
        /// Hàm lẩy ra danh sách tài sản theo danh sách Id của chứng từ
        /// </summary>
        /// <param name="ids">Danh sách Id của chứng từ </param>
        /// <returns>Danh sách tài sản</returns>
        /// Author: HMDUC (07/08/2023)
        public async Task<List<ReceiptAssetCommon>> GetListAssetByListReceiptId(List<Guid> ids)
        {
            var sqlCommand = "Proc_Receipt_GetAssetExistListId";
            string paramString = String.Join(",", ids);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ids", paramString);

            var result = await _uow.Connection.QueryAsync<ReceiptAssetCommon>(sqlCommand, parameters,commandType: System.Data.CommandType.StoredProcedure);

            return result.ToList();

        }

        /// <summary>
        /// Hàm lẩy ra danh sách tài sản khác với  danh sách Id của chứng từ
        /// </summary>
        /// <param name="ids">Danh sách Id của chứng từ </param>
        /// <returns>Danh sách tài sản</returns>
        /// Author: HMDUC (07/08/2023)
        public async Task<List<ReceiptAssetCommon>> GetListAssetByNotListReceiptId(List<Guid> ids)
        {
            var sqlCommand = "Proc_Receipt_GetAssetNotExistListId";
            string paramString = String.Join(",", ids);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ids", paramString);

            var result = await _uow.Connection.QueryAsync<ReceiptAssetCommon>(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return result.ToList();

        }




        #endregion
    }
}
