using Games.Controllers;
using Zenject;

public sealed class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<CameraController>()
            .AsCached();
    }
}