#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using static MInspector.MUtils;
using static MInspector.MGUI;



namespace MInspector
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>), true)]
    public class SerializedDictionaryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            var indentedRect = EditorGUI.IndentedRect(rect);

            void header()
            {
                var headerRect = indentedRect.SetHeight(EditorGUIUtility.singleLineHeight);

                void foldout()
                {
                    var fullHeaderRect = headerRect.MoveX(3).AddWidthFromRight(17);

                    if (fullHeaderRect.IsHovered())
                        fullHeaderRect.Draw(Greyscale(1, .07f));

                    SetGUIColor(Color.clear);
                    SetGUIEnabled(true);

                    if (GUI.Button(fullHeaderRect.AddWidth(-50), ""))
                        prop.isExpanded = !prop.isExpanded;

                    ResetGUIColor();
                    ResetGUIEnabled();



                    var triangleRect = rect.SetHeight(EditorGUIUtility.singleLineHeight);

                    SetGUIEnabled(true);

                    EditorGUI.Foldout(triangleRect, prop.isExpanded, "");

                    ResetGUIEnabled();


                }
                void label()
                {
                    SetLabelBold();
                    SetLabelFontSize(12);
                    SetGUIColor(Greyscale(.9f));
                    SetGUIEnabled(true);

                    GUI.Label(headerRect, prop.displayName);

                    ResetGUIEnabled();
                    ResetGUIColor();
                    ResetLabelStyle();

                }
                void count()
                {
                    kvpsProp.arraySize = EditorGUI.DelayedIntField(headerRect.SetWidthFromRight(48 + EditorGUI.indentLevel * 15), kvpsProp.arraySize);
                }
                void repeatedKeysWarning()
                {
                    if (!curEvent.isRepaint) return;


                    var hasRepeatedKeys = false;
                    var hasNullKeys = false;

                    for (int i = 0; i < kvpsProp.arraySize; i++)
                    {
                        hasRepeatedKeys |= kvpsProp.GetArrayElementAtIndex(i).FindPropertyRelative("isKeyRepeated").boolValue;
                        hasNullKeys |= kvpsProp.GetArrayElementAtIndex(i).FindPropertyRelative("isKeyNull").boolValue;
                    }

                    if (!hasRepeatedKeys && !hasNullKeys) return;



                    var warningTextRect = headerRect.AddWidthFromRight(-prop.displayName.GetLabelWidth(isBold: true));
                    var warningIconRect = warningTextRect.SetHeightFromMid(20).SetWidth(20);

                    var warningText = (hasRepeatedKeys && hasNullKeys) ? "Repeated and null keys"
                                                     : hasRepeatedKeys ? "Repeated keys"
                                                         : hasNullKeys ? "Null keys" : "";



                    GUI.Label(warningIconRect, EditorGUIUtility.IconContent("Warning"));


                    SetGUIColor(new Color(1, .9f, .03f) * 1.1f);

                    GUI.Label(warningTextRect.MoveX(16), warningText);

                    ResetGUIColor();

                }

                foldout();
                label();
                count();
                repeatedKeysWarning();

            }
            void list_()
            {
                if (!prop.isExpanded) return;

                SetupList(prop);

                list.DoList(indentedRect.AddHeightFromBottom(-EditorGUIUtility.singleLineHeight - 3));
            }


            SetupProps(prop);

            header();
            list_();

        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            SetupProps(prop);

            var height = EditorGUIUtility.singleLineHeight;

            if (prop.isExpanded)
            {
                SetupList(prop);
                height += list.GetHeight() + 3;
            }

            return height;
        }

        float GetListElementHeight(int index)
        {
            var kvpProp = kvpsProp.GetArrayElementAtIndex(index);
            var keyProp = kvpProp.FindPropertyRelative("Key");
            var valueProp = kvpProp.FindPropertyRelative("Value");

            float propHeight(SerializedProperty prop)
            {
                // var height = typeof(Editor).Assembly.GetType("UnityEditor.ScriptAttributeUtility").InvokeMethod("GetHandler", prop).InvokeMethod<float>("GetHeight", prop, GUIContent.none, true);
                var height = EditorGUI.GetPropertyHeight(prop);

                if (!IsSingleLine(prop) && prop.type != "EventReference")
                    height -= 10;

                return height;

            }

            return Mathf.Max(propHeight(keyProp), propHeight(valueProp));

        }

        void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            Rect keyRect;
            Rect valueRect;
            Rect dividerRect;

            var kvpProp = kvpsProp.GetArrayElementAtIndex(index);
            var keyProp = kvpProp.FindPropertyRelative("Key");
            var valueProp = kvpProp.FindPropertyRelative("Value");

            void drawProp(Rect rect, SerializedProperty prop)
            {
                if (IsSingleLine(prop)) { EditorGUI.PropertyField(rect.SetHeight(EditorGUIUtility.singleLineHeight), prop, GUIContent.none); return; }


                prop.isExpanded = true;

                GUI.BeginGroup(rect);

                if (prop.type == "EventReference") // don't hide first line for FMOD EventReference
                {
                    EditorGUIUtility.labelWidth = 1;
                    EditorGUI.PropertyField(rect.SetPos(0, 0), prop, GUIContent.none);
                    EditorGUIUtility.labelWidth = 0;
                }
                else
                    EditorGUI.PropertyField(rect.SetPos(0, -20), prop, true);

                GUI.EndGroup();

            }

            void rects()
            {
                var dividerWidh = 6f;

                var dividerPos = dividerPosProp.floatValue.Clamp(.2f, .8f);

                var fullRect = rect.AddWidthFromRight(-1).AddHeightFromMid(-2);

                keyRect = fullRect.SetWidth(fullRect.width * dividerPos - dividerWidh / 2);
                valueRect = fullRect.SetWidthFromRight(fullRect.width * (1 - dividerPos) - dividerWidh / 2);
                dividerRect = fullRect.MoveX(fullRect.width * dividerPos - dividerWidh / 2).SetWidth(dividerWidh).Resize(-1);

            }
            void key()
            {
                drawProp(keyRect, keyProp);

            }
            void warning()
            {
                var isKeyRepeated = kvpProp.FindPropertyRelative("isKeyRepeated").boolValue;
                var isKeyNull = kvpProp.FindPropertyRelative("isKeyNull").boolValue;

                if (!isKeyRepeated && !isKeyNull) return;


                var warningRect = keyRect.SetWidthFromRight(20).SetHeight(20).MoveY(-1);

                if (kvpProp.FindPropertyRelative("Key").propertyType == SerializedPropertyType.ObjectReference)
                    warningRect = warningRect.MoveX(-17);


                GUI.Label(warningRect, EditorGUIUtility.IconContent("Warning"));

            }
            void value()
            {
                drawProp(valueRect, valueProp);
            }
            void divider()
            {
                EditorGUIUtility.AddCursorRect(dividerRect, MouseCursor.ResizeHorizontal);

                if (!rect.IsHovered()) return;

                if (dividerRect.IsHovered())
                {
                    if (curEvent.isMouseDown)
                        isDividerDragged = true;

                    if (curEvent.isMouseUp || curEvent.isMouseMove || curEvent.isMouseLeaveWindow)
                        isDividerDragged = false;
                }

                if (isDividerDragged && curEvent.isMouseDrag)
                    dividerPosProp.floatValue += curEvent.mouseDelta.x / rect.width;

            }

            rects();
            key();
            warning();
            value();
            divider();

        }

        void DrawDictionaryIsEmpty(Rect rect) => GUI.Label(rect, "Dictionary is empty");



        IEnumerable<SerializedProperty> GetChildren(SerializedProperty prop, bool enterVisibleGrandchildren)
        {
            var startPath = prop.propertyPath;

            var enterVisibleChildren = true;

            while (prop.NextVisible(enterVisibleChildren) && prop.propertyPath.StartsWith(startPath))
            {
                yield return prop;
                enterVisibleChildren = enterVisibleGrandchildren;
            }

        }

        bool IsSingleLine(SerializedProperty prop) => prop.propertyType != SerializedPropertyType.Generic || !prop.hasVisibleChildren;



        public void SetupList(SerializedProperty prop)
        {
            if (list != null) return;

            SetupProps(prop);

            this.list = new ReorderableList(kvpsProp.serializedObject, kvpsProp, true, false, true, true);
            this.list.drawElementCallback = DrawListElement;
            this.list.elementHeightCallback = GetListElementHeight;
            this.list.drawNoneElementCallback = DrawDictionaryIsEmpty;

        }
        ReorderableList list;
        bool isDividerDragged;


        public void SetupProps(SerializedProperty prop)
        {
            if (this.prop != null) return;

            this.prop = prop;
            this.kvpsProp = prop.FindPropertyRelative("serializedKvps");
            this.dividerPosProp = prop.FindPropertyRelative("dividerPos");


        }
        SerializedProperty prop;
        SerializedProperty kvpsProp;
        SerializedProperty dividerPosProp;

    }
}
#endif