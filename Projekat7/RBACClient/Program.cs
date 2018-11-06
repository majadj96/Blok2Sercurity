using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RBACClient
{
    class Program
    {
        public static Dictionary<string, List<string>> GroupsAndPermissionsDict = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9998/RBACChange";


            ResXResourceReader rsxr = new ResXResourceReader("..\\..\\..\\Common\\GroupsAndPermisions.resx");
            foreach (DictionaryEntry d in rsxr)
            {
                string name = d.Key.ToString();
                string value = d.Value.ToString();
                string[] split = value.Split(',');
                List<string> listaPermisija = split.ToList();
                GroupsAndPermissionsDict.Add(name, listaPermisija);
            }
            int brojac = 0;
            foreach (string grupe in GroupsAndPermissionsDict.Keys)
            {
                Console.WriteLine(brojac+++".Grupa: " + grupe);

                Console.WriteLine("Permisije");
                foreach (string permisija in GroupsAndPermissionsDict[grupe])
                {
                    Console.Write(permisija);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Izaberi grupu: ");
            int grupa = Int32.Parse(Console.ReadLine());
            string naziv = GroupsAndPermissionsDict.ElementAt(grupa).Key;
            Console.WriteLine("Izabrana grupa je: " + naziv);
            GroupsAndPermissionsDict[naziv] = new List<string>();

            Console.WriteLine("Dodajte nove permisije: ");
            string permis=string.Empty;

            do
            {
                Console.WriteLine("AD-Administrate");
                Console.WriteLine("R-Read");
                Console.WriteLine("E-Edit");
                Console.WriteLine("A-Access");
                Console.WriteLine("0 - Gotovo");

                permis = Console.ReadLine();

                switch (permis)
                {
                    case "AD":
                        GroupsAndPermissionsDict[naziv].Add("Administrate");

                        break;
                    case "R":
                        GroupsAndPermissionsDict[naziv].Add("Read");

                        break;
                    case "E":
                        GroupsAndPermissionsDict[naziv].Add("Edit");

                        break;
                    case "A":
                        GroupsAndPermissionsDict[naziv].Add("Access");
                        break;
                }

                Console.WriteLine("Da li zelite da dodate jos permisija? [y/n]");

                permis = Console.ReadLine();

            } while (permis != "n");

            Console.WriteLine("************************");
            brojac = 0;
            foreach (string grupe in GroupsAndPermissionsDict.Keys)
            {
                Console.WriteLine(brojac++ + ".Grupa: " + grupe);

                Console.WriteLine("Permisije");
                foreach (string permisija in GroupsAndPermissionsDict[grupe])
                {
                    Console.Write(permisija);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }


            using (MakeProxy proxy = new MakeProxy(binding, address))
            {
                proxy.Change(GroupsAndPermissionsDict);
                Console.ReadLine();
            }
            
        }
    }
}
