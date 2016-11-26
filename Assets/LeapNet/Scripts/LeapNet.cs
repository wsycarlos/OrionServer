using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace LeapNet
{
    public class LeapNet : NetworkManager
    {
        public TextMesh debug;

        void Start()
        {
            StartServer();
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            debug.gameObject.SetActive(true);
            Debug.Log("Client Connect!");
        }
        
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            Debug.Log("Client Disconnect!");
            debug.gameObject.SetActive(false);
        }
    }
}