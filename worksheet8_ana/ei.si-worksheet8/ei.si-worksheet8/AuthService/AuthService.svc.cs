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
        public string[] GetHashes()
        {
            SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();

            //Computed Hashes:
            /*
             hash1: pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=
             hash2: s6jg4fmrG/46NvIx9nb3i7MKUZ0rIebFMMDu6Ou0pdA=
             */

            byte[] hash1 = sha.ComputeHash(Encoding.UTF8.GetBytes("123"));
            byte[] hash2 = sha.ComputeHash(Encoding.UTF8.GetBytes("456"));

            return new string[] { Convert.ToBase64String(hash1), Convert.ToBase64String(hash2) };
        }

        public User GetUser(int id)
        {
            return SqlServerHelper.GetUser(id);
        }

        public UserMessage GetUsers(string login, string password)
        {
            int userID = SqlServerHelper.UserExists(login, password);
            // Authentication
            if (userID == 0) { // Se o user não existir

                // Not Authenticated
                return new UserMessage {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authenticaded"
                    }, Users = null
                }; 
            }

            // Authenticated
            User user = SqlServerHelper.GetUser(userID);
            if (user.Role != "Admins") {

                // Not Authorized
                return new UserMessage
                {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authorized"
                    },
                    Users = null
                };
            }
            
            return new UserMessage
            {
                Metadata = new ServiceMetadata
                {
                    Status = true,
                    Message = "Success"
                },
                Users = SqlServerHelper.GetUsers().ToArray()
            };  // Converter a lista num array
        }

        public UserMessage GetUsersByCertificate(string pkcsBase64)
        {
            SignedCms signedCms = new SignedCms();
            signedCms.Decode(Convert.FromBase64String(pkcsBase64));

            try
            {
                signedCms.CheckSignature(false);

                string thumbprint = signedCms.Certificates[0].Thumbprint.ToLower();
                User user = SqlServerHelper.GetUserByThumbPrint(thumbprint);
                if(user == null) {
                    // Not Authorized
                    return new UserMessage
                    {
                        Metadata = new ServiceMetadata
                        {
                            Status = false,
                            Message = "Not Authenticated"
                        },
                        Users = null
                    };

                }

                // Authenticated
                if (user.Role != "Admins")
                {

                    // Not Authorized
                    return new UserMessage
                    {
                        Metadata = new ServiceMetadata
                        {
                            Status = false,
                            Message = "Not Authorized"
                        },
                        Users = null
                    };
                }

                return new UserMessage
                {
                    Metadata = new ServiceMetadata
                    {
                        Status = true,
                        Message = "Success"
                    },
                    Users = SqlServerHelper.GetUsers().ToArray()
                };  // Converter a lista num array
            }
            catch (Exception ex) //Not Authenticated
            {
                // Not Authenticated
                return new UserMessage
                {
                    Metadata = new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authenticaded"
                    },
                    Users = null
                };

            }

            //int userID = SqlServerHelper.UserExists(login, password);
            //// Authentication
            //if (userID == 0)
            //{ // Se o user não existir

            //    // Not Authenticated
            //    return new UserMessage
            //    {
            //        Metadata = new ServiceMetadata
            //        {
            //            Status = false,
            //            Message = "Not Authenticaded"
            //        },
            //        Users = null
            //    };
            //}
        }

        public ServiceMetadata UpdateUserDescription(string login, string password, string description)
        {
            int id;
            try
            {
                id = SqlServerHelper.UserExists(login, password);
                if (id == 0)
                { // Se o user não existir
                    return new ServiceMetadata
                        {
                            Status = false,
                            Message = "Not Authenticaded"
                        };
                }

                User user = SqlServerHelper.GetUser(id);
                if (user.Role == "Guests")
                {

                    // Not Authorized
                    return new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authorized"
                    };
                }

                return new ServiceMetadata
                {
                    Status = SqlServerHelper.UpdateUserDescription(id, description) != 0,
                    Message = "Success"
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Exception Caught: " + ex.Message
                }; 
            }
        }

        public ServiceMetadata UpdateUserDescriptionByCertificate(string pkcs7Base64)
        {
            SignedCms signedCms = new SignedCms();

            signedCms.Decode(Convert.FromBase64String(pkcs7Base64)); //decode e voltar a ficar em bytes

            try
            {
                signedCms.CheckSignature(false); //se fosse true só iamos ver a signature
            }
            catch (Exception ex)
            {
                //fica aqui porque se falhar significa que não está logado
                //NULL = not authenticated
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authenticated"
                };
            }

            string thumbprint = signedCms.Certificates[0].Thumbprint.ToLower(); //[0] porque só temos 1 certificado

            User user = SqlServerHelper.GetUserByThumbPrint(thumbprint); //para a linha abaixo e guardar o id

            if (user == null)
            {
                //Not authenticated
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authenticated"
                };
            }
            if (user.Role == "Guests")
            {
                //not authorized if guest
                return new ServiceMetadata
                {
                    Status = false,
                    Message = "Not Authorized"
                };
            }

            string description = Encoding.UTF8.GetString(signedCms.ContentInfo.Content);
            return new ServiceMetadata
            {
                Status = SqlServerHelper.UpdateUserDescription(user.Id, description) == 1,
                Message = "Success"
            };
            //return (SqlServerHelper.UpdateUserDescription(id, description) == 1);
        }

        public ServiceMetadata UpdateUserPassword(int id, string pkcs7Base64)
        {
            
            string fileName_b = AppDomain.CurrentDomain.BaseDirectory + @"\estg.ei.si.b.pfx";
            using (X509Certificate2 certificate_b = new X509Certificate2(fileName_b,"ei.si"))
            {
                SignedCms signedCms = new SignedCms();

                signedCms.Decode(Convert.FromBase64String(pkcs7Base64));

                try
                {
                    signedCms.CheckSignature(false); //se fosse true só iamos ver a signature
                }
                catch (Exception ex)
                {
                    //fica aqui porque se falhar significa que não está logado
                    //NULL = not authenticated
                    return new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authenticated"
                    };
                }

                EnvelopedCms envelopedCms = new EnvelopedCms();
                envelopedCms.Decode(signedCms.ContentInfo.Content); // Password cifrada

                envelopedCms.Decrypt(); // password
                string password = Encoding.UTF8.GetString(envelopedCms.ContentInfo.Content);

                User user = SqlServerHelper.GetUser(id);

                if (user == null)
                {
                    //Not authenticated
                    return new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authenticated"
                    };
                }
                if (user.Role == "Guests")
                {
                    //not authorized if guest
                    return new ServiceMetadata
                    {
                        Status = false,
                        Message = "Not Authorized"
                    };
                }


                return new ServiceMetadata
                {
                    Status = SqlServerHelper.UpdateUserPassword(id, password) == 1,
                    Message = "Success"
                };
            }
             //decode e voltar a ficar em bytes

            
        }

        public string VerifyAcessToBD()
            {
                User user = SqlServerHelper.GetUser(1);
                if (user == null)
                    return "Erro ao aceder à base de dados";
                return user.Name;
            }


        }

}
