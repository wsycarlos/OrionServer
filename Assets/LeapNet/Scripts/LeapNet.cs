using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using VoiceChat.Networking;

namespace LeapNet
{
    public class LeapNet : NetworkManager
    {
        public GameObject customPref;

        void Start()
        {
            StartHost();
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            Debug.Log("Client Connect!");
            base.OnServerConnect(conn);
            conn.SetChannelOption(2, ChannelOption.MaxPendingBuffers, 128);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            Debug.Log("Client Disconnect!");
            base.OnServerDisconnect(conn);
            VoiceChatNetworkProxy.OnManagerServerDisconnect(conn);
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        public override void OnStartServer()
        {
            VoiceChatNetworkProxy.OnManagerStartServer();
        }

        public override void OnStartClient(NetworkClient client)
        {
            VoiceChatNetworkProxy.OnManagerStartClient(client, customPref);
        }

        public override void OnStopClient()
        {
            VoiceChatNetworkProxy.OnManagerStopClient();
        }


        public override void OnStopServer()
        {
            VoiceChatNetworkProxy.OnManagerStopServer();
        }

        public override void OnClientConnect(NetworkConnection connection)
        {
            base.OnClientConnect(connection);
            VoiceChatNetworkProxy.OnManagerClientConnect(connection);
        }
    }
}