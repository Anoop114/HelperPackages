#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using static MFolders.VUtils;
using static MFolders.VGUI;


namespace MFolders
{
    public class MFoldersData : ScriptableObject
    {
        public SerializableDictionary<string, FolderData> folderDatas_byGuid = new SerializableDictionary<string, FolderData>();

        [System.Serializable]
        public class FolderData
        {
            public int colorIndex;
            public string iconNameOrGuid = "";

        }




        [CustomEditor(typeof(MFoldersData))]
        class Editor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                var style = EditorStyles.label;
                style.wordWrap = true;


                void normal()
                {
                    if (storeDataInMetaFiles) return;

                    SetGUIEnabled(false);
                    BeginIndent(0);

                    Space(10);
                    EditorGUILayout.LabelField("This file contains data about which icons and colors are assigned to folders", style);

                    Space(6);
                    GUILayout.Label("If there are multiple people working on the project, you might want to store this data in .meta files of folders to avoid merge conflicts. To do that, click the ... button at the top right corner and click 'Store data in .meta files of folders' ", style);

                    EndIndent(10);
                    ResetGUIEnabled();
                }
                void meta()
                {
                    if (!storeDataInMetaFiles) return;

                    SetGUIEnabled(false);
                    BeginIndent(0);

                    Space(10);
                    EditorGUILayout.LabelField("mFolders currently stores data in .meta files of folders", style);

                    Space(6);
                    GUILayout.Label("If you want this data to be stored in this file, click the ... button at the top right corner and click 'Store data in .meta files' ", style);

                    EndIndent(10);
                    ResetGUIEnabled();
                }

                normal();
                meta();


            }
        }

        public static bool storeDataInMetaFiles { get => EditorPrefs.GetBool("mFolders-storeDataInMetaFilesEnabled", false); set => EditorPrefs.SetBool("mFolders-storeDataInMetaFilesEnabled", value); }

        [ContextMenu("Store data in .meta files of folders")]
        public void MigrateDataBetweenMetaFilesAndSO() => storeDataInMetaFiles = !storeDataInMetaFiles; // todo


    }
}
#endif