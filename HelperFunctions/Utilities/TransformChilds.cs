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

    public static class GetMainParent
    {
        public static Transform FindParent(this Transform t)
        {
            var temp = t;
            while (true)
            {
                if (temp.parent == null) return temp;
                temp = temp.parent;
            }
        }
    }
}