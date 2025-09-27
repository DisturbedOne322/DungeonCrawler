using Gameplay.Rewards;
using UnityEditor;
using UnityEngine;

namespace Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(DropEntry))]
    public class DropEntryDrawer : PropertyDrawer
    {
        private const float LabelHeight = 14f;
        private const float FieldSpacing = 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty itemProp = property.FindPropertyRelative("_item");
            SerializedProperty weightProp = property.FindPropertyRelative("_weight");
            SerializedProperty amountProp = property.FindPropertyRelative("_amount");

            float totalWidth = position.width;
            float fieldWidth = totalWidth / 3f;
            float y = position.y;

            // Draw labels
            Rect itemLabelRect = new Rect(position.x, y, fieldWidth, LabelHeight);
            Rect weightLabelRect = new Rect(position.x + fieldWidth, y, fieldWidth, LabelHeight);
            Rect amountLabelRect = new Rect(position.x + 2 * fieldWidth, y, fieldWidth, LabelHeight);

            EditorGUI.LabelField(itemLabelRect, "Item");
            EditorGUI.LabelField(weightLabelRect, "Weight");
            EditorGUI.LabelField(amountLabelRect, "Amount");

            y += LabelHeight + FieldSpacing;

            // Draw fields
            Rect itemRect = new Rect(position.x, y, fieldWidth, position.height - LabelHeight - FieldSpacing);
            Rect weightRect = new Rect(position.x + fieldWidth, y, fieldWidth, position.height - LabelHeight - FieldSpacing);
            Rect amountRect = new Rect(position.x + 2 * fieldWidth, y, fieldWidth, position.height - LabelHeight - FieldSpacing);

            EditorGUI.PropertyField(itemRect, itemProp, GUIContent.none);
            EditorGUI.PropertyField(weightRect, weightProp, GUIContent.none);
            EditorGUI.PropertyField(amountRect, amountProp, GUIContent.none);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Provide extra height for labels
            return base.GetPropertyHeight(property, label) + LabelHeight + FieldSpacing;
        }
    }
}