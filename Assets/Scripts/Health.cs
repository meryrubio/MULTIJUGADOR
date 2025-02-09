using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviourPun
{
    public float maxHealth = 100f; // Salud m�xima
    private float currentHealth; // Salud actual

    public float healthRegenAmount = 5f; // Cantidad de salud a regenerar
    public float regenInterval = 5f; // Intervalo de tiempo para regenerar salud
    private float lastDamageTime; // Tiempo del �ltimo da�o recibido

    void Start()
    {
        void Start()
        {
            currentHealth = maxHealth; // Inicializa la salud actual
            lastDamageTime = Time.time; // Inicializa el tiempo del �ltimo da�o
            //InvokeRepeating("RegenerateHealth", regenInterval, regenInterval); // Llama a la funci�n de regeneraci�n en intervalos
        }
    }

    public void TakeDamage(float amount)
    {
        if (!photonView.IsMine) return; // Solo el jugador local puede recibir da�o

        //currentHealth -= damage; // Reduce la salud actual
        if (currentHealth < 0) currentHealth = 0; // Aseg�rate de que no sea negativa

        lastDamageTime = Time.time; // Actualiza el tiempo del �ltimo da�o

        // Llama a la funci�n para actualizar la barra de salud
        UpdateHealthUI();

        // Si la salud llega a 0, el jugador muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para actualizar la UI de salud
    private void UpdateHealthUI()
    {
        // Aqu� puedes llamar a un m�todo en tu controlador de jugador para actualizar la barra de salud
        PlayerMovement_RB playermovement_rb = GetComponent<PlayerMovement_RB>();
        if (playermovement_rb != null)
        {
            //playermovement_rb.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    // M�todo que se llama cuando el jugador muere
    private void Die()
    {
        //// Aqu� puedes manejar la l�gica de muerte, como reiniciar el jugador o mostrar una pantalla de muerte
        //Debug.Log($"{photonView.Owner.NickName} ha muerto.");
        // Por ejemplo, puedes desactivar el objeto del jugador
        gameObject.SetActive(false);
    }
}
