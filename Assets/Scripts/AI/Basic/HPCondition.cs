using System;
using UnityEngine;

namespace MonsterAISystem
{
  public class HPCondition : IStateCondition
  {
    private int id;
    private float hp;
    private float hp2;
    private bool percentage;
    private int hpConditionType;

    public string CheckCondition()
    {
      return string.Empty;
    }

    public void SetData(IDataBase data)
    {
      JsonHPCondition jsonHP = (JsonHPCondition)data;

      hp = jsonHP.hp;
      hp2 = jsonHP.hp2;
      id = jsonHP.typeID;
      percentage = jsonHP.percentage;
      hpConditionType = jsonHP.hpConditionType;
    }

    public int GetID { get { return id; } }

  }

  public enum HPConditionType
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
    [SerializeField] public int hpConditionType;
  }
}


