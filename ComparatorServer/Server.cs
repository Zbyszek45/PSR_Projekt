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

            var filess = fileManager.Unzip(fileName: "sample_archive.zip");

            //Console.WriteLine(zip.Entries.Count);
            var files = new List<TextFile>() {
                new TextFile("File1.txt", "test1"),
                new TextFile("File2.txt", "test2"),
                new TextFile("File3.txt", "test3"),
                new TextFile("File4.txt", "test4"),
                new TextFile("File5.txt", "test5 super longer message"),
                new TextFile("File77.txt", "test77"),
                new TextFile("File9.txt", "test9"),
                new TextFile("File8.txt", "test8")
            };
            int numberOfClients = 8;

            fileManager.splitFiles(files: files, hostsNumber: numberOfClients);
            Console.ReadLine();
        }
    }
}
