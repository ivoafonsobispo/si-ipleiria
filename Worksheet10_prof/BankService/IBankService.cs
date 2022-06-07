using System.Runtime.Serialization;
using System.ServiceModel;

namespace AuthService
{
    [ServiceContract]
    public interface IBankService
    {
        [OperationContract]
        BalanceResponse GetBalance(string pkcs7Base64);
    }

}
