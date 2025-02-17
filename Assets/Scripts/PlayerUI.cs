using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviourPun
{
    public TextMeshProUGUI playerNameText; //objeto de texto
    public Image BarraDeVida;

    private float maxHealth = 100f; // Salud máxima
    private float currentHealth; // Salud actual
    private Health healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<Health>();

        // el nombre del jugador se muestre al inicio
        if (playerNameText != null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }
        maxHealth = healthComponent.maxHealth;
    }

    private void Update()
    {
        BarraDeVida.fillAmount = healthComponent.currentHealth/maxHealth;
    }


   
}
