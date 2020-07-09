using System;
using System.IO;
using System.Text;

namespace ChangeDirectoryName
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\emag\hp\hp_rename\";            
            
            var directory = Directory.GetDirectories(path);
            string[] lines = new string[directory.Length];
            int i = 0;

            foreach (string s in directory)
            {
                Console.WriteLine(s);
            }

            ;

            //string[] files = Directory.GetFiles(dir);
            //foreach (string file in files)
            //    Console.WriteLine(Path.GetFileName(file));
        }
    }
}
