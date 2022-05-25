using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AuthService
{
    [DataContract]
    public class User
    {
        // Definir DataMember para todos os campos que queremos enviar, não colocar na password
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Name { get; set; }
        public string Password { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Role { get; set; } //Adicionado 

    }
}