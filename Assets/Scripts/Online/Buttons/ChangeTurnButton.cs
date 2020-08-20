using Games.Buttons;
using Online.Controllers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Online.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class ChangeTurnButton : MonoBehaviour
    {
        private TurnChanger _turnChanger;

        [Inject]
        private void Construct(TurnChanger turnChanger)
        {
            _turnChanger = turnChanger;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Where(_ => _turnChanger.IsPlayerTurn)
                .Subscribe(_ => _turnChanger.ChangeTurn())
                .AddTo(this);
        }
    }
}