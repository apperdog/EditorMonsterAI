using System;
using GlobalEnum;
using UnityEngine;
using StateControl;
using MonsterAISystem;
using System.Collections.Generic;

public class MonsterAI : MonoBehaviour
{
  public string monsterID;

  public List<string> resourcesID;
  public MonsterAIBehaviour monsterAI;

  public void StartAI()
  {
    monsterAI = new MonsterAIBehaviour(resourcesID, monsterID);
  }
}

namespace MonsterAISystem
{

  public class MonsterAIBehaviour : IMonsterAI
  {
    private string monsterID;

    private AIStateType aiState;

    private MonsterStateMachine[] stateMachineList;

    public MonsterAIBehaviour(List<string> resourcesID, string monsterID)
    {
      this.monsterID = monsterID;

      aiState = AIStateType.StateUpdate;

      LoadData(resourcesID);
    }

    /// <summary>
    /// 讀取 AI的資料
    /// </summary>
    public void LoadData(List<string> resourcesIDList)
    {
      stateMachineList = new MonsterStateMachine[resourcesIDList.Count];

      for (int i = 0; i < resourcesIDList.Count; i++)
        stateMachineList[i] = LoadData(resourcesIDList[i]);
    }

    public MonsterStateMachine LoadData(string resourcesID)
    {
      //
      MonsterStateMachine monsterStateMachine = new MonsterStateMachine(this);

      // 取得 json資料
      TextAsset text = Resources.Load<TextAsset>(
        string.Format("1.MonsterAI/1.1.Json/{0}", resourcesID));

      // json資料轉換
      JsonDataList jsonData = JsonUtility.FromJson<JsonDataList>(text.text);

      // 狀態條件
      for (int i = jsonData.dataBases.Count - 1; i > -1 ; i--)
      {
        if (typeof(JsonCondition).IsInstanceOfType(jsonData.dataBases[i]))
        {
          JsonCondition json = (JsonCondition)jsonData.dataBases[i];

          Type type = Type.GetType(json.createType);
          var ob = Activator.CreateInstance(type);

          IStateCondition condition = (IStateCondition)ob;
          condition.SetData(json);
          condition.SetMonsterID = monsterID;

          monsterStateMachine.SetCondition(condition);

          jsonData.dataBases.RemoveAt(i);
        }
      }

      // 狀態
      for (int i = 0; i < jsonData.dataBases.Count; i++)
      {
        // 
        if (typeof(JsonMonsterAI).IsInstanceOfType(jsonData.dataBases[i]))
        {
          JsonMonsterAI json = (JsonMonsterAI)jsonData.dataBases[i];

          Type type = Type.GetType(json.createType);
          MonsterStateBase monsterState = (MonsterStateBase)Activator.CreateInstance(type);

          monsterState.SetData(json, monsterID);

          monsterStateMachine.SetState(json, monsterState);
        }
      }

      monsterStateMachine.StartAI();

      return monsterStateMachine;
    }

    public void OnUpdate()
    {
      for(int i = 0; i < stateMachineList.Length; i++)
      {
        switch (aiState)
        {
          case AIStateType.Condition:
            stateMachineList[i].CheckCondition();
            break;

          case AIStateType.StateUpdate:
            stateMachineList[i].OnUpdate();
            break;
        }
      }

      switch (aiState)
      {
        case AIStateType.Condition:
          aiState = AIStateType.StateUpdate;
          break;

        case AIStateType.StateUpdate:
          aiState = AIStateType.Condition;
          break;
      }
    }

    public AIStateType AIState
    {
      set { aiState = value; }
      get { return aiState; }
    }
  }
}

