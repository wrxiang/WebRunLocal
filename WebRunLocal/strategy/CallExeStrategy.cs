using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using WebRunLocal.dto;

namespace WebRunLocal.strategy
{
    //调用Exe策略实现
    class CallExeStrategy : IQuoteStrategy
    {

        public void invokeLocalApp(InputDTO inputDTO, OutputDTO outputDTO, String localAppPath)
        {
            string args = " ";
            for (int i = 0; inputDTO.param!=null && i < inputDTO.param.Count; i++)
            {
                args += inputDTO.param[i].value + " ";
            }

            Process pro = new Process();
            pro.StartInfo.FileName = localAppPath;
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardInput = true;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.CreateNoWindow = true;
            pro.StartInfo.Arguments = args;
            pro.Start();//启动程序

            outputDTO.code = ResultCode.Success;

           
        }
    }
}
