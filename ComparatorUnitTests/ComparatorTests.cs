using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ComparatorServer;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ComparatorUnitTests
{
    [TestClass]
    public class ComparatorTests
    {
        [TestMethod]
        public void TestSimpleCompare()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "program test";
            string target = "programowanie";

            var expectedResults = new HashSet<string>() { "program" };
            var result = comparator.Compare(source, target);

            Assert.IsTrue(expectedResults.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestSimpleNoMatches()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "ala ma kota";
            string target = "politechnika świętokrzyska";

            var emptyList = new HashSet<string>();

            Assert.IsTrue(emptyList.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestSingleWordFirstShort()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "program";
            string target = "programowanie";

            var expectedResults = new HashSet<string>() { "program" };
            var result = comparator.Compare(source, target);

            Assert.IsTrue(expectedResults.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestSingleWordSecondShort()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "programowanie";
            string target = "program";

            var expectedResults = new HashSet<string>() { "program" };
            var result = comparator.Compare(source, target);

            Assert.IsTrue(expectedResults.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestWithSpaces()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "ala ma kota";
            string target = "ala ma psa";

            var expectedResults = new HashSet<string>() { "ala ma" };

            Assert.IsTrue(expectedResults.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestMultipleMatches()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "ala ma kota a kot ma ale super zwierzaczek";
            string target = "ale ala ma psa a nie kota, super to tez jest zwierzaczek";

            var expectedResults = new HashSet<string>() { "ala ma", "super", "zwierzaczek" };

            Assert.IsTrue(expectedResults.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestSamePatternMultitpleTimesSingle()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "ala ma kota ala ma kota";
            string target = "ala ma psa ala nie ma psa";

            var expectedResults = new HashSet<string>() { "ala ma" };

            Assert.IsTrue(expectedResults.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestSamePatternMultitpleTimesBoth()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "ala ma kota ale ma kota";
            string target = "ala ma psy ale ma papugę";

            var expectedResults = new HashSet<string>() { "ala ma", "ale ma" };

            Assert.IsTrue(expectedResults.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestPolishCharacters()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "ala ma żółwika super ąęśćóżźł";
            string target = "ala lubi żółwika nie jest ąęśćóżźł fajnie";

            var expectedResults = new HashSet<string>() { "żółwika", "ąęśćóżźł" };

            Assert.IsTrue(expectedResults.SequenceEqual(comparator.Compare(source, target)));
        }

        [TestMethod]
        public void TestEmptyFiles()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            var emptyList = new HashSet<string>();

            Assert.IsTrue(emptyList.SequenceEqual(comparator.Compare("test", "")));
            Assert.IsTrue(emptyList.SequenceEqual(comparator.Compare("", "test")));
            Assert.IsTrue(emptyList.SequenceEqual(comparator.Compare("", "")));
        }

        [TestMethod]
        public void TestFoundLessThanPatternLength()
        {
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "alan123";
            string target = "alan345";

            var emptyList = new HashSet<string>();

            Assert.IsTrue(emptyList.SequenceEqual(comparator.Compare(source, target)));
        }
    }
}
