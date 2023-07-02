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
            var sqlCommandPagging = "Proc_Asset_Pagging";
            var sqlCommandAll = $"CALL Proc_Asset_GetAll";

            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@searchInput", searchInput);
            parameters.Add("@m_DepartmentName", m_DepartmentName);
            parameters.Add("@m_CategoryName", m_CategoryName);
            parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var assetsPagging = await sqlConnection.QueryAsync<Asset>(sqlCommandPagging, parameters, commandType: System.Data.CommandType.StoredProcedure);
            var assetAll = await sqlConnection.QueryAsync<Asset>(sqlCommandAll);


            int totalRowTable = parameters.Get<int>("totalRecord");
            int totalRow = parameters.Get<int>("totalRow");
            decimal totalCost = 0;
            float totalDepreciation = 0;
            float toltalResidualPrice = 0;
            var listAsset = totalRowTable > totalRow ? assetsPagging : assetAll;


            CalculateDepreciationAndResidual(assetsPagging);
            CalculateDepreciationAndResidual(listAsset);


            foreach (var asset in listAsset)
            {

                totalCost += (decimal)asset.Cost;
                totalDepreciation += (float)asset.DepreciationAmount;
                toltalResidualPrice += (float)asset.ResidualPrice;
            }

            var response = new
            {
                data = assetsPagging,
                totalRecord = totalRowTable,
                totalRow = totalRow,
                summaryData = new
                {
                    total_quantity = listAsset.Sum(asset => asset.Quantity),
                    total_cost = totalCost,
                    total_depreciation = totalDepreciation,
                    total_residual_price = toltalResidualPrice,

                }
            };

            return response;
        }


        /// <summary>
        /// Funtion calculate  Depreciation Year/ Amount, Residual Price
        /// </summary>
        /// <param name="assets"></param>
        /// Author: HMDUC (19/06/2023)
        private void CalculateDepreciationAndResidual(IEnumerable<Asset> assets)
        {
            foreach (var asset in assets)
            {
                //Giá trị hao mòn năm = 
                asset.DepreciationYear = decimal.Round((decimal)(1 / (float)asset.LifeTime * (float)asset.Cost), 3);

                //Hao mòn lũy kế 
                asset.DepreciationAmount = (decimal)((float)asset.DepreciationYear * (float)(DateTime.Now.Year - asset.TrackedYear));

                //Giá trị còn lại
                asset.ResidualPrice = (decimal)((float)asset.Quantity * (float)asset.Cost - (float)asset.DepreciationAmount);
            }
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
        /// Function get List Asset to Export Excel
        /// </summary>
        /// <returns></returns>
        /// Author: HMDUC (29/06/2023)
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


        /// <summary>
        /// Binding data for Excel
        /// </summary>
        /// <param name="workSheet"></param>
        /// <param name="assets"></param>
        /// Author: HMDUC (29/06/2023)
        private void BindingFormatExcel(ExcelWorksheet workSheet, IEnumerable<Asset> assets)
        {

            var lastColumnName = (char)('A' + excelColummnProperties.Count);


            int columnIndex = 1;

            foreach (var col in excelColummnProperties)
            {
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


            //style header row
            using (var range = workSheet.Cells[$"A1:{lastColumnName}1"])
            {
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 14;
                range.Style.Font.Color.SetColor(Color.Black);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }


            int rowIndex = 2;
            foreach (var asset in assets)
            {
                columnIndex = 1;
                foreach (var col in excelColummnProperties)
                {
                    var colValue = col.GetValue(asset);
                    var colType = Nullable.GetUnderlyingType(col.PropertyType) ?? col.PropertyType;
                    var value = "";

                    switch (colType.Name)
                    {
                        case "DateTime":
                            value = (colValue as DateTime?)?.ToString("dd/MM/yyyy");
                            workSheet.Cells[rowIndex, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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

        /// <summary>
        /// Declare list ExcelColumn Attribbute
        /// </summary>
        /// Author: HMDUC (29/06/2023)
        private readonly List<PropertyInfo> excelColummnProperties = typeof(Asset)
       .GetProperties()
       .Where(c => c.GetCustomAttributes(typeof(ExcelColumnAttribute), true).Length > 0)
       .ToList();
    }
}