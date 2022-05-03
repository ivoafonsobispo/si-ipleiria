using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AuthService
{
    public class AuthService : IAuthService
    {
        public User[] GetUsers(string login, string password)
        {
            if (SqlServerHelper.UserExists(login,password) == 0)
            {
                return null;
            }

            return SqlServerHelper.GetUsers().ToArray();
        }

        /// <summary>
        /// Exemplo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string VerifyAcessToBD()
        {
            User user = SqlServerHelper.GetUser(1);
            if (user == null)
                return "Erro ao aceder à base de dados";
            return user.Name;
        }


    }

}
