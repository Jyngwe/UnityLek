using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public int damagePerHit = 20;
    public float timeBetweenAttacks = 0.15f;
    public float range = 100f;

    float timer;
    Ray attackRay;
    RaycastHit attackHit;
    int shootableMask;
    ParticleSystem SpearHitParticle;
    Light spearLight;
    float effectsDisplayTime = 0.2f;

	// Use this for initialization
	void Awake () {

        shootableMask = LayerMask.GetMask("Shootable");
        SpearHitParticle = GetComponent<ParticleSystem>();
        spearLight = GetComponent<Light>();
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer >= timeBetweenAttacks)
        {
            Attack();
        }

        if(timer >= timeBetweenAttacks * effectsDisplayTime)
        {
            DisableEffects();
        }
		
	}

    public void DisableEffects()
    {
        spearLight.enabled = false;
    }

    void Attack()
    {
        timer = 0f;

        spearLight.enabled = true;

        SpearHitParticle.Stop();
        SpearHitParticle.Play();

        if(Physics.Raycast (attackRay, out attackHit, range, shootableMask))
        {
            EnemyHealth1 enemyHealth = attackHit.collider.GetComponent<EnemyHealth1>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerHit, attackHit.point);
            }
        }
    
    }
}
