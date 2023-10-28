using QFramework;

namespace GJFramework
{
    public class EnemyMoveLeft : MoveLeftState<EnemyState>
    {
        private new EnemyController mTarget;
        public EnemyMoveLeft(FSM<EnemyState> fsm, PawnController target) : base(fsm, target)
        {
            mTarget = target as EnemyController;
        }
    }
}