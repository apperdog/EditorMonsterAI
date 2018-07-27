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

  /// <summary>
  /// AI 容器
  /// </summary>
  public class MonsterStateMachine
  {
    public StateMachine<IMonsterAI> machine;  // 狀態容器

    private Dictionary<int, MonsterStateBase> monsterStates;  // 狀態條件
    private Dictionary<int, IStateCondition> monsterCondition;  // 狀態

    public MonsterStateMachine(IMonsterAI monsterAI)
    {
      machine = new StateMachine<IMonsterAI>(monsterAI);
      monsterStates = new Dictionary<int, MonsterStateBase>();
      monsterCondition = new Dictionary<int, IStateCondition>();
    }

    /// <summary>
    /// 添加狀態
    /// </summary>
    public void SetCondition(IStateCondition condition)
    {
      monsterCondition.Add(condition.GetID, condition);
    }

    public void SetState(MonsterStateBase monsterState)
    {
      monsterStates.Add(monsterState.GetID, monsterState);
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

      //// 循環所有條件檢查
      //while (e.MoveNext())
      //{
      //  // 取得切換狀態名稱
      //  string change = e.Current.Value.CheckCondition();

      //  // 如果有切換狀態名稱
      //  if (!string.IsNullOrEmpty(change))
      //  {
      //    ChangeState(change);
      //    break;
      //  }
      //}
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
    [SerializeField] public List<int> currestConditionID;

    public void Destory()
    {

    }
  }
}

