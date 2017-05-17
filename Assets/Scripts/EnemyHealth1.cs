using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth1 : MonoBehaviour
{

    public float sinkSpeed = 5.5f;
    public int scoreValue = 10;
    public bool hasHealthPack;
    public GameObject healthBar;
    public float startingHealth;
    public float currentHealth;

    bool isDead;
    bool isSinking;

    Animator anim;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    NavMeshAgent nav;
    HealthPack healthPack;
    EnemyAttack enemyAttack;
    GameObject enemyManager;
    EnemyManager enemyManagerScript;

    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        healthPack = GetComponent<HealthPack>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyManager = GameObject.FindGameObjectWithTag("Respawn");
        enemyManagerScript = enemyManager.GetComponent<EnemyManager>();
         
        currentHealth = startingHealth;
 
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.transform.localScale = new Vector3(currentHealth / startingHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if(currentHealth/startingHealth < 0)
        {
            healthBar.transform.localScale = new Vector3(0, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

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
        if(hasHealthPack)
        {
            enemyManagerScript.SpawnHealthPack(transform.position.x, transform.position.y, transform.position.z);
            hasHealthPack = false;
        }

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
        enemyAttack.playerInRange = false;
        gameObject.SetActive(false);
    }

    // Animation event som kallar på denna metod. Fungerar dock sådär i dagsläget. 
    // Tanken är att ca 1 sekund in i "dödsanimationen" skall kroppen börja sjunka ner i marken.
    public void StartSinking()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;

    }
}
