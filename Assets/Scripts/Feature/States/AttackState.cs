using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class AttackState<T> : AbstractState<T, PawnController>
    {
        public AttackState(FSM<T> fsm, PawnController target) : base(fsm, target)
        {
            
        }
        
        protected override void OnEnter()
        {
            
        }
        
    }
}