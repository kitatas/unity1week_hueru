using Configs;
using Games.SceneLoaders;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Online.Buttons
{
    [RequireComponent(typeof(Button))]
    public class DisconnectButton : MonoBehaviour
    {
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
                .Subscribe(_ =>
                {
                    Disconnect();
                    _sceneLoader.Load(SceneName.Title);
                })
                .AddTo(this);
        }

        private static void Disconnect()
        {
            if (PhotonNetwork.room.PlayerCount == 1)
            {
                PhotonNetwork.room.IsOpen = true;
            }

            PhotonNetwork.Disconnect();
        }
    }
}