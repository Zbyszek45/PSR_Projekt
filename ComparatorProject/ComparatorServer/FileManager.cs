using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComparatorServer
{
    class FileManager
    {
        private List<FileComp> files = new List<FileComp>();
        private static int idGen = 0;
        private long ziarn = 0;
        private List<FilePairRef> pairs = new List<FilePairRef>();
        private int pattern;

        public void create_file_list(ListView lw, long z, int p)
        {
            ziarn = z;
            pattern = p;
            files.Clear();
            pairs.Clear();
            idGen = 0;
            // Adding files to list
            foreach (ListViewItem x in lw.Items)
            {
                files.Add(new FileComp(x.Text, idGen.ToString() , x.SubItems[1].Text));
                idGen++;
            }
            // Sorting files with lambda
            files.Sort((a, b) =>
            {
                FileInfo f1 = new FileInfo(b.path);
                FileInfo f2 = new FileInfo(a.path);
                return (int)f1.Length - (int)f2.Length;
            });

            foreach (FileComp f in files)
            {
                Console.WriteLine(f);
            }

            // Creat the list of pairs needed to compare
            for (int i=0; i<files.Count; i++)
            {
                for (int j=i+1; j<files.Count; j++)
                {
                    FilePairRef tmp = new FilePairRef(files[i], files[j]);
                    tmp.lengthSum += new FileInfo(tmp.f1.path).Length;
                    tmp.lengthSum += new FileInfo(tmp.f2.path).Length;
                    pairs.Add(tmp);
                }
            }

            // tmp show pairs:
            foreach (FilePairRef fp in pairs)
            {
                fp.wasChecked = false;
                Console.WriteLine(fp);
            }
        }

        public List<FilePairRef> getFilesToCompare()
        {
            // check if there is any left comparison
            bool areAllChecked = true;
            foreach (FilePairRef fp in pairs)
            {
                if (!fp.wasChecked)
                {
                    areAllChecked = false;
                }
            }
            if (areAllChecked) return null;

            // there is something to compare so...
            List<FilePairRef> retFiles = new List<FilePairRef>();
            long length = 0;
            foreach (FilePairRef fp in pairs)
            {
                if (!fp.wasChecked)
                {
                    retFiles.Add(fp);
                    fp.wasChecked = true;
                    length += fp.lengthSum;
                    if ((length / 1024) > ziarn) break;
                }
            }
            /*
            Console.WriteLine("Files that some client got: ");
            foreach (FilePairRef fc in retFiles)
            {
                Console.WriteLine(fc);
            }
            */
            return retFiles;
        }

        //seters geters
        public List<FileComp> get_file_list()
        {
            return files;
        }

        public int get_pattern()
        {
            return pattern;
        }

        public String get_by_uniq(String f)
        {
            String s = "";
            foreach (FileComp fc in files)
            {
                if (String.Compare(fc.path, f) == 0)
                {
                    s = fc.name;
                }
            }
            return s;
        }
    }
}
