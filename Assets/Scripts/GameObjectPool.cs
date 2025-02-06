using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{

    private List<GameObject> _pool;
    public GameObject objectToPool;
    [Tooltip("Initial pool size")]
    public uint poolSize; //iunt = entero in signo
    [Tooltip("If true, size increments")]
    public bool shouldExpand = false; //la pool tiene que tener la opcion de poder expandirse

    // Start is called before the first frame update
    void Start()
    {
        _pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)//instancie x objetos al inicio
        {
            AddGameObjectToPool();
        }
    }

    public GameObject GimmeInactiveGameObject()
    {
        foreach (GameObject obj in _pool)
        {
            if (!obj.activeSelf)//si el objeto esta desactivado, significa qu esta guardado(en el estuche)
            {
                return obj; //te lo puedo dar, lo duevuelvo
            }
        }

        if (shouldExpand)
        {
            return AddGameObjectToPool();
        }

        return null;
    }
    GameObject AddGameObjectToPool()
    {
        GameObject clone = Instantiate(objectToPool);
        clone.SetActive(false);//lo desactivamos para que pueda ser usado
        _pool.Add(clone);// lo añadimos a la lista

        return clone;
    }
}