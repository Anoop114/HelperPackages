#if UNITY_EDITOR
using UnityEditor;
using static MInspector.VUtils;


namespace MInspector
{
    [FilePath("Library/mInspector State.asset", FilePathAttribute.Location.ProjectFolder)]
    public class MInspectorState : ScriptableSingleton<MInspectorState>
    {

        public SerializableDictionary<string, AttributesState> attributeStates_byScriptName = new();

        [System.Serializable]
        public class AttributesState
        {
            public SerializableDictionary<string, int> selectedSubtabIndexes_byTabPath = new();
            public SerializableDictionary<string, bool> isExpandeds_byFoldoutPath = new();
            public SerializableDictionary<string, bool> isExpandeds_byButtonPath = new();

        }






        public SerializableDictionary<int, ItemState> itemStates_byItemId = new();

        [System.Serializable]
        public class ItemState
        {
            public string _name;
            public string sceneGameObjectIconName;
        }






        public static void Clear()
        {
            instance.attributeStates_byScriptName.Clear();
            instance.itemStates_byItemId.Clear();

        }

        public static void Save() => instance.Save(true);

    }
}
#endif