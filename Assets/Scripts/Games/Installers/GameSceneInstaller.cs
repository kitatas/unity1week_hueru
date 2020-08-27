using Games.Controllers;
using Zenject;

namespace Games.Installers
{
    /// <summary>
    /// SceneContextに紐づいているMonoInstaller
    /// </summary>
    public sealed class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<CameraController>()
                .AsCached();
        }
    }
}