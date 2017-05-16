using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPackCollider : MonoBehaviour {

    GameObject player;
    PlayerHealth playerHealth;
    public Slider healthSlider;
    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerHealth.gainHealth(10);
            gameObject.SetActive(false);
        }
    }

}
