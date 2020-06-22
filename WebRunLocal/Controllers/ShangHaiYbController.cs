using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Http;
using WebRunLocal.Filters;
using WebRunLocal.Utils;

namespace WebRunLocal.Controllers
{
    /// <summary>
    /// 上海医保五期动态库
    /// </summary>
    [RoutePrefix("api/shanghaiyb")]
    public class ShangHaiYbController : ApiController
    {

        [DllImport(@"SendRcv4.dll", EntryPoint = "SendRcv4")]
        public static extern IntPtr SendRcv4(IntPtr startParams, IntPtr input, IntPtr outstr);

        [Route("send")]
        [HttpPost]
        [ActionFilter]
        public HttpResponseMessage SendRcv4([FromBody]object message)
        {
            byte[] startParamsBuf = Encoding.Default.GetBytes("12345678");
            IntPtr startParamsPtr = Marshal.AllocHGlobal(startParamsBuf.Length);
            Marshal.Copy(startParamsBuf, 0, startParamsPtr, startParamsBuf.Length);

            byte[] inputBuf = Encoding.Default.GetBytes(message.ToString());
            IntPtr inputPtr = Marshal.AllocHGlobal(inputBuf.Length);
            Marshal.Copy(inputBuf, 0, inputPtr, inputBuf.Length);

            byte[] outputBuf = new byte[0x102400];
            IntPtr outputPtr = Marshal.AllocHGlobal(outputBuf.Length);
            Marshal.Copy(outputBuf, 0, outputPtr, outputBuf.Length);

            string result;
            string serverRootDir = Directory.GetCurrentDirectory();
            try
            {
                //医保dll还会调用其他DLL,需要改变当前进程的目录位置,解决必须将动态库放在根目录下的情况
                Directory.SetCurrentDirectory(Path.Combine(serverRootDir, @"Plugins\Shanghaiyb"));

                SendRcv4(startParamsPtr, inputPtr, outputPtr);
                Marshal.Copy(outputPtr, outputBuf, 0, outputBuf.Length);
                result = Encoding.Default.GetString(outputBuf);

                result = result.Replace("\0",string.Empty);
            }
            catch (Exception e)
            {
                LoggerHelper.WriteLog("调用上海医保动态库异常", e);

                JObject jObject = new JObject
                {
                    ["xxfhm"] = "500",
                    ["fhxx"] = "调用医保动态库失败,原因:" + e.Message
                };

                result = jObject.ToString();
            }
            finally 
            {
                Directory.SetCurrentDirectory(serverRootDir);
                Marshal.FreeHGlobal(startParamsPtr);
                Marshal.FreeHGlobal(inputPtr);
                Marshal.FreeHGlobal(outputPtr);
            }

            HttpResponseMessage resonse = new HttpResponseMessage
            {
                Content = new StringContent(result, Encoding.UTF8, "application/json")
            };
            return resonse;
        }
    }
}
