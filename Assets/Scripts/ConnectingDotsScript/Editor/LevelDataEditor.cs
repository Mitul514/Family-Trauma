using UnityEditor;
using UnityEngine;

/// <summary>
/// Defines the <see cref="LevelDataEditor" />.
/// </summary>
[CustomEditor(typeof(LevelDataScriptable))]
public class LevelDataEditor : Editor
{
  /// <summary>
  /// The OnInspectorGUI.
  /// </summary>
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();
    LevelDataScriptable script = (LevelDataScriptable)target;
    EditorGUILayout.BeginHorizontal();
    if (GUILayout.Button("Save"))
    {
      script.SerializeData();
    }
    if (GUILayout.Button("Reset"))
    {
      script.ResetValue();
    }
    EditorGUILayout.EndHorizontal();
  }
}
