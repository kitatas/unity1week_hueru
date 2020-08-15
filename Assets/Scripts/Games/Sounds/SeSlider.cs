using Configs;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Games.Sounds
{
    public sealed class SeSlider : Slider
    {
        private SeController _seController;

        [Inject]
        private void Construct(SeController seController)
        {
            _seController = seController;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _seController.PlaySe(SeType.Decision);
        }
    }
}