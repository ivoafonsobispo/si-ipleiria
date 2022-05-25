using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AuthService
{
    [DataContract]
    public class UsersMessage
    {
        [DataMember]
        public ServiceMetadata Metadata { get; set; }

        [DataMember]
        public User[] Users { get; set; }
    }
}