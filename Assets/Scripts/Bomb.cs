using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviourPun
{
    public float speed;
    private Rigidbody _rb;
    public float explosionRadius = 5f; // Radio de la explosión
    public float explosionDamage = 50f; // Daño de la explosión
    public float countdown = 3f; // Tiempo antes de explotar
    public float launchForce = 10f; // Fuerza de lanzamiento
    public Vector3 launchDirection = new Vector3(1, 1, 0); // Dirección de lanzamiento


    private float countdownTimer;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer -= Time.deltaTime;

        if (countdownTimer <= 0f)
        {
            Explode();
        }
    }
}
