using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement_RB : MonoBehaviourPun
{
    private Rigidbody rb;
    public float speed, jumpForce, rotationSpeed, sphereRadius;/*gravityscale*/ // rotationSpeed o mouseSens para que el diseñador pueda elegir a que velocidad rotar
    private float X, Z, mouseX; // input
    private bool jumpPressed;
    public string GroundName;
    private CameraWork _cameraWork;

    public TextMeshProUGUI playerNameText; //objeto de texto



    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        // usado en GameManager.cs: mantenemos un registro de la instancia localPlayer para evitar la instanciación cuando los niveles están sincronizados
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        // marcamos como no destruir al cargar para que la instancia sobreviva a la sincronización de niveles, dando así una experiencia fluida cuando se cargan los niveles.
        DontDestroyOnLoad(this.gameObject);//**************************
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

        // el nombre del jugador se muestre al inicio
        if (playerNameText != null)
        {
            playerNameText.text = PhotonNetwork.NickName;
        }


        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        }

#if UNITY_5_4_OR_NEWER
        // Unity 5.4 tiene una nueva gestión de escenas. registre un método para llamar a CalledOnLevelWasLoaded.
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif

        /* gravityscale = -Mathf.Abs(gravityscale);*/ //la gravedad tinene que ser negativa para que vaya para abajo, entonces se multiplica el menos por el valosr absoluto de gravityscale.
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        //en edit - project settings - input manager - axes - estan todas las que exitsen y se pueden modificar y añadir
        X = Input.GetAxis("Horizontal"); //Raw = crudo, es para que no tenga ningún tipo de suavizado (joystick)
        Z = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X"); //mouse para la rotación / la sensibilidad del raton ya va asociada al input

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jumpPressed = true;
        }

        //RotatePlayer();

        playerNameText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0)); // Ajusta el nombre del jugador???

    }

    void RotatePlayer()
    {
        Vector3 rotation = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime; // gira en el eje Y -> mouseX
        transform.Rotate(rotation); //rota en la direccion en la que está / quaterniones/
        // si quiero que un objeto rote automaticamente solo ponemos una velocidad constante -> Vector3 rotation = new Vector3(0, 5, 0) * Time.deltaTime;
        // si quiero que quire en cualquier eje porngo el mouseX en la coordenada -> (mouseX, 0, 0) en x, (0, 0, mouseX) en z
    }

    private void FixedUpdate()
    {
        //Dividimos en metodos el codigo para organizarlo -> REFACTORIZAR el código
        ApplySpeed();
        ApplyJumpForce();

    }

    void ApplySpeed()
    {
        rb.velocity = (transform.forward * speed * Z) + (transform.right * speed * X) + new Vector3(0, rb.velocity.y, 0); //esta forma es para que la gravedad sea ajustable a cada objeto / GRAVEDAD BASE DE UNITY

        /*+ (transform.up * gravityscale)*/ //forward es el eje z -> el azul // vector por escalar -> (0,0,1)*5 // z para que avance solo cuando pulsemos las teclas asiganadas ese eje
                                            //right es el eje x -> el rojo // ""
                                            //up es el eje y -> el verde // la gravedad -> NO realista, personalizada
                                            //sumamos coordenadas (0,0,5)+(0,5,0)=(0,5,5)

        /* rb.AddForce(transform.up * gravityscale);*/ //up es el eje y -> el verde // gravedad realista

    }

    void ApplyJumpForce()
    {
        if (jumpPressed)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); //para que la velocidad en x ,z se mantenga  y en y se reinicie.
            rb.AddForce(transform.up * jumpForce);
            jumpPressed = false;
        }
    }

    private bool IsGrounded() // para que detecte que toque el suelo para saltar
    {
        RaycastHit[] colliders = Physics.SphereCastAll(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius, Vector3.up);
        //array de colliders

        for (int i = 0; i < colliders.Length; i++) //recorremos elemento a elemnto
        {
            // y comprobamos si ese elemto es suelo
            if (colliders[i].collider.gameObject.layer == LayerMask.NameToLayer(GroundName))// el collider punto . . -> es bucear para encontrar las propiedades del elemnto que buscamos
            {                                                                               // la layer es un numero y la he puesto un nombre, asi que busca en el array de layers el nombre que le he puesto y se asigna
                return true;
            }

        }
        return false;
    }

    private void OnDrawGizmos() //para que se vea la esfera que toca al suelo
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius);
    }


    #region MonoBehaviour CallBacks


#if !UNITY_5_4_OR_NEWER

void OnLevelWasLoaded(int level)
{
    this.CalledOnLevelWasLoaded(level);
}
#endif

    void CalledOnLevelWasLoaded(int level)
    {
        // comprueba si estamos fuera de la Arena y si es el caso, haz spawn alrededor del centro de la arena en una zona segura
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }
    }

    #endregion

#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
#endif

#if UNITY_5_4_OR_NEWER
    public void OnDisable()
    {
        // Always call the base to remove callbacks
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
#endif
}