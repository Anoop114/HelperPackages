#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace HelperTools.Scene
{
    /// <summary>
    /// Hold scene reference in editor and give the name of that scene if needed.
    /// </summary>
    [System.Serializable]
    public class SceneField
    {

        [SerializeField] private Object mSceneAsset;

        [SerializeField] private string mSceneName = "";
        public string SceneName => mSceneName;

        public static implicit operator string( SceneField sceneField )
        {
            return sceneField.SceneName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            var sceneAsset = property.FindPropertyRelative("mSceneAsset");
            var sceneName = property.FindPropertyRelative("mSceneName");
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            if (sceneAsset != null)
            {
                sceneAsset.objectReferenceValue = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false); 

                if( sceneAsset.objectReferenceValue != null )
                {
                    sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset)?.name;
                }
            }
            EditorGUI.EndProperty();
        }
    }
#endif
}