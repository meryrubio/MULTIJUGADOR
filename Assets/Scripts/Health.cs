using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviourPun
{
    public float maxHealth = 100f; // Salud máxima
    private float currentHealth; // Salud actual

    public float healthRegenAmount = 5f; // Cantidad de salud a regenerar
    public float regenInterval = 5f; // Intervalo de tiempo para regenerar salud
    private float lastDamageTime; // Tiempo del último daño recibido

    void Start()
    {
        void Start()
        {
            currentHealth = maxHealth; // Inicializa la salud actual
            lastDamageTime = Time.time; // Inicializa el tiempo del último daño
            //InvokeRepeating("RegenerateHealth", regenInterval, regenInterval); // Llama a la función de regeneración en intervalos
        }
    }

    public void TakeDamage(float amount)
    {
        if (!photonView.IsMine) return; // Solo el jugador local puede recibir daño

        //currentHealth -= damage; // Reduce la salud actual
        if (currentHealth < 0) currentHealth = 0; // Asegúrate de que no sea negativa

        lastDamageTime = Time.time; // Actualiza el tiempo del último daño

        // Llama a la función para actualizar la barra de salud
        UpdateHealthUI();

        // Si la salud llega a 0, el jugador muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para actualizar la UI de salud
    private void UpdateHealthUI()
    {
        // Aquí puedes llamar a un método en tu controlador de jugador para actualizar la barra de salud
        PlayerMovement_RB playermovement_rb = GetComponent<PlayerMovement_RB>();
        if (playermovement_rb != null)
        {
            //playermovement_rb.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    // Método que se llama cuando el jugador muere
    private void Die()
    {
        //// Aquí puedes manejar la lógica de muerte, como reiniciar el jugador o mostrar una pantalla de muerte
        //Debug.Log($"{photonView.Owner.NickName} ha muerto.");
        // Por ejemplo, puedes desactivar el objeto del jugador
        gameObject.SetActive(false);
    }
}
