using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttack : MonoBehaviour
{

    public float timeBetweenAttacks = 3f;
    public int attackDamage = 40;
    public float range = 100f;
    public float chargeUpTime = 3;

    LineRenderer attackLine;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    BossHealth bossHealth;
    NavMeshAgent nav;
    Transform boss;


    public bool playerInRange;
    float timer;
    float effectsDisplayTime = 0.2f;

    // Use this for initialization
    void Awake()
    {
        boss = GetComponentInParent<Transform>();
        attackLine = GetComponent<LineRenderer>();
        nav = GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        bossHealth = GetComponentInParent<BossHealth>();
        anim = GetComponentInParent<Animator>();
        

    }


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) <= range)
        {
            anim.SetTrigger("InRange");
            nav.speed = 0;
            chargeUpTime = chargeUpTime - Time.deltaTime;
            
            if (timer >= timeBetweenAttacks && bossHealth.currentHealth > 0 && chargeUpTime <= 0)
            {
                Attack();
            }
            else if(chargeUpTime <= 0)
            {
                chargeUpTime = 3;
            }
        }
        else
        {
            nav.speed = 10;
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }

        if (timer >= effectsDisplayTime)
        {
            attackLine.enabled = false;
        }
    }

    void Attack()
    {
        timer = 0f;
    
        if (playerHealth.currentHealth > 0 && bossHealth.currentMana > 20)
        {
            attackLine.enabled = true;
            attackLine.SetPosition(0, new Vector3(boss.position.x, boss.position.y + 15, boss.position.z));
            attackLine.SetPosition(1, new Vector3(player.transform.position.x, player.transform.position.y + 4, player.transform.position.z));

            //TakeDamage är funktion i playerHealth-skript
            playerHealth.TakeDamage(attackDamage);

            bossHealth.currentMana -= 20f;

        }
    }
}
