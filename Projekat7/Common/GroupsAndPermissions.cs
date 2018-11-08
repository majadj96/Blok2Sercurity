using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Common
{
    public class GroupsAndPermissions
    {
        public static Dictionary<string, List<string>> GroupsAndPermissionsDict = new Dictionary<string, List<string>>();
        public GroupsAndPermissions()
        {
            UpdatePermissionsGroup();
        }

        public List<string> GetPermissions(string group)
        {
            GroupsAndPermissionsDict.TryGetValue(group, out List<string> retList);
            return retList;
        }

        public void UpdatePermissionsGroup()
        {
            GroupsAndPermissionsDict = new Dictionary<string, List<string>>();
            
            ResXResourceReader rsxr = new ResXResourceReader("..\\..\\..\\Common\\GroupsAndPermisions.resx");
            foreach (DictionaryEntry d in rsxr)
            {

                string name = d.Key.ToString();
                string value = d.Value.ToString();
                string[] split = value.Split(',');
                List<string> listaPermisija = split.ToList();
                GroupsAndPermissionsDict.Add(name, listaPermisija);
            }
        }
    }
}
