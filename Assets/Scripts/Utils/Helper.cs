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
    }

}