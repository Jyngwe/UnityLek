using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 5f;
    public Transform[] spawnPoints;
    NavMeshAgent nav;


    public int pooledZombies = 4;
    List<GameObject> zombies;

    // I början av skriptet skapas alla objekt av zombies (går att sätta den variabeln både i skriptet och i Unity
    // Sedan sätts dessa som aktiva/inaktiva och återanvänds allteftersom de dödas. 
  
    void Start ()
    {
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
                nav = zombies[i].GetComponent<NavMeshAgent>();

                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                zombies[i].transform.position = spawnPoints[spawnPointIndex].position;
                zombies[i].transform.rotation = spawnPoints[spawnPointIndex].rotation;
                zombies[i].SetActive(true);
                nav.enabled = true;
                break;
            }
        }   
    }

}
