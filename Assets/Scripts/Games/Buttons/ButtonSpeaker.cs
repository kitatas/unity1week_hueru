using System;
using Configs;
using Games.Sounds;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    public sealed class ButtonSpeaker : MonoBehaviour
    {
        [SerializeField] private ButtonType buttonType = default;

        private SeController _seController;

        [Inject]
        private void Construct(SeController seController)
        {
            _seController = seController;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _seController.PlaySe(GetSeType()))
                .AddTo(this);
        }

        private SeType GetSeType()
        {
            switch (buttonType)
            {
                case ButtonType.Decision:
                    return SeType.Decision;
                case ButtonType.Cancel:
                    return SeType.Cancel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
            }
        }
    }
}