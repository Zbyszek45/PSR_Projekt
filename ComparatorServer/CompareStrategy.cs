using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorServer
{
    public class CompareStrategy
    {
        public HashSet<TextFile> files;
        public HashSet<FileComparison> fileComparison;
        public int id;

        public CompareStrategy(int id)
        {
            this.id = id;
            fileComparison = new HashSet<FileComparison>();
            files = new HashSet<TextFile>();
        }
    }
}
