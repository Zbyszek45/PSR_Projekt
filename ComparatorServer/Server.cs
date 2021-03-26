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
            var comparator = new Comparator(patternLength: 5, caseSensitive: true);
            string source = "program";
            string target = "programowanie";

            var expectedResults = new List<string>() { "program" };
            //var result = comparator.Compare(source, target);

            comparator.Compare(source, target);
            Console.ReadLine();
        }
    }
}
