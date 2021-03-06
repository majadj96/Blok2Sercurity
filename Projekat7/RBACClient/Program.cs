﻿using System;
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

            //ELENA 9.11.
            Console.WriteLine("Unesite ipadresu za KanalKaRBACServisu:");
            string add = Console.ReadLine();

            string address = "net.tcp://" + add + ":9998/RBACChange";

            Dictionary<string, List<string>> GroupsAndPermissionsDict = new Dictionary<string, List<string>>();

            MakeProxy proxy = new MakeProxy(binding, address);

            GroupsAndPermissionsDict = proxy.GetDictionary();
            
           
            int brojac = 0;

            foreach (string grupe in GroupsAndPermissionsDict.Keys)
            {
                Console.WriteLine(brojac+++". Grupa: " + grupe);

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


            using (MakeProxy proxy1 = new MakeProxy(binding, address))
            {
                proxy1.Change(GroupsAndPermissionsDict);
                Console.ReadLine();
            }
            
        }
    }
}
