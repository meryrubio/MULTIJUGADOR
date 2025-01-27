using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviourPun
{

    private Animator animator;
    private PlayerMovement_RB playerMovement_RB;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement_RB = GetComponent<PlayerMovement_RB>();
    }

    // Update is called once per frame
    void Update()
    {
        //esta instancia representa a la persona física jugando en este ordenador dentro de esta aplicación.
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        //bool shiftPressed = Input.GetKey(KeyCode.LeftShift);

        //if(x!= 0 || z!= 0)
        //{
        //    //se esta moviendo 
        //    if(shiftPressed) //si se esta moviendo y tiene shifpresionado
        //    {
        //        //y ademas está corriendo
        //        animator.SetBool("isrunning", true);
        //        animator.SetBool("iswalking", false);
        //    }
        //    else
        //    {
        //        //y ademas esta andando
        //        animator.SetBool("isrunning", false);
        //        animator.SetBool("iswalking", true);
        //    }
        //}
        //else
        //{
        //    //esta quieto
        //    animator.SetBool("isrunning", false);
        //    animator.SetBool("iswalking", false);
        //}
    }

    //private void LateUpdate()
    //{
    //    animator.SetFloat("Speed", playerMovement_RB.GetCurrentSpeed() / playerMovement_RB.runningSpeed);
    //}
}
