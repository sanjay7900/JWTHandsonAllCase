using Dapper;
using iTextSharp.text.pdf;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JWTHandsonAllCase.Core.Dapper
{
    public class DapperQueryService:IDapperQueryService
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _sqlConnection;

        public DapperQueryService(IConfiguration configuration)
        {
            _config= configuration;
            _sqlConnection = _config.GetConnectionString("JWTHandson").Connect();

        }

        public async Task<List<T>> QueryAsync<T>(string ProcedureName, object paramObject)
        {
            try
            {
                _sqlConnection.Open();  
                var data = await _sqlConnection.QueryAsync<T>(ProcedureName, paramObject);  
                 return data.AsList();  

            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally { _sqlConnection.Close(); } 
        }

        public async Task<List<T>> QueryAsync<T>(string ProcedureName)
        {
            try
            {
                _sqlConnection.Open();
                var data = await _sqlConnection.QueryAsync<T>(ProcedureName,new {});
                return data.AsList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { _sqlConnection.Close(); }
        }

        public  async Task<T> QueryFirstOrDefaultAsync<T>(string ProcedureName, object paramObject)
        {
            try
            {
                _sqlConnection.Open();
                var data = await _sqlConnection.QueryFirstOrDefaultAsync<T>(ProcedureName, paramObject, commandType: CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { _sqlConnection.Close(); }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string ProcedureName)
        {
            try
            {
                _sqlConnection.Open();
                var data = await _sqlConnection.QueryFirstOrDefaultAsync<T>(ProcedureName, commandType: CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { _sqlConnection.Close(); }
        }
        
        public async Task<(List<T1>, List<T2>)> QueryMultipleAsListAsync<T1, T2>(string storedProcedure, object param = null)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                var data = await _sqlConnection.QueryMultipleAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                var t1= await data.ReadAsync<T1>(); 
                var t2= await data.ReadAsync<T2>();
                return  (t1.AsList(), t2.AsList());
            }
            catch (Exception ex)
            {
              await _sqlConnection.CloseAsync();
                throw;
            }
            finally { _sqlConnection.Close(); }
           
        }

        public async Task<(List<T1>, List<T2>, List<T3>)> QueryMultipleAsListAsync<T1, T2, T3>(string storedProcedure, object param = null)
        {

            try
            {
                await _sqlConnection.OpenAsync();
                var data = await _sqlConnection.QueryMultipleAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                var t1 = await data.ReadAsync<T1>();
                var t2 = await data.ReadAsync<T2>();
                var t3=await data.ReadAsync<T3>();  
                return (t1.AsList(), t2.AsList(),t3.AsList());
            }
            catch (Exception ex)
            {
                await _sqlConnection.CloseAsync();
                throw;
            }
            finally { _sqlConnection.Close(); }
        }

        public async Task<(List<T1>, List<T2>, List<T3>, List<T4>)> QueryMultipleAsListAsync<T1, T2, T3, T4>(string storedProcedure, object param = null)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                var data = await _sqlConnection.QueryMultipleAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                var t1 = await data.ReadAsync<T1>();
                var t2 = await data.ReadAsync<T2>();
                var t3 = await data.ReadAsync<T3>();
                var t4= await data.ReadAsync<T4>(); 
                return (t1.AsList(), t2.AsList(), t3.AsList(), t4.AsList());
            }
            catch (Exception ex)
            {
                await _sqlConnection.CloseAsync();
                throw;
            }
            finally { _sqlConnection.Close(); }
        }

        public async Task<(List<T1>, List<T2>, List<T3>, List<T4>, List<T5>)> QueryMultipleAsListAsync<T1, T2, T3, T4, T5>(string storedProcedure, object param = null)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                var data = await _sqlConnection.QueryMultipleAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                var t1 = await data.ReadAsync<T1>();
                var t2 = await data.ReadAsync<T2>();
                var t3 = await data.ReadAsync<T3>();
                var t4 = await data.ReadAsync<T4>();
                var t5 = await data.ReadAsync<T5>();    
                return (t1.AsList(), t2.AsList(), t3.AsList(), t4.AsList(),t5.AsList());
            }
            catch (Exception ex)
            {
                await _sqlConnection.CloseAsync();
                throw;
            }
            finally { _sqlConnection.Close(); }
        }

        public async Task<(T1, T2)> QueryMultipleAsync<T1, T2>(string storedProcedure, object param = null)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                var data = await _sqlConnection.QueryMultipleAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                var t1 = await data.ReadFirstOrDefaultAsync<T1>();
                var t2 = await data.ReadFirstOrDefaultAsync<T2>();
               
                return (t1, t2);
            }
            catch (Exception ex)
            {
                await _sqlConnection.CloseAsync();
                throw;
            }
            finally { _sqlConnection.Close(); }
        }

        public async Task<(T1, T2, T3)> QueryMultipleAsync<T1, T2, T3>(string storedProcedure, object param = null)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                var data = await _sqlConnection.QueryMultipleAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                var t1 = await data.ReadFirstOrDefaultAsync<T1>();
                var t2 = await data.ReadFirstOrDefaultAsync<T2>();
                var t3 = await data.ReadFirstOrDefaultAsync<T3>();  

                return (t1, t2, t3);
            }
            catch (Exception ex)
            {
                await _sqlConnection.CloseAsync();
                throw;
            }
            finally { _sqlConnection.Close(); }
        }

        public async Task<(T1, T2, T3, T4)> QueryMultipleAsync<T1, T2, T3, T4>(string storedProcedure, object param = null)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                var data = await _sqlConnection.QueryMultipleAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                var t1 = await data.ReadFirstOrDefaultAsync<T1>();
                var t2 = await data.ReadFirstOrDefaultAsync<T2>();
                var t3 = await data.ReadFirstOrDefaultAsync<T3>();
                var t4 = await data.ReadFirstOrDefaultAsync<T4>();

                return (t1, t2, t3,t4);
            }
            catch (Exception ex)
            {
                await _sqlConnection.CloseAsync();
                throw;
            }
            finally { _sqlConnection.Close(); }
        }

        public async Task<(T1, T2, T3, T4, T5)> QueryMultipleAsync<T1, T2, T3, T4, T5>(string storedProcedure, object param = null)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                var data = await _sqlConnection.QueryMultipleAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                var t1 = await data.ReadFirstOrDefaultAsync<T1>();
                var t2 = await data.ReadFirstOrDefaultAsync<T2>();
                var t3 = await data.ReadFirstOrDefaultAsync<T3>();
                var t4 = await data.ReadFirstOrDefaultAsync<T4>();
                var t5 = await data.ReadFirstOrDefaultAsync<T5>();  

                return (t1, t2, t3, t4,t5);
            }
            catch (Exception ex)
            {
                await _sqlConnection.CloseAsync();
                throw;
            }
            finally { _sqlConnection.Close(); }
        }
    }
}
