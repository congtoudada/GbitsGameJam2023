/****************************************************
  文件：LevelDirector.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class LevelDirector : MonoSingleton<LevelDirector>, IController
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnDestroy()
        {
            
        }

        public IArchitecture GetArchitecture()
        {
            return LevelApp.Interface;
        }
    }
}


