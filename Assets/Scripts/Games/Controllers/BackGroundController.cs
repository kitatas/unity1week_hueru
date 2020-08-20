using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Controllers
{
    [RequireComponent(typeof(Canvas))]
    public sealed class BackGroundController : MonoBehaviour
    {
        [SerializeField] private Image image = null;

        private static readonly int _mainTex = Shader.PropertyToID("_MainTex");

        private void Start()
        {
            var canvas = GetComponent<Canvas>();

            this.UpdateAsObservable()
                .Where(_ => canvas.worldCamera == null)
                .Subscribe(_ => canvas.worldCamera = FindObjectOfType<Camera>())
                .AddTo(this);

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var scroll = Mathf.Repeat(Time.time * 0.2f, 1.0f);
                    var offset = new Vector2(scroll, scroll);
                    image.material.SetTextureOffset(_mainTex, offset);
                })
                .AddTo(this);
        }
    }
}