using System.Data;
using System.Data.SqlClient;

namespace AMLDotNetCore.Shared
{
    public class AdoDotNetService
    {
        public readonly string _connectionString;

        public AdoDotNetService(string connectionSting) 
        {
            _connectionString = connectionSting;
        }
        public DataTable Query(string query, params SqlParameterModel[] sqlParameters) 
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query,connection);
            foreach (var sqlParameter in sqlParameters)
            {
                cmd.Parameters.AddWithValue(sqlParameter.Name,sqlParameter.Value);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();

            return dataTable;

        }


        public int Execute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public SqlParameterModel() { }
        public SqlParameterModel(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
