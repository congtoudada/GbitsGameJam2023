using QFramework;

namespace GJFramework
{
    public class PlayerAbsorb : AbsorbState<PlayerState>
    {
        private new PlayerController mTarget;
        protected bool isOver = false;
        public PlayerAbsorb(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
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
            mTarget.animController.SetBool(PawnController.IS_ABSORB, true);
            mTarget.playerAnimEvent.OnActionOver += AbsorbOver;
            ActionKit.Delay(0.5f, () =>
            {
                mTarget.CanDoNextState();
            }).Start(mTarget);
        }

        private void AbsorbOver(string name)
        {
            if (name == "absorb")
            {
                mTarget.playerAnimEvent.OnActionOver -= AbsorbOver;
                isOver = true;
            }
        }

        protected override void OnExit()
        {
            mTarget.animController.SetBool(PawnController.IS_ABSORB, false);
        }
    }
}