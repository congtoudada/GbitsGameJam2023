/****************************************************
  文件：PlayerData.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using CT.Tools;
using UnityEngine;

namespace GJFramework
{
    
    [CreateAssetMenu(menuName = "CGJFramework/PlayerProfile", fileName = "PlayerProfile", order = 31)]
    public class PlayerData : ScriptableObject
    {
        public float max_sp = 100.0f;
        public float sp_revocer_time = 5.0f;
        public float lock_ratio = 0.5f;
        
        public SerializableDictionary<PlayerState, int> ActionPriorityDict = new SerializableDictionary<PlayerState, int>
        {
            m_Dictionary =
            {
                { PlayerState.None, -1 },
                { PlayerState.Idle, 0 },
                { PlayerState.MoveUp, 10 },
                { PlayerState.MoveDown, 10 },
                { PlayerState.MoveLeft, 10 },
                { PlayerState.MoveRight, 10 },
                { PlayerState.Absorb, 5 },
                { PlayerState.AbsorbInplace, 5 },
                { PlayerState.Attack, 5 },
                { PlayerState.Sprint, 20 },
                { PlayerState.Die, 99}
            }
        };
        
        public SerializableDictionary<PlayerState, float> ActionSpDict = new SerializableDictionary<PlayerState, float>
        {
            m_Dictionary =
            {
                { PlayerState.Absorb, 25.0f },
                { PlayerState.AbsorbInplace, 25.0f },
                { PlayerState.Attack, 25.0f },
                { PlayerState.Sprint, 35.0f },
            }
        };
    }
}


