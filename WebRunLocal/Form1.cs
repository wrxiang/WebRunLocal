using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebRunLocal.dto;
using WebRunLocal.utils;
using WebRunLocal.strategy;

namespace WebRunLocal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        //日志文件路径
        private string logFilePath { get { return Path.Combine(Directory.GetCurrentDirectory(), "Log"); } }
        //http监听
        private static HttpListener httpListener = new HttpListener();
        //http监听地址
        private string httpListenerAddress = "http://127.0.0.1:{0}/WebRunLocal/";
        //是否打印输入输出数据
        private bool pramaterLoggerPrint { get { return bool.Parse(ConfigurationManager.AppSettings["PramaterLoggerPrint"]); } }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.mainNotifyIcon.Visible = true;
            this.Hide();

            DirectoryInfo fi = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Plugins"));
            if (!fi.Exists)
            {
                fi.Create();
            }


            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);//可以获取IPv4的地址
            IPAddress localaddrList = localhost.AddressList[0];

            //监听端口
            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"].ToString();


            //设置软件自动启动
            AutoStartByRegistry.setMeStart(bool.Parse(ConfigurationManager.AppSettings["AutoStart"]));

            //创建桌面快捷方式
            if (bool.Parse(ConfigurationManager.AppSettings["DesktopLnk"].ToString()))
            {
                LnkUtil.createDesktopQuick();
            }


            httpListener.Prefixes.Add(string.Format(httpListenerAddress, lisenerPort));
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

        /// <summary>
        /// 窗体的关闭方法，实现关闭后最小化到托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 注意判断关闭事件reason来源于窗体按钮，否则用菜单退出时无法退出!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //取消"关闭窗口"事件
                e.Cancel = true; // 取消关闭窗体 

                //使关闭时窗口向右下角缩小的效果
                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                this.Hide();
                return;
            }
        }



        /// <summary>
        /// 托盘双击操作，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainNotifyIcon_MouseDoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.mainNotifyIcon.Visible = true;
            this.Hide();
        }

        /// <summary>
        /// 还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemNormal_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？退出后所有插件均不能使用!", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.mainNotifyIcon.Visible = false;
                this.Close();
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        

    }
}
