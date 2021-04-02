using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ComparatorServer
{
    class FileManager
    {

        public FileManager()
        {

        }

        public List<String> Unzip(string fileName)
        {
            var result = new List<String>();
            var fs = File.Open(fileName, FileMode.Open);
            ZipArchive archive = new ZipArchive(fs);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                var osr = new StreamReader(entry.Open(), Encoding.Default);
                result.Add(osr.ReadToEnd());
            }

            return result;
        }

        public Dictionary<int, List<string>> splitFiles(List<string> files, int hostsNumber)
        {
            var result = new Dictionary<int, List<string>>();
            if (files.Count < 2)
            {
                return result;
            }

            var filesSortedBySize = files.OrderBy(f => f.Length).ToList();


            return result;
        }
    }
}
