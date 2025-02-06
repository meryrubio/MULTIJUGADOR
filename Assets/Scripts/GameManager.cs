using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.TextCore.Text;

namespace Com.MyCompany.MyGame
{

    [System.Serializable]
    public enum Characters //tipo enumerado, le puede poner nombres a los numeros: el 0=juandi, 1= mery
    {
        JUANDI, MERY
    }
    public class GameManager : MonoBehaviourPunCallbacks
    {

        public static GameManager instance;


        [Tooltip("The prefab to use for representing the player")]
        public GameObject[] playerPrefab;

        //[HideInInspector]
        public Characters characterType; //variable para los personajes, sea posible elegir el tipo de personaje

        private void Awake()
        {
            if (!instance)// si instance no tiene info
            {
                instance = this; //si isma llega a la fiesta y ve que no hay nadie guapo isma se queda en la fiesta / instance se asigna a este objeto
                DontDestroyOnLoad(gameObject);// para que no se destruya en la carga de escenas
            }
            else // si ya hay alguin mas guapo antes que isma / si instance tiene info
            {
                Destroy(gameObject); // isma se va / se destruye el gameobject, para que no haya dos o mas gms en el juego
            }
        }

        public void InstancePlayer()
        {
            if (PlayerManager.LocalPlayerInstance == null)//Con estas modificaciones, podemos implementar la comprobación para instanciar sólo si es necesario
                                                          //dentro del script GameManager.
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                // estamos en una habitación. genera un personaje para el jugador local. se sincroniza usando PhotonNetwork.Instantiate
                StartCoroutine(DelayInstance());
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }

        }

        IEnumerator DelayInstance()// esperar segundo para que el segundo cargue y pueda ver
        {
            yield return new WaitForSeconds(0.3f);

            switch(characterType)
            {
                case Characters.JUANDI:
                    PhotonNetwork.Instantiate(this.playerPrefab[0].name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                    break;
                case Characters.MERY:
                    PhotonNetwork.Instantiate(this.playerPrefab[1].name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                    break;
            }


        }


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


        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            /* AudioManager.instance.ClearAudios();*/ // oye, audioManager, limpia todos los sonidos que estan sonando
        }


        public void ExitGame()
        {
            Debug.Log("EXIT!!");
            Application.Quit();// cierra la aplicación
        }
    }

}
