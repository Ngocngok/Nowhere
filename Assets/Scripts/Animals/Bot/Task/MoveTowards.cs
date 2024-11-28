using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Nowhere.NPC.Task
{
    public class MoveTowards : Action
    {

        public SharedTransform target;
        public NavMeshAgent agent;
        public Animator animator;

        private bool _isDestinationReached = false;
        private bool _canSampleDestination = false;
        private bool _waitUpdatePath = false;

        public override void OnStart()
        {
            base.OnStart();

            if(NavMesh.SamplePosition(target.Value.position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                _canSampleDestination = true;
                Vector3 ground = hit.position;
                agent.SetDestination(ground);
                _waitUpdatePath = true;
            }

        }

        public override TaskStatus OnUpdate()
        {
            if (!_canSampleDestination)
            {
                return TaskStatus.Failure;
            }

            if (_waitUpdatePath)
            {
                _waitUpdatePath = false;
                return TaskStatus.Running;
            }

            if (agent.remainingDistance < .1f)
            {
                animator.SetFloat(AnimationHash.speedHash, 0);
                return TaskStatus.Success;
            }

            animator.SetFloat(AnimationHash.speedHash, agent.velocity.magnitude / agent.speed);
            return TaskStatus.Running;

        }

    }

}