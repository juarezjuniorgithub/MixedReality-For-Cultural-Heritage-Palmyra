using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public static PhotonRoom Room;
        [SerializeField] bool oculus;

        [SerializeField] private GameObject photonUserPrefab = default;
        [SerializeField] private GameObject photonUserOculusPrefab = default;


        // private PhotonView pv;
        private Player[] photonPlayers;
        private int playersInRoom;
        private int myNumberInRoom;

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom++;
        }

        private void Awake()
        {
            if (Room == null)
            {
                Room = this;
            }
            else
            {
                if (Room != this)
                {
                    Destroy(Room.gameObject);
                    Room = this;
                }
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void Start()
        {
            // Allow prefabs not in a Resources folder
            if (PhotonNetwork.PrefabPool is DefaultPool pool)
            {
                if (photonUserPrefab != null) pool.ResourceCache.Add(photonUserPrefab.name, photonUserPrefab);
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            photonPlayers = PhotonNetwork.PlayerList;
            playersInRoom = photonPlayers.Length;
            myNumberInRoom = playersInRoom;
            PhotonNetwork.NickName = myNumberInRoom.ToString();

            StartGame();
        }

        private void StartGame()
        {
            CreatPlayer();

            if (!PhotonNetwork.IsMasterClient) return;
        }

        private void CreatPlayer()
        {
            if (oculus) {
                var player = PhotonNetwork.Instantiate(photonUserOculusPrefab.name, Vector3.zero, Quaternion.identity);
            } else {
                var player = PhotonNetwork.Instantiate(photonUserPrefab.name, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
