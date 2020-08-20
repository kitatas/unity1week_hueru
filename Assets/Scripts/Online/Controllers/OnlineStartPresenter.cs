using DG.Tweening;
using Games.Players;
using TMPro;
using UnityEngine;

namespace Online.Controllers
{
    /// <summary>
    /// StartPresenterと併用
    /// ゲーム開始演出を行うクラス
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public sealed class OnlineStartPresenter : MonoBehaviour
    {
        [SerializeField] private RectTransform backTitleButton = null;
        [SerializeField] private RectTransform turnEndButton = null;
        [SerializeField] private TextMeshProUGUI playerName = null;
        [SerializeField] private TextMeshProUGUI enemyName = null;

        private readonly float _animationTime = 0.25f;
        private readonly float _buttonHeight = 75f;

        /// <summary>
        /// 開始演出の実行
        /// </summary>
        public void Play()
        {
            DisplayPlayerName();
            ShowTurnEndButton();
        }

        /// <summary>
        /// 対戦相手の名前を取得
        /// </summary>
        private void DisplayPlayerName()
        {
            playerName.text = $"{PlayerNameRegister.PlayerName}さん";
            enemyName.text = $"{MatchingController.EnemyName}さん";
        }

        /// <summary>
        /// ターン終了ボタンを画面内に
        /// </summary>
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

        /// <summary>
        /// タイトルに戻るボタンを画面内に
        /// </summary>
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