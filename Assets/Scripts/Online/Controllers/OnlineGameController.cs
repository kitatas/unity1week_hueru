using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Games.Controllers;
using Games.StageObjects;
using Online.Buttons;
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
        private OnlineStartPresenter _onlineStartPresenter;
        private ChangeTurnButton _changeTurnButton;
        private StageObjectRepository _stageObjectRepository;

        [Inject]
        private void Construct(StartPresenter startPresenter, OnlineStartPresenter onlineStartPresenter,
            ChangeTurnButton changeTurnButton, StageObjectRepository stageObjectRepository)
        {
            _photonView = GetComponent<PhotonView>();
            _startPresenter = startPresenter;
            _onlineStartPresenter = onlineStartPresenter;
            _changeTurnButton = changeTurnButton;
            _stageObjectRepository = stageObjectRepository;

            _isStart = false;
            _isFinish = new ReactiveProperty<bool>(false);
            _isFinish
                .Where(x => x)
                .Subscribe(_ =>
                {
                    // 終了演出
                })
                .AddTo(this);
        }

        public IObservable<Unit> PlayingAsObservable =>
            this.UpdateAsObservable()
                .Where(_ => _isStart && _isFinish.Value == false);

        public void SetStartGame()
        {
            _photonView.RPC(nameof(StartGame), PhotonTargets.All);
        }

        [PunRPC]
        private void StartGame()
        {
            matchingText.gameObject.SetActive(false);
            _changeTurnButton.SetPlayerTurn(PhotonNetwork.isMasterClient);
            _startPresenter.Play(() =>
            {
                _isStart = true;

                if (PhotonNetwork.isMasterClient)
                {
                    GenerateStageObject(Vector3.zero);
                }
            });

            _onlineStartPresenter.DisplayText();

            var token = this.GetCancellationTokenOnDestroy();
            CheckDisconnectedAsync(token).Forget();
        }

        private async UniTaskVoid CheckDisconnectedAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(() => PhotonNetwork.room.PlayerCount != 2, cancellationToken: token);

            matchingText.gameObject.SetActive(true);
            matchingText.text = $"通信が切断されました。";
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