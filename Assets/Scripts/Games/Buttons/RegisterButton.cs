using Games.Players;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    /// <summary>
    /// 名前登録を行うボタン
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class RegisterButton : MonoBehaviour
    {
        private PlayerNameRegister _playerNameRegister;

        [Inject]
        private void Construct(PlayerNameRegister playerNameRegister)
        {
            _playerNameRegister = playerNameRegister;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _playerNameRegister.UpdatePlayerName())
                .AddTo(this);
        }
    }
}