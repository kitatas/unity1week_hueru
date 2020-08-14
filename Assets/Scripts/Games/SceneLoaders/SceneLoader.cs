using Configs;
using Zenject;

namespace Games.SceneLoaders
{
    public sealed class SceneLoader
    {
        private ZenjectSceneLoader _zenjectSceneLoader;

        [Inject]
        private void Construct(ZenjectSceneLoader zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
        }

        public void Load(SceneName sceneName)
        {
            _zenjectSceneLoader.LoadScene(sceneName.ToString());
        }
    }
}