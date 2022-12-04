using UnityEngine;

namespace GameManager.SavesManagement
{
    public static class SavePath
    {
        public static string Path => Application.persistentDataPath;
    }
}