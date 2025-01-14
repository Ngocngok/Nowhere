using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animal
{
    public class AnimationController : MonoBehaviour
    {
		
		#region AnimationHash
		private static readonly int attackHash = Animator.StringToHash("Attack");
		private static readonly int bounceHash = Animator.StringToHash("Bounce");
		private static readonly int clickedHash = Animator.StringToHash("Clicked");
		private static readonly int deathHash = Animator.StringToHash("Death");
		private static readonly int eatHash = Animator.StringToHash("Eat");
		private static readonly int fearHash = Animator.StringToHash("Fear");
		private static readonly int flyHash = Animator.StringToHash("Fly");
		private static readonly int hitHash = Animator.StringToHash("Hit");
		private static readonly int idleAHash = Animator.StringToHash("Idle_A");
		private static readonly int idleBHash = Animator.StringToHash("Idle_B");
		private static readonly int idleCHash = Animator.StringToHash("Idle_C");
		private static readonly int jumpHash = Animator.StringToHash("Jump");
		private static readonly int rollHash = Animator.StringToHash("Roll");
		private static readonly int runHash = Animator.StringToHash("Run");
		private static readonly int sitHash = Animator.StringToHash("Sit");
		private static readonly int spinHash = Animator.StringToHash("Spin");
		private static readonly int swimHash = Animator.StringToHash("Swim");
		private static readonly int walkHash = Animator.StringToHash("Walk");

		public static readonly int onlySunPointSkillHash = Animator.StringToHash("skill_only_sun_point");
		public static readonly int poofSkillHash = Animator.StringToHash("skill_poof");
		public static readonly int kamehaSkillHash = Animator.StringToHash("skill_kameha");
		#endregion



		[SerializeField] private Animator _animator;

	}

}