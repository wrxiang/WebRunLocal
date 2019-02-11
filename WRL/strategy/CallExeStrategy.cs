using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WRL.dto;

namespace WRL.strategy
{
    //调用Exe策略实现
    class CallExeStrategy : IQuoteStrategy
    {

        public void invokeLocalApp(InputDTO inputDTO, OutputDTO outputDTO, string serviceInstallPath)
        {
            string localAppPath = Path.Combine(serviceInstallPath, inputDTO.path);
            //判断本地程序是否存在
            if (!File.Exists(localAppPath))
            {
                outputDTO.code = ResultCode.Erroe;
                outputDTO.msg = "【第三方程序集不存在】";

                return;
            }

            string args = string.Empty;
            for (int i = 0; i < inputDTO.param.Count; i++ )
            {
                args += inputDTO.param[i].value + " ";
            }


            Interop.CreateProcess(localAppPath, args);

            outputDTO.code = ResultCode.Success;
        }
    }
}
