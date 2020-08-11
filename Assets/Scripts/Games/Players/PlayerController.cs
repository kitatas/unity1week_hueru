using Configs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Games.Players
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject[] obj = null;

        private void Start()
        {
            var mainCamera = GetCamera();

            // オブジェクトのクリック
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ =>
                {
                    // Rayでのオブジェクト取得
                    var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    var hit = Physics2D.Raycast(ray.origin, ray.direction);
                    if (hit == false)
                    {
                        return;
                    }

                    var clickObject = hit.collider.gameObject;
                    if (clickObject.CompareTag(Tag.STAGE_OBJECT))
                    {
                        // ふやす処理
                        Instantiate(GetGenerateObject(), clickObject.transform.position, Quaternion.identity);
                    }
                })
                .AddTo(this);
        }

        private static Camera GetCamera()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }

            return mainCamera;
        }

        private GameObject GetGenerateObject()
        {
            return obj[Random.Range(0, obj.Length)];
        }
    }
}