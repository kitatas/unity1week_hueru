using Configs;
using Games.Players;
using Games.Score;
using Games.StageObjects;
using Online.Controllers;
using UniRx;
using UnityEngine;
using Zenject;

namespace Online.Players
{
    /// <summary>
    /// PUN用
    /// Player制御のクラス
    /// </summary>
    public sealed class OnlinePlayerController : MonoBehaviour
    {
        private IPlayerInput _playerInput;
        private PlayerRaycaster _playerRaycaster;
        private IScoreUpdatable _scoreUpdatable;
        private OnlineGameController _onlineGameController;
        private StageObjectRepository _stageObjectRepository;
        private TurnChanger _turnChanger;

        [Inject]
        private void Construct(IPlayerInput playerInput, PlayerRaycaster playerRaycaster,
            IScoreUpdatable scoreUpdatable, OnlineGameController onlineGameController,
            StageObjectRepository stageObjectRepository, TurnChanger turnChanger)
        {
            _playerInput = playerInput;
            _playerRaycaster = playerRaycaster;
            _scoreUpdatable = scoreUpdatable;
            _onlineGameController = onlineGameController;
            _stageObjectRepository = stageObjectRepository;
            _turnChanger = turnChanger;
        }

        private void Start()
        {
            // StageObjectのクリック
            _onlineGameController.PlayingAsObservable
                .Where(_ => _turnChanger.IsPlay)
                .Where(_ => _playerInput.InputMouseButton)
                .Subscribe(_ =>
                {
                    var clickObject = _playerRaycaster.GetClickObject(_playerInput.MousePosition);
                    if (clickObject == null)
                    {
                        return;
                    }

                    if (clickObject.CompareTag(Tag.STAGE_OBJECT))
                    {
                        _scoreUpdatable.UpdateScore();
                        _stageObjectRepository.GenerateOnlineStageObject(clickObject.transform.position);
                        _turnChanger.IncreaseClickCount();
                    }
                })
                .AddTo(this);
        }
    }
}