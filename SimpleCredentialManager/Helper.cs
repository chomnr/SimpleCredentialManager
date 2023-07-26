using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager
{
    internal class Helper
    {
        public static void CreateKeyFile(string fileName, string key, string iv) {
            using (FileStream fs = File.Create(fileName + ".scrt"))
            {
                byte[] info = new UTF8Encoding().GetBytes(key + " " + iv);
                fs.Write(info, 0, info.Length);
            }
        }

        public static void CreateStoreFile(string fileName)
        {
            using (FileStream fs = File.Create(fileName + ".scm"))
            {
                byte[] info = new UTF8Encoding().GetBytes("");
                fs.Write(info, 0, info.Length);
            }
        }

        public static void SaveFile() { }

        public static void PrintHeader()
        {
            Console.WriteLine("  ______  ______  __    __    ");
            Console.WriteLine(" /\\  ___\\/\\  ___\\/\\ \"-./  \\   ");
            Console.WriteLine(" \\ \\___  \\ \\ \\___\\ \\ \\-./\\ \\  ");
            Console.WriteLine("  \\/\\_____\\ \\_____\\ \\_\\ \\ \\_\\ ");
            Console.WriteLine("   \\/_____/\\/_____/\\/_/  \\/_/ v1.0.0");
        }
    }
}
