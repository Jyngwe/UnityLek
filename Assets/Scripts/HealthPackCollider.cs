using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPackCollider : MonoBehaviour {

    public Slider healthSlider;

    GameObject player;
    PlayerHealth playerHealth;
    CountDownTimer countDownTimer;
 
    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        countDownTimer = GetComponent<CountDownTimer>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerHealth.gainHealth(10);
            gameObject.SetActive(false);
            countDownTimer.timeLeft = countDownTimer.defTime;
        }
    }

}
