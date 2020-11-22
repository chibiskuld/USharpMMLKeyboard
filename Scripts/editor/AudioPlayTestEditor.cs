//C# Example (LookAtPointEditor.cs)
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioPlayTest))]
[CanEditMultipleObjects]
public class AudioPlayTestEditor : Editor
{
    AudioPlayTest audioPlayTest;
    SerializedProperty note;

    void OnEnable()
    {
        audioPlayTest = (AudioPlayTest)target;
        note = serializedObject.FindProperty("note");
    }

    public override void OnInspectorGUI()
    {
        if (note != null)
        {
            EditorGUILayout.PropertyField(note, new GUIContent("Play Note:"));
        }
        if (GUILayout.Button("Play Test"))
        {

            audioPlayTest.Play();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
