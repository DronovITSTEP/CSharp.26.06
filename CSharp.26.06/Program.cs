using System;
using System.IO;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CSharp._26._06
{
    /*
     class GarbageHelper
    {
        public void MakeGarbage()
        {
            for (int i = 0; i < 10000; i++)
            {
                Person p = new Person();
            }
        }
    }
    class Person
    {
        string _name;
        string _email;
        byte _byte;
    }

    class FinalizeExample : IDisposable
    {
        bool isDisposed = false;
        void Cleaning(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    WriteLine("Освобождение управялемых ресурсов");

                WriteLine("Освобождение неуправялемых ресурсов");
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Cleaning(true);
            GC.SuppressFinalize(this);
        }
        public void Show()
        {
            WriteLine("Show");
        }
        ~FinalizeExample()
        {
            Cleaning(false);
        }
    }
    */

    internal class Program
    {
        // FileStream
        // FileMode
        // Append
        // Create
        // CreateNew
        // Open
        // OpenOrCreate
        // Truncate
        // FileAccess
        // Write
        // Read
        // ReadWrite
        // FileShare
        // Delete
        // Inheritable
        // None
        // Read
        // ReadWrite
        // Write
        static void WriteFile(string filePath)
        {
            using (FileStream fs = new FileStream
                (filePath, FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                string str = ReadLine();
                byte[] writeBytes = Encoding.Default.GetBytes(str);
                fs.Write(writeBytes, 0, writeBytes.Length);
            }
        }
        static string ReadFile(string filePath)
        {
            using (FileStream fs = new FileStream
                (filePath, FileMode.Open,
                FileAccess.Read, FileShare.Read))
            {
                byte[] readBytes = new byte[(int)fs.Length];
                fs.Read(readBytes, 0, readBytes.Length);
                return Encoding.Default.GetString(readBytes);
            }
        }

        // StreamWriter
        // Write()
        // WriteLine()
        static void WriteStream(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    string str = ReadLine();
                    sw.WriteLine(str);
                    foreach (var s in str)
                    {
                        sw.Write(s + "  ");
                    }
                }
            }
        }
        // StreamReader
        // Read()
        // ReadBlock()
        // ReadLine()
        // ReadToEnd()

        // BaseStream
        // CurrentEncoding
        // EndOfStream
        static void ReadStream(string filePath)
        {
            using (FileStream fs = new FileStream
                (filePath, FileMode.Open))
            {
                using (StreamReader sr =
                    new StreamReader(fs, Encoding.Default))
                {
                    WriteLine(sr.ReadToEnd());
                }
            }
        }

        // BinaryWriter
        static void WriteBinary(string filePath)
        {
            using (FileStream fs = new FileStream
                (filePath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter
                    (fs, Encoding.Default))
                {
                    string str = ReadLine();
                    double pi = 3.1415926;
                    int num = 654;

                    bw.Write(str);
                    bw.Write(pi);
                    bw.Write(num);
                }
            }
        }
        // BinaryReader
        static void ReaderBinary(string filePath)
        {
            using (FileStream fs = new FileStream
                (filePath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader
                    (fs, Encoding.Default))
                {
                    WriteLine(br.ReadString());
                    WriteLine(br.ReadDouble());
                    WriteLine(br.ReadInt32());
                }
            }
        }

        // DirectoryInfo FileInfo
        static void DirInfo()
        {
            DirectoryInfo dir = new DirectoryInfo(".");
            WriteLine(dir.FullName);
            WriteFile(dir.CreationTime.ToLongDateString());

            FileInfo[] files = dir.GetFiles();
            foreach (var item in files)
            {
                WriteLine(item.Name);
            }
        }
        static void WriteFile(FileInfo f)
        {
            using (FileStream fs = f.Open(FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                string str = ReadLine();
                byte[] writeBytes = Encoding.Default.GetBytes(str);
                fs.Write(writeBytes, 0, writeBytes.Length);
            }
        }
        static string ReadFile(FileInfo f)
        {
            using (FileStream fs = f.Open
                (FileMode.Open,
                FileAccess.Read, FileShare.Read))
            {
                byte[] readBytes = new byte[(int)fs.Length];
                fs.Read(readBytes, 0, readBytes.Length);
                return Encoding.Default.GetString(readBytes);
            }
        }
        static void CreateDir(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (!dir.Exists) dir.Create();
            DirectoryInfo dir1 = dir.CreateSubdirectory("SubDir");
            WriteLine(dir1.FullName);
            FileInfo fileInfo = new FileInfo(dir1 + @"\test.bin");
            WriteFile(fileInfo);
            WriteLine(ReadFile(fileInfo));

            FileInfo[] files = dir1.GetFiles("*.bin");
            foreach (FileInfo file in files)
            {
                WriteLine(file.Name);
            }
        }

        // File Directory 
        static void WriteFile1(string filePath)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                string str = ReadLine();
                sw.WriteLine(str);
            }
        }
        static string ReadFile1(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                return sr.ReadToEnd();
            }
        }
        static void Dir(string path)
        {
            if (Directory.Exists(path))
            {
                WriteLine(Directory.GetCreationTime(path));
                foreach (string file in Directory.GetFiles(path))
                    WriteLine(file);

                Directory.Delete(path, true);
            }
        }
        static void Main(string[] args)
        {
            /*
            WriteLine($"Максимальное поколение: {GC.MaxGeneration}");
            GarbageHelper hlp = new GarbageHelper();
            WriteLine(GC.GetGeneration(hlp));
            WriteLine(GC.GetTotalMemory(false));
            hlp.MakeGarbage();
            WriteLine(GC.GetTotalMemory(false));

            GC.Collect(0);
            WriteLine(GC.GetTotalMemory(false));
            WriteLine(GC.GetGeneration(hlp));

            GC.Collect();
            WriteLine(GC.GetTotalMemory(false));
            WriteLine(GC.GetGeneration(hlp));

            FinalizeExample example = new FinalizeExample();
            try
            {
                example.Show();
            }
            finally
            {
                example.Dispose();
            }

            using (FinalizeExample finalizeExample = new FinalizeExample())
            {
                finalizeExample.Show();
            }
*/

            //string filePath = "text.txt";
            string filePath = "text.dat";
            /*WriteFile(filePath);
            WriteLine(ReadFile(filePath));*/
            //WriteStream(filePath);
            //ReadStream(filePath);
            //WriteBinary(filePath);
            //ReaderBinary(filePath);

            //DirInfo();
            //CreateDir(@"C:\Test");
            Dir(@"C:\Test");
            //MemoryStream
            //BufferedStream           

            ReadKey();

        }
    }
}
