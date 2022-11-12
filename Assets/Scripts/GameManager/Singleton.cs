using UnityEngine;

namespace GameManager
{
    public abstract class Singleton<T> : MonoBehaviour where T :Singleton<T>
    {
        public static T Instance { get; private set; }
        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = (T)this; 
            } 
        }
    }
}