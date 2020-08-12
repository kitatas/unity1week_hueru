using Zenject;

namespace Games.Score
{
    public sealed class ScoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ScoreModel>()
                .AsCached();

            Container
                .Bind<ScorePresenter>()
                .AsCached()
                .NonLazy();
        }
    }
}