using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth1 enemyHealth;

    public bool playerInRange;
    float timer;

	// Use this for initialization
	void Awake () {

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth1>();
        anim = GetComponent<Animator>();
		
	}

    //Om zombien kolliderar med player är de inom range för att attackera
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update () {

        timer += Time.deltaTime;

        if(timer>= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
	}

    void Attack()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            anim.SetTrigger("InRange");
            //TakeDamage är funktion i playerHealth-skript
            playerHealth.TakeDamage(attackDamage);
     
        }
    }
}
