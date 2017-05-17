using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 5f;
    public Transform[] spawnPoints;
    public int pooledZombies;
    public int pooledPacks = 5;
    public GameObject healthPack;

    EnemyAttack enemyAttack;
    EnemyHealth1 enemyHealth;
    TextMesh text;
    NavMeshAgent navHealthPack;
    NavMeshAgent navZombie;
    List<GameObject> healthPacks;
    List<GameObject> zombies;


    // I början av skriptet skapas alla objekt av zombies (går att sätta den variabeln både i skriptet och i Unity
    // Sedan sätts dessa som aktiva/inaktiva och återanvänds allteftersom de dödas. 
  
    void Start ()
    {
        navHealthPack = GetComponent<NavMeshAgent>();
        healthPacks = new List<GameObject>();
        for (int i = 0; i < pooledPacks; i++)
        {
            GameObject obj = (GameObject)Instantiate(healthPack);
            obj.SetActive(false);
            healthPacks.Add(obj);
        }

        zombies = new List<GameObject>();
        for (int i = 0; i < pooledZombies; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemy);
            obj.SetActive(false);
            zombies.Add(obj);
        }
       
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    
	}


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        for (int i = 0; i < zombies.Count; i++)
        {
            if(!zombies[i].activeInHierarchy)
            {
                enemyHealth = zombies[i].GetComponent<EnemyHealth1>();
                navZombie = zombies[i].GetComponent<NavMeshAgent>();
                enemyAttack = zombies[i].GetComponent<EnemyAttack>();

                //Randomize storlek, snabbhet och attackDamage på zombies
                navZombie.transform.localScale = Vector3.one * (Random.Range(3f, 10f));

                navZombie.speed = 11 - navZombie.transform.localScale.x;
                enemyHealth.startingHealth = navZombie.transform.localScale.x * 20;
                enemyHealth.currentHealth = enemyHealth.startingHealth;

                enemyAttack.attackDamage = (int) navZombie.transform.localScale.x * 2;

                if (Random.Range(0, 100) < 30)
                {    
                    enemyHealth.hasHealthPack = true;
                }

                //Slumpa fram spawnpoint innan aktuell zombie blir aktiv
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                zombies[i].transform.position = spawnPoints[spawnPointIndex].position;
                zombies[i].transform.rotation = spawnPoints[spawnPointIndex].rotation;
                zombies[i].SetActive(true);
                navZombie.enabled = true;
                break;
            }
        }   
    }

    public void SpawnHealthPack(float x, float y, float z)
    {

        for (int i = 0; i < healthPacks.Count; i++)
        {
            if (!healthPacks[i].activeInHierarchy)
            {
                healthPacks[i].transform.position = new Vector3(x,y+2,z);
                healthPacks[i].transform.rotation = navZombie.transform.rotation;
                healthPacks[i].SetActive(true);
                break;

            }
        }

    }

}
