using System.Threading;
using Configs;
using Cysharp.Threading.Tasks;
using Games.Players;
using Games.Sounds;
using Online.StageObjects;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Online.Controllers
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class TurnChanger : MonoBehaviour
    {
        [SerializeField] private Button turnChangeButton = null;
        [SerializeField] private TextMeshProUGUI currentTurnText = null;

        private Turn _myTurn;
        private ReactiveProperty<Turn> _currentTurn;
        private ReactiveProperty<int> _clickCount;
        private ReactiveProperty<bool> _canChange;
        private bool _isWait;

        private PhotonView _photonView;
        private CancellationToken _token;
        private SeController _seController;
        private StageObjectContainer _stageObjectContainer;

        [Inject]
        private void Construct(SeController seController, StageObjectContainer stageObjectContainer)
        {
            _photonView = GetComponent<PhotonView>();
            _token = this.GetCancellationTokenOnDestroy();
            _seController = seController;
            _stageObjectContainer = stageObjectContainer;

            _myTurn = Turn.None;
            _isWait = false;
            ActivateTurnText(false);
        }

        private void Start()
        {
            _clickCount = new ReactiveProperty<int>(0);

            _clickCount
                .Where(x => x == 1)
                .Subscribe(_ => _canChange.Value = true)
                .AddTo(this);

            _clickCount
                .Where(x => x == 10)
                .Subscribe(_ => ChangeTurn())
                .AddTo(this);

            _currentTurn = new ReactiveProperty<Turn>(Turn.Master);
            _currentTurn
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ => StartTurn())
                .AddTo(this);

            _canChange = new ReactiveProperty<bool>(false);
            _canChange
                .Subscribe(x => turnChangeButton.interactable = x)
                .AddTo(this);
        }

        public void IncreaseClickCount()
        {
            _clickCount.Value++;
        }

        public void ChangeTurn()
        {
            _clickCount.Value = 0;
            _canChange.Value = false;
            _isWait = true;

            ChangeTurnAsync().Forget();
        }

        private async UniTaskVoid ChangeTurnAsync()
        {
            await UniTask.WaitUntil(_stageObjectContainer.IsAllSleep, PlayerLoopTiming.FixedUpdate, _token);

            _photonView.RPC(nameof(ChangeTurnRpc), PhotonTargets.All);
        }

        [PunRPC]
        private void ChangeTurnRpc()
        {
            _seController.PlaySe(SeType.Decision);
            _currentTurn.Value = _currentTurn.Value == Turn.Master ? Turn.Client : Turn.Master;
        }

        private void StartTurn()
        {
            currentTurnText.text = $"{GetPlayerName()}さんのターン";
            _isWait = false;
        }

        public bool IsPlayerTurn => _currentTurn.Value == _myTurn;

        public bool IsPlay => IsPlayerTurn && _isWait == false;

        private string GetPlayerName()
        {
            return IsPlayerTurn ? PlayerNameRegister.PlayerName : PhotonNetwork.otherPlayers[0].NickName;
        }

        public void InitializeTurn()
        {
            _myTurn = PhotonNetwork.isMasterClient ? Turn.Master : Turn.Client;
            ActivateTurnText(true);
            StartTurn();
        }

        public void ActivateTurnText(bool value)
        {
            currentTurnText.enabled = value;
        }
    }
}