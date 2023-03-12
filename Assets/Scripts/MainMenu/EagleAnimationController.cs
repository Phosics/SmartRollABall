using UnityEngine;

namespace MainMenu
{
    public class EagleAnimationController : StateMachineBehaviour
    {
        // OnStateEnter is called when a transiton starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger("ChosenTransition", Random.Range(1, 4));
        }
    }
}