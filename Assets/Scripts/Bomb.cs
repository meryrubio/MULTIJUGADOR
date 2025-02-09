using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Bomb : MonoBehaviourPun
{

    public float launchSpeed; // Velocidad de lanzamiento
    public float explosionRadius; // Radio de explosi�n
    public float explosionDamage; // Da�o de la explosi�n
    public float maxTime; // Tiempo m�ximo antes de que la bomba explote
    private float currentTime;
    private Rigidbody _rb;
    private Vector3 _launchDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        // Lanza la bomba en la direcci�n especificada
        LaunchBomb();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > maxTime)
        {
            Explode();
        }
    }

    private void LaunchBomb()
    {
        // Aplica una fuerza inicial para lanzar la bomba
        _rb.velocity = _launchDirection * launchSpeed;
    }

    public void SetLaunchDirection(Vector3 direction)
    {
        _launchDirection = direction.normalized; // Normaliza la direcci�n
    }


    void Explode()
    {
        ////Instanciar el efecto de explosi�n
        //if (explosionEffect != null)
        //{
        //    Instantiate(explosionEffect, transform.position, transform.rotation);
        //}

        // Detectar objetos en el �rea de explosi�n
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // Suponiendo que los objetos tienen un componente "Health" que maneja el da�o
            Health health = collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(explosionDamage);
            }
        }

        // Destruir la bomba despu�s de la explosi�n
        PhotonNetwork.Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Visualizar el �rea de explosi�n en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}