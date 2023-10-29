/****************************************************
  文件：LevelItem.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using ch.sycoforge.Decal;
using CT.Tools;
using DG.Tweening;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GJFramework
{
    public class LevelItem : MonoBehaviour, ICanAbsorbed
    {
        [Range(1, 10)]
        public int number = 1;
        public int level;
        public int stage;
        public bool isOnlyOnce = false;
        public float canShowWaitTime = 10.0f;
        public float refreshTimeMin = 5.0f;
        public float refreshTimeMax = 20.0f;
        
        private MeshCollider collider;
        [HideInInspector]
        public bool isCanShow;

        private Material mat;

        // Start is called before the first frame update
        public void Init()
        {
            LevelDirector.Instance.OnProcessEvent += ChangeState;
            number = Random.Range(1, 10);
            collider = GetComponent<MeshCollider>();

            Texture tex = Resources.Load<Texture>(number.ToString());
            mat = GetComponent<MeshRenderer>().material;
            mat.SetTexture("_DigitalTex", tex);
            SwitchVisual(false);
            
            
            ActionKit.Sequence().Condition(() => isCanShow).Delay(Random.Range(0, canShowWaitTime)).Callback(() =>
            {
                collider.isTrigger = true;
                SwitchVisual(true);
            }).Start(this);
            
            
            //Debug
            collider.isTrigger = true;
            SwitchVisual(true);
            GetComponent<MeshRenderer>().material.SetFloat("_DigitalIntensity", 1.0f);
        }

        public int BeAbsorbed()
        {
            collider.isTrigger = false;
            SwitchVisual(false);
            if (!isOnlyOnce)
            {
                ActionKit.Delay(Random.Range(refreshTimeMin, refreshTimeMax), () =>
                {
                    collider.isTrigger = true;
                    SwitchVisual(true);
                }).Start(this);
            }
            return number;
        }

        private void SwitchVisual(bool isOn)
        {
            if (isOn)
            {
                mat.DOFloat(1.0f, "_DigitalIntensity", 1.0f);
            }
            else
            {
                mat.DOFloat(0f, "_DigitalIntensity", 1.0f);
            }
        }

        private void ChangeState(int level, int stage)
        {
            if (this.level == level && this.stage == stage)
            {
                isCanShow = true;
            }
        }

        // private void OnDisable()
        // {
        //     // LevelDirector.Instance.OnProcessEvent -= ChangeState;
        // }
    }
}


