using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebRunLocal.Entity
{
    class PublicValue
    {

        public static string httpListenerAddress = string.Empty;


        /// <summary>
        /// 返回编码枚举
        /// </summary>
        public enum ResultCode
        {
            Success = 0,
            Erroe = -1
        }

        /// <summary>
        /// 调用类型枚举
        /// </summary>
        public enum InvokeType
        { 
            DLL = 1,
            EXE = 2
        }


        /// <summary>
        /// 参数传递方式枚举 ,ByValue 表示值传递 ,ByRef 表示址传递,ByReturn 表示需要将值返回
        /// </summary>
        public enum ModePass
        {
            ByValue = 0,
            ByReturn = 1,
            ByRef = 2
        }
    }
}
