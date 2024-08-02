#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using static MHierarchy.VUtils;
using static MHierarchy.VGUI;



namespace MHierarchy
{
    public class MHierarchyData : ScriptableObject, ISerializationCallbackReceiver
    {

        public SerializableDictionary<string, SceneData> sceneDatas_byGuid = new SerializableDictionary<string, SceneData>();


        [System.Serializable]
        public class SceneData
        {
            public SerializableDictionary<GlobalID, GameObjectData> goDatas_byGlobalId = new SerializableDictionary<GlobalID, GameObjectData>();
        }


        [System.Serializable]
        public class GameObjectData
        {
            public int colorIndex;
            public string iconNameOrGuid = ""; // name for buildin icons, guid for custom ones

            [System.NonSerialized] // set in GetGameObjectData
            public SceneData sceneData;

        }

        public void OnBeforeSerialize() => MHierarchy.firstDataCacheLayer.Clear();
        public void OnAfterDeserialize() => MHierarchy.firstDataCacheLayer.Clear();





        [CustomEditor(typeof(MHierarchyData))]
        class Editor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                var style = EditorStyles.label;
                style.wordWrap = true;


                SetGUIEnabled(false);
                BeginIndent(0);

                Space(10);
                EditorGUILayout.LabelField("This file contains data about which icons and colors are assigned to objects", style);

                Space(6);
                GUILayout.Label("If there are multiple people working on the project, you might want to store this data in scenes to avoid merge conflicts. To do that, create a script that inherits from MHierarchy.MHierarchyDataComponent and add it to any object in the scene", style);

                EndIndent(10);
                ResetGUIEnabled();

            }
        }


    }
}
#endif