// File: Service1.svc.cs
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace ServiceFile
{
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return $"Vous avez entré : {value}";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
                throw new ArgumentNullException(nameof(composite));

            if (composite.BoolValue)
                composite.StringValue += " Suffix";

            return composite;
        }

        public bool UploadToTempFolder(byte[] fileBytes, string fileName, string pathFolder)
        {
            try
            {
                if (!Directory.Exists(pathFolder))
                    Directory.CreateDirectory(pathFolder);

                string fullPath = Path.Combine(pathFolder, fileName);
                File.WriteAllBytes(fullPath, fileBytes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public byte[] GetFileFromFolder(string fileName)
        {
            string folderPath = ConfigurationManager.AppSettings["FileUploadPath"];
            string fullPath = Path.Combine(folderPath, fileName);

            if (!File.Exists(fullPath)) return null;
            return File.ReadAllBytes(fullPath);
        }

        public bool TableauDeByteVersFicher(string pathDir, string fileName, byte[] fileData)
        {
            try
            {
                if (!Directory.Exists(pathDir))
                    Directory.CreateDirectory(pathDir);

                string filePath = Path.Combine(pathDir, fileName);
                File.WriteAllBytes(filePath, fileData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public byte[] FichierVersTableauDeByte(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            return File.ReadAllBytes(filePath);
        }

        public bool FileExists(string path, string partialName)
        {
            if (!Directory.Exists(path)) return false;

            return Directory.GetFiles(path, "*.zip")
                            .Any(file => Path.GetFileName(file).StartsWith(partialName));
        }
    }
}
