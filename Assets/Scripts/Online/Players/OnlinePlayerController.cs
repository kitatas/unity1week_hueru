using Configs;
using Games.Players;
using Games.Score;
using Online.Buttons;
using Online.Controllers;
using UniRx;
using UnityEngine;
using Zenject;

namespace Online.Players
{
    public sealed class OnlinePlayerController : MonoBehaviour
    {
        private IPlayerInput _playerInput;
        private PlayerRaycaster _playerRaycaster;
        private IScoreUpdatable _scoreUpdatable;
        private OnlineGameController _onlineGameController;
        private ChangeTurnButton _changeTurnButton;

        [Inject]
        private void Construct(IPlayerInput playerInput, PlayerRaycaster playerRaycaster,
            IScoreUpdatable scoreUpdatable, OnlineGameController onlineGameController,
            ChangeTurnButton changeTurnButton)
        {
            _playerInput = playerInput;
            _playerRaycaster = playerRaycaster;
            _scoreUpdatable = scoreUpdatable;
            _onlineGameController = onlineGameController;
            _changeTurnButton = changeTurnButton;
        }

        private void Start()
        {
            // オブジェクトのクリック
            _onlineGameController.PlayingAsObservable
                .Where(_ => _changeTurnButton.IsMyTurn)
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
                        _onlineGameController.GenerateStageObject(clickObject.transform.position);
                    }
                })
                .AddTo(this);
        }
    }
}