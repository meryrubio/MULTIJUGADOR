using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.PackageManager;

namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
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
            Connect();
        }

        #endregion


        #region Public Methods

        public void Connect() // Iniciamos el proceso de conexión.
        // - Si ya está conectado, intentamos entrar en una sala aleatoria
        // - Si aún no está conectado, Conectar esta instancia de aplicación a Photon Cloud 
        {
            if (PhotonNetwork.IsConnected) //comprobamos si estamos conectados o no, nos unimos si lo estamos , sino iniciamos la conexión con el servidor.
            {
                PhotonNetwork.JoinRandomRoom();
                //necesitamos en este punto intentar unirnos a una Random Room. Si falla, recibiremos una notificación en OnJoinRandomFailed() y crearemos una.
            }
            else
            {
                //antes que nada debemos conectarnos a Photon Online Server.
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()//cuando te conectas al servidor
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        }

        public override void OnDisconnected(DisconnectCause cause)//cuando te desconectas
        {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        #endregion
    }

}


