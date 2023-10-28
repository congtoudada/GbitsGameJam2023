using DG.Tweening;
using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class PlayerSprint : SprintState<PlayerState>
    {
        private new PlayerController mTarget;
        public PlayerSprint(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }
        
        protected override void OnEnter()
        {
            // mTarget.sp -= mTarget.playerData.sprint_sp;
            base.OnEnter();
        }
    }
}