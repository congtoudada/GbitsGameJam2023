using QFramework;

namespace GJFramework
{
    public class EnemyAttack : AttackState<EnemyState>
    {
        private new EnemyController mTarget;
        public EnemyAttack(FSM<EnemyState> fsm, PawnController target) : base(fsm, target)
        {
            mTarget = target as EnemyController;
        }
    }
}