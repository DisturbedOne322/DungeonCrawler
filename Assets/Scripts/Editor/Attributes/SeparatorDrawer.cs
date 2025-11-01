using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SeparatorAttribute))]
public class SeparatorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var sep = (SeparatorAttribute)attribute;

        var lineRect = new Rect(position.x, position.y - sep.Padding - sep.Thickness, position.width, sep.Thickness);

        EditorGUI.DrawRect(lineRect, sep.Color);

        position.y += 0;
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var sep = (SeparatorAttribute)attribute;
        return EditorGUI.GetPropertyHeight(property, label, true) + sep.Thickness + sep.Padding;
    }
}