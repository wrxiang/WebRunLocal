using System;
using System.IO;
using System.Linq;

namespace AutoUpdateClient.Utils
{
    public class FileUtil
    {
        /// <summary>
        /// 删除指定文件夹下当前日期前days天文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="days"></param>
        public static void DeleteFiles(string dir, int days)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();
            DirectoryInfo[] directs = d.GetDirectories();//文件夹

            var now = DateTime.Now;
            foreach (FileInfo file in files)
            {
                var createTime = File.GetCreationTime(file.FullName);
                var elapsedTicks = now.Ticks - createTime.Ticks;
                var elapsedSpan = new TimeSpan(elapsedTicks);
                if (elapsedSpan.TotalDays > days)
                {
                    file.Delete();
                }
            }
            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo folder in directs)
            {
                var createTime = File.GetLastWriteTime(folder.FullName);
                var elapsedTicks = now.Ticks - createTime.Ticks;
                var elapsedSpan = new TimeSpan(elapsedTicks);
                if (elapsedSpan.TotalDays > days)
                {
                    Directory.Delete(folder.FullName, true);
                }
                else
                {
                    DeleteFiles(folder.FullName, days);
                }
            }
        }


        /// <summary>
        /// 移动文件并覆盖目标文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void MoveAndCoverDirectory(string source, string target)
        {
            var sourcePath = source.TrimEnd('\\', ' ');
            var targetPath = target.TrimEnd('\\', ' ');
            var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories)
                                 .GroupBy(s => Path.GetDirectoryName(s));

            foreach (var folder in files)
            {
                var targetFolder = folder.Key.Replace(sourcePath, targetPath);
                Directory.CreateDirectory(targetFolder);
                foreach (var file in folder)
                {
                    var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
                    if (File.Exists(targetFile))
                    {
                        File.Delete(targetFile);
                    }
                    File.Move(file, targetFile);
                }
            }
            Directory.Delete(source, true);
        }

    }
}
