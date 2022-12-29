using UnityEngine;

namespace DebugTools
{
    /// <summary>
    /// A class that exists for debugging reasons. It logs messages in the console.
    /// </summary>
    public static class DebugMessageSender
    {
        public static bool IsActive = true;
        
        public static void SendDebugMessage(string message)
        {
            if (!IsActive)
                return;
            
            Debug.Log(message);
        }
    }
}