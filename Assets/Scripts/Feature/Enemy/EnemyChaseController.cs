using UnityEngine;

namespace GJFramework
{
    public class EnemyChaseController : EnemyController
    {        
        private EnemyAnimEvent enemyAnimEvent;
        // Start is called before the first frame update
        protected void Start()
        {
            base.Start();
            FSM.AddState(EnemyState.None, new NoneState<EnemyState>(FSM, this));
            FSM.AddState(EnemyState.Chase, new EnemyChase(FSM, this));
            FSM.AddState(EnemyState.MoveTo, new EnemyMoveTo(FSM, this));
            
            
            enemyAnimEvent = GetComponentInChildren<EnemyAnimEvent>();
            enemyAnimEvent.OnActionOver += s =>
            {
                if (s == "chase" && FSM.CurrentStateId == EnemyState.None)
                {
                    FSM.ChangeState(EnemyState.Chase);
                }
            };
            
            FSM.StartState(EnemyState.Chase);
        }



        void Update()
        {
            base.Update();
        }
    }
}