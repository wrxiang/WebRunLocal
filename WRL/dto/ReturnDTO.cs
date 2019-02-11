using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WRL.dto
{
    [DataContract]
    class ReturnDTO
    {
        //本地程序返回标志
        [DataMember]
        private object CODE = string.Empty;

        public object code
        {
            get { return CODE; }
            set { CODE = value; }
        }

        //本地程序返回结果值
        [DataMember]
        private List<string> VALUES = new List<string>();

        public List<string> values
        {
            get { return VALUES; }
            set { VALUES = value; }
        }



    }
}
