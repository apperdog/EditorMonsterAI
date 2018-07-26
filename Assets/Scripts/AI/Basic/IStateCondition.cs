using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterAISystem
{
  public interface IStateCondition
  {

  }

  public class JsonCondition : JsonBase, IDataBase
  {
    [SerializeField] public int typeID;
    [SerializeField] public int nextConditionID;

    public void Destory()
    {

    }
  }
}

