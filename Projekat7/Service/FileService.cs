using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    class FileService : IFileService
    {
        IPrincipal principal = Thread.CurrentPrincipal;

        public void CreateFile(string fileName)
        {
            if (principal.IsInRole("Read"))
                File.Create(fileName);
            else
            {
                //loger
                SecurityException se = new SecurityException();
                Console.WriteLine("This user dont have permission", se.Message);
            }
            
        }

     

        public void CreateFolder(string foldername)
        {
            Console.WriteLine("Method Create Folder called {0}",foldername);
        }
    }
}
