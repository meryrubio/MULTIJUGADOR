using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private List<GameObject> audioList;// todos los obljetos que esten sonando nos los vamos a guardar

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioList = new List<GameObject>();
    }

    private void Start()
    {

    }


    public AudioSource PlayAudio(AudioClip audioClip, string gameObjectName, bool isLoop = false, float volume = 1.0f) // isLoop y float volume --> parametros por defectos (valores por defecto), tienen que ir al final y no pueden estar intercalados
    {
        GameObject audioObject = new GameObject(gameObjectName); // para darle nombre e identificarlo en la escena 
        //ÇaudioObject.transform.SetParent(transform); // todos los objetos del audio que se van creando sean hijos del audio manager.
        AudioSource audioSourceComponent = audioObject.AddComponent<AudioSource>();// le añado el componente de AudioSource 
        audioSourceComponent.clip = audioClip; // asignamos el clip al compenente y el clip que asignamos es nuestro metodo
        audioSourceComponent.volume = volume; // ""
        audioSourceComponent.loop = isLoop; // ""

        audioSourceComponent.Play(); //inicie el audio

        audioList.Add(audioObject); // llevar un seguimiento de los objetos que estan sonando en la escena

        if (!isLoop) //si el audio no esta en loop espera a que acabe para destruirlo
        {
            StartCoroutine(WaitAudioEnd(audioSourceComponent)); //**corrutina
        }


        return audioSourceComponent; // porsiaca  desde otros componentes queremos utilizar otros parametros del AudioSource
    }

    public AudioSource PlayAudio3D(AudioClip audioClip, string gameObjectName, GameObject owner, float minDistance, float maxDistance, bool isLoop = false, float volume = 1.0f)
    {
        AudioSource audioSorce = PlayAudio(audioClip, gameObjectName, isLoop, volume);
        audioSorce.gameObject.transform.SetParent(owner.transform);
        audioSorce.spatialBlend = 1f;
        audioSorce.minDistance = minDistance;
        audioSorce.maxDistance = maxDistance;
        audioSorce.gameObject.transform.localPosition = Vector3.zero;
        return audioSorce;
    }

    //IEnumerator --> es una **corrutina, es un mecanismo que tiene unity para simular una especie de hilos y procesos (unity no tinene hilos y procesos); permite ejecutarse en cada frame o, si queremos que espere un cierto numero de segundos antes de continuar su ejecucion.
    IEnumerator WaitAudioEnd(AudioSource src) //esperar a que el audio acabe
    {
        while (src && src.isPlaying)
        {
            yield return null;  // yield le devuelve el control a unity
        }

        Destroy(src.gameObject);
    }

    public void ClearAudios()
    {
        // por todos audios de la lista tenemos que destruirlos a todos, es un recorrido --> foreach
        foreach (GameObject audioObject in audioList)
        {
            Destroy(audioObject);
        }

        audioList.Clear(); //borra todo el contenido de la lista ( por qué se queda memoria y hay que borrarla tambien)
    }
}
