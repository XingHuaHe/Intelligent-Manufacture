using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_XRF_TecsondeSync
{
    class FileUtils
    {
        public static void SaveBinaryFile(string sFileName, byte[] labelContent)//保存二进制文件
        {
            FileStream fs = null;
            BinaryWriter binWriter = null;
            try
            {
                fs = new FileStream(sFileName, FileMode.OpenOrCreate);
                binWriter = new BinaryWriter(fs);

                binWriter.Write(labelContent, 0, labelContent.Length);
            }
            catch (Exception)
            {
            }
            finally
            {
                try
                {
                    binWriter.Close();
                    fs.Close(); 
                }
                catch (Exception)
                { 
                }
            }
        }
        public static byte[] ReadBinaryFile(string sFileName)
        {
            byte[] bBuffer = null;
            FileStream fs = null;
            BinaryReader binReader = null;
            try
            {
                fs = new FileStream(sFileName, FileMode.Open);
                binReader = new BinaryReader(fs);

                bBuffer = new byte[fs.Length];
                binReader.Read(bBuffer, 0, (int)fs.Length);

            }
            catch (Exception)
            {
            }
            finally
            {
                try
                {
                    binReader.Close();
                    fs.Close();
                }
                catch (Exception)
                {
                }
            }
            return bBuffer;
        }

        // 选择文件：
        
        public static string SelectFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            string file = null;
            if (dialog.ShowDialog() == true)
            {
                file = dialog.FileName;
            }
            return file;
        }

        public static void WriteFile(string path, string content)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Append);
                //获得字节数组
                byte[] data = System.Text.Encoding.Default.GetBytes(content);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
               
            }
            catch (Exception)
            {

            }
            finally
            {
                try
                {
                    fs.Flush();
                    fs.Close();
                }
                catch (Exception)
                {
                }
            }
            
        }

        public static string ReadFile(string path)
        {
            StreamReader sr = null;
            String line = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                sr = new StreamReader(path, Encoding.Default);
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line.ToString());
                    sb.Append(line.ToString());
                }
                return sb.ToString();
            }
            catch (Exception)
            {
            }
            finally
            {
                try
                {
                    sr.Close();
                }
                catch (Exception)
                {
                }
            }
            return null;
        }

        public static byte[] ReadFileSeek(string path, int index, long size)
        {
            byte[] result = null;
            long length = (long)index * (long)size + size;
            FileStream file = null;
            try
            {
                file = new FileStream(path, FileMode.Open);
                if (length > file.Length)
                    result = new byte[file.Length - ((long)index * (long)size)];
                else
                    result = new byte[size];
                file.Seek((long)index * (long)size, SeekOrigin.Begin);
                //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,
                //它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
                file.Read(result, 0, result.Length);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                try
                {
                    file.Close();
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        public static void WriteFileSeek(string filename, byte[] bytes)
        {
            FileStream file = null;
            try
            {
                //long index = 3 * 1024;
                //byte[] bytes = Encoding.UTF8.GetBytes(filecontent);
                //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(filecontent);
                //Console.WriteLine("数组长度："+bytes.Length);
                file = new FileStream(filename, FileMode.OpenOrCreate);
                long currentIndex = file.Seek(file.Length, SeekOrigin.Begin);
                file.Write(bytes, 0, bytes.Length);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                try
                {
                    file.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        public static void DeleteFile(string fileName)
        {
            if (fileName == null) return;
            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete(); //删除单个文件
            }
        }

        // 选择路径
        /*public static string SelectPath()
        {
            string path = string.Empty;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            return path;
        }*/

        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        //throw new Exception("创建目标目录失败：" + ex.Message);
                        ex.ToString();
                        return;
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
            else
            {
                //throw new DirectoryNotFoundException("源目录不存在！");
                return;
            }
        }

    }
}
