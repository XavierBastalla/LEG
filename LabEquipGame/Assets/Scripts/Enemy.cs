using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Fmod stuff
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public float moveSpeed = 5f;

    //public AudioSource damageSound; // Reference to the AudioSource for damage
    public EventReference damageSound;

    private Transform player;


    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the tag "Player"
    }

    void Update()
    {
        MoveTowardsPlayer();
    }
    
    void MoveTowardsPlayer()
    {
        if (player == null)
            return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //PlaySound(damageSound);

        FMOD.Studio.EventInstance damageSFX;
        damageSFX = FMODUnity.RuntimeManager.CreateInstance(damageSound); //creates the event
        damageSFX.start();    //plays the event
        damageSFX.release(); // destroys the event after it plays

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
       
        // Implement death actions (e.g., play death animation, increase player score, etc.)
        Destroy(gameObject);
    }

    /*void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                gameObject.SetActive(true);  // Enable the GameObject
                audioSource.Play();          // Play the sound
                gameObject.SetActive(false); // Disable the GameObject after playing
            }
        }
    }*/
}