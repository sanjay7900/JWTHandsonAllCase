using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;


public static class ConnectionProvider
{
    public static SqlConnection  Connect(this string connectionString)
    {
        return new SqlConnection (connectionString);    

    }
}

