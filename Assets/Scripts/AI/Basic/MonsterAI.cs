using System;
using UnityEngine;
using StateControl;
using System.Collections.Generic;

namespace MonsterAISystem
{
  public class MonsterAI : MonoBehaviour
  {
    public MonsterAIBehaviour monsterAI;
  }

  [Serializable]
  public class MonsterAIBehaviour : IMonsterAI
  {
    [SerializeField]
    private AIStateType aiState;
    [SerializeField]
    public MonsterStateMachine stateMachine { get; private set; }

    [HideInInspector]
    [SerializeField]
    private Vector2 editorPostion;

    public MonsterAIBehaviour()
    {
      aiState = AIStateType.StateUpdate;
      stateMachine = new MonsterStateMachine(this);
    }

    public void OnUpdate()
    {
      switch (aiState)
      {
        case AIStateType.StateUpdate:
          stateMachine.OnUpdate();
          break;
      }
    }

    public void CheckCondition()
    {

    }

    public AIStateType AIState
    {
      set { aiState = value; }
      get { return aiState; }
    }

#if UNITY_EDITOR
    public Vector2 EditorPostion
    {
      get { return editorPostion; }
      set { editorPostion = value; }
    }
#endif
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

      if(monsterStates.TryGetValue(key, out monsterState))
        machine.ChangeState(monsterState);
    }
  }

  public interface IMonsterAI
  {
    AIStateType AIState { get; set; }
  }

  public enum AIStateType
  {
    StateUpdate,
  }
}

