using QFramework;

namespace GJFramework
{
    public class EnemyMoveUp : MoveUpState<EnemyState>
    {
        private new EnemyController mTarget;
        public EnemyMoveUp(FSM<EnemyState> fsm, PawnController target) : base(fsm, target)
        {
            mTarget = target as EnemyController;
        }
    }
}