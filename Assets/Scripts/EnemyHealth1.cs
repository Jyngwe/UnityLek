using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth1 : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 5.5f;
    public int scoreValue = 10;

    Animator anim;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    NavMeshAgent nav;
    
    bool isDead;
    bool isSinking;

    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;

    }

    // Update is called once per frame
    void Update()
    {

        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;
        hitParticles.Stop();
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }

    }

    void Death()
    {
        isDead = true;
 
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Death");
        // Invoke = spela x funktion efter delay (i sekunder)
        Invoke("setInactive", 1);

    }

    // Funktion som sätter objektet till inaktivt, så att det kan användas igen i EnemyManger-skriptet
    void setInactive()
    {
        nav.enabled= false;
        isDead = false;
        currentHealth = startingHealth;
        gameObject.SetActive(false);

    }


    // Animation event som kallar på denna metod. Fungerar dock sådär i dagsläget. 
    // Tanken är att ca 1 sekund in i "dödsanimationen" skall kroppen börja sjunka ner i marken.
    public void StartSinking()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        
      
    }
}
