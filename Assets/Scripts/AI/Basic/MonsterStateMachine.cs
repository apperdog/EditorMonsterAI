using System;
using GlobalEnum;
using UnityEngine;
using StateControl;
using System.Collections.Generic;

namespace MonsterAISystem
{

  /// <summary>
  /// AI 容器
  /// </summary>
  public class MonsterStateMachine
  {
    public StateMachine machine;  // 狀態容器

    private Dictionary<int, MonsterStateBase> monsterStates;  // 狀態條件
    private Dictionary<int, IStateCondition> monsterCondition;  // 狀態

    public MonsterStateMachine()
    {
      machine = new StateMachine();
      monsterStates = new Dictionary<int, MonsterStateBase>();
      monsterCondition = new Dictionary<int, IStateCondition>();
    }

    /// <summary>
    /// 添加狀態條件
    /// </summary>
    public void SetCondition(IStateCondition condition)
    {
      monsterCondition.Add(condition.GetID, condition);
    }

    /// <summary>
    /// 添加狀態
    /// </summary>
    public void SetState(JsonMonsterAI jsonMonster, MonsterStateBase monsterState)
    {
      monsterStates.Add(monsterState.GetID, monsterState);
    }

    public void StartAI()
    {
      List<int> list = new List<int>(monsterStates.Keys);

      machine.SetCurrestState(monsterStates[list[0]]);
    }

    /// <summary>
    /// 執行當前狀態的行為
    /// </summary>
    public void OnUpdate()
    {
      machine.StateUpdate();
    }

    /// <summary>
    /// 條件檢查
    /// </summary>
    public void CheckCondition()
    {
      int change = -1;

      // 取得當前狀態 ID
      IMonsterStateBase ai = (IMonsterStateBase)machine.GetCurrestState;
      int aiID = ai.GetID;

      // 遍尋所有
      var e = monsterCondition.GetEnumerator();

      while (e.MoveNext())
      {
        IStateCondition condition = e.Current.Value;
        int id = condition.CurrestStateID;

        // 該條件得當前狀態 ID與當前狀態 ID相同的話
        if (id == aiID)
          change = condition.CheckCondition();

        if(change > -1)
        {
          ChangeState(change);
          break;
        }
      }
    }

    /// <summary>
    /// 切換狀態
    /// </summary>
    public void ChangeState(int key)
    {
      MonsterStateBase monsterState = null;

      if (monsterStates.TryGetValue(key, out monsterState))
        machine.ChangeState(monsterState);
    }
  }

  [Serializable]
  public class JsonMonsterAI : JsonBase, IDataBase
  {
    [SerializeField] public bool bFrist;
    [SerializeField] public int typeID;
    [SerializeField] public string enter, excuse, exit;

    public void Destory()
    {

    }
  }
}

