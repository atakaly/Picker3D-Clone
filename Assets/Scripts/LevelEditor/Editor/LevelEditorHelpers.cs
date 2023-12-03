using Picker3D.Installers;
using Picker3D.LevelEditor;
using UnityEditor;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelEditorHelpers
    {
        [MenuItem("Tools/Level Editor/Find All Levels")]
        private static void UpdateGlobalGameData()
        {
            LevelDataSO[] levelDataArray = Resources.LoadAll<LevelDataSO>("");

            GlobalGameData globalGameData = Resources.Load<GlobalGameData>("Global Game Data");

            if (globalGameData != null)
            {
                globalGameData.AllLevelDatas.Clear();
                globalGameData.AllLevelDatas.AddRange(levelDataArray);

                EditorUtility.SetDirty(globalGameData);
                AssetDatabase.SaveAssets();

                Debug.Log("Levels added to Global Game Data");
            }
            else
            {
                Debug.LogError("Global Game Data not found.");
            }
        }
    }
}
