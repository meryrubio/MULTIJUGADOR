using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviourPun
{

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))//funcione en mando y raton
        {
            GameObject obj = PhotonNetwork.Instantiate(bullet.name, transform.position, Quaternion.identity);

            if (obj != null)
            {
                obj.SetActive(true);//quitarlo del estuche, ya no esta disponible en la pool
                obj.transform.position = transform.position;
                obj.GetComponent<Bullet>().SetDirection(transform.forward);
            }
        }
    }
}
