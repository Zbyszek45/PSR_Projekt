using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompStructures
{
    [Serializable]
    public class FilePair
    {
        public String f1;
        public String f2;

        public FilePair(String ff1, String ff2)
        {
            f1 = ff1;
            f2 = ff2;
        }

        public override string ToString()
        {
            return f1 + " " + f2;
        }
    }

    [Serializable]
    public class FilesHeader
    {
        public int patternLength;
        public List<FilePair> pairs;

        public override string ToString()
        {
            String s = "";
            foreach (FilePair fp in pairs)
            {
                s += fp;
                s += "\n";
            }
            return patternLength + "\n " + s;
        }
    }

    [Serializable]
    public class FilePos
    {
        public long i;
        public long j;

        public FilePos(long ii, long jj)
        {
            i = ii;
            j = jj;
        }

        public override string ToString()
        {
            return i + " " + j;
        }

        public bool compareTo(FilePos fp)
        {
            if (fp.i == this.i && fp.j == this.j)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    [Serializable]
    public class FileSingleResult
    {
        public FilePos f1;
        public FilePos f2;

        public FileSingleResult(FilePos f1, FilePos f2)
        {
            this.f1 = f1;
            this.f2 = f2;
        }
    }

    [Serializable]
    public class FilesCompResult
    {
        public String f1Name;
        public String f2Name;
        public List<FileSingleResult> results = new List<FileSingleResult>();

        public void add(FilePos fp1, FilePos fp2)
        {
            FileSingleResult tmp = new FileSingleResult(fp1, fp2);
            results.Add(tmp);
        }

        public override string ToString()
        {
            String s = "";
            foreach (FileSingleResult fsr in results)
            {
                s += f1Name + ": \n";
                s += fsr.f1 + ": \n";
                s += f2Name + ": \n";
                s += fsr.f2 + ": \n";
            }
            
            return s;
        }
    }

    [Serializable]
    public class ClientRespone
    {
        public List<FilesCompResult> res = new List<FilesCompResult>();
        public int time;
    }

    public class Class1
    {
    }
}
