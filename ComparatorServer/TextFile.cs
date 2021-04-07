using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorServer
{
    public class TextFile
    {
        public string body;
        public string name;

        public TextFile(string name, string body)
        {
            this.name = name;
            this.body = body;
        }
    }
}
