using Configs;
using UnityEngine;

namespace Games.Sounds
{
    /// <summary>
    /// SEのScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "SeTable", menuName = "DataTable/SeTable")]
    public sealed class SeTale : ScriptableObject
    {
        [SerializeField] private AudioClip decision = null;
        [SerializeField] private AudioClip cancel = null;
        [SerializeField] private AudioClip generate = null;
        [SerializeField] private AudioClip explode = null;
        [SerializeField] private AudioClip finish = null;
        [SerializeField] private AudioClip alert = null;
        [SerializeField] private AudioClip matched = null;
        [SerializeField] private AudioClip appear = null;

        /// <summary>
        /// 音ファイルとSeTypeの紐付け
        /// </summary>
        /// <returns></returns>
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
            seList[(int) SeType.Matched]  = matched;
            seList[(int) SeType.Appear]   = appear;

            return seList;
        }
    }
}