using Configs;
using Games.Buttons;
using Games.SceneLoaders;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Online.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class DisconnectButton : MonoBehaviour
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
            if (PhotonNetwork.connected == false)
            {
                return;
            }

            if (PhotonNetwork.room == null)
            {
                return;
            }

            if (PhotonNetwork.room.PlayerCount == 1)
            {
                PhotonNetwork.room.IsOpen = true;
            }

            PhotonNetwork.Disconnect();
        }
    }
}