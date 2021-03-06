using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Service
{
    public class RequestHandler
    {
        readonly private static string connectionString = "data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=C:\\Users\\Максим\\source\\repos\\NetworkChat\\Server\\Users.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework&quot";
        readonly private SqlConnection connection = new SqlConnection(connectionString);
        
        public string Start(string message, string request)
        {
            var result = string.Empty;
            if (message == "1")
            {
                var user = JsonConvert.DeserializeObject<User>(request);
                string mySelectQuery = "SELECT * FROM [User] WHERE [login] = '" + user.login + "'and [password]='" + user.password + "'";
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(mySelectQuery, connectionString))
                {
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    if (table.Rows.Count != 0)
                    {
                        result = user.login;
                    }
                    else
                    {
                        result = string.Empty;
                    }
                }
                return result;
            }
            else
            {
                var user = JsonConvert.DeserializeObject<User>(request);
                SqlCommand command = new SqlCommand("INSERT INTO [User] (login, password) VALUES (@login, @password)", connection);
                command.Parameters.Add("login", user.login);
                command.Parameters.Add("password", user.password);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                return user.login;
            }
        }
    }
}
