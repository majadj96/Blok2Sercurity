using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RBACClient
{
    class MakeProxy : ChannelFactory<IRBACChange>, IRBACChange, IDisposable
    {

        IRBACChange factory;

            public MakeProxy(NetTcpBinding binding, string address) : base(binding, address)
            {

                factory = this.CreateChannel();
            }

        public void Change()
        {
            try
            {
                factory.Change();
                Console.WriteLine("Change() allowed");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to Change(). {0}", comEx.Message);
            }
        }


       
    }
}
