using Configs;
using UnityEngine;
using Utility;
using Zenject;

namespace Games.Sounds
{
    public sealed class SeController : BaseAudioSource
    {
        private AudioClip[] _seList;

        [Inject]
        private void Construct(SeTale seTale)
        {
            _seList = seTale.GetSeList();
        }

        public void PlaySe(SeType seType)
        {
            if (_seList.TryGetValue((int) seType, out var clip))
            {
                AudioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError(nameof(seType) + "is not contain SeTable.");
            }
        }
    }
}