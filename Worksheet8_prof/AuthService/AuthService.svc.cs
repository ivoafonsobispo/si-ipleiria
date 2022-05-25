using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AuthService
{
    public class AuthService : IAuthService
    {
        public string[] GetHashs()
        {
           using (SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider())
           {
                string hash1 = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes("123")));
                string hash2 = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes("456")));
                return new string[] { hash1, hash2 };
           }
        }

        public DescriptionMessage GetUserDescription(string login, string password, string loginUser)
        {
            if (SqlServerHelper.UserExists(login, password) == 0)
            {
                // Not Autheticated
                return new DescriptionMessage
                {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authenticated"
                    },
                    Description = null
                };
            }
            // Authenticated
            return new DescriptionMessage
            {
                Metadata = new ServiceMetadata
                {
                    Status = true,
                    Message = "Success"
                },
                Description = SqlServerHelper.GetUser(loginUser).Description
            }; 
        }

        public UsersMessage GetUsers(string login, string password)
        {
            int userId = SqlServerHelper.UserExists(login, password);
            if (userId == 0)
            {
                // Not Authenticated
                return new UsersMessage {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Autheticated"
                    },
                    Users = null
                };
            }
            // Authenticated
            User user = SqlServerHelper.GetUser(userId);
            if(user.Role != "Admins")
            {
                // Not Authorized
                return new UsersMessage
                {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authorized"
                    },
                    Users = null
                }; ;
            }
            // Authorized
            return new UsersMessage
            {
                Metadata = new ServiceMetadata
                {
                    Status = true,
                    Message = "Success"
                },
                Users = SqlServerHelper.GetUsers().ToArray()
            };
        }

        public UsersMessage GetUsersByCertificate(string pkcs7Base64)
        {

            SignedCms signedCms = new SignedCms();

            signedCms.Decode(Convert.FromBase64String(pkcs7Base64));

            try
            {
                signedCms.CheckSignature(false);
            }
            catch (Exception exception)
            {
                // Not Authenticated
                return new UsersMessage
                {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Autheticated"
                    },
                    Users = null
                };
            }

            string thumbprint = signedCms.Certificates[0].Thumbprint.ToLower();
            User user = SqlServerHelper.GetUserByThumbprint(thumbprint);
            if (user == null)
            {
                // Not Authenticated
                return new UsersMessage
                {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Autheticated"
                    },
                    Users = null
                };
            }
            // Authenticated

            if (user.Role != "Admins")
            {
                // Not Authorized
                return new UsersMessage
                {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authorized"
                    },
                    Users = null
                }; ;
            }
            // Authorized
            return new UsersMessage
            {
                Metadata = new ServiceMetadata
                {
                    Status = true,
                    Message = "Success"
                },
                Users = SqlServerHelper.GetUsers().ToArray()
            };
        }

        public string Test()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "si.cert.c.pfx";
        }


        public ServiceMetadata UpdateUserDescription(string login, string password, string description)
        {
            int userId = SqlServerHelper.UserExists(login, password);
            if ( userId == 0)
            {
                // Not Autheticated
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authenticated"
                };
            }
            // Authenticated
            User user = SqlServerHelper.GetUser(userId);
            if(user.Role == "Guests")
            {
                // Not Authorized
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authorized"
                };
            }

            int rowsAffected = SqlServerHelper.UpdateUserDescription(userId, description);
            return new ServiceMetadata
            {
                Status = rowsAffected == 1,
                Message = "User Updated"
            };
        }


        public ServiceMetadata UpdateUserDescriptionByCertificate(string pkcs7Base64)
        {
            SignedCms signedCms = new SignedCms();

            signedCms.Decode(Convert.FromBase64String(pkcs7Base64));

            try
            {
                signedCms.CheckSignature(false);
            }
            catch (Exception exception)
            {
                // Not Authenticated
                return  new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Autheticated"
                };
            }

            string thumbprint = signedCms.Certificates[0].Thumbprint.ToLower();
            User user = SqlServerHelper.GetUserByThumbprint(thumbprint);

            if (user == null)
            {
                // Not Autheticated
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authenticated"
                };
            }
            // Authenticated
            if (user.Role == "Guests")
            {
                // Not Authorized
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authorized"
                };
            }

            string description = Encoding.UTF8.GetString(signedCms.ContentInfo.Content);

            int rowsAffected = SqlServerHelper.UpdateUserDescription(user.Id, description);
            return new ServiceMetadata
            {
                Status = rowsAffected == 1,
                Message = "User Updated"
            };
        }

        public ServiceMetadata UpdateUserPasswordByCertificate(string pkcs7Base64, string login)
        {
            SignedCms signedCms = new SignedCms();

            signedCms.Decode(Convert.FromBase64String(pkcs7Base64));

            try
            {
                signedCms.CheckSignature(false);
            }
            catch (Exception exception)
            {
                // Not Authenticated
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Autheticated"
                };
            }

            string thumbprint = signedCms.Certificates[0].Thumbprint.ToLower();
            User user = SqlServerHelper.GetUserByThumbprint(thumbprint);

            if (user == null)
            {
                // Not Autheticated
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authenticated"
                };
            }
            // Authenticated
            if (user.Role != "Admin")
            {
                // Not Authorized
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authorized"
                };
            }

            // Authorized
            EnvelopedCms envelopedCms = new EnvelopedCms();
            envelopedCms.Decode(signedCms.ContentInfo.Content);
            string filename = AppDomain.CurrentDomain.BaseDirectory + "si.cert.c.pfx";
            using (X509Certificate2 serverCertificate = new X509Certificate2(filename, "ei.si"))
            {
                try
                {
                    envelopedCms.Decrypt(new X509Certificate2Collection(serverCertificate));
                }
                catch (Exception)
                {
                    throw;
                }
            }

            string password = Encoding.UTF8.GetString(envelopedCms.ContentInfo.Content);

            int rowsAffected = SqlServerHelper.UpdateUserPassword(login, password);
            return new ServiceMetadata
            {
                Status = rowsAffected == 1,
                Message = "User Updated"
            };
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
