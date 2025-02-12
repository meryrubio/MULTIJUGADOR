using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;

public class ShootBomb : MonoBehaviourPun
{
    public GameObject bomb; // Prefab de la bomba
    public float launchForce = 10f; // Fuerza de lanzamiento

    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetButtonDown("Fire1")) // Funciona en mando y ratón
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instanciar la bomba en la red
        GameObject obj = PhotonNetwork.Instantiate(bomb.name, transform.position, Quaternion.identity);

        if (obj != null)
        {
            obj.SetActive(true); // Activar la bomba
            obj.transform.position = transform.position; // Asegurarse de que la posición es correcta

            // Aplicar fuerza para lanzar la bomba
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 launchDirection = transform.forward + Vector3.up; // Lanzar hacia adelante y arriba
                rb.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse); // Ajusta la fuerza según sea necesario
            }
        }
    }
}