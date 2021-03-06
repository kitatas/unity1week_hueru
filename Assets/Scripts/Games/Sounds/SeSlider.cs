using Configs;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Games.Sounds
{
    /// <summary>
    /// SE用のSlider
    /// </summary>
    public sealed class SeSlider : Slider
    {
        private SeController _seController;

        [Inject]
        private void Construct(SeController seController)
        {
            _seController = seController;
        }

        /// <summary>
        /// SliderのHandleを離した時に効果音を再生
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _seController.PlaySe(SeType.Decision);
        }
    }
}