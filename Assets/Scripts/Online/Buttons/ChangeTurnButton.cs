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
        private ReactiveProperty<bool> _canChange;

        private Button _button;
        private PhotonView _photonView;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _photonView = GetComponent<PhotonView>();

            ActivateTurnText(false);
            _button.interactable = false;

            _myTurn = Turn.None;
        }

        private void Start()
        {
            _currentTurn = new ReactiveProperty<Turn>(Turn.Master);
            _currentTurn
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ => ChangeTurn())
                .AddTo(this);

            _canChange = new ReactiveProperty<bool>(false);
            _canChange
                .Where(x => x)
                .Subscribe(_ => _button.interactable = true)
                .AddTo(this);

            _button
                .OnClickAsObservable()
                .Where(_ => IsMyTurn)
                .Subscribe(_ => _photonView.RPC(nameof(ChangeTurnRpc), PhotonTargets.All))
                .AddTo(this);
        }

        [PunRPC]
        private void ChangeTurnRpc()
        {
            _currentTurn.Value = _currentTurn.Value == Turn.Master ? Turn.Client : Turn.Master;
        }

        public void SetPlayerTurn(bool isMaster)
        {
            _myTurn = isMaster ? Turn.Master : Turn.Client;

            ActivateTurnText(true);
            ChangeTurn();
        }

        private void ChangeTurn()
        {
            currentTurnText.text = $"{GetPlayerName()}さんのターン";
            _canChange.Value = false;
            _button.interactable = false;
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

        public void SetCanChange()
        {
            _canChange.Value = true;
        }
    }
}