/****************************************************
  文件：NumberOpItem.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace GJFramework
{
    public class NumberOpItem : MonoBehaviour
    {
        public UIItemType type;
        public GameObject Arrow;
        public OpState opState;
        public int number = 0;
        public TMP_Text tmp_text;
        // Start is called before the first frame update
        void Start()
        {
            Arrow = transform.Find("Arrow").gameObject;
            // tmp_text = transform.Find("Text").GetComponent<TMP_Text>();
        }

        public void UpdateView()
        {
            if (type == UIItemType.Number)
            {
                tmp_text.text = number.ToString();
            }
            else
            {
                switch (this.opState)
                {
                    case OpState.Add:
                        tmp_text.text = "+";
                        break;
                    case OpState.SubStract:
                        tmp_text.text = "-";
                        break;
                    case OpState.Multiply:
                        tmp_text.text = "x";
                        break;
                    case OpState.Divide:
                        tmp_text.text = "÷";
                        break;
                    case OpState.Equal:
                        tmp_text.text = "=";
                        break;
                }
            }
        }

        public void Init(OpState state, int number)
        {
            this.opState = state;
            this.number = number;
            UpdateView();
        }
        
        public void InitSelf(UIItemType type)
        {
            this.type = type;
            opState = (OpState) Random.Range(0, 4);
            number = Random.Range(1, 11);
            UpdateView();
        }

        public void SwitchArrow(bool isOn)
        {
            if (Arrow != null)
            {
                Arrow.SetActive(isOn);
            }
        }


        public string GetText()
        {
            return tmp_text.text;
        }
    }
}


