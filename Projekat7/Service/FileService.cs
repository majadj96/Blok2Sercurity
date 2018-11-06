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
        

        public void CreateFile(string fileName)
        {
            IPrincipal principal = Thread.CurrentPrincipal;
            if (principal.IsInRole("Read"))
                File.Create(fileName);
            else
            {
                //loger
                SecurityException se = new SecurityException();
                Console.WriteLine("This user dont have permission", se.Message);
            }
          //string user = System.IO.File.GetAccessControl(fileName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();


        }



        public void CreateFolder(string foldername)
        {
            IPrincipal principal = Thread.CurrentPrincipal;
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
            IPrincipal principal = Thread.CurrentPrincipal;
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
            IPrincipal principal = Thread.CurrentPrincipal;
            var dir = new DirectoryInfo(folderName);

        }

        public void ModifyFile(string FileName)
        {
            IPrincipal principal = Thread.CurrentPrincipal;
            string user = System.IO.File.GetAccessControl(FileName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();

            if (principal.IsInRole("Edit"))
            {
                Console.WriteLine("Unesite dodatak fajlu:");
                string tekst = Console.ReadLine();
                File.AppendAllText(FileName,tekst);
               
            }
            else
            {
                //loger
                SecurityException se = new SecurityException();
                Console.WriteLine("This user dont have permission", se.Message);
            }
        }

        public void ModifyFolderName(string folderName,string newName)
        {
            IPrincipal principal = Thread.CurrentPrincipal;
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

        public void Read(string fileName)
        {
            IPrincipal principal = Thread.CurrentPrincipal;
            if (principal.IsInRole("Read")) {
                
                string readText = File.ReadAllText(fileName);
                Console.WriteLine(readText);
            }
            else
            {
                //loger
                SecurityException se = new SecurityException();
                Console.WriteLine("This user dont have permission", se.Message);
            }
        }
    }
}
