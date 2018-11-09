using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class RBACProxy : ChannelFactory<ISync>, ISync, IDisposable
    {
        ISync factory;

        public RBACProxy(NetTcpBinding binding, string address) : base(binding, address)
        {

            factory = this.CreateChannel();
        }



        public Dictionary<string, List<string>> GetDictionary()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            try
            {
                dict = factory.SetDictionary();
                Console.WriteLine("SetDictionary() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to SetDictionary(). {0}", comEx.Message);
            }

            return dict;
        }
    }
}
