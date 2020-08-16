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
        private readonly float _buttonHeight = 75f;

        public void Play()
        {
            DisplayPlayerName();
            ShowTurnEndButton();
        }

        private void DisplayPlayerName()
        {
            playerName.text = $"{PlayerNameRegister.PlayerName}さん";
            enemyName.text = $"{PhotonNetwork.otherPlayers[0].NickName}さん";
        }

        private void ShowTurnEndButton()
        {
            DOTween.Sequence()
                .Append(backTitleButton
                    .DOAnchorPosY(_buttonHeight * -1, _animationTime)
                    .SetEase(Ease.Linear))
                .Append(turnEndButton
                    .DOAnchorPosY(_buttonHeight, _animationTime)
                    .SetEase(Ease.Linear));
        }

        public void ShowBackTitleButton()
        {
            DOTween.Sequence()
                .Append(turnEndButton
                    .DOAnchorPosY(_buttonHeight * -1, _animationTime)
                    .SetEase(Ease.Linear))
                .Append(backTitleButton
                    .DOAnchorPosY(_buttonHeight, _animationTime)
                    .SetEase(Ease.Linear));
        }
    }
}