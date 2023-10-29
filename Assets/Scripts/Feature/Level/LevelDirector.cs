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
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GJFramework
{

    public class LevelDirector : MonoSingleton<LevelDirector>
    {
        // 关卡数 和 阶段数
        public event Action<int, int> OnProcessEvent;
        public Transform GroundMgr;
        public Transform EnemyMgr;
        private int groundCapacity;
        private bool isRefreshEnemy = true;
        private void Awake()
        {
            ResKit.Init();
        }

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 60;
            GroundMgr = GameObject.FindWithTag("GroundMgr").transform;
            EnemyMgr = GameObject.FindWithTag("EnemyMgr").transform;
            groundCapacity = GroundMgr.childCount;

            ActionKit.Repeat(-1)
                .Delay(3.0f)
                .Callback(() =>
                {
                    isRefreshEnemy = !isRefreshEnemy;
                    CreateLevelItem();
                    if (isRefreshEnemy)
                    {
                        CreateEnemy();
                    }
                }).Start(this);
        }

        public void CreateLevelItem()
        {
            int randomNode = Random.Range(0, groundCapacity);
            var node = GroundMgr.GetChild(randomNode);
            Debug.Log("CreateLevelItem: " + node.name);
            var levelItem = node.GetComponent<LevelItem>();
            if (levelItem == null)
            {
                levelItem = node.AddComponent<LevelItem>();
                levelItem.Init();
            }
        }

        public void CreateEnemy()
        {
            int randomMode = Random.Range(1, 4);
            GameObject enemy = Instantiate(Resources.Load<GameObject>("SlimePBR"), EnemyMgr);
            var profile = Resources.Load<PawnData>("Enemy" + randomMode.ToString());
            enemy.GetComponent<EnemyController>().Init(profile);
            Vector3 bornPoints = EnemyMgr.Find("BornPoints").GetChild(Random.Range(0, 3)).transform.position;
            enemy.transform.position = bornPoints;
        }


        private void OnDestroy()
        {
            
        }
    }
}


