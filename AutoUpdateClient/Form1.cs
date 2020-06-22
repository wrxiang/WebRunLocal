using AutoUpdateClient.Services;
using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdateClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            WindowState = FormWindowState.Minimized;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //隐藏主界面，更新程序在后台运行
            Hide();

            //开启WebSocket客户端
            string serviceAddr = ConfigurationManager.AppSettings["WebSocketServiceAddr"];
            WsocketClient.StartWebSocket(serviceAddr);

            Thread.Sleep(1000);
            WsocketClient.CheckWebSocketStatuc();
        }
    }
}
