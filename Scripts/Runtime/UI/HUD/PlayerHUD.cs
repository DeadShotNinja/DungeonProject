using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace DP.Runtime
{
    public class PlayerHUD : NetworkBehaviour
    {
        // TODO: Update this system, currently just used for testing.

        [SerializeField] private GameObject _playerPrefab;
        
        [Header("Test Buttons")]
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _startButton;

        private void Awake()
        {
            _hostButton.onClick.AddListener(StartHost);
            _joinButton.onClick.AddListener(StartClient);
            _startButton.onClick.AddListener(StartGame);
        }

        private void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        
        private void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }
        
        private void StartGame()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                foreach (ulong client in NetworkManager.Singleton.ConnectedClients.Keys)
                {
                    var spawnedPlayer = Instantiate(_playerPrefab);
                    spawnedPlayer.GetComponent<NetworkObject>().SpawnAsPlayerObject(client);
                    spawnedPlayer.GetComponent<PlayerEntity>().SetupForNetwork(client);
                }
                
                DisableHUDClientRpc();
            }
            
        }
        
        [ClientRpc]
        private void DisableHUDClientRpc()
        {
            gameObject.SetActive(false);
        }
    }
}
