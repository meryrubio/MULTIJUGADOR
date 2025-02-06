using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public GameObjectPool bulletPool;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))//funcione en mando y raton
        {
            GameObject obj = bulletPool.GimmeInactiveGameObject();

            if (obj != null)
            {
                obj.SetActive(true);//quitarlo del estuche, ya no esta disponible en la pool
                obj.transform.position = transform.position;
                obj.GetComponent<Bullet>().SetDirection(transform.forward);
            }
        }
    }
}
