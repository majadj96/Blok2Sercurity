using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class UpdateConfig : IUpdateConfig
    {
        public void UpdateConfiguration()
        {
            Console.WriteLine("Apdejtovao sam se " + DateTime.Now.ToString("hh.mm.ss.ffffff"));

            foreach(CustomPrincipal cp in InMemoryCash.PrincipalDict.Values)
            {
                cp.UpdatePermissions(cp.WindowsIdentity);
            }
            Console.WriteLine("Updated configuration");
        }
    }
}
