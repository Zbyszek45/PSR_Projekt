using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorServer
{
    class FileComp
    {
        public string orginalName;
        public string name;
        public string path;
        public int weight;

        public FileComp(string orginalName, string name, string path)
        {
            this.orginalName = orginalName;
            this.name = name + ".txt";
            this.path = path;
        }
    }
}
