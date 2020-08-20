using Online.StageObjects;
using Zenject;

namespace Online.Installers
{
    /// <summary>
    /// SceneContextに紐づいているMonoInstaller
    /// </summary>
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