using Helpers;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Renders key value pairs more concisely
/// </summary>
[CustomPropertyDrawer(typeof(GenericDictionary<,>.KeyValuePair))]
public class GenericDictionaryKVPPropertyDrawer : PropertyDrawer {
    static float lineHeight = EditorGUIUtility.singleLineHeight;
    static float vertSpace = EditorGUIUtility.standardVerticalSpacing;
    const float keyWidth = 250;
    const float valWidth = 50;

    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label) {
        var key = property.FindPropertyRelative("Key");
        var value = property.FindPropertyRelative("Value");
        Rect keyPos = new Rect(pos.xMin, pos.yMin, keyWidth, lineHeight);
        Rect valPos = new Rect(pos.xMin + keyWidth + vertSpace, pos.yMin, valWidth, lineHeight);

        EditorGUI.PropertyField(keyPos, key, GUIContent.none, true);
        EditorGUI.PropertyField(valPos, value, GUIContent.none, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return lineHeight + vertSpace;
    }
}