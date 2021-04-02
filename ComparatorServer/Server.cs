using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorServer
{
    class Server
    {
        static void Main(string[] args)
        {
            var fileManager = new FileManager();

            var files = fileManager.Unzip(fileName: "sample_archive.zip");

            //Console.WriteLine(zip.Entries.Count);
            Console.ReadLine();
        }
    }
}
