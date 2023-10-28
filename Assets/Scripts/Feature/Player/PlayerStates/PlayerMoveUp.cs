using QFramework;

namespace GJFramework
{
    public class PlayerMoveUp : MoveUpState<PlayerState>
    {
        private new PlayerController mTarget;
        public PlayerMoveUp(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }
    }
}