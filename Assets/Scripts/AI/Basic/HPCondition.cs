using System.Collections;
using System.Collections.Generic;
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
}


