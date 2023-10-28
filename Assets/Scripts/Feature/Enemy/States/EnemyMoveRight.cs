using QFramework;

namespace GJFramework
{
    public class EnemyMoveRight : MoveRightState<EnemyState>
    {
        private new EnemyController mTarget;
        public EnemyMoveRight(FSM<EnemyState> fsm, PawnController target) : base(fsm, target)
        {
            mTarget = target as EnemyController;
        }
    }
}