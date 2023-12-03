#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

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

            GUILayout.BeginArea(new Rect(10, 10, 200, 60));
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
