﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get { return WindowsIdentity; } }
        public WindowsIdentity WindowsIdentity;
        public List<string> permissions = new List<string>();
        

        public CustomPrincipal(WindowsIdentity windowsIdentity)
        {
            WindowsIdentity = windowsIdentity;
           
            UpdatePermissions(WindowsIdentity);
        }

        public void UpdatePermissions(WindowsIdentity windowsIdentity)
        {
            permissions = new List<string>();
            Program.UpdateDictionary();
           
            foreach (IdentityReference group in windowsIdentity.Groups)
            {
                
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));
                string[] split = name.ToString().Split('\\');
                string keyDict;
                if (split.Count() > 1)
                    keyDict = split[1];
                else
                    keyDict = " ";

              
                if (InMemoryCash.GroupsAndPermissionsDictionary.ContainsKey(keyDict))
                {
                    List<string> perms = InMemoryCash.GroupsAndPermissionsDictionary[keyDict];
                    foreach (string perm in perms)
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
