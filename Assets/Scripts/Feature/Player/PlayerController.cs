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
using UnityEngine;

namespace GJFramework
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerData data;
        private CharacterController controller;
        
        private bool isLockInput = false;
        private float horizontal = 0;
        private float vertical = 0;
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLockInput)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
                if (horizontal > 0)
                {
                    
                }
            }


        }
        
    }
}


