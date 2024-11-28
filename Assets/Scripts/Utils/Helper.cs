using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nowhere.Helper
{
    public static class Helper
    {
        public static Vector3 WithoutY(this Vector3 origin)
        {
            return new Vector3(origin.x, 0, origin.z);
        }

        public static void Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = Random.Range(0, n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static void Shuffle<T>(this List<T> array)
        {
            int n = array.Count;
            while (n > 1)
            {
                int k = Random.Range(0, n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

}