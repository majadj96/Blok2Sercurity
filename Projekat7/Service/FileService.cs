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
                    Console.WriteLine("Fajl je kreiran sa imenom {0}", fileName);

             }
             else
             {
                 //loger
                 SecurityException se = new SecurityException();
                 Console.WriteLine("This user dont have permission", se.Message);
             }
            
        }



        public void CreateFolder(string foldername)
        {
           
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Administrate"))
            {
                System.IO.Directory.CreateDirectory(foldername);
                Console.WriteLine("Folder je kreiran sa imenom {0}", foldername);
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
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Administrate"))
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Console.WriteLine("Fajl je obrisan sa imenom {0}", fileName);
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
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
             var dir = new DirectoryInfo(folderName);
             if (dir != null)
             {
                 dir.Delete(true);
                 Console.WriteLine("folder je obrisan sa imenom {0}", folderName);
             }
           

        }

        public void ModifyFile(string FileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
           
            if (principal.IsInRole("Edit"))
            {
                Console.WriteLine("Unesite dodatak fajlu:");
                string tekst = Console.ReadLine();
                File.AppendAllText(FileName, tekst);
                Console.WriteLine("Fajl je izmenjen sa imenom {0}", FileName);
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
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            
            if (principal.IsInRole("Edit"))
            {
                Directory.Move(folderName, newName);
                Console.WriteLine("ime Foldera {0} je izmenjeno, novo ime je: {1}", folderName, newName);
            }
            else
            {

                SecurityException se = new SecurityException();
                Console.WriteLine("This user dont have permission", se.Message);

            }
          
        }

        public void Read(string fileName)
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
           
           if (principal.IsInRole("Read"))
           {

               string readText = File.ReadAllText(fileName);
               Console.WriteLine("citanje iz fajla sa imenom {0}", fileName);
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
