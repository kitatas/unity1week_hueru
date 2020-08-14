using System;
using Games.Players;
using UnityEngine;
using Zenject;

namespace Online.Controllers
{
    public sealed class MatchingController : MonoBehaviour
    {
        private StartPresenter _startPresenter;

        [Inject]
        private void Construct(StartPresenter startPresenter)
        {
            _startPresenter = startPresenter;

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

        private void OnJoinedRoom()
        {
            PhotonNetwork.player.NickName = PlayerNameRegister.PlayerName;

            // room 内のプレイヤー人数が2人
            if (IsMaxPlayer())
            {
                PhotonNetwork.room.IsOpen = false;
                _startPresenter.SetStartGame();
            }
        }

        private static bool IsMaxPlayer()
        {
            return PhotonNetwork.room.PlayerCount == 2;
        }
    }
}