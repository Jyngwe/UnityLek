using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth1 : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;

    Animation ani;
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
        hitParticles = GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;

    }

    // Update is called once per frame
    void Update()
    {

        //if (isSinking)
        //{
        //    transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        //}

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
        Invoke("setInactive", 1);

    }

    void setInactive()
    {
        nav.enabled= false;
        isDead = false;
        currentHealth = startingHealth;
        gameObject.SetActive(false);
        
     
    }

    //public void StartSinking()
    //{
    //    GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
    //    GetComponent<Rigidbody>().isKinematic = true;
    //    isSinking = true;
      
    //}
}
