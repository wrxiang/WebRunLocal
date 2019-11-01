using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebRunLocal.dto
{
    /// <summary>
    /// 入参DTO
    /// </summary>
    [DataContract]
    public class InputDTO
    {
        //调用类型，1:调用DLL,2:调用EXE
        [DataMember]
        private int TYPE;

        public int type
        {
            get { return TYPE; }
            set { TYPE = value; }
        }

        //本地程序路径（相对路径）
        [DataMember]
        private string PATH;

        public string path
        {
            get { return PATH; }
            set { PATH = value; }
        }

        //调用方法名（调用DLL时使用）
        [DataMember]
        private string METHOD;

        public string method
        {
            get { return METHOD; }
            set { METHOD = value; }
        }

        //返回结果值类型（调用DLL时使用）
        [DataMember]
        private string RETRUN_TYPE;

        public string returnType
        {
            get { return RETRUN_TYPE; }
            set { RETRUN_TYPE = value; }
        }

        //调用本地程序插入的参数对象
        [DataMember]
        private List<ParamDTo> PARAM;

        internal List<ParamDTo> param
        {
            get { return PARAM; }
            set { PARAM = value; }
        }
    }
}
