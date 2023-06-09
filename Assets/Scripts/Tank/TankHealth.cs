﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{

    public float m_StartingHealth = 100f;       // The amount of health each tank starts with
//    public Slider m_Slider;                     // The slider to represent how much health the tank currently has
    public GameObject m_ExplosionPrefab;        // A prefab that will be instantiated in Awake, then used whenever the tank dies

    private float m_CurrentHealth;
    private bool m_Dead;
    private ParticleSystem m_ExplosionParticles;       // The particle system that will play when the tank is destroyed

    // Marcus Added Mechanic
    SpawnManager spawnManager;

    private void Awake()
    {
        // Instantiate the explosion prefab and get a reference to the particle system on it
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        // Disable the prefab so it can be activated when it's required
        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // when the tank is enabled, reset the tank's health and whether or not it's dead
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }

    private void SetHealthUI()
    {
  //      m_Slider.value = m_CurrentHealth;
    }

    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done
        m_CurrentHealth -= amount;

        // Change the UI elements appropriately
        SetHealthUI();

        // if the current health is at or below zero and it has not yet been registered, call OnDeath
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // Set the flag so that this function is only called once
        m_Dead = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding
        m_ExplosionParticles.Play();

        // Marcus Added Mechanic
        if(this.gameObject.CompareTag("Enemy"))
        {
            spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
            spawnManager.EnemyDied();
        }

        // Turn the tank off
        gameObject.SetActive(false);
    }
}
