using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]

    public interface ISync
    {
        [OperationContract]
        Dictionary<string, List<string>> SetDictionary();

        [OperationContract]
        string GetPort(string ip);

        [OperationContract]
        bool isAlive();

    }
}
