using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthPack : MonoBehaviour {

    public int pooledPacks = 5;
    public List<GameObject> healthPacks;
    public GameObject healthPack;
    TextMesh text;

    NavMeshAgent nav;

    // Use this for initialization
    void Start () {


        nav = GetComponent<NavMeshAgent>();
        healthPacks = new List<GameObject>();
        for (int i = 0; i < pooledPacks; i++)
        {
            GameObject obj = (GameObject)Instantiate(healthPack);
            obj.SetActive(false);
            healthPacks.Add(obj);
        }

    }
	
	public void Spawn () {

        for (int i = 0; i < healthPacks.Count; i++)
        {
            if (!healthPacks[i].activeInHierarchy)
            {
                healthPacks[i].transform.position = new Vector3(nav.transform.position.x, nav.transform.position.y + 2, nav.transform.position.z);
                healthPacks[i].transform.rotation = nav.transform.rotation;
                healthPacks[i].SetActive(true);
                return; 
            }
        }

    }

}
