using QFramework;

namespace GJFramework
{
    public class EnemyIdle : IdleState<EnemyState>
    {
        private new EnemyController mTarget;
        public EnemyIdle(FSM<EnemyState> fsm, PawnController target) : base(fsm, target)
        {
            mTarget = target as EnemyController;
        }
    }
}