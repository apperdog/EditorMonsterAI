using System;
using GlobalEnum;
using UnityEngine;
using StateControl;
using System.Collections.Generic;

namespace MonsterAISystem
{
  public class MonsterAI : MonoBehaviour
  {
    public string resourcesID;
    public MonsterAIBehaviour monsterAI;

    // Test
    private void Start()
    {
      CreateAI();
    }

    public void CreateAI()
    {
      monsterAI = new MonsterAIBehaviour(this);
    }
  }

  public class MonsterAIBehaviour : IMonsterAI
  {
    private MonsterAI unityAttributes;

    private AIStateType aiState;

    public MonsterStateMachine stateMachine { get; private set; }

    public MonsterAIBehaviour(MonsterAI monster)
    {
      unityAttributes = monster;
      aiState = AIStateType.StateUpdate;  
      stateMachine = new MonsterStateMachine(this);

      LoadData();
    }

    /// <summary>
    /// 讀取 AI的資料
    /// </summary>
    public void LoadData()
    {
      // 取得 json資料
      TextAsset text = Resources.Load<TextAsset>(
        string.Format("1.MonsterAI/1.1.Json/{0}", unityAttributes.resourcesID));

      // json資料轉換
      JsonDataList jsonData = JsonUtility.FromJson<JsonDataList>(text.text);

      // 
      for (int i = 0; i < jsonData.dataBases.Count; i++)
      {
        // 狀態
        if (typeof(JsonMonsterAI).IsInstanceOfType(jsonData.dataBases[i]))
        {

        }
        // 狀態條件
        else if(typeof(JsonCondition).IsInstanceOfType(jsonData.dataBases[i]))
        {

        }
      }
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
  }
}

