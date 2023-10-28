using QFramework;

namespace GJFramework
{
    public class PlayerMoveDown : MoveDownState<PlayerState>
    {
        private new PlayerController mTarget;
        public PlayerMoveDown(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }
    }
}