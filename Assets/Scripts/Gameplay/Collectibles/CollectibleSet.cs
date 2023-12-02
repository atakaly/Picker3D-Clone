using Picker3D.LevelEditor;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Gameplay.Collectibles
{
    public class CollectibleSet : LevelObjectBase
    {
        [SerializeField] private List<CollectibleItem> m_Collectibles;
        [SerializeField] private MeshType m_MeshType;

        public void Start()
        {
            for (int i = 0; i < m_Collectibles.Count; i++)
            {
                m_Collectibles[i].CollectibleMeshChanger.UpdateMesh(m_MeshType);
            }
        }
    }
}