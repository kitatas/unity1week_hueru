using Configs;
using Zenject;

namespace Games.Players
{
    /// <summary>
    /// GameObjectContextに紐づいているMonoInstaller
    /// </summary>
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