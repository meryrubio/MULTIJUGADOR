using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Photon Callbacks

        public override void OnLeftRoom()//Llamada cuando el jugador local abandona la sala. 
        {
            SceneManager.LoadScene(0);//Necesitamos cargar la escena de lanzamiento.
        }
        //si no estamos en una habitación necesitamos mostrar la escena Launcher,
        //así que vamos a escuchar a OnLeftRoom() Photon Callback y cargar la escena Launcher del lobby,
        //que está indexada 0 en la lista de escenas Build settings, que configuraremos en la sección Build Settings Scene List 


        public override void OnPlayerEnteredRoom(Player other) //Cada vez que un jugador entre a la sala, seremos informados, y llamaremos al método LoadArena()
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // no se ve si eres el jugador que se conecta, como no estas dentro no recibes los mensajes de room

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // llamado antes de OnPlayerLeftRoom

                LoadArena();// llamaremos a LoadArena() SOLO si somos el MasterClient usando PhotonNetwork.IsMasterClient.
            }
        }
        
        public override void OnPlayerLeftRoom(Player other) //Cada vez que un jugador sale de la sala, seremos informados, y llamaremos al método LoadArena()
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // se ve cuando otro se desconecta

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // llamado antes de OnPlayerLeftRoom

                LoadArena();// llamaremos a LoadArena() SOLO si somos el MasterClient usando PhotonNetwork.IsMasterClient.
                //llamamos al load arena tanto cuando entramos como cuando sale un jugador porque si somos 4 y se va uno hay que cargar una room de 3
            }
        }

        #endregion


        #region Public Methods

        public void LeaveRoom()//el jugador local abandone la sala de la Photon Network room
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion


        #region Private Methods

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)// primero comprobamos que somos el MasterClient usando PhotonNetwork.IsMasterClient
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");//si no somos master client = error
                return;
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);//Usamos PhotonNetwork.LoadLevel() para cargar el nivel que queremos
        }

        #endregion
    }
}
