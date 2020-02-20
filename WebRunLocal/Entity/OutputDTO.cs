using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebRunLocal.Entity
{
    /// <summary>
    /// 出参DTO
    /// </summary>
    [DataContract]
    class OutputDTO
    {
        //WRL服务调用标志，0:成功， -1:失败
        [DataMember]
        private int CODE ;

        public PublicValue.ResultCode code
        {
            get { return (PublicValue.ResultCode)CODE; }
            set { CODE = (int)value; }
        }

        //WRL服务调用返回信息
        [DataMember]
        private string MSG = string.Empty;

        public string msg
        {
            get { return MSG; }
            set { MSG = value; }
        }

        //本地程序返回结果
        [DataMember]
        private ReturnDTO RETURN = new ReturnDTO();

        internal ReturnDTO returns
        {
            get { return RETURN; }
            set { RETURN = value; }
        }
    }
}
