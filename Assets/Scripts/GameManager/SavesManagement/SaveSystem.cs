using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameManager.SavesManagement
{
    /// <summary>
    /// A generic static class that is responsible for managing files. 
    /// </summary>
    public static class SaveSystem
    {
        /// <summary>
        /// Saves an object to the file with given name.
        /// </summary>
        public static void SaveFile<T>(string fileName, T saveData) where T : class
        {
            if (fileName != null)
            {
                fileName = SavePath.Path + fileName;
                File.WriteAllText(fileName, JsonConvert.SerializeObject(saveData));
            }
        }
        /// <summary>
        /// Checks if file with given name exists.
        /// </summary>
        public static bool HasFile(string fileName)
        {
            return File.Exists(SavePath.Path + fileName);
        }
        /// <summary>
        /// Reads data from file with a given name.
        /// </summary>
        public static T ReadFile<T>(string fileName) where T : class
        {
            T saveData = null;

            if (fileName != null)
            {
                fileName = SavePath.Path + fileName;
                if (File.Exists(fileName))
                {
                    JObject o = JObject.Parse(File.ReadAllText(fileName));
                    JsonSerializer serializer = new JsonSerializer();
                    saveData = (T) serializer.Deserialize(new JTokenReader(o), typeof(T));
                }
            }
            return saveData;
        }

        /// <summary>
        /// Removes file with a given name.
        /// </summary>
        public static bool DeleteFile(string fileName)
        {
            fileName = SavePath.Path + fileName;
            
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                return true;
            }

            return false;
        }
    }
}