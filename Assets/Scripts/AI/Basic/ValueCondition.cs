using System;
using UnityEngine;
using StateControl;

namespace MonsterAISystem
{
  /// <summary>
  /// 數值條件
  /// </summary>
  public class ValueCondition : IStateCondition
  {
    private int id;
    private int nextID;
    private int currest;
    private float value1;
    private float value2;
    private bool percentage;
    private string monsterID;
    private GlobalEnum.ValueType valueType;
    private ValueConditionType valueConditionType;

    public int CheckCondition()
    {
      // 取得怪物資料
      MonsterDataList monsterDataList = DataSystem.GetSystem<MonsterDataList>();
      MonsterData monsterData = monsterDataList.GetData(monsterID);

      // 
      float v = 0;

      // 檢查類型
      switch (valueType)
      {
        case GlobalEnum.ValueType.HP:
          v = monsterData.hp;

          if (percentage)
            v = (float)v / monsterData.MaxHP;
          break;

        case GlobalEnum.ValueType.MP:
          v = monsterData.mp;

          if (percentage)
            v = (float)v / monsterData.MaxMP;
          break;
      }

      // 判斷條件
      switch (valueConditionType)
      {
        case ValueConditionType.Less:
          if (v < value1)
            return nextID;
          break;

        case ValueConditionType.Greater:
          if (v > value1)
            return nextID;
          break;

        case ValueConditionType.Between:
          if (value1 < v && v < value2)
            return nextID;
          break;
      }

      return -1;
    }

    public void SetData(IDataBase data)
    {
      JsonValueCondition json = (JsonValueCondition)data;

      value1 = json.value1;
      value2 = json.value2;
      id = json.typeID;
      currest = json.currestStateID;
      percentage = json.percentage;
      nextID = json.nextStateID;
      valueType = (GlobalEnum.ValueType)json.valueType;
      valueConditionType = (ValueConditionType)json.valueConditionType;
    }

    public int GetID { get { return id; } }

    public int CurrestStateID { get { return currest; } }

    public string SetMonsterID { set { monsterID = value; }}
  }

  public enum ValueConditionType
  {
    Less,
    Greater,
    Between
  }

  [Serializable]
  public class JsonValueCondition : JsonCondition
  {
    [SerializeField] public float value1;
    [SerializeField] public float value2;
    [SerializeField] public bool percentage;
    [SerializeField] public int valueType;
    [SerializeField] public int valueConditionType;
  }
}


