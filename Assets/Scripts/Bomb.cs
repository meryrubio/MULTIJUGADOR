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
    public float explosionRadius; // Radio de explosión
    public float explosionDamage; // Daño de la explosión
    public float maxTime; // Tiempo máximo antes de que la bomba explote
    private float currentTime;
    private Rigidbody _rb;
    private Vector3 _launchDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        // Lanza la bomba en la dirección especificada
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
        _launchDirection = direction.normalized; // Normaliza la dirección
    }


    void Explode()
    {
        ////Instanciar el efecto de explosión
        //if (explosionEffect != null)
        //{
        //    Instantiate(explosionEffect, transform.position, transform.rotation);
        //}

        // Detectar objetos en el área de explosión
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // Suponiendo que los objetos tienen un componente "Health" que maneja el daño
            Health health = collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(explosionDamage);
            }
        }

        // Destruir la bomba después de la explosión
        PhotonNetwork.Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Visualizar el área de explosión en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}