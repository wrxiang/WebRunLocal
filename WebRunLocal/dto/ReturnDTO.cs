using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebRunLocal.dto
{
    [DataContract]
    class ReturnDTO
    {
        //本地程序返回标志
        [DataMember]
        private object RESULT = string.Empty;

        public object result
        {
            get { return RESULT; }
            set { RESULT = value; }
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
