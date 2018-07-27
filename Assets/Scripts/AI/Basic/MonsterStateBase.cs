using System;
using UnityEngine;
using StateControl;
using UnityEngine.Events;

namespace MonsterAISystem
{
  [Serializable]
  public class MonsterStateBase : IMonsterStateBase
  {
    private int id;

    private UnityEvent enterMethod;
    private UnityEvent executeMethod;
    private UnityEvent exitMethod;

    /// <summary>
    /// 條件檢查
    /// </summary>
    public string CheckCondition()
    {
      string change = string.Empty;

      return change;
    }

    /// <summary>
    /// 進入該狀態時做的事
    /// </summary>
    public void Enter(IMonsterAI t)
    {

    }

    /// <summary>
    /// 該狀態做的事
    /// </summary>
    public void Execute()
    {

    }

    /// <summary>
    /// 離開該狀態做的事
    /// </summary>    
    public void Exit()
    {

    }

    public void SetData(IDataBase data)
    {
      JsonMonsterAI jsonMonster = (JsonMonsterAI)data;
      id = jsonMonster.typeID;
    }

    public int GetID { get { return id; } }

  }

  public interface IMonsterStateBase : IAIState<IMonsterAI>
  {
    int GetID { get; }
    string CheckCondition();
    void SetData(IDataBase data);
  }
}



