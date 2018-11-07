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
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
             if (principal.IsInRole("Read"))
             {
                    File.Create(fileName);
                    Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name,"CreateFile()");
                    Console.WriteLine("Fajl je kreiran sa imenom {0}", fileName);

             }
             else
             {
                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "CreateFile()",se.Message);
                Console.WriteLine("This user dont have permission", se.Message);
             }
            
        }



        public void CreateFolder(string foldername)
        {
           
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Administrate"))
            {
                System.IO.Directory.CreateDirectory(foldername);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "CreateFolder()");
                Console.WriteLine("Folder je kreiran sa imenom {0}", foldername);
            }
            else
            {
                //loger
                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "CreateFolder()", se.Message);
                Console.WriteLine("This user dont have permission", se.Message);
            }
          
        }

        public void DeleteFile(string fileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Administrate"))
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "DeleteFile()");
                    Console.WriteLine("Fajl je obrisan sa imenom {0}", fileName);
                }
                else
                {
                    SecurityException se = new SecurityException();
                    Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "DeleteFile()", se.Message);
                    Console.WriteLine("This file not exist", se.Message);
                }
            }
            else
            {
                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "DeleteFile()", se.Message);
                Console.WriteLine("This user dont have permission", se.Message);

            }
          
        }

     

        public void DeleteFolder(string folderName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
             var dir = new DirectoryInfo(folderName);
             if (dir != null)
             {
                 dir.Delete(true);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "DeleteFolder()");
                Console.WriteLine("folder je obrisan sa imenom {0}", folderName);
             }
            Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "DeleteFile()","File not exists");



        }

        public void ModifyFile(string FileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
           
            if (principal.IsInRole("Edit"))
            {
                Console.WriteLine("Unesite dodatak fajlu:");
                string tekst = Console.ReadLine();
                File.AppendAllText(FileName, tekst);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "ModifyFile()");

                Console.WriteLine("Fajl je izmenjen sa imenom {0}", FileName);
            }
            else
            {
                //loger
                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "ModifyFile()", se.Message);

                Console.WriteLine("This user dont have permission", se.Message);
            }
         
        }

        public void ModifyFolderName(string folderName,string newName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Edit"))
            {
                Directory.Move(folderName, newName);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "ModifyFolderName()");

                Console.WriteLine("ime Foldera {0} je izmenjeno, novo ime je: {1}", folderName, newName);
            }
            else
            {

                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "ModifyFolderName()", se.Message);

                Console.WriteLine("This user dont have permission", se.Message);

            }
          
        }

        public void Read(string fileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
           
           if (principal.IsInRole("Read"))
           {

               string readText = File.ReadAllText(fileName);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "Read()");

                Console.WriteLine("citanje iz fajla sa imenom {0}", fileName);
               Console.WriteLine(readText);
           }
           else
           {
               //loger
               SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "Read()", se.Message);

                Console.WriteLine("This user dont have permission", se.Message);
           }
         
        }
    }
}
