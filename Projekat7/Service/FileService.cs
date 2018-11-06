using Common;
using System;
using System.Collections.Generic;
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
        public void CreateFolder(string foldername)
        {
            IPrincipal principal = Thread.CurrentPrincipal;
            if (principal.IsInRole("Administrate"))
            {
                Console.WriteLine("Method Create Folder called {0}", foldername);
            }
            else
            {
                SecurityException ex = new SecurityException("No CreateFolder right!");
                throw ex; //loger
            }
            
        }
    }
}
