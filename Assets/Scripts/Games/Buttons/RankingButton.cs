using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Games.Buttons
{
    /// <summary>
    /// ランキングシーンを読み込むボタン
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class RankingButton : MonoBehaviour
    {
        [SerializeField] private GameObject effect = null;

        private Button _button;
        private Camera _mainCamera;
        private IScoreModel _scoreModel;
        private RankingLoader _rankingLoader;

        [Inject]
        private void Construct(Camera mainCamera, IScoreModel scoreModel)
        {
            _button = GetComponent<Button>();
            _mainCamera = mainCamera;
            _scoreModel = scoreModel;
            _rankingLoader = RankingLoader.Instance;

            _button.enabled = false;
        }

        private void Start()
        {
            _button
                .OnClickAsObservable()
                .Subscribe(_ => _rankingLoader.SendScoreAndShowRanking(_scoreModel.GetScore()))
                .AddTo(this);
        }

        /// <summary>
        /// ボタン表示時の演出
        /// </summary>
        public void PlayFadeIn()
        {
            _button.FadeInButton();

            var position = _button.image.rectTransform.GetWorldPosition(_mainCamera);
            Instantiate(effect, position, Quaternion.identity);
        }
    }
}