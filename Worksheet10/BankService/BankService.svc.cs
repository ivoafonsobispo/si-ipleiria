using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AuthService
{
    public class BankService : IBankService
    {
        public BalanceResponse GetBalance(string pkcs7Base64)
        {
            //Código do professor:
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
                return new BalanceResponse { StatusCode = StatusCode.AuthorizationError, Message = "Not Authenticated" };
            }

            string thumbprint = signedCms.Certificates[0].Thumbprint.ToLower(); //[0] porque só temos 1 certificado

             int userID = SqlServerHelper.UserExists(thumbprint); //para a linha abaixo e guardar o id

            if (userID == null)
            {
                //Not authenticated
                return new BalanceResponse { StatusCode = StatusCode.DatabaseError, Message = "No User in DB" };
            }

            byte[] pkcs7Data = signedCms.Encode();


            double balance = SqlServerHelper.GetBalance(userID);

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "si.cert.a.pfx";
            using (X509Certificate2 serverCertificate = new X509Certificate2(filepath, "ei.si"))
            using (X509Certificate2 clientCertificate = new X509Certificate2("si.cert.b.cer"))
            {

                #region Encrypt to Client
                ContentInfo contentToEncrypt = new ContentInfo(Encoding.UTF8.GetBytes(Convert.ToString(balance)));
                CmsRecipient cmsRecipient = new CmsRecipient(serverCertificate);
                EnvelopedCms enveloped = new EnvelopedCms(contentToEncrypt);

                enveloped.Encrypt(cmsRecipient);
                byte[] pkcsEncrypted = enveloped.Encode();
                #endregion

                #region Sign Data
                ContentInfo contentToSign = new ContentInfo(pkcsEncrypted);
                CmsSigner cmsSigner = new CmsSigner(clientCertificate);
                SignedCms signed = new SignedCms(contentToSign);

                signedCms.ComputeSignature(cmsSigner);
                byte[] pkcsSigned = signedCms.Encode();
                #endregion

                string pkcs7Base = Convert.ToBase64String(pkcsSigned);

                return new BalanceResponse
                {
                    PKCS7Base64Balance = pkcs7Base,
                    StatusCode = StatusCode.OK,
                    Message = "Success"
                };
            }
            return new BalanceResponse { StatusCode = StatusCode.CryptograficError, Message = "InSucess" };
           
        }
    }

}
