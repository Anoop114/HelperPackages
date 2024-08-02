#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using static MHierarchy.MHierarchyData;
using static MHierarchy.VUtils;
using static MHierarchy.VGUI;



namespace MHierarchy
{
    [ExecuteInEditMode]
    public abstract class MHierarchyDataComponent : MonoBehaviour, ISerializationCallbackReceiver
    {
        public void Awake()
        {
            void register()
            {
                MHierarchy.dataComponents_byScene[gameObject.scene] = this;
            }
            void handleSceneDuplication()
            {
                if (sceneData == null) return;
                if (!sceneData.goDatas_byGlobalId.Any()) return;


                var curSceneGuid = gameObject.scene.path.ToGuid();
                var dataSceneGuid = sceneData.goDatas_byGlobalId.Keys.First().guid;

                if (curSceneGuid == dataSceneGuid) return;


                var newDic = new SerializableDictionary<GlobalID, GameObjectData>();

                foreach (var kvp in sceneData.goDatas_byGlobalId)
                    newDic[new GlobalID(kvp.Key.ToString().Replace(dataSceneGuid, curSceneGuid))] = kvp.Value;


                sceneData.goDatas_byGlobalId = newDic;


                EditorSceneManager.MarkSceneDirty(gameObject.scene);
                EditorSceneManager.SaveScene(gameObject.scene);

            }

            register();
            handleSceneDuplication();

        }

        public SceneData sceneData;


        public void OnBeforeSerialize() => MHierarchy.firstDataCacheLayer.Clear();
        public void OnAfterDeserialize() => MHierarchy.firstDataCacheLayer.Clear();



        [CustomEditor(typeof(MHierarchyDataComponent), true)]
        class Editor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                var style = EditorStyles.label;
                style.wordWrap = true;


                SetGUIEnabled(false);
                BeginIndent(0);

                Space(4);
                EditorGUILayout.LabelField("This component stores mHierarchy icons and colors that are assigned to objects in this scene", style);

                Space(2);

                EndIndent(10);
                ResetGUIEnabled();

            }
        }

    }
}
#endif