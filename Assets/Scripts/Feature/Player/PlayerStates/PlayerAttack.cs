using QFramework;

namespace GJFramework
{
    public class PlayerAttack : AttackState<PlayerState>
    {
        private new PlayerController mTarget;
        public PlayerAttack(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }
    }
}