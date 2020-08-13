using Configs;
using Games.Controllers;
using Games.Score;
using UniRx;
using UnityEngine;
using Zenject;

namespace Games.Players
{
    public sealed class PlayerController : MonoBehaviour
    {
        private IPlayerInput _playerInput;
        private PlayerRaycaster _playerRaycaster;
        private IScoreUpdatable _scoreUpdatable;
        private GameController _gameController;

        [Inject]
        private void Construct(IPlayerInput playerInput, PlayerRaycaster playerRaycaster,
            IScoreUpdatable scoreUpdatable, GameController gameController)
        {
            _playerInput = playerInput;
            _playerRaycaster = playerRaycaster;
            _scoreUpdatable = scoreUpdatable;
            _gameController = gameController;
        }

        private void Start()
        {
            // オブジェクトのクリック
            _gameController.PlayingAsObservable
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
                        _gameController.GenerateStageObject(clickObject.transform.position);
                    }
                })
                .AddTo(this);
        }
    }
}