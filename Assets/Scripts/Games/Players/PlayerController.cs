using Configs;
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
        private StageObjectTable _stageObjectTable;

        [Inject]
        private void Construct(IPlayerInput playerInput, PlayerRaycaster playerRaycaster, StageObjectTable stageObjectTable)
        {
            _playerInput = playerInput;
            _playerRaycaster = playerRaycaster;
            _stageObjectTable = stageObjectTable;
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
                        Instantiate(_stageObjectTable.GetStageObject(), clickObject.transform.position, Quaternion.identity);
                    }
                })
                .AddTo(this);
        }
    }
}