using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorServer
{
    public class FileComparison
    {
        public string sourceName;
        public string targetName;

        public FileComparison(string sourceName, string targetName)
        {
            this.sourceName = sourceName;
            this.targetName = targetName;
        }
    }
}
