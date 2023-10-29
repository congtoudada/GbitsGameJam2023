using QFramework;

namespace GJFramework
{
    public class PlayerAttack : AttackState<PlayerState>
    {
        private new PlayerController mTarget;
        protected bool isOver = false;
        public PlayerAttack(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }

        protected override bool OnOverCondition()
        {
            return isOver;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            isOver = false;
            mTarget.animController.SetBool(PawnController.IS_ATTACK, true);
            mTarget.playerAnimEvent.OnActionOver += AttackOver;
            ActionKit.Delay(1.0f, () =>
            {
                mTarget.CanDoNextState();
            }).Start(mTarget);
        }

        private void AttackOver(string name)
        {
            if (name == "attack")
            {
                mTarget.playerAnimEvent.OnActionOver -= AttackOver;
                isOver = true;
            }
        }

        protected override void OnExit()
        {
            mTarget.animController.SetBool(PawnController.IS_ATTACK, false);
        }
    }
}