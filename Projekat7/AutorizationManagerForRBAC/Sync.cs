using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutorizationManagerForRBAC
{
    class Sync : ISync
    {
        public string GetPort(string ip)
        {
            Random r = new Random();
            int port = r.Next(50011, 50030);
            string portString = port.ToString();
            string adresa = ip + ":" + portString;

            RBACChange.ListOfServers.Add(adresa);
            return portString;
        }

        public Dictionary<string, List<string>> SetDictionary()
        {
            Dictionary<string, List<string>>  GroupsAndPermissionsDict = new Dictionary<string, List<string>>();

            ResXResourceReader rsxr = new ResXResourceReader("..\\..\\GroupsAndPermisions.resx");
            foreach (DictionaryEntry d in rsxr)
            {

                string name = d.Key.ToString();
                string value = d.Value.ToString();
                string[] split = value.Split(',');
                List<string> listaPermisija = split.ToList();
                GroupsAndPermissionsDict.Add(name, listaPermisija);
            }
            return GroupsAndPermissionsDict;
        }
    }
}
