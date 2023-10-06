using UnityEngine;

namespace HelperFunction.HelperFunctions.Utilities
{
    public static class TransformChild
    {
        public static void DeleteAllChild(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }
    }
}