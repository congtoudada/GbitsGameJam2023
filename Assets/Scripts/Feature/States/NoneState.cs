using QFramework;

namespace GJFramework
{
    public class NoneState<T> : AbstractState<T,PawnController>
    {
        public NoneState(FSM<T> fsm, PawnController target) : base(fsm, target)
        {
        }
    }
}