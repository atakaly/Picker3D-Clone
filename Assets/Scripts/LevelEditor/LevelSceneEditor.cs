#if UNITY_EDITOR
using UnityEditor;
#endif
using Picker3D.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelSceneEditor : MonoBehaviour
    {
        public List<LevelObjectBase> CurrentInLevelObjects;
        public LevelDataSO levelData;

        public void InitializeWithLevelData(LevelDataSO levelData)
        {
            this.levelData = levelData;

            ClearLevel();

            foreach (var objectData in levelData.ObjectsInLevel)
            {
#if UNITY_EDITOR
                var prefabInstance = PrefabUtility.InstantiatePrefab(objectData.LevelObject.gameObject) as GameObject;
#else
                var prefabInstance = Instantiate(objectData.LevelObject);
#endif
                prefabInstance.transform.position = objectData.Position;
                prefabInstance.transform.rotation = Quaternion.Euler(objectData.Rotation);
                CurrentInLevelObjects.Add(prefabInstance.GetComponent<LevelObjectBase>());
            }
        }

        public void UpdateObjectListAndSave()
        {
            CurrentInLevelObjects = new List<LevelObjectBase>(FindObjectsOfType<LevelObjectBase>());

            if (levelData != null)
            {
                levelData.ObjectsInLevel = GetInLevelObjectDatas();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(levelData);
                UnityEditor.AssetDatabase.SaveAssets();
                UnityEditor.AssetDatabase.Refresh();
#endif
            }
        }

        public List<LevelDataSO.InLevelObjectData> GetInLevelObjectDatas()
        {
            List<LevelDataSO.InLevelObjectData> inLevelObjectDatas = new List<LevelDataSO.InLevelObjectData>();

            foreach (var currentObject in CurrentInLevelObjects)
            {
#if UNITY_EDITOR
                LevelObjectBase prefabAsset = PrefabUtility.GetCorrespondingObjectFromSource(currentObject);
#endif
                var data = new LevelDataSO.InLevelObjectData
                {
                    LevelObject = prefabAsset,
                    Position = currentObject.transform.position,
                    Rotation = currentObject.transform.eulerAngles
                };

                inLevelObjectDatas.Add(data);
            }

            return inLevelObjectDatas;
        }

        private void ClearLevel()
        {
            for (int i = 0; i < CurrentInLevelObjects.Count; i++)
            {
                DestroyImmediate(CurrentInLevelObjects[i].gameObject);
            }
        }
    }
}
