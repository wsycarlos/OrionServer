using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace LeapNet
{
    public class LeapNet : NetworkManager
    {
        void Start()
        {
            StartServer();
        }

        //public override void OnServerConnect(NetworkConnection conn)
        //{
        //    Debug.Log("Client Connect!");
        //}
        
        //public override void OnServerDisconnect(NetworkConnection conn)
        //{
        //    Debug.Log("Client Disconnect!");
        //}

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }
}