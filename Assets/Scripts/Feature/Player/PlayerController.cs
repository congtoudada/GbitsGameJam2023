/****************************************************
  文件：PlayerController.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

namespace GJFramework
{
    public enum PlayerState
    {
        Idle,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Absorb,
        Attack,
        Sprint,
        Die
    }

    public class PlayerController : PawnController
    {
        public FSM<PlayerState> FSM = new FSM<PlayerState>();
        public float hp = 3;
        public float sp = 100;
        public PlayerData playerData;
        public List<PlayerState> CommandCache = new List<PlayerState>(commandCacheCount);
        public bool isLockInput = false; //全局锁输入
        // public bool isLockChangeState = false; //锁状态，无法通过按键主动切换
        public int current_lock_input_priority = 0; //当前锁输入的优先级

        private bool first_horizontal = true;
        private const int commandCacheCount = 2;
        // Start is called before the first frame update
        void Start()
        {
            base.Start();
            sp = playerData.max_sp;
            
            FSM.AddState(PlayerState.Idle, new PlayerIdle(FSM, this));
            FSM.AddState(PlayerState.MoveUp, new PlayerMoveUp(FSM, this));
            FSM.AddState(PlayerState.MoveDown, new PlayerMoveDown(FSM, this));
            FSM.AddState(PlayerState.MoveLeft, new PlayerMoveLeft(FSM, this));
            FSM.AddState(PlayerState.MoveRight, new PlayerMoveRight(FSM, this));
            FSM.AddState(PlayerState.Sprint, new PlayerSprint(FSM, this));
            FSM.StartState(PlayerState.Idle);
            
            FSM.OnStateChanged((pre_action, now_action) =>
            {
                Debug.Log($"[ {data.name} ] {pre_action} -> {now_action}");
                // if (now_action != PlayerState.Idle && data.isDebug)
                //     Debug.Log($"[ {data.name} ] {pre_action} -> {now_action}");

                if (pre_action != PlayerState.Idle)
                {
                    current_lock_input_priority = 0;
                }
            });
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

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                AddCommand(PlayerState.Sprint);
            }
        }

        bool AddCommand(PlayerState inputState)
        {
            var currentPrior = playerData.ActionPriorityDict[inputState];
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
                    Debug.Log("Process: " + CommandState);
                    CommandCache.RemoveAt(0);
                    FSM.ChangeState(CommandState);
                }
                else
                {
                    if (FSM.CurrentStateId != PlayerState.Idle)
                        FSM.ChangeState(PlayerState.Idle);
                }
            }
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
        }
        
    }
}


