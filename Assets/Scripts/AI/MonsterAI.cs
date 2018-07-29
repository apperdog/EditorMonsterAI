using System;
using GlobalEnum;
using UnityEngine;
using StateControl;
using MonsterAISystem;

public class MonsterAI : MonoBehaviour
{
  public string monsterID;
  public string resourcesID;

  public MonsterAIBehaviour monsterAI;

  public void CreateAI()
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

    public MonsterStateMachine stateMachine { get; private set; }

    public MonsterAIBehaviour(string resourcesID, string monsterID)
    {
      this.monsterID = monsterID;

      aiState = AIStateType.StateUpdate;  
      stateMachine = new MonsterStateMachine(this);

      LoadData(resourcesID);
    }

    /// <summary>
    /// 讀取 AI的資料
    /// </summary>
    public void LoadData(string resourcesID)
    {
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

          stateMachine.SetCondition(condition);

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

          stateMachine.SetState(json, monsterState);
        }
      }

      stateMachine.StartAI();
    }

    public void OnUpdate()
    {
      switch (aiState)
      {
        case AIStateType.Condition:
          stateMachine.CheckCondition();

          aiState = AIStateType.StateUpdate;
          break;

        case AIStateType.StateUpdate:
          stateMachine.OnUpdate();

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

