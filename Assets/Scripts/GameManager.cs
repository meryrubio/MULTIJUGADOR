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

        //si no estamos en una habitaci�n necesitamos mostrar la escena Launcher,
        //as� que vamos a escuchar a OnLeftRoom() Photon Callback y cargar la escena Launcher del lobby,
        //que est� indexada 0 en la lista de escenas Build settings, que configuraremos en la secci�n Build Settings Scene List 

        #endregion


        #region Public Methods

        public void LeaveRoom()//el jugador local abandone la sala de la Photon Network room
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
    }
}
