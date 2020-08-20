using TMPro;
using UnityEngine;

namespace Games.Players
{
    /// <summary>
    /// Player名の登録管理クラス
    /// </summary>
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

        /// <summary>
        /// Player名の更新
        /// </summary>
        public void UpdatePlayerName()
        {
            _playerName = _playerNameInputField.text;
        }
    }
}