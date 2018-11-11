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
        void CheckLevel();

        [OperationContract]
        bool CreateFolder(string foldername);

        [OperationContract]
        bool CreateFile(string fileName);

        [OperationContract]
        bool ModifyFolderName(string folderName, string newName);

        [OperationContract]
        bool ModifyFile(string FileName);

        [OperationContract]
        bool Read(string FileName);

        [OperationContract]
        bool DeleteFolder(string folderName);

        [OperationContract]
        bool DeleteFile(string fileName);
    }
}
