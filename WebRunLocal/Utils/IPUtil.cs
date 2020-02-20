using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;

namespace WebRunLocal.Utils
{
    class IPUtil
    {
        /// <summary>
        /// 获取本地IP，排除掉VMware等虚拟网卡
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIpByLocal()
        {
            List<string> listIP = new List<string>();
            ManagementClass mcNetworkAdapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc_NetworkAdapterConfig = mcNetworkAdapterConfig.GetInstances();
            foreach (ManagementObject mo in moc_NetworkAdapterConfig)
            {
                string mServiceName = mo["ServiceName"] as string;

                //过滤非真实的网卡
                if (!(bool)mo["IPEnabled"])
                { continue; }
                if (mServiceName.ToLower().Contains("vmnetadapter")
                 || mServiceName.ToLower().Contains("ppoe")
                 || mServiceName.ToLower().Contains("bthpan")
                 || mServiceName.ToLower().Contains("vpn")
                 || mServiceName.ToLower().Contains("ndisip")
                 || mServiceName.ToLower().Contains("sinforvnic"))
                {
                    continue;
                }


                string[] mIPAddress = mo["IPAddress"] as string[];

                if (mIPAddress != null)
                {
                    foreach (string ip in mIPAddress)
                    {
                        if (!ip.Contains(":") && ip != "0.0.0.0")
                        {
                            listIP.Add(ip);
                        }
                    }
                }
                mo.Dispose();
            }
            return listIP;
        }

        /// <summary>
        /// 获取本地连接IP地址 包含VMware等虚拟网卡IP
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIpByLocalAll()
        {
            List<string> listIP = new List<string>();
            IPAddress[] dnsIps = Dns.GetHostAddresses(Dns.GetHostName());
            for (int i = 0; i < dnsIps.Length; i++)
            {
                if (dnsIps[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    listIP.Add(dnsIps[i].ToString());
                }
            }

            return listIP;
        }
    }
}
