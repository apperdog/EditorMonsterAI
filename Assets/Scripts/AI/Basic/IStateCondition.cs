using UnityEngine;

namespace MonsterAISystem
{
  public interface IStateCondition
  {
    void SetData(IDataBase data);

    int GetID { get; }

    int CheckCondition();
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

