using Picker3D.LevelManagement;
using Zenject;

namespace Picker3D.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelObjectPool>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<LevelManager>()
                .AsSingle();
        }
    }
}