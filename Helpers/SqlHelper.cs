using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WebFramework.Models;

namespace WebFramework.Helpers
{
    public class SqlHelper
    {
        //server refers to the instance connection,
        //user name and password refer to the configured values during DB creation

        private AppDb _database = new AppDb("server=mysql-database.c9ie7rbjrfmg.us-east-1.rds.amazonaws.com;user id=admin;password=password;port=3306;database=EmployeeDb;");

        private void BindParams(MySqlCommand cmd, Employee employee)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@EmployeeId",
                DbType = DbType.Int32,
                Value = employee.EmployeeId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@FirstName",
                DbType = DbType.String,
                Value = employee.FirstName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@LastName",
                DbType = DbType.String,
                Value = employee.LastName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Address",
                DbType = DbType.String,
                Value = employee.Address,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@City",
                DbType = DbType.String,
                Value = employee.City,
            });
        }

        private async Task<List<Employee>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Employee>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Employee()
                    {
                        EmployeeId = reader.GetInt32(0),
                        LastName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        Address = reader.GetString(3),
                        City = reader.GetString(4)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public async Task InsertAsync(Employee employee)
        {
            var cmd = _database.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Employees (EmployeeId, LastName,FirstName, Address, City) VALUES (@EmployeeId, @LastName, @FirstName, @Address, @City);";
            BindParams(cmd, employee);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Employee>> LatestPostsAsync()
        {
            var cmd = _database.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `EmployeeId`, `LastName`, `FirstName`, `Address`, `City` FROM `Employees` ORDER BY `EmployeeId` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

    }
}