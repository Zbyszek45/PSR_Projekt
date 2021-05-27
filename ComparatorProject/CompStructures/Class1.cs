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
        public List<FilePos> f1 = new List<FilePos>();
        public List<FilePos> f2 = new List<FilePos>();

        public bool isInFile1(FilePos newFp)
        {
            bool res = false;
            foreach (FilePos fp in f1)
            {
                if (fp.compareTo(newFp))
                {
                    res = true;
                }
            }
            return res;
        }

        public bool isInFile2(FilePos newFp)
        {
            bool res = false;
            foreach (FilePos fp in f2)
            {
                if (fp.compareTo(newFp))
                {
                    res = true;
                }
            }
            return res;
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
            bool compExist = false;
            foreach (FileSingleResult fsr in results)
            {
                if (fsr.isInFile1(fp1))
                {
                    fsr.f2.Add(fp2);
                    compExist = true;
                }
                else if (fsr.isInFile2(fp2))
                {
                    fsr.f1.Add(fp1);
                    compExist = true;
                }
            }

            if (!compExist)
            {
                Console.WriteLine("Not found so new result");
                FileSingleResult tmp = new FileSingleResult();
                tmp.f1 = new List<FilePos>();
                tmp.f2 = new List<FilePos>();
                tmp.f1.Add(fp1);
                tmp.f2.Add(fp2);
                results.Add(tmp);
            }
        }

        public override string ToString()
        {
            String s = "";
            foreach (FileSingleResult fsr in results)
            {
                s += f1Name + ": \n";
                foreach (FilePos fp in fsr.f1)
                {
                    s += fp + "\n";
                }
                s += f2Name + ": \n";
                foreach (FilePos fp in fsr.f2)
                {
                    s += fp + "\n";
                }
            }
            
            return s;
        }
    }

    [Serializable]
    public class ClientRespone
    {
        public List<FilesCompResult> res = new List<FilesCompResult>();

    }

    public class Class1
    {
    }
}
