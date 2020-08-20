using Zenject;

namespace Games.Score
{
    /// <summary>
    /// SceneContextに紐づいているMonoInstaller
    /// </summary>
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