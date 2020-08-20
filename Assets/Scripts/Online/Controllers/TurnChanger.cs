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
    /// <summary>
    /// ターン切り替えを制御するクラス
    /// </summary>
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
            // 1ターンでふやした数
            _clickCount = new ReactiveProperty<int>(0);

            _clickCount
                .Where(x => x == 1)
                .Subscribe(_ => _canChange.Value = true)
                .AddTo(this);

            _clickCount
                .Where(x => x == 10)
                .Subscribe(_ => ChangeTurn())
                .AddTo(this);

            // ターンが切り替わった時
            _currentTurn = new ReactiveProperty<Turn>(Turn.Master);
            _currentTurn
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ => StartTurn())
                .AddTo(this);

            // ターン終了ボタンの有効 / 無効化
            _canChange = new ReactiveProperty<bool>(false);
            _canChange
                .Subscribe(x => turnChangeButton.interactable = x)
                .AddTo(this);
        }

        /// <summary>
        /// 1ふやした数の増加
        /// </summary>
        public void IncreaseClickCount()
        {
            _clickCount.Value++;
        }

        /// <summary>
        /// ターン切り替え準備
        /// </summary>
        public void ChangeTurn()
        {
            _clickCount.Value = 0;
            _canChange.Value = false;
            _isWait = true;

            ChangeTurnAsync().Forget();
        }

        /// <summary>
        /// 静止判定後、ターン切り替え
        /// </summary>
        /// <returns></returns>
        private async UniTaskVoid ChangeTurnAsync()
        {
            currentTurnText.text = $"判定中...";

            await UniTask.WaitUntil(_stageObjectContainer.IsAllSleep, PlayerLoopTiming.FixedUpdate, _token);

            _photonView.RPC(nameof(ChangeTurnRpc), PhotonTargets.All);
        }

        /// <summary>
        /// ターン切り替え
        /// </summary>
        [PunRPC]
        private void ChangeTurnRpc()
        {
            _seController.PlaySe(SeType.Decision);
            _currentTurn.Value = _currentTurn.Value == Turn.Master ? Turn.Client : Turn.Master;
        }

        /// <summary>
        /// ターン開始
        /// </summary>
        private void StartTurn()
        {
            currentTurnText.text = $"{GetPlayerName()}さんのターン";
            _isWait = false;
        }

        /// <summary>
        /// 自分のターンであるかの判定
        /// </summary>
        public bool IsPlayerTurn => _currentTurn.Value == _myTurn;

        /// <summary>
        /// ふやせる状態であるか
        /// </summary>
        public bool IsPlay => IsPlayerTurn && _isWait == false;

        /// <summary>
        /// ターンのプレイヤー名を取得
        /// </summary>
        /// <returns></returns>
        private string GetPlayerName()
        {
            return IsPlayerTurn ? PlayerNameRegister.PlayerName : MatchingController.EnemyName;
        }

        /// <summary>
        /// ゲーム開始時のターン初期化
        /// </summary>
        public void InitializeTurn()
        {
            _myTurn = PhotonNetwork.isMasterClient ? Turn.Master : Turn.Client;
            ActivateTurnText(true);
            StartTurn();
        }

        /// <summary>
        /// ターンTextの有効 / 無効化
        /// </summary>
        /// <param name="value"></param>
        public void ActivateTurnText(bool value)
        {
            currentTurnText.enabled = value;
        }
    }
}