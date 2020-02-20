using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using WebRunLocal.Entity;
using WebRunLocal.Utils;

namespace WebRunLocal.Managers
{
    class RequestHandler
    {

        /// <summary>
        /// 根据入参请求处理业务
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public OutputDTO BusinessHandler(string message) 
        {
            InputDTO inputDTO;
            OutputDTO outputDTO = new OutputDTO();
            try
            {
                inputDTO = JSONUtil.Deserialize<InputDTO>(message);
            }
            catch (Exception e)
            {
                LoggerHelper.WriteLog("JSON反序列化失败", e);

                outputDTO.code = PublicValue.ResultCode.Erroe;
                outputDTO.msg = "【json格式不正确】";

                return outputDTO;
            }

            string serverRootDir = Directory.GetCurrentDirectory();
            string localAppPath = Path.Combine(serverRootDir, inputDTO.path);

            //检查本地程序是否存在
            if (!File.Exists(localAppPath))
            {
                outputDTO.code = PublicValue.ResultCode.Erroe;
                outputDTO.msg = "【所调用程序不存在，请检查Plugin目录下是否存在】";

                return outputDTO;
            }

            //根据参数实例化调用程序接口
            IDynamicInvoke dynamicInvoke;
            if (inputDTO.type == (int)PublicValue.InvokeType.DLL)
            {
                dynamicInvoke = new DynamicInvokeDLL();
            }
            else if (inputDTO.type == (int)PublicValue.InvokeType.EXE)
            {
                dynamicInvoke = new DynamicInvokeExe();
            }
            else
            {
                outputDTO.code = PublicValue.ResultCode.Erroe;
                outputDTO.msg = "【调用类型不正确，请检查，TYPE = " + inputDTO.type + "】";
                return outputDTO;
            }

            try
            {
                ReturnDTO resultDTO = dynamicInvoke.invokeLocalApp(inputDTO, localAppPath);
                outputDTO.returns = resultDTO;

                return outputDTO;
            }
            finally
            {
                Directory.SetCurrentDirectory(serverRootDir);
            }
        }
    }
}
