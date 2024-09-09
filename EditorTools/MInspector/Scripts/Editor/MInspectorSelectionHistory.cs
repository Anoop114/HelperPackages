#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static MInspector.MGUI;



namespace MInspector
{
    public class MInspectorSelectionHistory : ScriptableSingleton<MInspectorSelectionHistory>
    {
        public void MoveBack()
        {
            var prevState = prevStates.Last();

            instance.RecordUndo("MInspectorSelectionHistory.MoveBack");

            prevStates.Remove(prevState);
            nextStates.Add(curState);
            curState = prevState;


            ignoreThisSelectionChange = true;

            prevState.selectedObjects.ToArray().SelectInInspector(frameInHierarchy: false, frameInProject: false);

        }
        public void MoveForward()
        {
            var nextState = nextStates.Last();

            instance.RecordUndo("MInspectorSelectionHistory.MoveForward");

            nextStates.Remove(nextState);
            prevStates.Add(curState);
            curState = nextState;


            ignoreThisSelectionChange = true;

            nextState.selectedObjects.ToArray().SelectInInspector(frameInHierarchy: false, frameInProject: false);

        }

        static void OnSelectionChange()
        {
            if (ignoreThisSelectionChange) { ignoreThisSelectionChange = false; return; }

            if (curEvent.modifiers == EventModifiers.Command && curEvent.keyCode == KeyCode.Z) return;
            if (curEvent.modifiers == (EventModifiers.Command | EventModifiers.Shift) && curEvent.keyCode == KeyCode.Z) return;

            if (curEvent.modifiers == EventModifiers.Control && curEvent.keyCode == KeyCode.Z) return;
            if (curEvent.modifiers == EventModifiers.Control && curEvent.keyCode == KeyCode.Y) return;


            instance.RecordUndo(Undo.GetCurrentGroupName());

            instance.prevStates.Add(instance.curState);
            instance.curState = new SelectionState() { selectedObjects = Selection.objects.ToList() };
            instance.nextStates.Clear();

            if (instance.prevStates.Count > 50)
                instance.prevStates.RemoveAt(0);

        }

        static bool ignoreThisSelectionChange;


        public List<SelectionState> prevStates = new();
        public List<SelectionState> nextStates = new();
        public SelectionState curState;

        [System.Serializable]
        public class SelectionState { public List<Object> selectedObjects = new(); }



        [InitializeOnLoadMethod]
        static void Init()
        {
            Selection.selectionChanged -= OnSelectionChange;
            Selection.selectionChanged += OnSelectionChange;


            // var globalEventHandler = typeof(EditorApplication).GetFieldValue<EditorApplication.CallbackFunction>("globalEventHandler");
            // typeof(EditorApplication).SetFieldValue("globalEventHandler", ClearHistories + (globalEventHandler - ClearHistories));


            instance.curState = new SelectionState() { selectedObjects = Selection.objects.ToList() };

        }





        // static void ClearHistories() // just for debug
        // {
        //     if (curEvent.holdingAnyModifierKey) return;
        //     if (!curEvent.isKeyDown || curEvent.keyCode != KeyCode.Y) return;

        //     MInspectorSelectionHistory.instance.prevStates.Clear();
        //     MInspectorSelectionHistory.instance.nextStates.Clear();

        //     Undo.ClearAll();

        //     MInspectorMenu.RepaintInspectors();

        // }



    }
}
#endif