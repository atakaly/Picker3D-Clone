using Picker3D.Gameplay;
using Picker3D.Gameplay.PickerSystem;
using Picker3D.LevelManagement;
using Picker3D.UI;
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

            Container.BindInterfacesAndSelfTo<UIManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<Picker>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}