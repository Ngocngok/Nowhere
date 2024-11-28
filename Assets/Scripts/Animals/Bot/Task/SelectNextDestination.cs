using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Nowhere.Helper;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nowhere.NPC.Task
{
    public class SelectNextDestination : Conditional
    {
        public SharedTransform target;

        public List<Transform> targets;

        public override void OnStart()
        {
            base.OnStart();

            List<Transform> availbleTargets = targets.Where(x => x != target.Value).ToList();
            availbleTargets.Shuffle();
            target.Value = availbleTargets[0];
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }

}