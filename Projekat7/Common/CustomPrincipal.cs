using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get { return windowsIdentity; } }
        WindowsIdentity windowsIdentity;
        List<string> permissions = new List<string>();

        public CustomPrincipal(WindowsIdentity windowsIdentity)
        {
            this.windowsIdentity = windowsIdentity;

            //dodaj pewrmisije
            foreach (IdentityReference group in windowsIdentity.Groups)
            {
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));

                if (GroupsAndPermissions.GroupsAndPermissionsDict.ContainsKey(name.ToString()))
                {
                    List<string> perms = GroupsAndPermissions.GroupsAndPermissionsDict[name.ToString()];
                    foreach(string perm in perms)
                    {
                        if (!permissions.Contains(perm))
                            permissions.Add(perm);
                    }
                    
                }
            }
        }

        public bool IsInRole(string permission)
        {

            foreach (string s in permissions)
            {
                if (s.Equals(permission))
                    return true;
            }
            return false;
            
            
            
        }
    }
}
