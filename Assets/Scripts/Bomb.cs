using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Bomb : MonoBehaviourPun
{
 
    public float explosionRadius = 5f; // Radio de explosi�n
    public float explosionDamage = 50f; // Da�o de la explosi�n
    /*public GameObject explosionEffect;*/ // Prefab de efecto de explosi�n

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        // Instanciar el efecto de explosi�n
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