using System;
using System.Threading;
using Configs;
using Cysharp.Threading.Tasks;
using Games.Controllers;
using Games.Sounds;
using Games.StageObjects;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Online.Controllers
{
    public sealed class OnlineGameController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI matchingText = null;
        private bool _isStart;
        private ReactiveProperty<bool> _isDisconnected;
        private ReactiveProperty<bool> _isFinish;

        private PhotonView _photonView;
        private StartPresenter _startPresenter;
        private TurnChanger _turnChanger;
        private OnlineStartPresenter _onlineStartPresenter;
        private StageObjectRepository _stageObjectRepository;
        private SeController _seController;

        [Inject]
        private void Construct(StartPresenter startPresenter, TurnChanger turnChanger,
            OnlineStartPresenter onlineStartPresenter, OnlineEndPresenter onlineEndPresenter,
            StageObjectRepository stageObjectRepository, SeController seController)
        {
            _photonView = GetComponent<PhotonView>();
            _startPresenter = startPresenter;
            _turnChanger = turnChanger;
            _onlineStartPresenter = onlineStartPresenter;
            _stageObjectRepository = stageObjectRepository;
            _seController = seController;

            _isStart = false;

            _isDisconnected = new ReactiveProperty<bool>(false);
            _isDisconnected
                .Where(x => x)
                .Subscribe(_ => _onlineStartPresenter.ShowBackTitleButton())
                .AddTo(this);

            _isFinish = new ReactiveProperty<bool>(false);
            _isFinish
                .Where(x => x)
                .Subscribe(_ =>
                {
                    // 終了演出
                    var finishType = _turnChanger.IsPlayerTurn ? FinishType.Lose : FinishType.Win;
                    onlineEndPresenter.Play(finishType);
                    _isDisconnected.Value = true;
                    _turnChanger.ActivateTurnText(false);
                    _seController.PlaySe(SeType.Finish);
                })
                .AddTo(this);
        }

        public IObservable<Unit> PlayingAsObservable =>
            this.UpdateAsObservable()
                .Where(_ => _isStart && _isDisconnected.Value == false && _isFinish.Value == false);

        public void StartGame()
        {
            _photonView.RPC(nameof(StartGameRpc), PhotonTargets.All);
        }

        [PunRPC]
        private void StartGameRpc()
        {
            var token = this.GetCancellationTokenOnDestroy();
            StartGameAsync(token).Forget();
            CheckDisconnectedAsync(token).Forget();
        }

        private async UniTaskVoid StartGameAsync(CancellationToken token)
        {
            _seController.PlaySe(SeType.Matched);
            matchingText.text = $"{MatchingController.GetEnemyName()}さんとマッチングしました。";

            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: token);

            if (_isDisconnected.Value)
            {
                return;
            }

            matchingText.enabled = false;
            _onlineStartPresenter.Play();

            _startPresenter.Play(() =>
            {
                if (_isDisconnected.Value)
                {
                    return;
                }

                _isStart = true;
                _turnChanger.InitializeTurn();

                if (PhotonNetwork.isMasterClient)
                {
                    GenerateStageObject(Vector3.zero);
                }
            });
        }

        private async UniTaskVoid CheckDisconnectedAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(() => PhotonNetwork.room.PlayerCount != 2, cancellationToken: token);

            _isDisconnected.Value = true;

            if (_isFinish.Value == false)
            {
                _seController.PlaySe(SeType.Alert);
                matchingText.enabled = true;
                matchingText.text = "通信が切断されました。";
            }
        }

        public void GenerateStageObject(Vector3 generatePosition)
        {
            PhotonNetwork.Instantiate(
                _stageObjectRepository.GetGenerateStageObjectName(),
                _stageObjectRepository.GetGeneratePosition(generatePosition),
                Quaternion.identity,
                0);
        }

        public void SetGameOver()
        {
            _isFinish.Value = true;
        }
    }
}