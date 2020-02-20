using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WebRunLocal.Entity;

namespace WebRunLocal.Managers
{
    /// <summary>
    /// 动态调用Exe实现类
    /// </summary>
    class DynamicInvokeExe : IDynamicInvoke
    {
        public ReturnDTO invokeLocalApp(Entity.InputDTO inputDTO, string localAppPath)
        {
            string args = " ";
            for (int i = 0; inputDTO.param != null && i < inputDTO.param.Count; i++)
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


            ReturnDTO returnDTO = new ReturnDTO();

            return returnDTO;
        }
    }
}
