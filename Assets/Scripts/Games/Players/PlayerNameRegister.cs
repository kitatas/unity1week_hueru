using TMPro;
using UnityEngine;

namespace Games.Players
{
    [RequireComponent(typeof(TMP_InputField))]
    public sealed class PlayerNameRegister : MonoBehaviour
    {
        private TMP_InputField _playerNameInputField;

        private static string _playerName = "";

        public static string PlayerName => _playerName == "" ? "名無し" : _playerName;

        private void Awake()
        {
            _playerNameInputField = GetComponent<TMP_InputField>();

            if (_playerName != "")
            {
                _playerNameInputField.text = _playerName;
            }
        }

        public void UpdatePlayerName()
        {
            _playerName = _playerNameInputField.text;
        }
    }
}