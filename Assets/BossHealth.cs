using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth : MonoBehaviour
{

    public int scoreValue = 20;
    public GameObject healthBar;
    public GameObject manaBar;
    public ScoreManager scoreManager;

    public float startingHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;

    Animator anim;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    NavMeshAgent nav;
    EnemyAttack enemyAttack;

    bool isDead;
    bool isSinking;
    bool loopHandle;
    

    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyAttack = GetComponent<EnemyAttack>();

        currentHealth = startingHealth;
        currentMana = 0;
        loopHandle = true;

        StartCoroutine(regenMana());
    }

    // Update is called once per frame
    void Update()
    {
        // Uppdaterar bossens Health Bar
        healthBar.transform.localScale = new Vector3(currentHealth / startingHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if (currentHealth / startingHealth < 0)
        {
            healthBar.transform.localScale = new Vector3(0, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

        //Uppdaterar bossens Mana Bar
        manaBar.transform.localScale = new Vector3(currentMana / maxMana, manaBar.transform.localScale.y, manaBar.transform.localScale.z);

        
    }

    IEnumerator regenMana()
    {
        while (loopHandle)
        {
            if (currentMana < 100)
            {
                currentMana += 5;
            }
            else if (currentMana >= 100)
            {
                currentMana = 100;
            }
            yield return new WaitForSeconds(2f);
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
            loopHandle = false;
            Death();
        }

    }

    void Death()
    {

        isDead = true;
        //capsuleCollider.isTrigger = true;
        loopHandle = true;
        anim.SetTrigger("Die");
        gameObject.SetActive(false);
        nav.enabled = false;
        currentHealth = startingHealth;
        ScoreManager.score += scoreValue;
        scoreManager.bossAlive = false;

    }


}
