using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class InMemoryCash
    {
        public static Dictionary<SecurityIdentifier, CustomPrincipal> PrincipalDict = new Dictionary<SecurityIdentifier, CustomPrincipal>();
        public static Dictionary<string, List<string>> GroupsAndPermissionsDictionary = new Dictionary<string, List<string>>();
    }
}
