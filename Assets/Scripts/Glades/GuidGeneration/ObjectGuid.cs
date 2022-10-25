using UnityEditor;
using UnityEngine;

namespace Glades.GuidGeneration
{
    public class ObjectGuid : MonoBehaviour
    {
        public string Guid { get; private set; }
        private void Reset()
        {
            Guid = GUID.Generate().ToString();
        }
    }
}