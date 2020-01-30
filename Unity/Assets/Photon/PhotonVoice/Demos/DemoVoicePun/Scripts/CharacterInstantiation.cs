// ----------------------------------------------------------------------------
// <copyright file="CharacterInstantiation.cs" company="Exit Games GmbH">
// Photon Voice Demo for PUN- Copyright (C) 2016 Exit Games GmbH
// </copyright>
// <summary>
// Class that handles character instantiation when the actor is joined.
// It adds multiple prefabs support to OnJoinedInstantiate.
// </summary>
// <author>developer@photonengine.com</author>
// ----------------------------------------------------------------------------

namespace ExitGames.Demos.DemoPunVoice
{
    using ExitGames.Client.Photon;
    using Photon.Realtime;
    using UnityEngine;
    using Photon.Pun;

    public class CharacterInstantiation : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        public Transform SpawnPosition1;
        public Transform SpawnPosition2;
        public Transform SpawnPosition3;
        public Transform SpawnPosition4;
        
        public float PositionOffset = 2.0f;
        public GameObject[] PrefabsToInstantiate; // set in inspector

        public delegate void OnCharacterInstantiated(GameObject character);
        public static event OnCharacterInstantiated CharacterInstantiated;

        private const byte manualInstantiationEventCode = 1;

        [SerializeField]
        private bool manualInstantiation;

        public override void OnJoinedRoom()
        {
            if (this.PrefabsToInstantiate != null)
            {
                int index = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % 4;
                Vector3 spawnPos = Vector3.zero;
                Transform[] SpawnPositions = new Transform[]
                {
                    SpawnPosition1,
                    SpawnPosition2,
                    SpawnPosition3,
                    SpawnPosition4
                };
                spawnPos = SpawnPositions[PhotonNetwork.CountOfPlayers].position;
                
                //Vector3 random = Random.insideUnitSphere;
                //random = this.PositionOffset * random.normalized;
                //spawnPos += random;
                //Camera.main.transform.position += spawnPos;


                if (manualInstantiation)
                {
                    this.ManualInstantiation(index, spawnPos);
                }
                else
                {
                    GameObject o = PrefabsToInstantiate[index];
                    o = PhotonNetwork.Instantiate(o.name, spawnPos, Quaternion.identity);
                    if (CharacterInstantiated != null)
                    {
                        CharacterInstantiated(o);
                    }
                }
            }
        }

        private void ManualInstantiation(int index, Vector3 position)
        {
            GameObject prefab = PrefabsToInstantiate[index];
            GameObject player = Instantiate(prefab, position, Quaternion.identity);
            PhotonView photonView = player.GetComponent<PhotonView>();

            if (PhotonNetwork.AllocateViewID(photonView))
            {
                object[] data =
                {
                    index, player.transform.position, photonView.ViewID
                };

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others,
                    CachingOption = EventCaching.AddToRoomCache
                };

                PhotonNetwork.RaiseEvent(manualInstantiationEventCode, data, raiseEventOptions, SendOptions.SendReliable);
                if (CharacterInstantiated != null)
                {
                    CharacterInstantiated(player);
                }
            }
            else
            {
                Debug.LogError("Failed to allocate a ViewId.");

                Destroy(player);
            }
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == manualInstantiationEventCode)
            {
                object[] data = photonEvent.CustomData as object[];
                int prefabIndex = (int) data[0];
                GameObject prefab = PrefabsToInstantiate[prefabIndex];
                Vector3 position = (Vector3)data[1];
                GameObject player = Instantiate(prefab, position, Quaternion.identity);
                PhotonView photonView = player.GetComponent<PhotonView>();
                photonView.ViewID = (int) data[2];
            }
        }
    }
}