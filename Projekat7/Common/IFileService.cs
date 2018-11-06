using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
   public interface IFileService
    {
        [OperationContract]
        void CreateFolder(string foldername);

        [OperationContract]
        void CreateFile();

        [OperationContract]
        void ModifyFolderName();

        [OperationContract]
        void ModifyFile();

        [OperationContract]
        void Read();

        [OperationContract]
        void DeleteFolder();

        [OperationContract]
        void DeleteFile();
    }
}
