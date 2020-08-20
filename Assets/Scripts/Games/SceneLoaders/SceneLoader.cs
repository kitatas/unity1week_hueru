using Configs;
using Zenject;

namespace Games.SceneLoaders
{
    /// <summary>
    /// シーン遷移を行うクラス
    /// </summary>
    public sealed class SceneLoader
    {
        private ZenjectSceneLoader _zenjectSceneLoader;

        [Inject]
        private void Construct(ZenjectSceneLoader zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
        }

        /// <summary>
        /// シーン遷移
        /// </summary>
        /// <param name="sceneName"></param>
        public void Load(SceneName sceneName)
        {
            _zenjectSceneLoader.LoadScene(sceneName.ToString());
        }
    }
}