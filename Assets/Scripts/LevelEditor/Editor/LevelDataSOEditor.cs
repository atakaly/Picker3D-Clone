using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    [CustomEditor(typeof(LevelDataSO))]
    public class LevelDataSOEditor : Editor
    {
        private static LevelSceneEditor levelSceneEditor;
        private static LevelDataSO currentLevelData;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (currentLevelData == null)
                currentLevelData = (LevelDataSO)target;   

            GUILayout.Space(10);

            if (GUILayout.Button("Edit Level"))
            {
                EditLevel((LevelDataSO)target);
                currentLevelData = (LevelDataSO)target;
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Save Level"))
            {
                SaveLevelChanges();
            }
        }

        private static void EditLevel(LevelDataSO levelData)
        {
            EditorSceneManager.OpenScene("Assets/Scenes/LevelEditor.unity", OpenSceneMode.Single);

            levelSceneEditor = FindObjectOfType<LevelSceneEditor>();

            if (levelSceneEditor != null)
            {
                levelSceneEditor.InitializeWithLevelData(levelData);
            }
        }

        private static void SaveLevelChanges()
        {
            if (levelSceneEditor == null)
                levelSceneEditor = FindObjectOfType<LevelSceneEditor>();
            else
                return;

            currentLevelData.ObjectsInLevel = levelSceneEditor.GetInLevelObjectDatas();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}