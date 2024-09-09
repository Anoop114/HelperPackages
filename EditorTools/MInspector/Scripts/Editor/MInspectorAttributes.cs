using System;
using UnityEngine;


namespace MInspector
{

    public class FoldoutAttribute : Attribute
    {
        public string name;

        public FoldoutAttribute(string name) => this.name = name;

    }
    public class EndFoldoutAttribute : Attribute { }



    public class TabAttribute : Attribute
    {
        public string name;

        public TabAttribute(string name) => this.name = name;

    }
    public class EndTabAttribute : Attribute { }



    public class ButtonAttribute : Attribute
    {
        public string name = "";
        public int size = 30;
        public int space = 0;
        public string color = "Grey";

        public ButtonAttribute() => this.name = "";
        public ButtonAttribute(string name) => this.name = name;

    }



    public class VariantsAttribute : PropertyAttribute
    {
        public object[] variants;

        public VariantsAttribute(params object[] variants) => this.variants = variants;

    }



    public abstract class IfAttribute : Attribute
    {
        public string variableName;
        public object variableValue;

#if UNITY_EDITOR
        public bool Evaluate(object target)
        {
            if (target.GetType().GetFieldInfo(variableName) == null &&
                target.GetType().GetPropertyInfo(variableName) == null)
                return false;

            var curValue = target.GetMemberValue(variableName);

            return object.Equals(curValue, variableValue);

        }
#endif

        public IfAttribute(string boolName) { this.variableName = boolName; this.variableValue = true; }
        public IfAttribute(string variableName, object variableValue) { this.variableName = variableName; this.variableValue = variableValue; }

    }
    public class EndIfAttribute : Attribute { }

    public class HideIfAttribute : IfAttribute
    {
        public HideIfAttribute(string boolName) : base(boolName) { }
        public HideIfAttribute(string variableName, object variableValue) : base(variableName, variableValue) { }
    }
    public class ShowIfAttribute : IfAttribute
    {
        public ShowIfAttribute(string boolName) : base(boolName) { }
        public ShowIfAttribute(string variableName, object variableValue) : base(variableName, variableValue) { }
    }
    public class EnableIfAttribute : IfAttribute
    {
        public EnableIfAttribute(string boolName) : base(boolName) { }
        public EnableIfAttribute(string variableName, object variableValue) : base(variableName, variableValue) { }
    }
    public class DisableIfAttribute : IfAttribute
    {
        public DisableIfAttribute(string boolName) : base(boolName) { }
        public DisableIfAttribute(string variableName, object variableValue) : base(variableName, variableValue) { }
    }



    public class ReadOnlyAttribute : Attribute { }



    public class ShowInInspectorAttribute : Attribute { }


    [AttributeUsage(AttributeTargets.Method)]
    public class OnValueChangedAttribute : Attribute
    {
        public string[] variableNames;

        public OnValueChangedAttribute(string variableName) => this.variableNames = new[] { variableName };
        public OnValueChangedAttribute(params string[] variableNames) => this.variableNames = variableNames;
    }

    public class ButtonSizeAttribute : System.Attribute
    {
        public float size;

        public ButtonSizeAttribute(float size) => this.size = size;
    }

    public class ButtonSpaceAttribute : System.Attribute
    {
        public float space;

        public ButtonSpaceAttribute() => this.space = 10;
        public ButtonSpaceAttribute(float space) => this.space = space;
    }



    public class RangeResettable : PropertyAttribute
    {
        public float min;
        public float max;

        public RangeResettable(float min, float max) { this.min = min; this.max = max; }
    }








}