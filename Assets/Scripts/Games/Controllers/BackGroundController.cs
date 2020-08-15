using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Controllers
{
    public sealed class BackGroundController : MonoBehaviour
    {
        private Canvas _canvas;
        [SerializeField] private Image image = null;
        private static readonly int _mainTex = Shader.PropertyToID("_MainTex");

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => _canvas.worldCamera == null)
                .Subscribe(_ => _canvas.worldCamera = Camera.main)
                .AddTo(this);

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var scroll = Mathf.Repeat (Time.time * 0.2f, 1);
                    var offset = new Vector2 (scroll, scroll);
                    image.material.SetTextureOffset(_mainTex, offset);
                })
                .AddTo(this);
            
            // image.material.SetTextureOffset(_mainTex, Vector2.zero);
        }
    }
}