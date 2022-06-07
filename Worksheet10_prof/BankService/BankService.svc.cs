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
            #region Receive From Server
            SignedCms clientSignedCms = new SignedCms();

            clientSignedCms.Decode(Convert.FromBase64String(pkcs7Base64));

            try
            {
                clientSignedCms.CheckSignature(false);
            } catch
            {
                return new BalanceResponse
                {
                    StatusCode = StatusCode.AuthenticationError,
                    Message = "Invalid Signature",
                    PKCS7Base64Balance = null
                };
            }

            X509Certificate2 clientCertificate = clientSignedCms.Certificates[0];
            #endregion

            string thumbprint = clientCertificate.Thumbprint.ToLower();

            int userID = SqlServerHelper.UserExists(thumbprint);
            if (userID == 0)
            {
                return new BalanceResponse
                {
                    StatusCode = StatusCode.AuthenticationError,
                    Message = "User Not Found",
                    PKCS7Base64Balance = null
                };
            }

            // Authenticated

            int accountID = BitConverter.ToInt32(clientSignedCms.ContentInfo.Content, 0);
            User user = SqlServerHelper.GetUser(userID);

            if (user.AccountID != accountID)
            {
                return new BalanceResponse
                {
                    StatusCode = StatusCode.AuthenticationError,
                    Message = "Not the Right User",
                    PKCS7Base64Balance = null
                };
            }
            double balance = SqlServerHelper.GetBalance(accountID);
            if (balance == -1)
            {
                return new BalanceResponse
                {
                    StatusCode = StatusCode.DatabaseError,
                    Message = "Balance Not Found For AccountID",
                    PKCS7Base64Balance = null
                };
            }

            string certPath = AppDomain.CurrentDomain.BaseDirectory + "si.cert.b.pfx";
            using (X509Certificate2 serverCertificate = new X509Certificate2(certPath, "ei.si"))
            {
                ContentInfo signatureContentInfo = new ContentInfo(BitConverter.GetBytes(balance));
                SignedCms serverSignedCms = new SignedCms(signatureContentInfo);

                CmsSigner serverCmsSigner = new CmsSigner(serverCertificate);
                serverSignedCms.ComputeSignature(serverCmsSigner);

                byte[] signaturePkcs7 = serverSignedCms.Encode();

                ContentInfo encryptionContentInfo = new ContentInfo(signaturePkcs7);
                EnvelopedCms envelopedCms = new EnvelopedCms(encryptionContentInfo);

                CmsRecipient cmsRecipient = new CmsRecipient(clientCertificate);

                envelopedCms.Encrypt(cmsRecipient);

                byte[] pkcs7Encryption = envelopedCms.Encode();

                return new BalanceResponse
                {
                    StatusCode = StatusCode.OK,
                    Message = "OK",
                    PKCS7Base64Balance = Convert.ToBase64String(pkcs7Encryption)
                };
            }
        }
    }

}
