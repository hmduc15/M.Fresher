using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly string? _connectionString;

        public BaseRepository(IConfiguration configuration) 
        {
            _connectionString = configuration["ConnectionString"];
        }

        /// <summary>
        ///  Function Get list Record
        /// </summary>
        /// <returns>List Asset</returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<List<TEntity>> GetAllAsync()
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //connect mysql
            var sqlConnection = new MySqlConnection(_connectionString);
            var sqlCommand = $"CALL Proc_{tableName}_GetAll";

            //opent connection
            await sqlConnection.OpenAsync();

            var assets = await sqlConnection.QueryAsync<TEntity>(sql: sqlCommand);

            await sqlConnection.CloseAsync();

            return assets.ToList();

        } 
  
        /// <summary>
        ///  Functio Get  by Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<TEntity> GetByCodeAsync(string code)
        {
            //Get tableName Entity
            var tableName = typeof (TEntity).Name;

            var sqlConnection = new MySqlConnection(_connectionString);
            var sqlCommand = $"SELECT * FROM {tableName} WHERE {tableName}Code = @code";

            //open connection
            await sqlConnection.OpenAsync();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var entity = await sqlConnection.QueryFirstOrDefaultAsync<TEntity>(sql: sqlCommand, param: parameters);

            await sqlConnection.CloseAsync();

            return entity;
        }

        /// <summary>
        /// Function  Get Record by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Entity</returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //connect mysql
            var sqlConnection = new MySqlConnection(_connectionString);  
            var sqlCommand = $"SELECT * FROM {tableName} WHERE {tableName}Id = @id";
          
            //open connection
            await sqlConnection.OpenAsync();  

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var entity = await sqlConnection.QueryFirstOrDefaultAsync<TEntity>(sql: sqlCommand, param: parameters);
            
            //close connection
            await sqlConnection.CloseAsync();   

            return entity;
        }


        /// <summary>
        /// Function Insert a record 
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>
        /// Row Affected
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<int> InsertAsync(TEntity entity)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //connect sql
            var sqlConnection = new MySqlConnection(_connectionString);

            //open connection
            await sqlConnection.OpenAsync();

            var sqlCommandProc = $"Proc_{tableName}_Insert";
            var sqlCommand = sqlConnection.CreateCommand();
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


            var rowAffected = await sqlConnection.ExecuteAsync(sql: sqlCommandProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            //close connecion
            await sqlConnection.CloseAsync();

            return rowAffected;

        }

        /// <summary>
        /// Function Update Asset
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>
        ///  Asset
        /// </returns>
        ///  Author: HMDUC (19/06/2023)
        public async Task<int> UpdateAsync(TEntity entity)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //connect sql
            var sqlConnection = new MySqlConnection(_connectionString);

            //open connection
            await sqlConnection.OpenAsync();

            var sqlCommandProc = $"Proc_{tableName}_Update";
            var sqlCommand = sqlConnection.CreateCommand();
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


            var rowAffected = await sqlConnection.ExecuteAsync(sql: sqlCommandProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            //close connecion
            await sqlConnection.CloseAsync();

            return rowAffected;
        }



        /// <summary>
        /// Function Delete  by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        ///  Count Row Affect
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<int> DeleteEntityAsync(Guid id)
        {
            //Get tableName Entity
            var tableName = typeof (TEntity).Name;  

            //connect sql
            var sqlConnection = new MySqlConnection(_connectionString);
            var sqlCommand = $"DELETE FROM {tableName} WHERE {tableName}Id = @id";

            //open connection
            await sqlConnection.OpenAsync();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            int rowsAffected = await sqlConnection.ExecuteAsync(sql: sqlCommand, param: parameters);

            //close connection
            await sqlConnection.CloseAsync();

            return rowsAffected;
        }


        /// <summary>
        /// Function Delete Multiple 
        /// </summary>
        /// <param name="ids">List Id</param>
        /// <returns>
        /// Count Row Affect
        /// </returns>
        /// Author: HMDUC (19/06/2023)
        public async Task<int> DeleteEntityMulAsync(List<Guid> ids)
        {
            //Get tableName Entity
            var tableName = typeof(TEntity).Name;

            //connect sql
            var sqlConnection = new MySqlConnection(_connectionString);
            var sqlCommand = $"Proc_{tableName}_DeleteMulti";

            DynamicParameters parameters = new DynamicParameters();
            var paramString = string.Join(",", ids);
            parameters.Add("@ids", paramString);

            int rowsAffected = await sqlConnection.ExecuteAsync(sql: sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            return rowsAffected;
        }
    }
}
