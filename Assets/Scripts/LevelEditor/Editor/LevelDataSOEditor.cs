﻿using Picker3D.Gameplay.BasketSystem;
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

            ShowBasketFields(currentLevelData);

            GUILayout.Space(10);

            if (GUILayout.Button("Edit Level"))
            {
                EditLevel((LevelDataSO)target);
                currentLevelData = (LevelDataSO)target;
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

        private static void ShowBasketFields(LevelDataSO levelData)
        {
            if (levelData.ObjectsInLevel == null || levelData.ObjectsInLevel.Count == 0)
                return;

            for (int i = 0; i < levelData.ObjectsInLevel.Count; i++)
            {
                var objData = levelData.ObjectsInLevel[i];
                if (objData.LevelObject is Basket)
                {
                    GUILayout.Space(5);
                    objData.RequiredBallCount = EditorGUILayout.IntField("Basket Required Ball Count:", objData.RequiredBallCount);
                }
            }
        }
    }
}
