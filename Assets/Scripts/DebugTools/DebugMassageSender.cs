using UnityEngine;

namespace DebugTools
{
    public static class DebugMassageSender
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