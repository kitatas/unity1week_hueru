using UnityEngine;

namespace Games.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseAudioSource : MonoBehaviour
    {
        private AudioSource _audioSource;

        protected AudioSource AudioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    _audioSource = GetComponent<AudioSource>();
                }

                return _audioSource;
            }
        }
    }
}