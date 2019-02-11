using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WRL.dto
{
    /// <summary>
    /// 调用本地程序传入的参数对象
    /// </summary>
    [DataContract]
    class ParamDTo
    {
        //参数的类型（调用DLL时使用）
        [DataMember]
        private string TYPE;

        public string type
        {
            get { return TYPE; }
            set { TYPE = value; }
        }

        //参数值
        [DataMember]
        private string VALUE;

        public string value
        {
            get { return VALUE; }
            set { VALUE = value; }
        }

        //参数的传递方式（调用DLL时使用，0:值传递，1:值需要返回）
        [DataMember]
        private string MODE;

        public string mode
        {
            get { return MODE; }
            set { MODE = value; }
        }

    }
}
