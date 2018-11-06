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
            if (principal.IsInRole("Administrate"))
            {
              System.IO.Directory.CreateDirectory(foldername);
            }
            else
            {
                //loger
                SecurityException se = new SecurityException();
                Console.WriteLine("This user dont have permission", se.Message);
            }


        }

        public void DeleteFile(string fileName)
        {
            if (principal.IsInRole("Administrate"))
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                else
                {
                    SecurityException se = new SecurityException();
                    Console.WriteLine("This file not exist", se.Message);
                }
            }
            else
            {
                SecurityException se = new SecurityException();
                Console.WriteLine("This user dont have permission", se.Message);

            }
        }

     

        public void DeleteFolder(string folderName)
        {

            var dir = new DirectoryInfo(folderName);

        }

        public void ModifyFile()
        {
            throw new NotImplementedException();
        }

        public void ModifyFolderName(string folderName,string newName)
        {
            if (principal.IsInRole("Edit"))
            {
                Directory.Move(folderName, newName);
            }
            else
            {
                
                SecurityException se = new SecurityException();
                Console.WriteLine("This user dont have permission", se.Message);

            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }
    }
}
