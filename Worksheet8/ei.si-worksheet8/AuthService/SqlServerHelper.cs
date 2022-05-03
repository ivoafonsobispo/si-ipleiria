﻿using System;
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
            //
            // TODO: Add constructor logic here
            //        
        }


        /// <summary>
        /// Verify if user exists
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>Returns the id of user or 0 if user do not exists</returns>
        public static int UserExists(string login, string password)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();

                // attention...
                cmd.CommandText = "SELECT id FROM Users where login = @login AND password = @password";

                cmd.Parameters.AddWithValue("login", login);
                cmd.Parameters.AddWithValue("password", password);

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



        /// <summary>
        /// Update the User(id) Description
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="description"> Description to update</param>
        /// <returns>number of rows updated (0 or 1)</returns>
        public static int UpdateUserDescription(int id, string description)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();

                // todo ...
                cmd.CommandText = "UPDATE ...";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int rowsAffected = (int)cmd.ExecuteNonQuery();
                return rowsAffected;
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


        /// <summary>
        /// Gets a User from Database 
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>Returns a User if id exists or null</returns>
        public static User GetUser(int id)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                // isto DEVE que ser alterado .... para usar SQLParameters
                cmd.CommandText = "SELECT * FROM Users where id = " + id.ToString();
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


        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>List with the users or null</returns>
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Users";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                // todo : obter lista de utilizadores
                while (reader.Read())
                {
                    User user = LoadUser(reader);
                    users.Add(user);
                }
                //...........
                return users;
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



        /// <summary>
        /// Gets the user info from the current row of SqlReader
        /// </summary>
        /// <param name="reader">SqlReader</param>
        /// <returns>User info</returns>
        private static User LoadUser(SqlDataReader reader)
        {
            User user = new User();
            user.Id = reader.GetSqlInt32(reader.GetOrdinal("Id")).Value;
            user.Login = reader.GetString(reader.GetOrdinal("Login"));
            user.Name = reader.GetString(reader.GetOrdinal("Name"));
            user.Password = reader.GetString(reader.GetOrdinal("Password"));
            if (!reader.IsDBNull(reader.GetOrdinal("Description")))
                user.Description = reader.GetString(reader.GetOrdinal("Description"));
            return user;
        }

    }
}