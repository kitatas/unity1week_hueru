using Configs;
using UnityEngine;

namespace Games.Sounds
{
    [CreateAssetMenu(fileName = "SeTable", menuName = "DataTable/SeTable")]
    public sealed class SeTale : ScriptableObject
    {
        [SerializeField] private AudioClip decision = null;
        [SerializeField] private AudioClip cancel = null;
        [SerializeField] private AudioClip generate = null;
        [SerializeField] private AudioClip explode = null;
        [SerializeField] private AudioClip finish = null;
        [SerializeField] private AudioClip alert = null;

        public AudioClip[] GetSeList()
        {
            var seCount = System.Enum.GetValues(typeof(SeType)).Length;
            var seList = new AudioClip[seCount];
            seList[(int) SeType.Decision] = decision;
            seList[(int) SeType.Cancel]   = cancel;
            seList[(int) SeType.Generate] = generate;
            seList[(int) SeType.Explode]  = explode;
            seList[(int) SeType.Finish]   = finish;
            seList[(int) SeType.Alert]    = alert;

            return seList;
        }
    }
}