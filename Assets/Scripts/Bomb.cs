using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Bomb : MonoBehaviourPun
{
 
    public float explosionRadius = 5f; // Radio de explosión
    public float explosionDamage = 50f; // Daño de la explosión
    /*public GameObject explosionEffect;*/ // Prefab de efecto de explosión

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        // Instanciar el efecto de explosión
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