using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Networking;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonLobby : MonoBehaviourPunCallbacks
    {
        public static PhotonLobby Lobby;

        private int roomNumber = 1;
        private int userIdCount;
        public string roomName = "";

        private void Awake()
        {
            if (Lobby == null)
            {
                Lobby = this;
            }
            else
            {
                if (Lobby != this)
                {
                    Destroy(Lobby.gameObject);
                    Lobby = this;
                }
            }

            DontDestroyOnLoad(gameObject);

            GenericNetworkManager.OnReadyToStartNetwork += StartNetwork;
        }

        public override void OnConnectedToMaster()
        {
            var randomUserId = UnityEngine.Random.Range(0, 999999);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.AuthValues = new AuthenticationValues();
            PhotonNetwork.AuthValues.UserId = randomUserId.ToString();
            userIdCount++;
            PhotonNetwork.NickName = PhotonNetwork.AuthValues.UserId;
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            Debug.Log("\nPhotonLobby.OnJoinedRoom()");
            Debug.Log("Current room name: " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("Other players in room: " + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("Total players in room: " + (PhotonNetwork.CountOfPlayersInRooms + 1));
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            CreateRoom();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("\nPhotonLobby.OnCreateRoomFailed()");
            Debug.LogError("Creating Room Failed");
            CreateRoom();
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            roomNumber++;
        }

        public void OnCancelButtonClicked()
        {
            PhotonNetwork.LeaveRoom();
        }

        private void StartNetwork()
        {
            // Try pinging google to see if response - Try Web Request
            StartCoroutine(checkInternetConnection((isConnected)=>{
            // handle connection status here
            if(isConnected)
            {
                PhotonNetwork.OfflineMode = false;
                Lobby = this;
                PhotonNetwork.ConnectUsingSettings();
                Debug.Log("You are Online. Multiplayer");
            }
            else
            {
                PhotonNetwork.OfflineMode = true;
                PhotonNetwork.ConnectUsingSettings();
                Debug.Log("No Internet connection. Offline mode ");
            }
            }));
            
        }

        IEnumerator checkInternetConnection(Action<bool> action)
        {
            UnityWebRequest www = new UnityWebRequest("http://google.com");
            yield return www.SendWebRequest();
            
            if (www.isNetworkError || www.isHttpError) {
                action (false);
            } else {
                action (true);
            }
        } 

        private void CreateRoom()
        {
            var roomOptions = new RoomOptions {IsVisible = true, IsOpen = true, MaxPlayers = 10, EmptyRoomTtl = 5};
            if(roomName != "") {
                PhotonNetwork.CreateRoom(roomName, roomOptions);

            } else {
                PhotonNetwork.CreateRoom("Room" + UnityEngine.Random.Range(1, 3000), roomOptions);
            }
        }
    }
}
