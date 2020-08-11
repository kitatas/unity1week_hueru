using Configs;
using Zenject;

namespace Games.Players
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IPlayerInput>()
                .To<InputProvider>()
                .AsCached();

            Container
                .Bind<PlayerRaycaster>()
                .AsCached();
        }
    }
}