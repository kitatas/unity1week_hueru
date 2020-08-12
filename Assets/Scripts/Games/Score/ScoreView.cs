using TMPro;
using UnityEngine;

namespace Games.Score
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText = null;

        public void UpdateScore(int scoreValue)
        {
            scoreText.text = $"{scoreValue}";
        }
    }
}