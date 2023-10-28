using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class AbsorbState<T> : AbstractState<T, PawnController>
    {
        public AbsorbState(FSM<T> fsm, PawnController target) : base(fsm, target)
        {
            
        }
        
        protected override void OnEnter()
        {
            
        }
        
        
        
    }
}