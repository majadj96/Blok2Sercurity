using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class FileService : IFileService
    {
        public void CreateFolder(string foldername)
        {
            Console.WriteLine("Method Create Folder called {0}",foldername);
        }
    }
}
