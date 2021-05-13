using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComparatorServer
{
    class ClientFM
    {
        public ClientFM(int x)
        {
            id = x;
        }
        public int id;
        public List<FileComp> files = new List<FileComp>();
    }

    class FileManager
    {
        private List<FileComp> files = new List<FileComp>();
        private List<ClientFM> clients = new List<ClientFM>();
        private static int idGen = 0;
        private int ziarn = 0;

        public void create_file_list(ListView lw, int z, int client_count)
        {
            ziarn = z;
            files.Clear();
            clients.Clear();
            idGen = 0;
            foreach (ListViewItem x in lw.Items)
            {
                files.Add(new FileComp(x.Text, idGen.ToString() , x.SubItems[1].Text));
                idGen++;
            }

            for (int i =0; i < client_count; i++)
            {
                clients.Add(new ClientFM(i));
            }

            if (ziarn != 0 && files.Count() > 0)
            {

            }
        }

        public List<FileComp> getFilesToCompare(int id)
        {
            return files;
        }

        public bool checkIfClientHasFile(int id, FileComp file)
        {
            foreach (FileComp f in clients[id].files)
            {
                if (f.name.Equals(file.name))
                {
                    return true;
                }
            }
            return false;
        }

        //seters geters
        public List<FileComp> get_file_list()
        {
            return files;
        }

    }
}
