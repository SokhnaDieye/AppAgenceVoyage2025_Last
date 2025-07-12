using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFile.Interface
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        bool UploadToTempFolder(byte[] fileBytes, string fileName, string pathFolder);

        [OperationContract]
        byte[] GetFileFromFolder(string fileName);

        [OperationContract]
        bool TableauDeByteVersFicher(string pathDir, string fileName, byte[] fileData);

        [OperationContract]
        byte[] FichierVersTableauDeByte(string filePath);

        [OperationContract]
        bool FileExists(string path, string partialName);
    }

}
