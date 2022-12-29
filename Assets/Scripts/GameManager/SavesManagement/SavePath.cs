using UnityEngine;

namespace GameManager.SavesManagement
{
    /// <summary>
    /// A static class that represents a permanent save path in the device.
    /// </summary>
    public static class SavePath
    {
        public static string Path => Application.persistentDataPath;
    }
}