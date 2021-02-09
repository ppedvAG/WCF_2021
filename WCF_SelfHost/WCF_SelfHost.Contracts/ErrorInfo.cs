using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF_SelfHost.Contracts
{
    [DataContract]
    public class ErrorInfo
    {
        [DataMember]
        public string Msg { get; set; }
    }
}
