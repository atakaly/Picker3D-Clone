using Picker3D.Gameplay;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelSceneEditor : MonoBehaviour
    {
        public List<LevelObjectBase> CurrentInLevelObjects;
        public LevelDataSO levelData;

        public void InitializeWithLevelData(LevelDataSO levelData)
        {
            ClearLevel();

            foreach (var objectData in levelData.ObjectsInLevel)
            {
                var instantiatedObject = Instantiate(objectData.LevelObject, objectData.Position, Quaternion.Euler(objectData.Rotation));
                CurrentInLevelObjects.Add(instantiatedObject.GetComponent<LevelObjectBase>());
            }

            this.levelData = levelData;
        }

        public void UpdateObjectListAndSave()
        {
            CurrentInLevelObjects = FindObjectsOfType<LevelObjectBase>().ToList();

            if (levelData != null)
            {
                levelData.ObjectsInLevel = GetInLevelObjectDatas();
                EditorUtility.SetDirty(levelData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        public List<LevelDataSO.InLevelObjectData> GetInLevelObjectDatas()
        {
            List<LevelDataSO.InLevelObjectData> inLevelObjectDatas = new List<LevelDataSO.InLevelObjectData>();

            for (int i = 0; i < CurrentInLevelObjects.Count; i++)
            {
                LevelObjectBase prefabAsset = PrefabUtility.GetCorrespondingObjectFromSource(CurrentInLevelObjects[i]);

                var data = new LevelDataSO.InLevelObjectData
                {
                    LevelObject = prefabAsset,
                    Position = CurrentInLevelObjects[i].transform.position,
                    Rotation = CurrentInLevelObjects[i].transform.eulerAngles
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
