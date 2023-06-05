using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    private void Reset()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ConfigManager.Instance.a = 1;
    }

    RaycastHit[] groundHit = new RaycastHit[1];

    // Update is called once per frame
    void Update()
    {

        if (agent.remainingDistance < .15f)
        {
            animator.SetInteger(AnimationHash.runTypeHash, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.RaycastNonAlloc(Camera.main.ScreenPointToRay(Input.mousePosition + Vector3.forward * 100), groundHit) > 0)
            {
                animator.SetInteger(AnimationHash.runTypeHash, 2);
                agent.SetDestination(groundHit[0].point);
            }
        }


        animator.SetFloat(AnimationHash.speedHash, agent.velocity.magnitude / agent.speed);
    }
}
