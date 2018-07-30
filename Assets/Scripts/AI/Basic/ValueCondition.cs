using System;
using UnityEngine;
using StateControl;

namespace MonsterAISystem
{
  public class ValueCondition : IStateCondition
  {
    private int id;
    private int nextID;
    private float value1;
    private float value2;
    private bool percentage;
    private string monsterID;
    private GlobalEnum.ValueType valueType;
    private ValueConditionType valueConditionType;

    public int CheckCondition()
    {
      MonsterDataList monsterDataList = DataSystem.GetSystem<MonsterDataList>();
      MonsterData monsterData = monsterDataList.GetData(monsterID);

      float v = 0;

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
      JsonHPCondition json = (JsonHPCondition)data;

      value1 = json.hp;
      value2 = json.hp2;
      id = json.typeID;
      percentage = json.percentage;
      nextID = json.nextConditionID;
      valueType = (GlobalEnum.ValueType)json.valueType;
      valueConditionType = (ValueConditionType)json.valueConditionType;
    }

    public int GetID { get { return id; } }

    public string SetMonsterID { set { monsterID = value; }}
  }

  public enum ValueConditionType
  {
    Less,
    Greater,
    Between
  }

  [Serializable]
  public class JsonHPCondition : JsonCondition
  {
    [SerializeField] public float hp;
    [SerializeField] public float hp2;
    [SerializeField] public bool percentage;
    [SerializeField] public int valueType;
    [SerializeField] public int valueConditionType;
  }
}


