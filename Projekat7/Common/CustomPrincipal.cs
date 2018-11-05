using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity => throw new NotImplementedException();

        public bool IsInRole(string permission)
        {
            throw new NotImplementedException();
        }
    }
}
