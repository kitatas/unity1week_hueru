using System;
using Games.Players;
using UnityEngine;
using Zenject;

namespace Online.Controllers
{
    /// <summary>
    /// マッチング制御のクラス
    /// </summary>
    public sealed class MatchingController : MonoBehaviour
    {
        private OnlineGameController _onlineGameController;

        [Inject]
        private void Construct(OnlineGameController onlineGameController)
        {
            _onlineGameController = onlineGameController;

            PhotonNetwork.ConnectUsingSettings(Application.version);
        }

        /// <summary>
        /// ロビーに入ると呼ばれる
        /// </summary>
        private void OnJoinedLobby()
        {
            // ルームに入室する
            PhotonNetwork.JoinRandomRoom();
        }

        /// <summary>
        /// room入室失敗時に呼ばれる
        /// </summary>
        private void OnPhotonRandomJoinFailed()
        {
            var roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.MaxPlayers = 2;
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
                {{"CustomProperties", "カスタムプロパティ"}};
            roomOptions.CustomRoomPropertiesForLobby = new string[] {"CustomProperties"};

            var roomName = Guid.NewGuid().ToString("N").Substring(0, 4);
            PhotonNetwork.CreateRoom(roomName, roomOptions, null);
        }

        /// <summary>
        /// room入室時に呼ばれる
        /// </summary>
        private void OnJoinedRoom()
        {
            PhotonNetwork.player.NickName = PlayerNameRegister.PlayerName;

            if (IsMaxPlayer())
            {
                PhotonNetwork.room.IsOpen = false;
                _onlineGameController.StartGame();
            }
        }

        /// <summary>
        /// room 内のプレイヤー人数が2人
        /// </summary>
        /// <returns></returns>
        private static bool IsMaxPlayer()
        {
            return PhotonNetwork.room.PlayerCount == 2;
        }

        /// <summary>
        /// 対戦相手名
        /// </summary>
        public static string EnemyName => PhotonNetwork.otherPlayers[0].NickName;
    }
}