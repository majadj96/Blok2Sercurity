﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IRBACChange
    {
        [OperationContract]
        void Change(Dictionary<string, List<string>> GroupsAndPermissionsDict);


        [OperationContract]
        Dictionary<string, List<string>> GetDictionary();
    }
}
