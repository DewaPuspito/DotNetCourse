using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IntermediateProgram.Data
{
    public class DapperExample
    {
        // private IConfiguration _config;
        private string _connectionString;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public DapperExample(IConfiguration config)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            // _config = config;
#pragma warning disable CS8601 // Possible null reference assignment.
            _connectionString = config.GetConnectionString("DefaultConnection");
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        
        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString );
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString );
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString );
            return (dbConnection.Execute(sql) > 0);
        }

        public int ExeceuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString );
            return dbConnection.Execute(sql);
        }
    }
}