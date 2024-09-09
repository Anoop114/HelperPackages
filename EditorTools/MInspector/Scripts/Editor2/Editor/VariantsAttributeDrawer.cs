#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MAttribute
{
    [CustomPropertyDrawer(typeof(VariantsAttribute))]
    public class VariantsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            var variants = ((VariantsAttribute)attribute).variants;


            EditorGUI.BeginProperty(rect, label, prop);

            var iCur = prop.hasMultipleDifferentValues ? -1 : variants.ToList().IndexOf(prop.GetBoxedValue());

            var iNew = EditorGUI.IntPopup(rect, label.text, iCur, variants.Select(r => r.ToString()).ToArray(), Enumerable.Range(0, variants.Length).ToArray());

            if (iNew != -1)
                prop.SetBoxedValue(variants[iNew]);
            else if (!prop.hasMultipleDifferentValues)
                prop.SetBoxedValue(variants[0]);

            EditorGUI.EndProperty();

        }
    }
}
#endif