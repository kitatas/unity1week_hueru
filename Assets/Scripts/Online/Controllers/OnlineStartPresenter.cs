using Games.Players;
using TMPro;
using UnityEngine;

namespace Online.Controllers
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class OnlineStartPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName = null;
        [SerializeField] private TextMeshProUGUI enemyName = null;

        public void DisplayText()
        {
            playerName.text = $"Player:{PlayerNameRegister.PlayerName}";
            enemyName.text = $"Enemy:{PhotonNetwork.otherPlayers[0].NickName}";
        }
    }
}