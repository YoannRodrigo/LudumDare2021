using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace post
{
    [CustomPropertyDrawer(typeof(ContentData))]
    public class ContentDataEditor : PropertyDrawer
    {
        private SerializedProperty _text;
        private SerializedProperty _image;
        private SerializedProperty _model3D;

        private void OnEnable()
        {
            EditorGUILayout.PropertyField(_text, new GUIContent("Text"));
            EditorGUILayout.PropertyField(_image, new GUIContent("Image"));
            EditorGUILayout.PropertyField(_model3D, new GUIContent("Model3D"));
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUIStyle style = new GUIStyle();
            EditorStyles.textField.wordWrap = true;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.cyan;
            EditorGUI.BeginProperty(position, label , property);
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Content",style);
            SerializedProperty textProperty = property.FindPropertyRelative(nameof(ContentData.Text));
            textProperty.stringValue = EditorGUILayout.TextArea(textProperty.stringValue,GUILayout.ExpandHeight(true),GUILayout.MinHeight(80));
            EditorStyles.textField.wordWrap = true;
            SerializedProperty imageProperty = property.FindPropertyRelative(nameof(ContentData.Image));
            SerializedProperty model3DProperty = property.FindPropertyRelative(nameof(ContentData.model3D));
            EditorGUILayout.PropertyField(imageProperty);
            EditorGUILayout.PropertyField(model3DProperty);
            EditorGUI.EndProperty();
            EditorGUI.indentLevel--;
            EditorGUILayout.Separator();
        }
    }
}