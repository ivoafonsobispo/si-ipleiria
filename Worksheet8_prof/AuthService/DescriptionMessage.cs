﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AuthService
{
    [DataContract]
    public class DescriptionMessage
    {
        [DataMember]
        public ServiceMetadata Metadata { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}