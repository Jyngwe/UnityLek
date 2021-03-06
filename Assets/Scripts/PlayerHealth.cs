﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Text deathText;

    Animator anim;
    PlayerController playerMovement;
    bool isDead;
    bool damaged;
	
	void Awake () {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerController>();
        currentHealth = startingHealth;
	}
	
	void Update () {
		
        if(damaged)
        {
            // Blinkar röd färg på skärmen i någon millisekund när man tar skada
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}

    public void TakeDamage (int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
       
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void gainHealth(int amount)
    {
        if (currentHealth < 100)
        {
            currentHealth += amount;
            healthSlider.value = currentHealth;
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        deathText.enabled = true;
        playerMovement.enabled = false;
    }
}
