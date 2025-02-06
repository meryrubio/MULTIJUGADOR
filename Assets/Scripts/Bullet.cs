using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    public float speed;
    public float maxTime;
    private float currentTime;
    private Vector3 _dir;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > maxTime)
        {
            currentTime = 0;
            //gameObject.SetActive(false);//se devuelve a la pool
            PhotonNetwork.Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        _rb.velocity = speed * _dir;
    }
    public void SetDirection(Vector3 valeu)
    {
        _dir = valeu;
    }
}

