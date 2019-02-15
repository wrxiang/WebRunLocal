using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using WRL.dto;
using WRL.strategy;
using WRL.utils;

namespace WRL
{
    public partial class WRLService : ServiceBase
    {
        //服务运行状态
        private bool isRunning = false;
        //服务安装路径
        private string serviceInstallPath;
        //日志文件路径
        private string serviceLogFilePath;
        //http监听
        private static HttpListener httpListener = new HttpListener();
        //http监听地址
        private string httpListenerAddress = "http://localhost:ListenerPort/WebRunLocal/";
        //是否打印输入输出数据
        private bool pramaterLoggerPrint { get { return bool.Parse(ConfigurationManager.AppSettings["PramaterLoggerPrint"]); } }


        public WRLService()
        {
            InitializeComponent();

            serviceInstallPath = getServiceInstallPath("WRL");
            serviceLogFilePath = Path.Combine(serviceInstallPath, "Log");
            httpListenerAddress = httpListenerAddress.Replace("ListenerPort", ConfigurationManager.AppSettings["ListenerPort"]);
        }

        protected override void OnStart(string[] args)
        {
            if (!isRunning)
            {
                httpListener.Prefixes.Add(httpListenerAddress);
                httpListener.Start();
                Thread ThrednHttpPostRequest = new Thread(new ThreadStart(httpRequestHandle));
                ThrednHttpPostRequest.Start();
                isRunning = true;
                LoggerManager.writeLog(serviceLogFilePath, "服务已启动……");
            }
        }

        protected override void OnStop()
        {
            if (isRunning)
            {
                httpListener.Stop();
                LoggerManager.writeLog(serviceLogFilePath, "服务已停止……");
            }
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
                        LoggerManager.writeLog(serviceLogFilePath, "服务器:【收到】" + message);
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
                        LoggerManager.writeLog(serviceLogFilePath, "【json格式不正确】,异常：" + e.ToString());

                        outputDTO.code = ResultCode.Erroe;
                        outputDTO.msg = "【json格式不正确】";
                        outputStreamToClient(httpListenerContext, outputDTO);
                        return;
                    }

                    //调用本地程序
                    QuoteContext quoteContext = new QuoteContext();
                    try
                    {
                        quoteContext.invokeLocalApp(inputDTO, outputDTO, serviceInstallPath);
                    }
                    catch (Exception e)
                    {
                        outputDTO.code = ResultCode.Erroe;
                        outputDTO.msg = "【调取插件异常】";
                        LoggerManager.writeLog(serviceLogFilePath, outputDTO.msg + e.ToString());
                    }
                    finally 
                    {
                        Directory.SetCurrentDirectory(serviceInstallPath);
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
                    LoggerManager.writeLog(serviceLogFilePath, "服务器【返回】" + rtnMsg);
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
                LoggerManager.writeLog(serviceLogFilePath, "【生成出参失败】,异常：" + e.ToString());
            }
        }

        /// <summary>
        /// 获取服务安装路径
        /// </summary>
        /// <returns></returns>
        private string getServiceInstallPath(string serviceName)
        {
            string key = @"SYSTEM\CurrentControlSet\Services\" + serviceName;
            string exePath = Registry.LocalMachine.OpenSubKey(key).GetValue("ImagePath").ToString();
            exePath = exePath.Replace("\"", string.Empty);

            FileInfo fi = new FileInfo(exePath);
            return fi.Directory.ToString();
        }
    }
}
