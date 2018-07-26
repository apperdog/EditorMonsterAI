using System;
using GlobalEnum;
using UnityEngine;
using StateControl;
using System.Collections.Generic;

namespace MonsterAISystem
{
  public interface IMonsterAI
  {
    AIStateType AIState { get; set; }
  }

  [Serializable]
  public class MonsterStateMachine
  {
    [SerializeField]
    public StateMachine<IMonsterAI> machine;

    [SerializeField]
    public Dictionary<string, MonsterStateBase> monsterStates;

    public MonsterStateMachine(IMonsterAI monsterAI)
    {
      machine = new StateMachine<IMonsterAI>(monsterAI);
      monsterStates = new Dictionary<string, MonsterStateBase>();
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
      var e = monsterStates.GetEnumerator();

      // 循環所有條件檢查
      while (e.MoveNext())
      {
        // 取得切換狀態名稱
        string change = e.Current.Value.CheckCondition();

        // 如果有切換狀態名稱
        if (!string.IsNullOrEmpty(change))
        {
          ChangeState(change);
          break;
        }
      }
    }

    /// <summary>
    /// 切換狀態
    /// </summary>
    public void ChangeState(string key)
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
    [SerializeField] public List<int> currestConditionID;

    public void Destory()
    {

    }
  }
}

