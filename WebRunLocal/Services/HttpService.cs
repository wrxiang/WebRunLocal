using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebRunLocal.Services
{
    /// <summary>
    /// HTTP service.
    /// </summary>
    public class HttpService : IDisposable
    {
        /// <summary>
        /// HTTP server's listening port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// HTTP self hosting.
        /// </summary>
        private readonly HttpSelfHostServer _server;

        /// <summary>
        /// HTTP server.
        /// </summary>
        /// <param name="port">Listening port.</param>
        public HttpService(int port)
        {
            this.Port = port;

            var config = new HttpSelfHostConfiguration($"http://0.0.0.0:{this.Port}");

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}");

            _server = new HttpSelfHostServer(config);
        }

        #region HTTP Service

        /// <summary>
        /// start HTTP server.
        /// </summary>
        public Task StartHttpServer()
        {
            return _server.OpenAsync();
        }

        /// <summary>
        /// Close HTTP server.
        /// </summary>
        public Task CloseHttpServer()
        {
            return _server.CloseAsync();
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}
