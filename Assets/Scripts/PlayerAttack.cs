using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public int damagePerHit = 100;
    public float timeBetweenAttacks = 1f;
    public float range = 50f;
    Animator anim;

    ParticleSystem gunParticles;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem SpearHitParticle;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

	// Use this for initialization
	void Awake () {

        shootableMask = LayerMask.GetMask("Shootable");
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunParticles = GetComponent<ParticleSystem>();
   
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
        gunLight.enabled = false;
        gunLine.enabled = false;
    }

    void Attack()
    {
        timer = 0f;

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = -transform.right;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth1 enemyHealth = shootHit.collider.GetComponent<EnemyHealth1>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerHit, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    
    }
}
