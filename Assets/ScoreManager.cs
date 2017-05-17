using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    public GameObject boss;
    public Text bossText;
    public NavMeshAgent nav;
    public Transform spawnPoint;
    public bool bossAlive;


    Text text;
	// Use this for initialization
	void Start () {

        bossAlive = false;
        text = GetComponent<Text>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Score: " + score;

        if(score % 100 == 0 && score != 0 && !bossAlive)
        {
                Spawn();
        }
	}

    void RemoveBossText()
    {
        bossText.enabled = false;
    }

    void Spawn()
    {
        nav.transform.position = spawnPoint.transform.position;
        nav.enabled = true;
        bossText.enabled = true;
        Invoke("RemoveBossText", 1f);
        boss.SetActive(true);
        bossAlive = true;
    }
}
