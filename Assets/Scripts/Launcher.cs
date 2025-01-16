using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.PackageManager;


namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public GameObject controlPanel, loadingPanel;

        bool isConnecting;//Mantener el seguimiento del proceso actual

        //modificaremos el número máximo de jugadores por sala y lo expondremos en el inspector para poder configurarlo sin tocar el propio código.
        // lo mismo para el número máximo de jugadores por sala.
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        //las regiones nos permiten agrupar secciones del código para que sea más fácil de leer y navegar.

        #region Private Serializable Fields

        #endregion


        #region Private Fields

        string gameVersion = "1";//El número de versión de este cliente.
                                 //Los usuarios están separados entre sí por gameVersion

        #endregion


        #region MonoBehaviour CallBacks
        //Método MonoBehaviour llamado en GameObject por Unity durante la fase de inicialización temprana.

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true; // esto asegura que podamos usar PhotonNetwork.LoadLevel() en el cliente maestro
                                                         // y que todos los clientes en la misma sala sincronicen su nivel automáticamente
        }

        void Start()
        {
            loadingPanel.SetActive(false);//nada mas empezamos queremos que salgan los botones y luego el de carga
            controlPanel.SetActive(true);
        }

        #endregion


        #region Public Methods

        public void Connect() // Iniciamos el proceso de conexión.
        // - Si ya está conectado, intentamos entrar en una sala aleatoria
        // - Si aún no está conectado, Conectar esta instancia de aplicación a Photon Cloud 
        {
            loadingPanel.SetActive(true ); //queremos que se active el loading y ya los botones se escondan
            controlPanel.SetActive(false );

            if (PhotonNetwork.IsConnected) //comprobamos si estamos conectados o no, nos unimos si lo estamos , sino iniciamos la conexión con el servidor.
            {
                PhotonNetwork.JoinRandomRoom();
                //necesitamos en este punto intentar unirnos a una Random Room. Si falla, recibiremos una notificación en OnJoinRandomFailed() y crearemos una.
            }
            else
            {
                //antes que nada debemos conectarnos a Photon Online Server.
                isConnecting = PhotonNetwork.ConnectUsingSettings();//mantener un registro de la voluntad de unirse a una sala,
                                                                    //porque cuando volvamos del juego recibiremos un callback de que estamos conectados,
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()//cuando te conectas al servidor
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

            // no queremos hacer nada si no estamos intentando unirnos a una sala.
            // este caso donde isConnecting es falso es tipicamente cuando perdiste o saliste del juego,
            // cuando este nivel es cargado, OnConnectedToMaster será llamado, en ese caso no queremos hacer nada.
            if (isConnecting)
            {
                //Lo primero que intentamos hacer es unirnos a una potencial sala existente. Si la hay, bien, si no, seremos llamados con OnJoinRandomFailed()
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
          
        }

        public override void OnJoinRandomFailed(short returnCode, string message)//creamos una sala usando PhotonNetwork.CreateRoom() y,
                                                                                 //el callback PUN relacionado OnJoinedRoom() que informará a tu script cuando efectivamente nos unamos a una sala
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });//anadimos el numero max de personas a la sala que creamos
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)//Sólo cargamos si somos el primer jugador,de lo contrario confiamos en PhotonNetwork.AutomaticallySyncScene                                         
            {
                Debug.Log("We load the 'Room for 1' ");

               
                // Carga la Room Level.
                PhotonNetwork.LoadLevel("Room for 1");
            }
        }

        public override void OnDisconnected(DisconnectCause cause)//cuando te desconectas
        {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
            isConnecting = false;
        }

        #endregion
    }

}


