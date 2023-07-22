using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Field
        protected readonly string? _connectionString;

        protected readonly DbConnection _connection;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
            _connection = new MySqlConnection(_connectionString);
        }
        #endregion

        /// <summary>
        ///  Hàm lấy ra danh sách Entity từ DB
        /// </summary>
        /// <returns>Danh sách Entity</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetAllAsync
        public async Task<List<TEntity>> GetAllAsync()
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //connect mysql
            var sqlCommand = $"CALL Proc_{tableName}_GetAll";


            var assets = await _connection.QueryAsync<TEntity>(sql: sqlCommand);


            return assets.ToList();

        }
        #endregion

        /// <summary>
        ///  Hàm lấy Entity theo mã từ DB
        /// </summary>
        /// <param name="code">Mã Entity</param>
        /// <returns>Entity</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetByCodeAsync
        public async Task<TEntity> GetByCodeAsync(string code)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //Sql command line
            var sqlCommand = $"SELECT * FROM {tableName} WHERE {tableName}Code = @code";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var entity = await _connection.QueryFirstOrDefaultAsync<TEntity>(sql: sqlCommand, param: parameters);

            return entity;
        } 
        #endregion

        /// <summary>
        /// Hàm lấy Entity theo Id
        /// </summary>
        /// <param name="Id">Id của Entity</param>
        /// <returns>Entity</returns>
        /// Author: HMDUC (19/06/2023)
        #region GetByIdAsync
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //Sql command line
            var sqlCommand = $"SELECT * FROM {tableName} WHERE {tableName}Id = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var entity = await _connection.QueryFirstOrDefaultAsync<TEntity>(sql: sqlCommand, param: parameters);

            return entity;
        } 
        #endregion


        /// <summary>
        /// Hàm thêm mới Entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>
        /// Row Affected: Số dòng bị ảnh hưởng 
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region InsertAsync
        public async Task<int> InsertAsync(TEntity entity)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            var sqlCommandProc = $"Proc_{tableName}_Insert";
            var sqlCommand = (MySqlCommand)_connection.CreateCommand();
            sqlCommand.CommandText = sqlCommandProc;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlCommandBuilder.DeriveParameters(sqlCommand);
            DynamicParameters? parameters = new DynamicParameters();

            foreach (MySqlParameter param in sqlCommand.Parameters)
            {
                var paramKey = param.ParameterName;
                var propValue = paramKey.Replace("@m_", "");
                var paramValue = entity.GetType().GetProperty(propValue)?.GetValue(entity);

                parameters.Add(paramKey, paramValue);
            }


            var rowAffected = await _connection.ExecuteAsync(sql: sqlCommandProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);


            return rowAffected;

        } 
        #endregion

        /// <summary>
        /// Hàm cập nhật Entity
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>
        ///  Rows effected: Số dòng bị ảnh hưởng
        /// </returns>
        ///  Author: HMDUC (19/06/2023)
        #region UpdateAsync
        public async Task<int> UpdateAsync(TEntity entity)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //Sql command line
            var sqlCommandProc = $"Proc_{tableName}_Update";

            var sqlCommand = (MySqlCommand)_connection.CreateCommand();
            sqlCommand.CommandText = sqlCommandProc;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlCommandBuilder.DeriveParameters(sqlCommand);
            DynamicParameters? parameters = new DynamicParameters();

            foreach (MySqlParameter param in sqlCommand.Parameters)
            {
                var paramKey = param.ParameterName;
                var propValue = paramKey.Replace("@M_", "");
                var paramValue = entity?.GetType().GetProperty(propValue)?.GetValue(entity);

                parameters.Add(paramKey, paramValue);
            }


            var rowAffected = await _connection.ExecuteAsync(sql: sqlCommandProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            return rowAffected;
        } 
        #endregion



        /// <summary>
        /// Hàm xóa Entity theo Id
        /// </summary>
        /// <param name="Id">ID của Entity</param>
        /// <returns>
        ///  rowsAffected: Số dòng bị ảnh hưởng
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region DeleteEnityAsync
        public async Task<int> DeleteEntityAsync(Guid id)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //Sql command line
            var sqlCommand = $"DELETE FROM {tableName} WHERE {tableName}Id = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            int rowsAffected = await _connection.ExecuteAsync(sql: sqlCommand, param: parameters);


            return rowsAffected;
        } 
        #endregion


        /// <summary>
        /// Hàm xóa nhiều theo danh sách Id
        /// </summary>
        /// <param name="ids">Danh sách  Id</param>
        /// <returns>
        /// rowsAffected: Số dòng bị ảnh hưởng
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        #region DeleteMultipleAsync
        public async Task<int> DeleteEntityMulAsync(List<Guid> ids)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            using (_connection)
            {
                //Use transaction
                using (var transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        var sqlCommand = $"Proc_{tableName}_DeleteMulti";

                        DynamicParameters parameters = new DynamicParameters();
                        var paramString = string.Join(",", ids);
                        parameters.Add("@ids", paramString);

                        int rowsAffected = await _connection.ExecuteAsync(sql: sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);

                        transaction.Commit();

                        return rowsAffected;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }

        } 
        #endregion
    }
}
