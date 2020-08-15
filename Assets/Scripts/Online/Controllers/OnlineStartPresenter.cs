using DG.Tweening;
using Games.Players;
using TMPro;
using UnityEngine;

namespace Online.Controllers
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class OnlineStartPresenter : MonoBehaviour
    {
        [SerializeField] private RectTransform backTitleButton = null;
        [SerializeField] private RectTransform turnEndButton = null;
        [SerializeField] private TextMeshProUGUI playerName = null;
        [SerializeField] private TextMeshProUGUI enemyName = null;

        private readonly float _animationTime = 0.25f;

        public void Play()
        {
            DisplayPlayerName();
            ShowTurnEndButton();
        }

        private void DisplayPlayerName()
        {
            playerName.text = $"あなた:{PlayerNameRegister.PlayerName}さん";
            enemyName.text = $"対戦相手:{PhotonNetwork.otherPlayers[0].NickName}さん";
        }

        private void ShowTurnEndButton()
        {
            DOTween.Sequence()
                .Append(backTitleButton
                    .DOAnchorPosY(-backTitleButton.anchoredPosition.y, _animationTime)
                    .SetEase(Ease.Linear))
                .Append(turnEndButton
                    .DOAnchorPosY(-turnEndButton.anchoredPosition.y, _animationTime)
                    .SetEase(Ease.Linear));
        }

        public void ShowBackTitleButton()
        {
            DOTween.Sequence()
                .Append(turnEndButton
                    .DOAnchorPosY(-turnEndButton.anchoredPosition.y, _animationTime)
                    .SetEase(Ease.Linear))
                .Append(backTitleButton
                    .DOAnchorPosY(-backTitleButton.anchoredPosition.y, _animationTime)
                    .SetEase(Ease.Linear));
        }
    }
}