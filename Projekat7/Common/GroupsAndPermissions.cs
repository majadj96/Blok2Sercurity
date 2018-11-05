using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GroupsAndPermissions
    {
        public static Dictionary<string, List<string>> dictionary;
        public GroupsAndPermissions()
        {
            ResourceManager rm = new ResourceManager("Common.GroupsAndPermisions",Assembly.GetExecutingAssembly());

            string listaPermisijaReader = rm.GetString("Reader");
            string[] splitReader = listaPermisijaReader.Split(',');
            List<string> listaReader =  splitReader.ToList();

            string listaPermisijaModifier = rm.GetString("Modifier");
            string[] splitModifier = listaPermisijaModifier.Split(',');
            List<string> listaModifier = splitModifier.ToList();

            string listaPermisijaAdministrator = rm.GetString("Administrator");
            string[] splitAdministrator = listaPermisijaAdministrator.Split(',');
            List<string> listaAdministrator = splitAdministrator.ToList();

            dictionary = new Dictionary<string, List<string>>
            {
                { "Reader", listaReader },
                { "Modifier", listaModifier },
                { "Administrator", listaAdministrator },

                };
        }

        public List<string> GetPermissions(string group)
        {
            dictionary.TryGetValue(group, out List<string> retList);
            return retList;
        }
    }
}
