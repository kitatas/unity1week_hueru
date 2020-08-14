using Games.SceneLoaders;
using Zenject;

namespace Games.Installers
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<SceneLoader>()
                .AsCached();
        }
    }
}