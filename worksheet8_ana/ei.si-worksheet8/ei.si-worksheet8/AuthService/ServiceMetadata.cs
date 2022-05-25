using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AuthService
{
    [DataContract]
    public class ServiceMetadata
    {
        [DataMember]
        public bool Status { get; set; }

        [DataMember]
        public string Message { get; set; }

    }
}