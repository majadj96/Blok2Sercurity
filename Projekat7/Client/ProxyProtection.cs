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

        public void CreateFile()
        {
            throw new NotImplementedException();
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

        public void DeleteFile()
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder()
        {
            throw new NotImplementedException();
        }

        public void ModifyFile()
        {
            throw new NotImplementedException();
        }

        public void ModifyFolderName()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }
    }
}
