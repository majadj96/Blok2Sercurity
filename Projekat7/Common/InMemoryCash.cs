﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class InMemoryCash
    {
        public static Dictionary<SecurityIdentifier, CustomPrincipal> PrincipalDict = new Dictionary<SecurityIdentifier, CustomPrincipal>();
    }
}
