using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AuthService
{

    /// <summary>
    /// Summary description for SqlServerHelper
    /// </summary>
    public class SqlServerHelper
    {

        public SqlServerHelper()
        {
      
        }

        public static int UserExists(string thumbprint)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "SELECT id FROM Users where thumbprint = @thumbprint";
                cmd.Parameters.AddWithValue("thumbprint", thumbprint);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int id = (int)cmd.ExecuteScalar();
                return id;
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        public static User GetUser(int id)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Users where id = @id";
                cmd.Parameters.AddWithValue("id", @id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User user = LoadUser(reader);
                    return user;
                }

                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        public static Double GetBalance(int accountID)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "SELECT balance FROM users WHERE accountID = @accountID"; // TODO;
                cmd.Parameters.AddWithValue("accountID", accountID);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                double balance = (double)cmd.ExecuteScalar();
                return balance;
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        private static User LoadUser(SqlDataReader reader)
        {
            User user = new User();
            user.Id = reader.GetSqlInt32(reader.GetOrdinal("Id")).Value;
            user.Login = reader.GetString(reader.GetOrdinal("Login"));
            user.Name = reader.GetString(reader.GetOrdinal("Name"));
            user.Thumbprint = reader.GetString(reader.GetOrdinal("Thumbprint"));
            user.AccountID = reader.GetInt32(reader.GetOrdinal("AccountID"));
            return user;
        }

    }
}