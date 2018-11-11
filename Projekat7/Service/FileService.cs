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
        public void CheckLevel()
        {
        }

        public bool CreateFile(string fileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
             if (principal.IsInRole("Read"))
             {
                    File.Create(fileName);
                    Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name,"CreateFile()");
                    Console.WriteLine("File is created with name {0}", fileName);
                    return true;
             }
             else
             {
                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "CreateFile()",se.Message);
                Console.WriteLine("This user dont have permission", se.Message);
                return false;
             }
            
        }



        public bool CreateFolder(string foldername)
        {
           
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Administrate"))
            {
                System.IO.Directory.CreateDirectory(foldername);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "CreateFolder()");
                Console.WriteLine("Folder is created with name {0}", foldername);
                return true;
            }
            else
            {
                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "CreateFolder()", se.Message);
                Console.WriteLine("This user dont have permission", se.Message);
                return false;
            }
          
        }

        public bool DeleteFile(string fileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Administrate"))
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "DeleteFile()");
                    Console.WriteLine("File {0} is deleted.", fileName);
                    return true;
                }
                else
                {
                    SecurityException se = new SecurityException();
                    Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "DeleteFile()", se.Message);
                    Console.WriteLine("This file not exist", se.Message);
                    return false;
                }
            }
            else
            {
                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "DeleteFile()", se.Message);
                Console.WriteLine("This user dont have permission", se.Message);
                return false;

            }
          
        }

     

        public bool DeleteFolder(string folderName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
             var dir = new DirectoryInfo(folderName);
             if (dir != null)
             {
                 dir.Delete(true);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "DeleteFolder()");
                Console.WriteLine("Folder {0} is deleted.", folderName);
                return true;
             }
            Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "DeleteFile()","File not exists");
            return false;



        }

        public bool ModifyFile(string FileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
           
            if (principal.IsInRole("Edit"))
            {
                Console.WriteLine("Modify file:");
                string tekst = Console.ReadLine();
                File.AppendAllText(FileName, tekst);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "ModifyFile()");

                Console.WriteLine("File {0} is modified.", FileName);
                return true;
            }
            else
            {
                
                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "ModifyFile()", se.Message);

                Console.WriteLine("This user dont have permission", se.Message);
                return false;
            }
         
        }

        public bool ModifyFolderName(string folderName,string newName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Edit"))
            {
                Directory.Move(folderName, newName);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "ModifyFolderName()");

                Console.WriteLine("Folder name {0} is changed to {1}.", folderName, newName);
                return true;
            }
            else
            {

                SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "ModifyFolderName()", se.Message);

                Console.WriteLine("This user dont have permission", se.Message);
                return false;

            }
          
        }

        public bool Read(string fileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
           
           if (principal.IsInRole("Read"))
           {

               string readText = File.ReadAllText(fileName);
                Audit.LoggingSuccess(Thread.CurrentPrincipal.Identity.Name, "Read()");

                Console.WriteLine("Text from file {0}", fileName);
               Console.WriteLine(readText);
                return true;
           }
           else
           {
               SecurityException se = new SecurityException();
                Audit.LoggingFail(Thread.CurrentPrincipal.Identity.Name, "Read()", se.Message);

                Console.WriteLine("This user dont have permission", se.Message);
                return false;
           }
         
        }
    }
}
