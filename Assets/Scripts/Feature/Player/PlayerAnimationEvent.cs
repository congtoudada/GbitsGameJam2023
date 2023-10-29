/****************************************************
  文件：PlayerAnimationEvent.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJFramework
{
    public class PlayerAnimationEvent : MonoBehaviour
    {
        public event Action<string> OnActionOver;
        public GameObject attack_hand_FX1;
        public GameObject attack_hand_FX2;
        public GameObject attack_big_FX;
        public GameObject absorb_FX1;
        public GameObject absorb_FX2;

        public void ActionBegin(string actionName)
        {
            switch (actionName)
            {
                case "attack":
                    attack_hand_FX1.SetActive(true);
                    attack_hand_FX2.SetActive(true);
                    break;
                case "absorb":
                    absorb_FX2.SetActive(true);
                    break;
                case "absorb_inplace":
                    absorb_FX1.SetActive(true);
                    absorb_FX2.SetActive(true);
                    break;
            }
        }

        public void AttackSecond()
        {
            attack_big_FX.SetActive(true);
        }
        
        public void ActionOver(string actionName)
        {
            OnActionOver?.Invoke(actionName);
            switch (actionName)
            {
                case "attack":
                    attack_hand_FX1.SetActive(false);
                    attack_hand_FX2.SetActive(false);
                    attack_big_FX.SetActive(false);
                    break;
                case "absorb":
                    absorb_FX2.SetActive(false);
                    break;
                case "absorb_inplace":
                    absorb_FX1.SetActive(false);
                    absorb_FX2.SetActive(false);
                    break;
            }
        }
    }
}


