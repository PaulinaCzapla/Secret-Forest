using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameManager.SavesManagement
{
    public static class SaveSystem
    {
        public static void SaveFile<T>(string filePath, T saveData) where T : class
        {
            
            if (filePath != null)
            {
                filePath = SavePath.Path + filePath;
                File.WriteAllText(filePath, JsonConvert.SerializeObject(saveData));
            }
        }

        public static bool HasFile(string filePath)
        {
            return File.Exists(SavePath.Path + filePath);
        }
        
        public static T ReadFile<T>(string filePath) where T : class
        {
            T saveData = null;

            if (filePath != null)
            {
                filePath = SavePath.Path + filePath;
                if (File.Exists(filePath))
                {
                    JObject o = JObject.Parse(File.ReadAllText(filePath));
                    JsonSerializer serializer = new JsonSerializer();
                    saveData = (T) serializer.Deserialize(new JTokenReader(o), typeof(T));
                }
            }
            return saveData;
        }

        public static bool DeleteFile(string filePath)
        {
            filePath = SavePath.Path + filePath;
            
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}