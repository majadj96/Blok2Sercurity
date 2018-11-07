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
        void CreateFile(string fileName);

        [OperationContract]
        void ModifyFolderName(string folderName, string newName);

        [OperationContract]
        void ModifyFile(string FileName);

        [OperationContract]
        void Read(string FileName);

        [OperationContract]
        void DeleteFolder(string folderName);

        [OperationContract]
        void DeleteFile(string fileName);
    }
}
