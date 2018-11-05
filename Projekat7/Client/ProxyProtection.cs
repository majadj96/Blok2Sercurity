using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
            throw new NotImplementedException();
        }
    }
}
