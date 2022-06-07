using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AuthService
{
    public enum StatusCode
    {
        OK,
        AuthenticationError,
        AuthorizationError,
        CryptograficError,
        DatabaseError
    }

    [DataContract]
    public class BankServiceResponse
    {
        [DataMember]
        public StatusCode StatusCode { get; set; }
        [DataMember]
        public String Message { get; set; }
    }

    [DataContract]
    public class BalanceResponse: BankServiceResponse
    {
        [DataMember]
        public String PKCS7Base64Balance { get; set; }
    }
}