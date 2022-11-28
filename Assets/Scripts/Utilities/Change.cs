using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// An auxiliary class containing methods that help change only one of the structure values.
    /// </summary>
    public static class Change {
 
        public static Vector2 X(this Vector2 v, float x)
        {
            v.x = x;
            return v;
        }
 
        public static Vector2 Y(this Vector2 v, float y)
        {
            v.y = y;
            return v;
        }
 
        public static Vector3 X(this Vector3 v, float x)
        {
            v.x = x;
            return v;
        }
 
        public static Vector3 Y(this Vector3 v, float y)
        {
            v.y = y;
            return v;
        }
 
        public static Vector3 Z(this Vector3 v, float z)
        {
            v.z = z;
            return v;
        }
 
        public static Color R(this Color c, float r)
        {
            c.r = r;
            return c;
        }
 
        public static Color G(this Color c, float g)
        {
            c.g = g;
            return c;
        }
 
        public static Color B(this Color c, float b)
        {
            c.b = b;
            return c;
        }
 
        public static Color A(this Color c, float a)
        {
            c.a = a;
            return c;
        }
    }
}