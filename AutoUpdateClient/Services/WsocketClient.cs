using AutoUpdateClient.Managers;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using WebSocket4Net;

namespace AutoUpdateClient.Services
{
    public class WsocketClient
    {
        private static WebSocket webSocket4Net = null;
        private static readonly System.Timers.Timer timer = new System.Timers.Timer();

        /// <summary>
        /// 开启WebSocket
        /// </summary>
        public static void StartWebSocket(string serviceAddr)
        {
            webSocket4Net = new WebSocket(serviceAddr);
            webSocket4Net.MessageReceived += WebSocket4Net_MessageReceived;
            webSocket4Net.Closed += WebSocket4Net_Closed;
            webSocket4Net.Open();

            //设置心跳检测
            timer.Interval = 5 * 60000;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(CheckWebSocketStatuc);
        }

        /// <summary>
        /// 检查WebSocket状态
        /// </summary>
        public static void CheckWebSocketStatuc(object sender = null, ElapsedEventArgs e = null)
        {
            if (webSocket4Net.State == WebSocketState.Closing || webSocket4Net.State==WebSocketState.Closed)
            {
                webSocket4Net.Open();
            }

            webSocket4Net.Send("CheckVersion");
        }


        /// <summary>
        /// 服务端消息接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WebSocket4Net_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            AutoUpdateManager autoUpdateManager = new AutoUpdateManager();
            FileVersionInfo fv = FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, autoUpdateManager.exeName));
            if (fv.FileVersion.Equals(e.Message)) 
            {
                string updateFileUri = ConfigurationManager.AppSettings["UpdateFileUri"];
                autoUpdateManager.SilenceUpdate(updateFileUri);
            }
        }

        /// <summary>
        /// WebSocket连接断开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WebSocket4Net_Closed(object sender, EventArgs e)
        {
        }

    }
}
