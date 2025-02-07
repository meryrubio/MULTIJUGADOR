using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviourPun
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes agregar la lógica para la muerte del objeto
        PhotonNetwork.Destroy(gameObject);
    }
}
