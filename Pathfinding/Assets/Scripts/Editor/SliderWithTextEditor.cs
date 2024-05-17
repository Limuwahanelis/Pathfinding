using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SliderWithTextInput))]
public class SliderWithTextEditor : Editor
{
    private SerializedProperty _defaultValue;
    private SerializedProperty _maxValue;
    private SerializedProperty _minValue;
    private void OnEnable()
    {
        _defaultValue = serializedObject.FindProperty("_defaultValue");
        _maxValue = serializedObject.FindProperty("_maxValue");
        _minValue = serializedObject.FindProperty("_minValue");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.Slider(_defaultValue, _minValue.floatValue, _maxValue.floatValue);
        serializedObject.ApplyModifiedProperties();
    }
}
