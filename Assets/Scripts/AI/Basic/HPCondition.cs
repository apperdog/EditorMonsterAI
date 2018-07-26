using System;
using UnityEngine;

namespace MonsterAISystem
{
  public class HPCondition : IStateCondition
  {

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


