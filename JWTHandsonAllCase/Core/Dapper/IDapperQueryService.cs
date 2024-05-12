using iText.Kernel.Font;

namespace JWTHandsonAllCase.Core.Dapper
{
    public interface IDapperQueryService
    {
        Task<List<T>> QueryAsync<T>(string ProcedureName,object paramObject);
        Task<List<T>> QueryAsync<T>(string ProcedureName);
        Task<T> QueryFirstOrDefaultAsync<T>(string ProcedureName, object paramObject);
        Task<T> QueryFirstOrDefaultAsync<T>(string ProcedureName);
        Task<(T1, T2)> QueryMultipleAsync<T1, T2>(string storedProcedure, object param = null);
        Task<(T1, T2,T3)> QueryMultipleAsync<T1, T2,T3>(string storedProcedure, object param = null);
        Task<(T1, T2,T3,T4)> QueryMultipleAsync<T1, T2,T3, T4>(string storedProcedure, object param = null);
        Task<(T1, T2, T3, T4, T5)> QueryMultipleAsync<T1, T2, T3, T4, T5>(string storedProcedure, object param = null);
        Task<(List<T1>, List<T2>)> QueryMultipleAsListAsync<T1, T2>(string storedProcedure, object param = null);
        Task<(List<T1>, List<T2>, List<T3>)> QueryMultipleAsListAsync<T1, T2,T3>(string storedProcedure, object param = null);
        Task<(List<T1>, List<T2>, List<T3>, List<T4>)> QueryMultipleAsListAsync<T1, T2,T3,T4>(string storedProcedure, object param = null);
        Task<(List<T1>, List<T2>, List<T3>, List<T4>, List<T5>)> QueryMultipleAsListAsync<T1, T2,T3,T4,T5>(string storedProcedure, object param = null);

      
    }
}
