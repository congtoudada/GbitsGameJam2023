using QFramework;

namespace GJFramework
{
    public class EnemyMoveDown : MoveDownState<EnemyState>
    {
        private new EnemyController mTarget;
        public EnemyMoveDown(FSM<EnemyState> fsm, PawnController target) : base(fsm, target)
        {
            mTarget = target as EnemyController;
        }
    }
}