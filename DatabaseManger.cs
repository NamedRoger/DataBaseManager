using System.Data;
using System.Data.SqlClient;


namespace DataBaseManager
{
    public class DatabaseManger 
    {
        private string connectionString;

        public DatabaseManger(string connection)
        {
            this.connectionString = connection;
        }

        public DataTable ExecuteProc<TEntity>(string procName,TEntity entity)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                CreatorSqlParameter creatorSqlParameter = new CreatorSqlParameter();
                var sqlParameters = creatorSqlParameter.GetSqlParameters(entity);

                SqlCommand command = new SqlCommand(procName, connection) { CommandType = CommandType.StoredProcedure };
                foreach (var parameter in sqlParameters) command.Parameters.Add(parameter);
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                command.Dispose();
            }
            return dataTable;
        }

        public DataTable ExecuteProc(string procName,SqlParameter[] sqlParameters)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(procName, connection) {CommandType = CommandType.StoredProcedure };
                foreach (var parameter in sqlParameters) command.Parameters.Add(parameter);
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dataTable);
                command.Dispose();
            }
            return dataTable;
        }
    }
}
