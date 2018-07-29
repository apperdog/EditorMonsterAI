using System;
using UnityEngine;
using StateControl;
using System.Collections.Generic;

namespace MonsterAISystem
{
  [Serializable]
  public class MonsterStateBase : IMonsterStateBase
  {
    private int typeID;
    private string monsterID;

    private List<IStateCondition> conditionList; 

    private Action<string> enterMethod;
    private Action<string> executeMethod;
    private Action<string> exitMethod;

    public MonsterStateBase()
    {
      conditionList = new List<IStateCondition>();
    }

    /// <summary>
    /// 條件檢查
    /// </summary>
    public int CheckCondition()
    {
      for (int i = 0; i < conditionList.Count; i++)
      {
        int stateID = conditionList[i].CheckCondition();

        if (stateID > -1)
          return stateID;
      }

      return -1;
    }

    /// <summary>
    /// 進入該狀態時做的事
    /// </summary>
    public void Enter()
    {
      enterMethod(monsterID);
    }

    /// <summary>
    /// 該狀態做的事
    /// </summary>
    public void Execute()
    {
      executeMethod(monsterID);
    }

    /// <summary>
    /// 離開該狀態做的事
    /// </summary>    
    public void Exit()
    {
      exitMethod(monsterID);
    }

    public void SetData(IDataBase data, string id)
    {
      JsonMonsterAI jsonMonster = (JsonMonsterAI)data;

      typeID = jsonMonster.typeID;
      monsterID = id;

      enterMethod = GetMethod(jsonMonster.enter);
      executeMethod = GetMethod(jsonMonster.excuse);
      exitMethod = GetMethod(jsonMonster.exit);
    }

    private Action<string> GetMethod(string typeName)
    {
      if (!string.IsNullOrEmpty(typeName))
      {
        Type type = Type.GetType(string.Format("MonsterAISystem.{0}", typeName));

        var m = Activator.CreateInstance(type);
        AIMethod<string> aiMethod = (AIMethod<string>)m;

        return aiMethod.Method;
      }

      return NoThing;
    }

    public void SetCondition(IStateCondition stateCondition)
    {
      conditionList.Add(stateCondition);
    }

    public void NoThing(string id)
    {

    }

    public int GetNodeID { get { return typeID; } }

  }

  public interface IMonsterStateBase : IAIState
  {
    int GetNodeID { get; }
    int CheckCondition();
    void SetData(IDataBase data, string id);
    void SetCondition(IStateCondition stateCondition);
    void NoThing(string id);
  }
}



