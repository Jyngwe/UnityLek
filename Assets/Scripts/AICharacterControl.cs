using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        BossAttack bossAttack;
        Animator anim;
        PlayerHealth playerHealth;
        BossHealth bossHealth;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            anim = GetComponent<Animator>();
            playerHealth = target.GetComponent<PlayerHealth>();
            bossHealth = GetComponent<BossHealth>();
            bossAttack = GetComponentInChildren<BossAttack>();
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = true;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (playerHealth.currentHealth >= 0 && bossHealth.currentHealth >= 0 && agent.enabled)
            {
                if (target != null)
                    agent.SetDestination(target.position);

                if (Vector3.Distance(transform.position, target.transform.position) > bossAttack.range)
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                    character.Move(Vector3.zero, false, false);
            }
            else
            {
                agent.enabled = false;
            }
        }

    }
}
