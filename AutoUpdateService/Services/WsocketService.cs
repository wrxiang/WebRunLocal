using Fleck;
using System.Collections.Generic;
using System.Linq;

namespace AutoUpdateService.Services
{
    class WsocketService
    {
        public static List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();

        /// <summary>
        /// 启动WebSocket客户端
        /// </summary>
        /// <param name="lisenerPort"></param>
        public static void StartWebSocketService(string lisenerPort)
        {
            var server = new WebSocketServer("ws://0.0.0.0:" + lisenerPort);

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    allSockets.Add(socket);
                };

                socket.OnClose = () =>
                {
                    allSockets.Remove(socket);
                };

                socket.OnMessage = message =>
                {
                    allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                };

                socket.OnError = exception =>
                {
                    allSockets.Remove(socket);
                };
            });
        }


    }
}
