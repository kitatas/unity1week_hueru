using TMPro;
using UnityEngine;

namespace Games.Score
{
    /// <summary>
    /// スコアのView
    /// </summary>
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText = null;

        /// <summary>
        /// スコア値をTextに反映
        /// </summary>
        /// <param name="scoreValue"></param>
        public void UpdateScore(int scoreValue)
        {
            scoreText.text = $"{scoreValue:000}";
        }
    }
}