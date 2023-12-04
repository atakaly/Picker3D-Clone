#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Picker3D.LevelEditor
{
    [CustomEditor(typeof(LevelSceneEditor))]
    public class LevelSceneEditorEditor : Editor
    {
        public void OnEnable()
        {
            SceneView.duringSceneGui += DuringSceneGUI;
        }

        public void OnDisable()
        {
            SceneView.duringSceneGui -= DuringSceneGUI;
        }

        public void DuringSceneGUI(SceneView sceneView)
        {
            Handles.BeginGUI();

            GUILayout.BeginArea(new Rect(10, 10, 250, 90));

            GUI.Box(new Rect(0, 0, 250, 90), GUIContent.none);

            GUILayout.Label("SAVE", EditorStyles.boldLabel);
            GUI.color = Color.green;

            if (GUILayout.Button("Save Current Level"))
            {
                LevelSceneEditor levelSceneEditor = (LevelSceneEditor)target;
                levelSceneEditor.UpdateObjectListAndSave();
            }

            GUILayout.EndArea();

            Handles.EndGUI();
        }
    }
}
#endif
