using Configs;
using Games.Score;
using Games.StageObjects;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Games.Players
{
    public sealed class PlayerController : MonoBehaviour
    {
        private IPlayerInput _playerInput;
        private PlayerRaycaster _playerRaycaster;
        private IScoreUpdatable _scoreUpdatable;
        private StageObjectRepository _stageObjectRepository;

        [Inject]
        private void Construct(IPlayerInput playerInput, PlayerRaycaster playerRaycaster,
            IScoreUpdatable scoreUpdatable, StageObjectRepository stageObjectRepository)
        {
            _playerInput = playerInput;
            _playerRaycaster = playerRaycaster;
            _scoreUpdatable = scoreUpdatable;
            _stageObjectRepository = stageObjectRepository;
        }

        private void Start()
        {
            // オブジェクトのクリック
            this.UpdateAsObservable()
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
                        _stageObjectRepository.GenerateStageObject(clickObject.transform.position);
                    }
                })
                .AddTo(this);
        }
    }
}