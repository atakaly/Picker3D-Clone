using Picker3D.LevelEditor;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Picker3D.Installers
{
    [CreateAssetMenu(menuName = "Global Game Data")]
    public class GlobalGameData : ScriptableObjectInstaller<GlobalGameData>
    {
        public List<LevelDataSO> AllLevelDatas;

        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();
        }
    }
}