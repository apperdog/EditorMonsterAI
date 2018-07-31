using UnityEngine;

namespace StateControl
{
  public interface IStateCondition
  {
    /// <summary>
    /// 設置資料
    /// </summary>
    void SetData(IDataBase data);

    /// <summary>
    /// 取得該判斷條件 ID
    /// </summary>
    int GetID { get; }

    /// <summary>
    /// 設置怪物ID，以便尋找資料用
    /// </summary>
    string SetMonsterID { set; }

    /// <summary>
    /// 判斷條件是否達成，達成返回切換條件 ID
    /// </summary>
    int CheckCondition();

    /// <summary>
    /// 取得當前狀態 ID
    /// </summary>
    int CurrestStateID { get; }
  }

  public class JsonCondition : JsonBase, IDataBase
  {
    [SerializeField] public int typeID;
    [SerializeField] public int nextStateID;
    [SerializeField] public int currestStateID;

    public void Destory()
    {

    }
  }
}

