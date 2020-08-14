using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Players
{
    public sealed class PlayerNameRegister : MonoBehaviour
    {
        [SerializeField] private TMP_InputField tmpInputField = null;
        [SerializeField] private Button registerButton = null;

        private static string _playerName = "";

        public static string PlayerName => _playerName == "" ? "名無し" : _playerName;

        private void Awake()
        {
            if (_playerName != "")
            {
                tmpInputField.text = _playerName;
            }
        }

        private void Start()
        {
            registerButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    // 
                    _playerName = tmpInputField.text;
                })
                .AddTo(this);
        }
    }
}