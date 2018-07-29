using System;
using UnityEngine;

namespace MonsterAISystem
{
  public class ValueCondition : IStateCondition
  {
    private int id;
    private float hp;
    private float hp2;
    private bool percentage;
    private ValueType valueType;
    private ValueConditionType valueConditionType;

    public int CheckCondition()
    {
      return -1;
    }

    public void SetData(IDataBase data)
    {
      JsonHPCondition json = (JsonHPCondition)data;

      hp = json.hp;
      hp2 = json.hp2;
      id = json.typeID;
      percentage = json.percentage;
      valueType = (ValueType)json.valueType;
      valueConditionType = (ValueConditionType)json.valueConditionType;
    }

    public int GetID { get { return id; } }

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


