using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelSceneEditor : MonoBehaviour
    {
        public void InitializeWithLevelData(LevelDataSO levelData)
        {
            ClearLevel();

            foreach (var objectData in levelData.ObjectsInLevel)
            {
                Instantiate(objectData.LevelObject, objectData.Position, Quaternion.Euler(objectData.Rotation));
            }
        }

        private void ClearLevel()
        {
            GameObject[] levelObjects = FindObjectsOfType<GameObject>();
            foreach (var levelObject in levelObjects)
            {
                DestroyImmediate(levelObject);
            }
        }
    }
}
