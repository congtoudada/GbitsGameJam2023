using QFramework;

namespace GJFramework
{
    public class PlayerIdle : IdleState<PlayerState>
    {
        private new PlayerController mTarget;
        public PlayerIdle(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            mTarget = target as PlayerController;;
        }

        protected override bool OnCondition()
        {
            return true;
        }

        protected override void OnEnter()
        {

        }
    }
}