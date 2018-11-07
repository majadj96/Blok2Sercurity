using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    class FileService : IFileService
    {
        

        public void CreateFile(string fileName)
        {
            OperationContext.Current.IncomingMessageHeaders
            IPrincipal principal = Common.CustomAuthorizationPolicy.CustomPrincipalInstance;
            IIdentity id = principal.Identity;
            WindowsIdentity Identity = id as WindowsIdentity;

            using (Identity.Impersonate())
            {
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
        }



        public void CreateFolder(string foldername)
        {
           // IPrincipal principal = Thread.CurrentPrincipal;
           
            IPrincipal principal = Common.CustomAuthorizationPolicy.CustomPrincipalInstance;
            IIdentity id = principal.Identity;
            WindowsIdentity Identity = id as WindowsIdentity;
           

           using (Identity.Impersonate())
           {
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
        
            string user = System.IO.File.GetAccessControl(foldername).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            Console.WriteLine("FOlder je kreiran od strane {0}", user);
        }

        public void DeleteFile(string fileName)
        {
            IPrincipal principal = Common.CustomAuthorizationPolicy.CustomPrincipalInstance;
            IIdentity id = principal.Identity;
            WindowsIdentity Identity = id as WindowsIdentity;


            using (Identity.Impersonate())
            {
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
        }

     

        public void DeleteFolder(string folderName)
        {
            IPrincipal principal = Common.CustomAuthorizationPolicy.CustomPrincipalInstance;
            IIdentity id = principal.Identity;
            WindowsIdentity Identity = id as WindowsIdentity;
            
            using (Identity.Impersonate())
            {
                var dir = new DirectoryInfo(folderName);
                if (dir != null)
                {
                    dir.Delete(true);
                    Console.WriteLine("folder je obrisan sa imenom {0}", folderName);
                }
            }

        }

        public void ModifyFile(string FileName)
        {
            IPrincipal principal = Common.CustomAuthorizationPolicy.CustomPrincipalInstance;
            IIdentity id = principal.Identity;
            WindowsIdentity Identity = id as WindowsIdentity;
            string user = System.IO.File.GetAccessControl(FileName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();

            using (Identity.Impersonate())
            {
                if (principal.IsInRole("Edit") || user == Identity.Name)
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
        }

        public void ModifyFolderName(string folderName,string newName)
        {
            IPrincipal principal = Common.CustomAuthorizationPolicy.CustomPrincipalInstance;
            IIdentity id = principal.Identity;
            WindowsIdentity Identity = id as WindowsIdentity;

            using (Identity.Impersonate())
            {
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
        }

        public void Read(string fileName)
        {
            IPrincipal principal = Common.CustomAuthorizationPolicy.CustomPrincipalInstance;
            IIdentity id = principal.Identity;
            WindowsIdentity Identity = id as WindowsIdentity;
           

            using (Identity.Impersonate())
            {
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
}
