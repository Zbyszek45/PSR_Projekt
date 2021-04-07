using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ComparatorServer;
using System.Linq;

namespace ComparatorUnitTests
{
    [TestClass]
    public class FileManagerTests
    {

        #region Dodatkowe atrybuty testu
        //
        // Można użyć następujących dodatkowych atrybutów w trakcie pisania testów:
        //
        // Użyj ClassInitialize do uruchomienia kodu przed uruchomieniem pierwszego testu w klasie
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Użyj ClassCleanup do uruchomienia kodu po wykonaniu wszystkich testów w klasie
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Użyj TestInitialize do uruchomienia kodu przed uruchomieniem każdego testu 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Użyj TestCleanup do uruchomienia kodu po wykonaniu każdego testu
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFileSplit()
        {
            var files = new List<TextFile>() {
                new TextFile("File1.txt", "test1_longer"),
                new TextFile("File2.txt", "test2"),
                new TextFile("File3.txt", "test3"),
                new TextFile("File4.txt", "test4"),
                new TextFile("File5.txt", "test5"),
                new TextFile("File6.txt", "test6"),
                new TextFile("File7.txt", "test7"),
                new TextFile("File8.txt", "test8")
            };
            int numberOfClients = 8;
            var fm = new FileManager();

            var result = fm.splitFiles(files: files, hostsNumber: numberOfClients);

            Assert.AreEqual(result.Count, 7);

            // Podział na serwery

            // Serwer 1

            var result1 = result[0];
            Assert.AreEqual(result1.files.Count, 5);
            Assert.AreEqual(result1.fileComparison.Count, 4);

            // Zawiera najdłuższy plik, pozostałe w normalnej kolejności
            Assert.IsTrue(result1.files.Contains(files[0]));
            Assert.IsTrue(result1.files.Contains(files[1]));
            Assert.IsTrue(result1.files.Contains(files[2]));
            Assert.IsTrue(result1.files.Contains(files[3]));
            Assert.IsTrue(result1.files.Contains(files[4]));

            Assert.IsTrue(pairExists("File1.txt", "File2.txt", result1.fileComparison));
            Assert.IsTrue(pairExists("File1.txt", "File3.txt", result1.fileComparison));
            Assert.IsTrue(pairExists("File1.txt", "File4.txt", result1.fileComparison));
            Assert.IsTrue(pairExists("File1.txt", "File5.txt", result1.fileComparison));

            // Serwer 2

            var result2 = result[1];
            Assert.AreEqual(result2.files.Count, 6);
            Assert.AreEqual(result2.fileComparison.Count, 4);

            // Zawiera najdłuższy plik, pozostałe w normalnej kolejności
            Assert.IsTrue(result2.files.Contains(files[0]));
            Assert.IsTrue(result2.files.Contains(files[1]));
            Assert.IsTrue(result2.files.Contains(files[5]));
            Assert.IsTrue(result2.files.Contains(files[6]));
            Assert.IsTrue(result2.files.Contains(files[7]));

            Assert.IsTrue(pairExists("File1.txt", "File6.txt", result2.fileComparison));
            Assert.IsTrue(pairExists("File1.txt", "File7.txt", result2.fileComparison));
            Assert.IsTrue(pairExists("File1.txt", "File8.txt", result2.fileComparison));
            Assert.IsTrue(pairExists("File2.txt", "File3.txt", result2.fileComparison));

            // Serwer 3

            var result3 = result[2];
            Assert.AreEqual(result3.files.Count, 5);
            Assert.AreEqual(result3.fileComparison.Count, 4);

            // Zawiera najdłuższy plik, pozostałe w normalnej kolejności
            Assert.IsTrue(result3.files.Contains(files[1]));
            Assert.IsTrue(result3.files.Contains(files[3]));
            Assert.IsTrue(result3.files.Contains(files[4]));
            Assert.IsTrue(result3.files.Contains(files[5]));
            Assert.IsTrue(result3.files.Contains(files[6]));

            Assert.IsTrue(pairExists("File2.txt", "File4.txt", result3.fileComparison));
            Assert.IsTrue(pairExists("File2.txt", "File5.txt", result3.fileComparison));
            Assert.IsTrue(pairExists("File2.txt", "File6.txt", result3.fileComparison));
            Assert.IsTrue(pairExists("File2.txt", "File7.txt", result3.fileComparison));

            // Serwer 4

            var result4 = result[3];
            Assert.AreEqual(result4.files.Count, 6);
            Assert.AreEqual(result4.fileComparison.Count, 4);

            // Zawiera najdłuższy plik, pozostałe w normalnej kolejności
            Assert.IsTrue(result4.files.Contains(files[1]));
            Assert.IsTrue(result4.files.Contains(files[2]));
            Assert.IsTrue(result4.files.Contains(files[3]));
            Assert.IsTrue(result4.files.Contains(files[4]));
            Assert.IsTrue(result4.files.Contains(files[5]));
            Assert.IsTrue(result4.files.Contains(files[7]));

            Assert.IsTrue(pairExists("File2.txt", "File8.txt", result4.fileComparison));
            Assert.IsTrue(pairExists("File3.txt", "File4.txt", result4.fileComparison));
            Assert.IsTrue(pairExists("File3.txt", "File5.txt", result4.fileComparison));
            Assert.IsTrue(pairExists("File3.txt", "File6.txt", result4.fileComparison));

            // Serwer 5

            var result5 = result[4];
            Assert.AreEqual(result5.files.Count, 6);
            Assert.AreEqual(result5.fileComparison.Count, 4);

            // Zawiera najdłuższy plik, pozostałe w normalnej kolejności
            Assert.IsTrue(result5.files.Contains(files[2]));
            Assert.IsTrue(result5.files.Contains(files[3]));
            Assert.IsTrue(result5.files.Contains(files[4]));
            Assert.IsTrue(result5.files.Contains(files[5]));
            Assert.IsTrue(result5.files.Contains(files[6]));
            Assert.IsTrue(result5.files.Contains(files[7]));

            Assert.IsTrue(pairExists("File3.txt", "File7.txt", result5.fileComparison));
            Assert.IsTrue(pairExists("File3.txt", "File8.txt", result5.fileComparison));
            Assert.IsTrue(pairExists("File4.txt", "File5.txt", result5.fileComparison));
            Assert.IsTrue(pairExists("File4.txt", "File6.txt", result5.fileComparison));

            // Serwer 6

            var result6 = result[5];
            Assert.AreEqual(result6.files.Count, 5);
            Assert.AreEqual(result6.fileComparison.Count, 4);

            // Zawiera najdłuższy plik, pozostałe w normalnej kolejności
            Assert.IsTrue(result6.files.Contains(files[3]));
            Assert.IsTrue(result6.files.Contains(files[4]));
            Assert.IsTrue(result6.files.Contains(files[5]));
            Assert.IsTrue(result6.files.Contains(files[6]));
            Assert.IsTrue(result6.files.Contains(files[7]));

            Assert.IsTrue(pairExists("File4.txt", "File7.txt", result6.fileComparison));
            Assert.IsTrue(pairExists("File4.txt", "File8.txt", result6.fileComparison));
            Assert.IsTrue(pairExists("File5.txt", "File6.txt", result6.fileComparison));
            Assert.IsTrue(pairExists("File5.txt", "File7.txt", result6.fileComparison));

            // Serwer 7

            var result7 = result[6];
            Assert.AreEqual(result7.files.Count, 4);
            Assert.AreEqual(result7.fileComparison.Count, 4);

            // Zawiera najdłuższy plik, pozostałe w normalnej kolejności
            Assert.IsTrue(result7.files.Contains(files[4]));
            Assert.IsTrue(result7.files.Contains(files[5]));
            Assert.IsTrue(result7.files.Contains(files[6]));
            Assert.IsTrue(result7.files.Contains(files[7]));

            Assert.IsTrue(pairExists("File5.txt", "File8.txt", result7.fileComparison));
            Assert.IsTrue(pairExists("File6.txt", "File7.txt", result7.fileComparison));
            Assert.IsTrue(pairExists("File6.txt", "File8.txt", result7.fileComparison));
            Assert.IsTrue(pairExists("File7.txt", "File8.txt", result7.fileComparison));


            bool pairExists(string first, string second, HashSet<FileComparison> list)
            {
                return list.ToList()
                    .Find(o =>
                         (o.sourceName == first && o.targetName == second)
                         || (o.sourceName == second && o.targetName == first)) != null;
            }
        }
    }
}
