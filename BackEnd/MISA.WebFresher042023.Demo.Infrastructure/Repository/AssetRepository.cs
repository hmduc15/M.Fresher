using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Domain.Entity;
using MySqlConnector;
using Dapper;
using MISA.WebFresher042023.Demo.Domain.Resources;
using System.Data;
using static Dapper.SqlMapper;
using OfficeOpenXml;
using static MISA.WebFresher042023.Demo.Domain.MISAAttribute.CustomAttribute;
using System;
using OfficeOpenXml.Style;
using System.Reflection;
using System.Drawing;
using MISA.WebFresher042023.Demo.Domain;
using Newtonsoft.Json;
using MISA.WebFresher042023.Demo.Application;
using MISA.WebFresher042023.Demo.Domain.Interface.Repository;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    /// <summary>
    /// Implement Interfacse Asset Repository
    /// </summary>
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {

        #region Constructor
        public AssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        #endregion


        /// <summary>
        /// Hàm check trùng mã tài sản
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần check</param>
        /// <param name="assetId">ID tài sản</param>
        /// <returns>
        /// True: trùng
        /// False: không trùng
        /// </returns>
        #region CheckExistCode
        public bool CheckExistAssetCode(string? assetCode, Guid? assetId = null)
        {
            var sqlCommand = "Proc_Asset_CheckDuplicate";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@M_AssetId", assetId);
            parameters.Add("@M_AssetCode", assetCode);

            var result = _uow.Connection.QueryFirstOrDefault<bool>(sqlCommand, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result;

        }
        #endregion

        /// <summary>
        /// Hàm check ds tài sản có chứng từ hay không phục vụ nghiệp vụ xóa tài sản
        /// </summary>
        /// <param name="ids">danh sách tài sản</param>
        /// <returns>Danh sách chứng từ của tài sản nếu có</returns>
        /// Author: HMDUC (05/08/2023)
        public async Task<List<ReceiptAssetCommon>> GetReceiptByAssetId(List<Guid> ids)
        {
            var sqlCommand = "Proc_Asset_CheckExistReceipt";

            DynamicParameters parameters = new DynamicParameters();
            var param = string.Join(",", ids);
            parameters.Add("@ids", param);

            var result = await _uow.Connection.QueryAsync<ReceiptAssetCommon>(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result.ToList();
        }

        /// <summary>
        /// Hàm phân trang va tìm kiếm
        /// </summary>
        /// <param name="pageSize">Số dòng hiển  thị</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="searchInput">Từ khóa cần tìm kiếm</param>
        /// <param name="m_DepartmentName">Tên bộ phận sử dụng</param>
        /// <param name="m_CategoryName">Tên loại tài sản</param>
        /// <returns>
        ///  Object {ListAsset, totalPage}
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region GetPagging
        public async Task<object> GetPagging(int pageSize, int pageNumber, string? searchInput, string? M_DepartmentName, string? M_CategoryName)
        {
            var sqlCommandPagging = "Proc_Asset_Pagging";


            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@searchInput", searchInput);
            parameters.Add("@M_DepartmentName", M_DepartmentName);
            parameters.Add("@M_CategoryName", M_CategoryName);
            parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _uow.Connection.QueryMultipleAsync(sqlCommandPagging, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            //Get assetPagging
            var assetPagging = await result.ReadAsync<Asset>();

            //Get assetAll
            var assetAll = await result.ReadAsync<Asset>();


            int totalRowTable = parameters.Get<int>("totalRecord");
            int totalRow = parameters.Get<int>("totalRow");

            var response = new
            {
                data = assetPagging.ToList(),
                totalRecord = totalRowTable,
                totalRow = totalRow,
                dataSumaryAll = assetAll.ToList(),
            };

            return response;
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
            var sqlCommand = "Proc_Asset_Chose";

            DynamicParameters parameters = new DynamicParameters();
            var paramString = JsonConvert.SerializeObject(ids);
            parameters.Add("@ids", paramString);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _uow.Connection.QueryMultipleAsync(sqlCommand,parameters,commandType: CommandType.StoredProcedure,transaction: _uow.Transaction);

            //Get asset Pagging
            var assetPagging = await result.ReadAsync<Asset>();

            //Get assetAll
            var assetAll = await result.ReadAsync<Asset>();

            int totalRow = parameters.Get<int>("totalRow");

            var response = new
            {
                data = assetPagging.ToList(),
                totalRow = totalRow,
                dataSumaryAll = assetAll.ToList(),
            };

            return response;
        }


        /// <summary>
        /// Hàm lấy tất cả các tài sản theo mã chứng từ
        /// </summary>
        /// <param name="receiptId">Mã chứng từ</param>
        /// <returns>
        /// Danh sách tài sản
        /// </returns>
        /// Author: HMDUC (28/07/2023)
        public async Task<List<Asset>> GetAssetAllByReceipId(Guid receiptId)
        {
            var sqlCommand = "Proc_ReceipAssett_GetAll";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@receiptId", receiptId);

            var listAsset = await _uow.Connection.QueryAsync<Asset>(sqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _uow.Transaction);


            return listAsset.ToList();
        }

        /// <summary>
        /// Hàm lấy mã tài sản mới nhất phục vụ thêm mới tài sản
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetNewCode
        public async Task<string> GetNewCodeAsync()
        {
            //Get tableName Entity
            var tableName = typeof(Asset).Name;

            //Sql command line
            var sqlCommand = $"CALL Proc_{tableName}_GetNew";

            var newCode = await _uow.Connection.QueryAsync<string>(sql: sqlCommand, transaction: _uow.Transaction);

            return string.Join(",", newCode);

        }
        #endregion

        /// <summary>
        /// Hàm export excel
        /// </summary>
        /// <returns>file</returns>
        /// Author: HMDUC (29/06/2023)
        #region GetListExport
        public async Task<Stream> GetListExport()
        {
            //Sql command line
            var sqlCommand = $"CALL Proc_Asset_GetAll";

            var listExport = await _uow.Connection.QueryAsync<Asset>(sql: sqlCommand, transaction: _uow.Transaction);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var stream = new MemoryStream();
            var package = new ExcelPackage(stream);
            var workSheet = package.Workbook.Worksheets.Add("AssetList");
            package.Workbook.Properties.Author = $"{ResourceVN.Author_Excel}";
            package.Workbook.Properties.Title = $"{ResourceVN.Title_Excel}";

            BindingFormatExcel(workSheet, listExport);

            package.Save();
            stream.Position = 0;

            return package.Stream;
        }
        #endregion


        /// <summary>
        /// Hàm binding data từ Entity lên Excel
        /// </summary>
        /// <param name="workSheet">worket sheet</param>
        /// <param name="assets">Tài sản cần xuất file Excel</param>
        /// Author: HMDUC (29/06/2023)
        #region BindingData Entity to Excel
        private void BindingFormatExcel(ExcelWorksheet workSheet, IEnumerable<Asset> assets)
        {

            var lastColumnName = (char)('A' + excelColummnProperties.Count);


            int columnIndex = 1;

            //Get header excel
            foreach (var col in excelColummnProperties)
            {
                if (col.Name.EndsWith("Id") || col.Name == "DepreciationYear" || col.Name == "DepreciationAmount" || col.Name == "ResidualPrice")
                {
                    continue;
                }

                var excelColumnName = (col.GetCustomAttributes(typeof(ExcelColumnAttribute), true)[0] as ExcelColumnAttribute)?.ColumnName;
                workSheet.Cells[1, columnIndex].Value = excelColumnName;
                columnIndex++;
            }

            //style main excel
            using (var range = workSheet.Cells[$"A1:{lastColumnName}{assets.Count() + 1}"])
            {
                range.Style.Font.Name = "Times New Roman";
                range.Style.Font.Size = 12;
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            }


            //style header 
            using (var range = workSheet.Cells[$"A1:{lastColumnName}1"])
            {
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 14;
                range.Style.Font.Color.SetColor(Color.Black);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }


            // Binding data to Sheet
            int rowIndex = 2;
            foreach (var asset in assets)
            {
                columnIndex = 1;
                foreach (var col in excelColummnProperties)
                {

                    //Remove Property Id, DepreciationYear, DepreciationAmount, ResidualPrice
                    if (col.Name.EndsWith("Id") || col.Name == "DepreciationYear" || col.Name == "DepreciationAmount" || col.Name == "ResidualPrice")
                    {
                        continue;
                    }

                    var colValue = col.GetValue(asset);

                    //Get Type Data Original 
                    var colType = Nullable.GetUnderlyingType(col.PropertyType) ?? col.PropertyType;
                    var value = "";

                    switch (colType.Name)
                    {
                        case "DateTime":
                            value = (colValue as DateTime?)?.ToString("dd/MM/yyyy");
                            workSheet.Cells[rowIndex, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            break;
                        case "Decimal":
                            value = (colValue as Decimal?)?.ToString();
                            workSheet.Cells[rowIndex, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            break;
                        case "Int32":
                            value = (colValue as Int32?)?.ToString();
                            workSheet.Cells[rowIndex, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            break;
                        case "Single":
                            value = (colValue as float?)?.ToString();
                            workSheet.Cells[rowIndex, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            break;
                        default:
                            value = colValue?.ToString();
                            break;

                    }

                    workSheet.Cells[rowIndex, columnIndex].Value = value;
                    columnIndex++;
                }
                rowIndex++;
            }


            workSheet.Cells.AutoFitColumns();
        }
        #endregion

        /// <summary>
        /// Định nghĩa list ExcelColumn Attribbute
        /// </summary>
        /// Author: HMDUC (29/06/2023)
        #region Field
        private readonly List<PropertyInfo> excelColummnProperties = typeof(Asset)
     .GetProperties()
     .Where(c => c.GetCustomAttributes(typeof(ExcelColumnAttribute), true).Length > 0)
     .ToList();
        #endregion
    }
}