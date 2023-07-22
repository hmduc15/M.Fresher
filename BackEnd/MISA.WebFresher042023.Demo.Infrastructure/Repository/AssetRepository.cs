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
using System.Text.RegularExpressions;
using OfficeOpenXml;
using static MISA.WebFresher042023.Demo.Core.MISAAttribute.CustomAttribute;
using System;
using OfficeOpenXml.Style;
using System.Reflection;
using System.Drawing;
using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using System.Data.Common;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    /// <summary>
    /// Implement Interfacse Asset Repository
    /// </summary>
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {


        #region Constructor
        public AssetRepository(IConfiguration configuration) : base(configuration)
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

            var result = _connection.QueryFirstOrDefault<bool>(sqlCommand, parameters, commandType: CommandType.StoredProcedure);

            return result;

        } 
        #endregion

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

            var result = await _connection.QueryMultipleAsync(sqlCommandPagging, parameters, commandType: CommandType.StoredProcedure);

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
        /// Hàm lấy mã tài sản mới nhất phục vụ thêm mới tài sản
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetNewCode
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
        #endregion

        /// <summary>
        /// Hàm export excel
        /// </summary>
        /// <returns>file</returns>
        /// Author: HMDUC (29/06/2023)
        #region GetListExport
        public async Task<Stream> GetListExport()
        {
            //connect mysql
            var sqlConnection = new MySqlConnection(_connectionString);
            var sqlCommand = $"CALL Proc_Asset_GetAll";

            //opent connection
            await sqlConnection.OpenAsync();

            var listExport = await sqlConnection.QueryAsync<Asset>(sql: sqlCommand);


            //close connection
            await sqlConnection.CloseAsync();

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