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

            Console.WriteLine("Updated configuration");
        }
    }
}
