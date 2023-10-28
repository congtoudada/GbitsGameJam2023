using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class IdleState<T> : AbstractState<T, PawnController>
    {
        public IdleState(FSM<T> fsm, PawnController target) : base(fsm, target)
        {
            
        }
        
        protected override void OnEnter()
        {

        }
        
    }
}