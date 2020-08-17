using Online.StageObjects;
using Zenject;

namespace Online.Installers
{
    public sealed class OnlineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<StageObjectContainer>()
                .AsCached();
        }
    }
}