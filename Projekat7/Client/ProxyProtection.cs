using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ProxyProtection : ChannelFactory<IFileService>, IFileService, IDisposable
    {
        IFileService factory;

        public ProxyProtection(NetTcpBinding binding, string address) : base(binding, address)
        {

            factory = this.CreateChannel();
        }

        public void CreateFolder(string foldername)
        {
            try
            {
                factory.CreateFolder(foldername);
                Console.WriteLine("CreateFolder() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to CreateFolder(). {0}", comEx.Message);
            }
        }

        public void CreateFile(string filename)
        {
            try
            {
                factory.CreateFile(filename);
                Console.WriteLine("CreateFile() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to CreateFolder(). {0}", comEx.Message);
            }
        }

        public void ModifyFolderName(string folderName, string newName)
        {
            try
            {
                factory.ModifyFolderName(folderName,newName);
                Console.WriteLine("ModifyFolderName() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to ModifyFolderName(). {0}", comEx.Message);
            }
        }

        public void ModifyFile(string FileName)
        {
            try
            {
                factory.ModifyFile(FileName);
                Console.WriteLine("ModifyFile() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to ModifyFile(). {0}", comEx.Message);
            }
        }

        public void Read(string FileName)
        {
            try
            {
                factory.Read(FileName);
                Console.WriteLine("Read() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to Read(). {0}", comEx.Message);
            }
        }

        public void DeleteFolder(string folderName)
        {
            try
            {
                factory.DeleteFolder(folderName);
                Console.WriteLine("DeleteFolder() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to DeleteFolder(). {0}", comEx.Message);
            }
        }

        public void DeleteFile(string fileName)
        {
            try
            {
                factory.DeleteFile(fileName);
                Console.WriteLine("DeleteFile() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to DeleteFile(). {0}", comEx.Message);
            }
        }
    }
}
