using AutoUpdateClient.Utils;
using SharpCompress.Archives;
using SharpCompress.Readers;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AutoUpdateClient.Managers
{
    public class AutoUpdateManager
    {
        public readonly string exeName = "WRL.exe";
        private readonly string tempPath = Path.Combine(Application.StartupPath, "temp"); 

        public AutoUpdateManager()
        {
            DirectoryInfo fi = new DirectoryInfo(tempPath);
            if (!fi.Exists)
            {
                fi.Create();
            }
        }


        /// <summary>
        /// 程序静默更新
        /// </summary>
        public void SilenceUpdate(string updateFileUri)
        {
            string[] pathSplitArray = updateFileUri.Split('\\');
            string fileName = pathSplitArray[pathSplitArray.Length - 1];
            string packageFilePath = Path.Combine(tempPath, fileName);

            //下载更新目录的压缩文件
            WebClient webClient = new WebClient();
            Uri downloadUri = new Uri(updateFileUri);
            webClient.DownloadFile(downloadUri, packageFilePath);

            //解压并删除压缩文件
            UnPackageFile(packageFilePath, tempPath);
            File.Delete(packageFilePath);

            //停止程序
            StopExe(exeName);

            //覆盖文件并删除下载的更新文件
            FileUtil.MoveAndCoverDirectory(tempPath, Application.StartupPath);

            //启动WRL程序
            StartExe(Path.Combine(Application.StartupPath, exeName));
        }


        /// <summary>
        /// WebRunLocal版本检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void VersionChecked(object sender=null, System.Timers.ElapsedEventArgs e=null)
        {
           // WsocketClient.CheckWebSocketStatuc("check version");
        }


        /// <summary>
        /// 解压文件至指定目录
        /// </summary>
        /// <param name="targetFile"></param>
        /// <param name="zipFile"></param>
        public void UnPackageFile(string targetFile, string zipFile) 
        {
            var archive = ArchiveFactory.Open(targetFile);
            ExtractionOptions extractionOptions = new ExtractionOptions();
            extractionOptions.ExtractFullPath = true;
            extractionOptions.Overwrite = true;

            foreach (var entry in archive.Entries)
            {
                if (!entry.IsDirectory)
                {
                    entry.WriteToDirectory(zipFile, extractionOptions);
                }
            }
        }

        /// <summary> 
        /// 启动程序
        /// </summary>
        /// <param name="exePath"></param>
        public void StartExe(string exePath)
        {
            if (File.Exists(exePath))
            {
                Process pro = new Process();
                pro.StartInfo.FileName = exePath;
                pro.Start();
            }
        }

        /// <summary>
        /// 停止程序
        /// </summary>
        /// <param name="exeName"></param>
        public void StopExe(string exeName)
        {
            Process[] exeArray = Process.GetProcessesByName(exeName.Split('.')[0]);
            foreach (Process process in exeArray)
            {
                process.Kill();
            }
        }
    }
}
