using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorServer
{
    class FilePairRef
    {
        public FileComp f1;
        public FileComp f2;
        public bool wasChecked = false;
        public long lengthSum = 0;

        public FilePairRef(FileComp ff1, FileComp ff2)
        {
            f1 = ff1;
            f2 = ff2;
        }

        public override string ToString()
        {
            return f1.name + " " + f2.name + " " + lengthSum;
        }
    }
}
