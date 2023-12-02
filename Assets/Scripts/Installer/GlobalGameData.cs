using Picker3D.Gameplay.Collectibles;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Picker3D.Installers
{
    [CreateAssetMenu(menuName = "Global Game Data")]
    public class GlobalGameData : ScriptableObjectInstaller<GlobalGameData>
    {
        public List<MeshTypePair> MeshTypePairs;

        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();
        }
    }
}