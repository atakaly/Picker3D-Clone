using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Gameplay.Collectibles
{
    public class CollectibleMeshChanger : MonoBehaviour
    {
        private MeshFilter m_MeshFilter;

        public List<MeshTypePair> MeshTypePairs;

        private void Awake()
        {
            m_MeshFilter = GetComponent<MeshFilter>();
        }

        public void UpdateMesh(MeshType meshType)
        {
            m_MeshFilter.mesh = GetMeshFromType(meshType);
        }

        private Mesh GetMeshFromType(MeshType meshType)
        {
            for (int i = 0; i < MeshTypePairs.Count; i++)
            {
                if (MeshTypePairs[i].MeshType == meshType)
                    return MeshTypePairs[i].Mesh;
            }

            return null;
        }
    }
}