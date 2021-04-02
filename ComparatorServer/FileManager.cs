using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ComparatorServer
{
    public class FileManager
    {

        public FileManager()
        {

        }

        public List<TextFile> Unzip(string fileName)
        {
            var result = new List<TextFile>();
            var fs = File.Open(fileName, FileMode.Open);
            ZipArchive archive = new ZipArchive(fs);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                var osr = new StreamReader(entry.Open(), Encoding.Default);

                result.Add(new TextFile(entry.Name, osr.ReadToEnd()));
            }

            return result;
        }

        public List<CompareStrategy> splitFiles(List<TextFile> files, int hostsNumber)
        {
            var result = new List<CompareStrategy>();
            if (files.Count < 2)
            {
                return result;
            }

            var filesSortedBySize = files.OrderByDescending(f => f.body.Length).ToList();

            double comparisonNumber = 0;

            for (int i = filesSortedBySize.Count - 1; i >= 0; i--)
            {
                comparisonNumber += i;
            }

            var fileComparisons = new List<FileComparison>();

            for (int firstPairIndex = 0; firstPairIndex < hostsNumber; firstPairIndex++)
            {
                for (int secondPairIndex = firstPairIndex + 1; secondPairIndex < hostsNumber; secondPairIndex++)
                {
                    var firstItem = filesSortedBySize[firstPairIndex];
                    var secondItem = filesSortedBySize[secondPairIndex];
                    fileComparisons.Add(new FileComparison(firstItem.name, secondItem.name));
                }
            }

            int filePairs = (int)Math.Ceiling(comparisonNumber / hostsNumber);
            var hostsToHandle = comparisonNumber / filePairs;

            for (int i = 0; i < hostsToHandle; i++)
            {
                var strategy = new CompareStrategy(id: i);

                var subFileComparisons = fileComparisons.Skip(i * filePairs).ToList();
                
                for (int a = 0; a < filePairs && a < subFileComparisons.Count(); a++)
                {
                    strategy.fileComparison.Add(subFileComparisons[a]);
                }

                result.Add(strategy);
            }

            // Powiąż pliki z każdą strategią.
            foreach (CompareStrategy strategy in result)
            {
                var uniqueFilenames = new HashSet<string>();

                foreach (FileComparison comparison in strategy.fileComparison)
                {
                    uniqueFilenames.Add(comparison.sourceName);
                    uniqueFilenames.Add(comparison.targetName);
                }

                foreach (string filename in uniqueFilenames)
                {
                    var foundFIle = files.Find(f => f.name == filename);
                    if (foundFIle != null)
                    {
                        strategy.files.Add(foundFIle);
                    }
                }
            }

            return result;
        }
    }
}
