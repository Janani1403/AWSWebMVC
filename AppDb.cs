using MySql.Data.MySqlClient;

namespace WebFramework
{
    public class AppDb
    {
        public MySqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection.Dispose();
    }
}