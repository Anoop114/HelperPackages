using UnityEngine;

namespace HelperFunction.Utilities
{
    public static class TransformChild
    {
        /// <summary>
        /// Delete all Children of the transform.
        /// </summary>
        public static void DeleteAllChild(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }
    }

    public static class GetMainParent
    {
        /// <summary>
        /// find the main parent of the given transform.
        /// </summary>
        /// <returns> Transform of the top most parent of that gameObject. </returns>
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