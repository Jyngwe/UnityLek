using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour {

    GameObject player;
    public float timeLeft;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        if(gameObject.activeInHierarchy)
        {
            timeLeft -= Time.deltaTime;
            gameObject.GetComponentInChildren<TextMesh>().text = timeLeft.ToString("0.0");
            gameObject.GetComponentInChildren<TextMesh>().transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
          
            if (timeLeft<=0)
            {
                gameObject.SetActive(false);
            }
        }

		
	}
}
