using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.PackageManager;

namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        //las regiones nos permiten agrupar secciones del c�digo para que sea m�s f�cil de leer y navegar.

        #region Private Serializable Fields

        #endregion


        #region Private Fields

        string gameVersion = "1";//El n�mero de versi�n de este cliente.
                                 //Los usuarios est�n separados entre s� por gameVersion

        #endregion


        #region MonoBehaviour CallBacks
        //M�todo MonoBehaviour llamado en GameObject por Unity durante la fase de inicializaci�n temprana.

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true; // esto asegura que podamos usar PhotonNetwork.LoadLevel() en el cliente maestro
                                                         // y que todos los clientes en la misma sala sincronicen su nivel autom�ticamente
        }

        void Start()
        {
            Connect();
        }

        #endregion


        #region Public Methods

        public void Connect() // Iniciamos el proceso de conexi�n.
        // - Si ya est� conectado, intentamos entrar en una sala aleatoria
        // - Si a�n no est� conectado, Conectar esta instancia de aplicaci�n a Photon Cloud 
        {
            if (PhotonNetwork.IsConnected) //comprobamos si estamos conectados o no, nos unimos si lo estamos , sino iniciamos la conexi�n con el servidor.
            {
                PhotonNetwork.JoinRandomRoom();
                //necesitamos en este punto intentar unirnos a una Random Room. Si falla, recibiremos una notificaci�n en OnJoinRandomFailed() y crearemos una.
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


