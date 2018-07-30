using UnityEngine;

namespace StateControl
{
  public interface IStateCondition
  {
    void SetData(IDataBase data);

    int GetID { get; }

    string SetMonsterID { set; }

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

