/****************************************************
  文件：PlayerController.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

namespace GJFramework
{
    public enum PlayerState
    {
        None,
        Idle,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Absorb,
        AbsorbInplace,
        Attack,
        Sprint,
        Die
    }
    
    public class PlayerController : PawnController
    {
        public FSM<PlayerState> FSM = new FSM<PlayerState>();
        public PlayerData playerData;
        public SPController spController;
        public Animator animController;
        [HideInInspector] public PlayerAnimationEvent playerAnimEvent;
        public float hp = 3;
        public float sp = 100;
        public List<PlayerState> CommandCache = new List<PlayerState>(commandCacheCount);
        public bool isLockInput = false; //全局锁输入
        // public bool isLockChangeState = false; //锁状态，无法通过按键主动切换
        public int current_lock_input_priority = 0; //当前锁输入的优先级
        public Transform[] raycastGroup;

        private bool first_horizontal = true;
        private const int commandCacheCount = 1;

        [HideInInspector]
        public GamePanel gamePanel
        {
            get
            {
                if (mGamePanel == null)
                    mGamePanel = UIKit.GetPanel<GamePanel>();
                return mGamePanel;
            }
        }

        private GamePanel mGamePanel;
        // Start is called before the first frame update
        void Start()
        {
            base.Start();
            sp = playerData.max_sp;
            
            FSM.AddState(PlayerState.None, new NoneState<PlayerState>(FSM, this));
            FSM.AddState(PlayerState.Idle, new PlayerIdle(FSM, this));
            FSM.AddState(PlayerState.MoveUp, new PlayerMoveUp(FSM, this));
            FSM.AddState(PlayerState.MoveDown, new PlayerMoveDown(FSM, this));
            FSM.AddState(PlayerState.MoveLeft, new PlayerMoveLeft(FSM, this));
            FSM.AddState(PlayerState.MoveRight, new PlayerMoveRight(FSM, this));
            FSM.AddState(PlayerState.Sprint, new PlayerSprint(FSM, this));
            FSM.AddState(PlayerState.Attack, new PlayerAttack(FSM, this));
            FSM.AddState(PlayerState.Absorb, new PlayerAbsorb(FSM, this));
            FSM.AddState(PlayerState.AbsorbInplace, new PlayerAbsorbInplace(FSM, this));
            FSM.StartState(PlayerState.Idle);
            
            FSM.OnStateChanged((pre_action, now_action) =>
            {
                if (data.isDebug)
                    Debug.Log($"[ {data.name} ] {pre_action} -> {now_action}");
                // if (now_action != PlayerState.Idle && data.isDebug)
                //     Debug.Log($"[ {data.name} ] {pre_action} -> {now_action}");

                if (pre_action != PlayerState.Idle && pre_action != PlayerState.None)
                {
                    current_lock_input_priority = 0;
                }
            });

            playerAnimEvent = transform.Find("Model").GetComponent<PlayerAnimationEvent>();
        }

        void PreProcessInput()
        {
            if (isLockInput) return;
            //没有锁输入
            bool isKeyDown = Input.anyKey;
            if (!isKeyDown) return;
            if (first_horizontal)
            {
                // first_horizontal = false;
                if (Input.GetKey(KeyCode.W))
                {
                    if (AddCommand(PlayerState.MoveUp)) first_horizontal = false;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (AddCommand(PlayerState.MoveDown)) first_horizontal = false;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    AddCommand(PlayerState.MoveLeft);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    AddCommand(PlayerState.MoveRight);
                }
            }
            else
            {
                // first_horizontal = true;
                if (Input.GetKey(KeyCode.A))
                {
                    if (AddCommand(PlayerState.MoveLeft)) first_horizontal = true;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    if (AddCommand(PlayerState.MoveRight)) first_horizontal = true;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    AddCommand(PlayerState.MoveUp);

                }
                else if (Input.GetKey(KeyCode.S))
                {
                    AddCommand(PlayerState.MoveDown);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                AddCommand(PlayerState.Sprint);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                AddCommand(PlayerState.Attack);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddCommand(PlayerState.Absorb);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                AddCommand(PlayerState.AbsorbInplace);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gamePanel.BackIdx();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                gamePanel.NextIdx();
            }
        }

        bool AddCommand(PlayerState inputState)
        {
            var currentPrior = playerData.ActionPriorityDict[inputState];
            // Debug.Log("cur: " + currentPrior + " vs. " + current_lock_input_priority);
            if (CommandCache.Count < commandCacheCount)
            {
                // 只有输入优先级大于当前优先级才放入缓存
                if (currentPrior > current_lock_input_priority)
                {
                    CommandCache.Add(inputState);
                    current_lock_input_priority = currentPrior;
                    return true;
                }
            }
            else
            {
                var lastPrior = playerData.ActionPriorityDict[CommandCache[commandCacheCount - 1]];
                if (currentPrior > lastPrior)
                {
                    // 覆盖并更新锁等级
                    CommandCache.Clear();
                    CommandCache.Add(inputState);
                    // CommandCache[commandCacheCount - 1] = inputState;
                    current_lock_input_priority = playerData.ActionPriorityDict[inputState];
                    return true;
                }
            }
            return false;
        }

        private void ProcessInput()
        {
            //如果没有锁状态
            // 根据当前状态和输入判断
            if (FSM.CurrentState.OverCondition())
            {
                if (CommandCache.Count > 0)
                {
                    var CommandState = CommandCache[0];
                    // Debug.Log("Process: " + CommandState);
                    CommandCache.RemoveAt(0);
                    if (CheckEnergy(CommandState))
                    {
                        FSM.ChangeState(PlayerState.None);
                        FSM.ChangeState(CommandState);
                    }
                }
                else
                {
                    if (FSM.CurrentStateId != PlayerState.Idle)
                        FSM.ChangeState(PlayerState.Idle);
                }
            }
        }

        private bool CheckEnergy(PlayerState inputState)
        {
            float cost = 0;
            if(playerData.ActionSpDict.TryGetValue(inputState, out cost))
            {
                if (sp >= cost)
                {
                    sp -= cost;
                    spController.sp.Value = sp / playerData.max_sp;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void CanDoNextState()
        {
            current_lock_input_priority -= 1;
            if (current_lock_input_priority == 0) current_lock_input_priority = 0;
        }

        // Update is called once per frame
        void Update()
        {
            //得到输入
            PreProcessInput();
            
            //处理输入
            ProcessInput();
            
            //更新状态机
            FSM.Update();
            
            //动作bug自动刷新
            if (FSM.FrameCountOfCurrentState > 60 * 10.0f)
            {
                FSM.ChangeState(PlayerState.Idle);
            }
            
            //恢复SP
            UpdateSP();
        }

        public void Hurt()
        {
            gamePanel.Hurt();
        }

        public void Recover()
        {
            gamePanel.Recover();
        }

        private void UpdateSP()
        {
            if (sp <= playerData.max_sp)
            {
                sp += playerData.max_sp * (1.0f / playerData.sp_revocer_time) * Time.deltaTime;
                if (sp > playerData.max_sp) sp = playerData.max_sp;
                spController.sp.Value = sp / playerData.max_sp;
            }
        }

        public void ProcessNumberOrOp(int id)
        {
            if (id <= 0 || id > 10)
            {
                Debug.Log("非法id: " +id);
                return;
            }
            spController.PlayShowAnim(id);
            gamePanel.ChangeValue(id);
        }
        
    }
}


