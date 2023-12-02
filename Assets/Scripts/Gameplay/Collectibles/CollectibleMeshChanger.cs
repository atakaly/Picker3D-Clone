using Picker3D.Installers;
using UnityEngine;
using Zenject;

namespace Picker3D.Gameplay.Collectibles
{
    public class CollectibleMeshChanger : MonoBehaviour
    {
        private MeshFilter m_MeshFilter;
        private GlobalGameData m_GlobalGameData;

        [Inject]
        public void Construct(GlobalGameData globalGameData)
        {
            m_GlobalGameData = globalGameData;
        }

        private void Awake()
        {
            m_MeshFilter = GetComponent<MeshFilter>();
        }

        public void UpdateMesh(MeshType meshType)
        {
            //m_MeshFilter.mesh = GetMeshFromType(meshType);
        }

        private Mesh GetMeshFromType(MeshType meshType)
        {
            for (int i = 0; i < m_GlobalGameData.MeshTypePairs.Count; i++)
            {
                if (m_GlobalGameData.MeshTypePairs[i].MeshType == meshType)
                    return m_GlobalGameData.MeshTypePairs[i].Mesh;
            }

            return null;
        }
    }
}