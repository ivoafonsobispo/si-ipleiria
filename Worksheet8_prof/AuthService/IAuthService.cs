using System.Runtime.Serialization;
using System.ServiceModel;

namespace AuthService
{
    [ServiceContract]
    public interface IAuthService
    {
        /// <summary>
        /// Verifica se é possível aceder à BD
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string VerifyAcessToBD();

        [OperationContract]
        UsersMessage GetUsers(string login, string password);

        [OperationContract]
        UsersMessage GetUsersByCertificate(string pkcs7Base64);

        [OperationContract]
        DescriptionMessage GetUserDescription(string login, string password, string loginUser);


        [OperationContract]
        ServiceMetadata UpdateUserDescription(string login, string password, string description);

        [OperationContract]
        ServiceMetadata UpdateUserDescriptionByCertificate(string pkcs7Base64);

        [OperationContract]
        string[] GetHashs();

        [OperationContract]
        string Test();

        [OperationContract]
        ServiceMetadata UpdateUserPasswordByCertificate(string pkcs7Base64, string login);
    }

}
