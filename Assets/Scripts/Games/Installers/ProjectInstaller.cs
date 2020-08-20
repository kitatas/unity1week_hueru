using Games.SceneLoaders;
using Zenject;

namespace Games.Installers
{
    /// <summary>
    /// ProjectContextに紐づいているMonoInstaller
    /// </summary>
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