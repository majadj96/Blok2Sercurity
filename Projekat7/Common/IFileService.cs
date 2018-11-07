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
        void CreateFolder(string folderName);

        [OperationContract]
        void CreateFile(string fileName);

        [OperationContract]
        void ModifyFolderName(string folderName,string newName);

        [OperationContract]
        void ModifyFile();

        [OperationContract]
        void Read();

        [OperationContract]
        void DeleteFolder(string folderName);

        [OperationContract]
        void DeleteFile(string fileName);
    }
}
