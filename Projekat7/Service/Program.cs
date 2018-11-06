using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {

            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);




            Console.WriteLine("Choose 't' for Transport Mode or 'm' for Message Mode");
            string mode = Console.ReadLine();

            HostProtection hostProtection = new HostProtection(mode);

            hostProtection.Open();
            hostProtection.Close();


        }
    }
}
