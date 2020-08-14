using Configs;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Online.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(PhotonView))]
    public sealed class ChangeTurnButton : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI myTurnText = null;
        [SerializeField] private TMPro.TextMeshProUGUI currentTurnText = null;
        private Turn _myTurn;
        private ReactiveProperty<Turn> _currentTurn;

        private Button _button;
        private PhotonView _photonView;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _photonView = GetComponent<PhotonView>();

            _button.interactable = false;

            _myTurn = Turn.None;
            myTurnText.text = $"自分のターン名:{_myTurn.ToString()}";

            _currentTurn = new ReactiveProperty<Turn>(Turn.Master);
            _currentTurn
                .Subscribe(_ =>
                {
                    // 
                    currentTurnText.text = $"現在のターン:{_currentTurn.Value.ToString()}";
                })
                .AddTo(this);
        }

        private void Start()
        {
            _button
                .OnClickAsObservable()
                .Where(_ => IsMyTurn)
                .Subscribe(_ =>
                {
                    // Debug.Log("ターン終了");
                    _photonView.RPC(nameof(ChangeTurn), PhotonTargets.All);
                })
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
            myTurnText.text = $"自分のターン名:{_myTurn.ToString()}";
            _button.interactable = _currentTurn.Value == _myTurn;
        }

        public bool IsMyTurn => _currentTurn.Value == _myTurn;
    }
}