using UnityEngine;

namespace EnemyScripts
{
    public class EnemyAnimationController
    {
        public static EnemyAnimationController Instance;
        
        private static readonly int AttackTrigger = Animator.StringToHash("AttackTrigger");
        private static readonly int RunTrigger = Animator.StringToHash("RunTrigger");
        private static readonly int IdleTrigger = Animator.StringToHash("IdleTrigger");
        private static readonly int DeathTrigger = Animator.StringToHash("DeathTrigger");

        static EnemyAnimationController()
        {
            if(Instance == null)
                Instance = new EnemyAnimationController();
        }

        public static EnemyAnimationController GetInstance()
        {
            return Instance;
        }

        public void PlayAttack(Animator animator)
        {
            animator.SetTrigger(AttackTrigger);
        }

        public void PlayRun(Animator animator)
        {
            animator.SetTrigger(RunTrigger);
        }

        public void PlayIdle(Animator animator)
        {
            animator.SetTrigger(IdleTrigger);
        }

        public void PlayDeath(Animator animator)
        {
            animator.SetTrigger(DeathTrigger);
        }
    
    }
}
