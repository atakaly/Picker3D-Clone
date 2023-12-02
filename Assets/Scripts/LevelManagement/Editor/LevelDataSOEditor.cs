using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    [CustomEditor(typeof(LevelDataSO))]
    public class LevelDataSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(10);

            if (GUILayout.Button("Edit Level"))
            {
                EditLevel((LevelDataSO)target);
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Save Level"))
            {
            }
        }

        private static void EditLevel(LevelDataSO levelData)
        {
            EditorSceneManager.OpenScene("Assets/Scenes/LevelEditor.unity", OpenSceneMode.Single);

            LevelSceneEditor levelEditor = FindObjectOfType<LevelSceneEditor>();

            if (levelEditor != null)
            {
                levelEditor.InitializeWithLevelData(levelData);
            }
        }
    }
}