using System.Threading;
using Cysharp.Threading.Tasks;
using Games.Players;
using TMPro;
using UnityEngine;

namespace Online.Controllers
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class StartPresenter : MonoBehaviour
    {
        private PhotonView _photonView;
        [SerializeField] private TextMeshProUGUI playerName = null;
        [SerializeField] private TextMeshProUGUI enemyName = null;
        [SerializeField] private TextMeshProUGUI matchingText = null;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void SetStartGame()
        {
            _photonView.RPC(nameof(StartGame), PhotonTargets.All);
        }

        [PunRPC]
        private void StartGame()
        {
            playerName.text = $"Player:{PlayerNameRegister.PlayerName}";
            enemyName.text = $"Enemy:{PhotonNetwork.otherPlayers[0].NickName}";
            matchingText.gameObject.SetActive(false);

            var token = this.GetCancellationTokenOnDestroy();
            CheckDisconnectedAsync(token).Forget();
        }

        private async UniTaskVoid CheckDisconnectedAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(() => PhotonNetwork.room.PlayerCount != 2, cancellationToken: token);

            matchingText.gameObject.SetActive(true);
            matchingText.text = $"通信が切断されました。";
        }
    }
}