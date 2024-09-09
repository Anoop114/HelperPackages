#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Type = System.Type;
using static MInspector.MUtils;
using static MInspector.MGUI;



namespace MInspector
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class MInspectorData : ScriptableObject, ISerializationCallbackReceiver
    {

        public List<Item> items = new();

        [System.Serializable]
        public class Item
        {
            public GlobalID globalId;


            public Type type => Type.GetType(_typeString) ?? typeof(DefaultAsset);
            public string _typeString;

            public Object obj
            {
                get
                {
                    if (_obj == null && !isSceneGameObject)
                        _obj = globalId.GetObject();

                    return _obj;

                    // updating scene objects here using globalId.GetObject() could cause performance issues on large scenes
                    // so instead they are batch updated in MInspector.UpdateBookmarkedObjectsForScene()

                }
            }
            public Object _obj;


            public bool isSceneGameObject;
            public bool isAsset;


            public bool isLoadable => obj != null;

            public bool isDeleted
            {
                get
                {
                    if (!isSceneGameObject)
                        return !isLoadable;

                    if (isLoadable)
                        return false;

                    if (!AssetDatabase.LoadAssetAtPath<SceneAsset>(globalId.guid.ToPath()))
                        return true;

                    for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                        if (EditorSceneManager.GetSceneAt(i).path == globalId.guid.ToPath())
                            return true;

                    return false;

                }
            }

            public string assetPath => globalId.guid.ToPath();


            public Item(Object o)
            {
                globalId = o.GetGlobalID();

                id = Random.value.GetHashCode();

                isSceneGameObject = o is GameObject go && go.scene.rootCount != 0;
                isAsset = !isSceneGameObject;

                _typeString = o.GetType().AssemblyQualifiedName;

                _name = o.name;

                _obj = o;

            }



            public float width => MInspectorNavbar.expandedItemWidth;




            public string name
            {
                get
                {
                    if (!isLoadable) return _name;

                    if (assetPath.GetExtension() == ".cs")
                        _name = obj.name.Decamelcase();
                    else
                        _name = obj.name;

                    return _name;

                }
            }
            public string _name { get => state._name; set => state._name = value; }

            public string sceneGameObjectIconName { get => state.sceneGameObjectIconName; set => state.sceneGameObjectIconName = value; }



            public MInspectorState.ItemState state
            {
                get
                {
                    if (!MInspectorState.instance.itemStates_byItemId.ContainsKey(id))
                        MInspectorState.instance.itemStates_byItemId[id] = new MInspectorState.ItemState();

                    return MInspectorState.instance.itemStates_byItemId[id];

                }
            }

            public int id;

        }



        public void OnAfterDeserialize() => MInspectorNavbar.repaintNeededAfterUndoRedo = true;
        public void OnBeforeSerialize() { }









        [CustomEditor(typeof(MInspectorData))]
        class Editor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                var style = new GUIStyle(EditorStyles.label) { wordWrap = true };


                SetGUIEnabled(false);
                BeginIndent(0);

                Space(10);
                EditorGUILayout.LabelField("This file stores bookmarks from mInspector's navigation bar", style);

                EndIndent(10);
                ResetGUIEnabled();

                // Space(15);
                // base.OnInspectorGUI();

            }
        }


    }
}
#endif