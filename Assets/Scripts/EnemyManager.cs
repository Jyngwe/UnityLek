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
    // Update is called once per frame

}
