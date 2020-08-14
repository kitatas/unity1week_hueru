using Configs;
using Games.SceneLoaders;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class LoadButton : MonoBehaviour
    {
        [SerializeField] private SceneName sceneName = default;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _sceneLoader.Load(sceneName))
                .AddTo(this);
        }
    }
}