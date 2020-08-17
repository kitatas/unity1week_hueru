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
            _isFinish = new ReactiveProperty<bool>(false);
            _isFinish
                .Where(x => x)
                .Subscribe(_ =>
                {
                    // 終了演出
                    var finishType = _turnChanger.IsPlayerTurn ? FinishType.Lose : FinishType.Win;
                    onlineEndPresenter.Play(finishType);
                    _onlineStartPresenter.ShowBackTitleButton();
                    _turnChanger.ActivateTurnText(false);
                    _seController.PlaySe(SeType.Finish);
                })
                .AddTo(this);
        }

        public IObservable<Unit> PlayingAsObservable =>
            this.UpdateAsObservable()
                .Where(_ => _isStart && _isFinish.Value == false);

        public void StartGame()
        {
            _photonView.RPC(nameof(StartGameRpc), PhotonTargets.All);
        }

        [PunRPC]
        private void StartGameRpc()
        {
            var token = this.GetCancellationTokenOnDestroy();
            GameStartAsync(token).Forget();
            CheckDisconnectedAsync(token).Forget();
        }

        private async UniTaskVoid GameStartAsync(CancellationToken token)
        {
            _seController.PlaySe(SeType.Matched);
            matchingText.text = $"{PhotonNetwork.otherPlayers[0].NickName}さんとマッチングしました。";

            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: token);

            matchingText.enabled = false;

            _startPresenter.Play(() =>
            {
                _isStart = true;

                if (PhotonNetwork.isMasterClient)
                {
                    GenerateStageObject(Vector3.zero);
                }
            });

            _onlineStartPresenter.Play();
            _turnChanger.InitializeTurn();
        }

        private async UniTaskVoid CheckDisconnectedAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(() => PhotonNetwork.room.PlayerCount != 2, cancellationToken: token);

            if (_isFinish.Value == false)
            {
                _isStart = false;
                _seController.PlaySe(SeType.Alert);
                matchingText.enabled = true;
                matchingText.text = "通信が切断されました。";
                _onlineStartPresenter.ShowBackTitleButton();
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