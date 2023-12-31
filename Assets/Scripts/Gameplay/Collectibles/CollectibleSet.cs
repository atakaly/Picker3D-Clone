﻿using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Gameplay.Collectibles
{
    public class CollectibleSet : LevelObjectBase
    {
        [SerializeField] private List<CollectibleItem> m_Collectibles;
        [SerializeField] private MeshType m_MeshType;

        private Vector3[] collectiblePositions;

        public MeshType MeshType
        {
            get
            {
                return m_MeshType;
            }

            set 
            { 
                m_MeshType = value;
                UpdateMeshes();
            }
        }

        private void Awake()
        {
            collectiblePositions = new Vector3[m_Collectibles.Count];

            for (int i = 0; i < m_Collectibles.Count; i++)
            {
                collectiblePositions[i] = m_Collectibles[i].transform.localPosition;
            }
        }

        public void UpdateMeshes()
        {
            for (int i = 0; i < m_Collectibles.Count; i++)
            {
                m_Collectibles[i].CollectibleMeshChanger.UpdateMesh(m_MeshType);
            }
        }

        public override void OnSpawn()
        {
            base.OnSpawn();

            UpdateMeshes();
            for (int i = 0; i < m_Collectibles.Count; i++)
            {
                m_Collectibles[i].transform.localPosition = collectiblePositions[i];
                m_Collectibles[i].ResetItem();
            }
        }
    }
}