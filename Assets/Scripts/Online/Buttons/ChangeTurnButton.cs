using Configs;
using Games.Players;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Online.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(PhotonView))]
    public sealed class ChangeTurnButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentTurnText = null;
        private Turn _myTurn;
        private ReactiveProperty<Turn> _currentTurn;

        private Button _button;
        private PhotonView _photonView;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _photonView = GetComponent<PhotonView>();

            ActivateTurnText(false);
            _button.interactable = false;

            _myTurn = Turn.None;

            _currentTurn = new ReactiveProperty<Turn>(Turn.Master);
            _currentTurn
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ => UpdateTurnText())
                .AddTo(this);
        }

        private void Start()
        {
            _button
                .OnClickAsObservable()
                .Where(_ => IsMyTurn)
                .Subscribe(_ => _photonView.RPC(nameof(ChangeTurn), PhotonTargets.All))
                .AddTo(this);
        }

        [PunRPC]
        private void ChangeTurn()
        {
            _currentTurn.Value = _currentTurn.Value == Turn.Master ? Turn.Client : Turn.Master;
            _button.interactable = IsMyTurn;
        }

        public void SetPlayerTurn(bool isMaster)
        {
            _myTurn = isMaster ? Turn.Master : Turn.Client;
            _button.interactable = _currentTurn.Value == _myTurn;

            ActivateTurnText(true);
            UpdateTurnText();
        }

        private void UpdateTurnText()
        {
            currentTurnText.text = $"{GetPlayerName()}さんのターン";
        }

        public bool IsMyTurn => _currentTurn.Value == _myTurn;

        private string GetPlayerName()
        {
            return IsMyTurn ? PlayerNameRegister.PlayerName : PhotonNetwork.otherPlayers[0].NickName;
        }

        public void ActivateTurnText(bool value)
        {
            currentTurnText.enabled = value;
        }
    }
}