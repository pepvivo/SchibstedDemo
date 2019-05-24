using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace WebApplication.Modules
{
    public interface IIOFile
    {
        T ReadFromJsonFile<T>(string fileName);
        void WriteToJsonFile<T>(T obj, string fileName);
        string[] GetDirectoryFilenames(string filePath,
                                        string fileExtension,
                                        SearchOption searchOption = SearchOption.TopDirectoryOnly);
        void CreateDirectory(string directoryPath);
        bool Exists(string fileName);
        void Delete(string fileName);
    }

    public class IOFile : IIOFile
    {
        public T ReadFromJsonFile<T>(string fileName) 
        {
            try
            {
                string fileData = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<T>(fileData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void WriteToJsonFile<T>(T obj, string fileName)
        {
            try
            {
                string userJson = JsonConvert.SerializeObject(obj);
                File.WriteAllText(fileName, userJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[] GetDirectoryFilenames(string filePath, 
                                                    string fileExtension, 
                                                    SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            try
            { 
                return Directory.GetFiles(filePath, $"*.{fileExtension}", searchOption);
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        public void CreateDirectory( string directoryPath)
        {            
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(string fileName)
        {
            try
            {
                return File.Exists(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}