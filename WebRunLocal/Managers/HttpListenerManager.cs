using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using WebRunLocal.Entity;
using WebRunLocal.Utils;

namespace WebRunLocal.Managers
{
    class HttpListenerManager
    {
        //http监听
        private HttpListener httpListener = new HttpListener();
        //http实际监听地址
        private string httpListenerAddress = "http://+:{0}/WRL/";
        private bool pramaterLoggerPrint = bool.Parse(ConfigurationManager.AppSettings["PramaterLoggerPrint"]);


        /// <summary>
        /// 开启Http监听并处理相关业务
        /// </summary>
        public void startHttpListener(string lisenerPort)
        {
            httpListenerAddress = string.Format(httpListenerAddress, lisenerPort);
            PublicValue.httpListenerAddress = httpListenerAddress;

            httpListener.Prefixes.Add(httpListenerAddress);
            httpListener.Start();
            Thread ThrednHttpPostRequest = new Thread(new ThreadStart(httpRequestHandle));
            ThrednHttpPostRequest.IsBackground = true;
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

                    string message = getHttpParameters(httpListenerContext.Request);
                    if (pramaterLoggerPrint)
                    {
                        LoggerHelper.WriteLog("入参：" + message);
                    }

                    OutputDTO outputDTO = new OutputDTO();
                    //调用业务处理类处理业务请求
                    RequestHandler requesthandler = new RequestHandler();
                    try
                    {
                        outputDTO = requesthandler.BusinessHandler(message);
                    }
                    catch(Exception e)
                    {
                        outputDTO = new OutputDTO();
                        outputDTO.code = PublicValue.ResultCode.Erroe;
                        outputDTO.msg = "【调取插件异常】";
                        LoggerHelper.WriteLog("调取插件异常", e);
                    }

                    //返回调用结果
                    string rtnMsg = JSONUtil.Serialize<OutputDTO>(outputDTO);
                    if (pramaterLoggerPrint)
                    {
                        LoggerHelper.WriteLog("出参：" + rtnMsg);
                    }

                    outputStreamToClient(httpListenerContext, rtnMsg);
                    
                }));
                threadsub.Start(context);
            }
        }

        /// <summary>
        /// 获取http请求参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string getHttpParameters(HttpListenerRequest request) 
        {
            Stream dataStream = null; ;
            StreamReader streamReader = null;

            try
            {
                dataStream = request.InputStream;
                streamReader = new StreamReader(dataStream);
                string prameterString = streamReader.ReadToEnd();

                return prameterString;
            }
            finally
            {
                streamReader.Close();
                dataStream.Close();
            }
        }

        
        /// <summary>
        /// 向客户端输出返回值
        /// </summary>
        /// <param name="httpListenerContext"></param>
        /// <param name="outputDTO"></param>
        private void outputStreamToClient(HttpListenerContext httpListenerContext, string rtnMsg)
        {
            httpListenerContext.Response.StatusCode = 200;
            httpListenerContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            httpListenerContext.Response.ContentType = "application/json";
            httpListenerContext.Response.ContentEncoding = Encoding.UTF8;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(rtnMsg);
            httpListenerContext.Response.ContentLength64 = buffer.Length;
            Stream outputStream = null;
            try
            {
                outputStream = httpListenerContext.Response.OutputStream;
                outputStream.Write(buffer, 0, buffer.Length);
            }
            finally 
            {
                outputStream.Close();
            }
        }

    }
}
