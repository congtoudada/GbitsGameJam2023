using System.Collections.Generic;
using CT.Tools;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace GJFramework
{
	public enum UIItemType
	{
		Number,
		Operation
	}
	
	public enum OpState
	{
		Add = 0,
		SubStract,
		Multiply,
		Divide,
		Equal
	}

	public class GamePanelData : UIPanelData
	{
	}
	public partial class GamePanel : UIPanel
	{
		public int hp;
		public const int ACCESS_TOTAL_COUNT = 7;
		public int lastId;
		public BindableProperty<int> currentId = new BindableProperty<int>();
		public Dictionary<int, NumberOpItem> uiDict = new Dictionary<int, NumberOpItem>();
		public int nowValue;
		private List<RectTransform> hpGroup = new List<RectTransform>();

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GamePanelData ?? new GamePanelData();
			// please add init code here
			currentId.SetValueWithoutEvent(0);
			lastId = 0;
			uiDict.Add(0, Number1);
			uiDict.Add(1, Operation1);
			uiDict.Add(2, Number2);
			uiDict.Add(3, Operation2);
			uiDict.Add(4, Number3);
			uiDict.Add(5, Operation3);
			uiDict.Add(6, Number4);
			uiDict.Add(7, Operation4);
			uiDict.Add(8, Number5);
			hp = 3;
			
			hpGroup.Add(life1);
			hpGroup.Add(life2);
			hpGroup.Add(life3);
			
			
			currentId.RegisterWithInitValue(v =>
			{
				uiDict[lastId].SwitchArrow(false);
				uiDict[v].SwitchArrow(true);
				lastId = v;
			});
			
			Random.InitState((int)TimeTool.GetTimeStamp(true));
			foreach (var key in uiDict.Keys)
			{
				if (key == 1 || key == 3 || key == 5 || key == 7)
				{
					uiDict[key].InitSelf(UIItemType.Operation);
				}
				else
				{
					uiDict[key].InitSelf( UIItemType.Number);
				}
			}

			int luck1 = Random.Range(0, 3);
			if (luck1 == 0)
			{
				uiDict[1].Init(OpState.Divide, 0);
			}
			else
			{
				uiDict[1].Init(OpState.Multiply, 0);
			}
			
			int luck2 = Random.Range(0, 3);
			if (luck2 == 0)
			{
				uiDict[5].Init(OpState.SubStract, 0);
			}
			else
			{
				uiDict[5].Init(OpState.Add, 0);
			}

			if (uiDict[1].opState == OpState.Divide && uiDict[5].opState == OpState.SubStract)
			{
				uiDict[3].Init(OpState.Multiply, 0);
			}
			uiDict[7].Init(OpState.Equal, 0);
			uiDict[8].number = CalSum();
			uiDict[8].UpdateView();
		}

		public void ChangeValue(int id)
		{
			if (id > 0 && id <= 10)
			{
				int result = (uiDict[currentId.Value].number + id) % 11;
				if (result == 0) result = 1;
				uiDict[currentId.Value].number = result;
				uiDict[currentId.Value].UpdateView();
			}

			uiDict[8].number = CalSum();
			uiDict[8].UpdateView();
			
		}

		private int CalSum()
		{
			int result = 0;
			OpState op1 = Operation1.opState;
			result = CalLoal(Number1.number, Number2.number, Operation1.opState);
			result = CalLoal(result, Number3.number, Operation2.opState);
			result = CalLoal(result, Number4.number, Operation3.opState);
			return result;
		}

		private int CalLoal(int a, int b, OpState op)
		{
			int result = 0;
			switch (op)
			{
				case OpState.Add:
					result = a + b;
					break;
				case OpState.SubStract:
					result = a - b;
					break;
				case OpState.Multiply:
					result = a * b;
					break;
				case OpState.Divide:
					result = a / b;
					break;
			}

			return result;
		}

		public void Hurt()
		{
			hp -= 1;
			if (hp < 0)
			{
				hp = 0;
			}
			hpGroup[hp].gameObject.SetActive(false);
			// transform.GetChild(hp).gameObject.SetActive(false);
		}

		public void Recover()
		{
			hp += 1;
			if (hp > 3) hp = 3;
			transform.GetChild(hp-1).gameObject.SetActive(true);
		}

		public void NextIdx()
		{
			if (currentId.Value + 2 > ACCESS_TOTAL_COUNT)
			{
				currentId.Value = 0;
			}
			else
			{
				currentId.Value += 2;
			}
			
		}

		public void BackIdx()
		{
			if (currentId.Value - 2 < 0)
			{
				currentId.Value = 6;
			}
			else
			{
				currentId.Value -= 2;
			}
		}
		
		
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
