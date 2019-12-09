using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using WebRunLocal.dto;
using WebRunLocal.strategy;

namespace WebRunLocal.utils
{
    class HttpListenerManager
    {
        //http监听
        private HttpListener httpListener = new HttpListener();
        
        //日志文件路径
        private string logFilePath { get { return Path.Combine(Directory.GetCurrentDirectory(), "Log"); } }
        //是否打印输入输出数据
        private bool pramaterLoggerPrint { get { return bool.Parse(ConfigurationManager.AppSettings["PramaterLoggerPrint"]); } }

        //http实际监听地址
        private string httpListenerAddress = "http://+:{0}/WRL/";

        public string HttpListenerAddress
        {
            get { return httpListenerAddress; }
        }


        /// <summary>
        /// 开启Http监听并处理相关业务
        /// </summary>
        public void startHttpListener(List<string> ipList, string lisenerPort)
        {
            httpListenerAddress = string.Format(httpListenerAddress, lisenerPort);

            httpListener.Prefixes.Add(httpListenerAddress);
            httpListener.Start();
            Thread ThrednHttpPostRequest = new Thread(new ThreadStart(httpRequestHandle));
            ThrednHttpPostRequest.Start();
        }



        /// <summary>
        /// 监听Http请求
        /// </summary>
        private void httpRequestHandle()
        {
            while (true)
            {
                HttpListenerContext context = httpListener.GetContext();
                Thread threadsub = new Thread(new ParameterizedThreadStart((requestContext) =>
                {
                    HttpListenerContext httpListenerContext = (HttpListenerContext)requestContext;
                    string message = getHttpJsonData(httpListenerContext.Request);

                    if (pramaterLoggerPrint)
                    {
                        LoggerManager.writeLog(logFilePath, "服务器:【收到】" + message);
                    }

                    //JSON字符串反序列化
                    InputDTO inputDTO = null;
                    OutputDTO outputDTO = new OutputDTO();
                    try
                    {
                        inputDTO = JsonHelper.Deserialize<InputDTO>(message);
                    }
                    catch (Exception e)
                    {
                        LoggerManager.writeLog(logFilePath, "【json格式不正确】,异常：" + e.ToString());

                        outputDTO.code = ResultCode.Erroe;
                        outputDTO.msg = "【json格式不正确】";
                        outputStreamToClient(httpListenerContext, outputDTO);
                        return;
                    }
                    string serverRootDir = Directory.GetCurrentDirectory();
                    string localAppPath = Path.Combine(serverRootDir, inputDTO.path);

                    //检查本地程序是否存在
                    if (!File.Exists(localAppPath))
                    {
                        outputDTO.code = ResultCode.Erroe;
                        outputDTO.msg = "【插件不存在，检查Plugin目录下是否存在】";
                    }
                    else
                    {
                        //调用本地程序
                        QuoteContext quoteContext = new QuoteContext();
                        try
                        {
                            quoteContext.invokeLocalApp(inputDTO, outputDTO, localAppPath);
                        }
                        catch (Exception e)
                        {
                            outputDTO.code = ResultCode.Erroe;
                            outputDTO.msg = "【调取插件异常】";
                            LoggerManager.writeLog(logFilePath, outputDTO.msg + e.ToString());
                        }
                        finally
                        {
                            Directory.SetCurrentDirectory(serverRootDir);
                        }
                    }

                    outputStreamToClient(httpListenerContext, outputDTO);
                }));
                threadsub.Start(context);
            }
        }



        /// <summary>
        /// 获取Http请求入参字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string getHttpJsonData(HttpListenerRequest request)
        {
            Stream jsonData = request.InputStream;
            StreamReader sRead = new StreamReader(jsonData);
            string content = sRead.ReadToEnd();
            sRead.Close();

            return content;
        }

        /// <summary>
        /// Http向客户端输出返回值
        /// </summary>
        /// <param name="httpListenerContext"></param>
        /// <param name="outputDTO"></param>
        private void outputStreamToClient(HttpListenerContext httpListenerContext, OutputDTO outputDTO)
        {

            try
            {
                string rtnMsg = JsonHelper.Serialize<OutputDTO>(outputDTO);
                if (pramaterLoggerPrint)
                {
                    LoggerManager.writeLog(logFilePath, "服务器【返回】" + rtnMsg);
                }

                httpListenerContext.Response.StatusCode = 200;
                httpListenerContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                httpListenerContext.Response.ContentType = "application/json";
                httpListenerContext.Response.ContentEncoding = Encoding.UTF8;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(rtnMsg);
                httpListenerContext.Response.ContentLength64 = buffer.Length;
                var output = httpListenerContext.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            catch (Exception e)
            {
                LoggerManager.writeLog(logFilePath, "【生成出参失败】,异常：" + e.ToString());
            }
        }

        /// <summary>
        /// 构造返回内容
        /// </summary>
        /// <param name="retValues"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private string createRtnMessage(List<object> retValues, object result)
        {
            JObject jobject = new JObject();
            jobject["CODE"] = (int)ResultCode.Success;
            jobject["MSG"] = string.Empty;

            JObject rtnValue = new JObject();
            JArray jarray = new JArray();
            rtnValue["CODE"] = result == null ? string.Empty : result.ToString();

            for (int i = 0; i < retValues.Count; i++)
            {
                jarray.Add(retValues[i].ToString());
            }

            rtnValue["VALUES"] = jarray;
            jobject["RETURN"] = rtnValue;
            return jobject.ToString();
        }

    }
}
