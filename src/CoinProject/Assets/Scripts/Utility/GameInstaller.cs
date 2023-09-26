using Coins;
using MainMenu;
using Zenject;

namespace Utility
{
    /// <summary>
    /// Handles the dependency injection bindings for the game using Zenject.
    /// This class ensures that game services and components are properly instantiated and injected where needed.
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        /// <summary>
        /// Configures the bindings for the game's dependencies.
        /// </summary>
        public override void InstallBindings()
        {
            // Ensures there's only one instance (singleton) and initializes it immediately upon binding (non-lazy).
            Container.Bind<ICoinManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IMainMenuUI>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<ICoinPersistence>().To<PlayerPrefsCoinPersistence>().AsSingle().NonLazy();

        }
    }
}