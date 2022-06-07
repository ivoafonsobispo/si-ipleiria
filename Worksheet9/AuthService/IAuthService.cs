using System.Runtime.Serialization;
using System.ServiceModel;

namespace AuthService
{
    [ServiceContract] // o AuthService vai ser usado como um ServiceContract
    public interface IAuthService
    {
        /// <summary>
        /// Verifica se é possível aceder à BD
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string VerifyAcessToBD();

        [OperationContract]
        UserMessage GetUsers(string login, string password);

        [OperationContract]
        User GetUser(int id);

        [OperationContract]
        ServiceMetadata UpdateUserDescription(string login, string password, string description);

        [OperationContract]
        ServiceMetadata UpdateUserDescriptionByCertificate(string pcks7Base64);

        [OperationContract]
        string[] GetHashes();

        [OperationContract]
        UserMessage GetUsersByCertificate(string pkcsBase64);

        //ServiceMetadata UpdateUserPassword(int id, string pkcs7Base64);
        [OperationContract]
        ServiceMetadata UpdateUserPassword(string pkcs7Base64, string login);
    }

}
