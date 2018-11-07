using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ILog
    {
        [OperationContract]
        void Logging(string methoodName,string userName);

        [OperationContract]
        void LoggingFail(string methodName, string userName,string reason);

         [OperationContract]
        void LoggingChange(string userName);


    }
}
