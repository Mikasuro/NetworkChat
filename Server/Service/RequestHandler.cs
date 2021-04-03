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
    class RequestHandler
    {
        readonly private static string connectionString = "data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=.\\Users.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework&quot";
        readonly private SqlConnection connection = new SqlConnection(connectionString);
        
        public string Start(string request)
        {
            var user = JsonConvert.DeserializeObject<User>(request);
            string mySelectQuery = "SELECT * FROM [User] WHERE [login] = '" + user.login + "'and [password]='" + user.password + "'";
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(mySelectQuery, connectionString))
            {
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                if (table.Rows.Count != 0)
                {
                    return user.login;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
