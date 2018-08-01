using System;
using StateControl;

namespace MonsterAISystem
{
  [Serializable]
  public class MonsterStateBase : IMonsterStateBase
  {
    private int typeID;
    private string monsterID;

    private Action<string> enterMethod;
    private Action<string> executeMethod;
    private Action<string> exitMethod;

    /// <summary>
    /// 進入該狀態時做的事
    /// </summary>
    public void Enter()
    {
      enterMethod(monsterID);
    }

    /// <summary>
    /// 該狀態做的事
    /// </summary>
    public void Execute()
    {
      executeMethod(monsterID);
    }

    /// <summary>
    /// 離開該狀態做的事
    /// </summary>    
    public void Exit()
    {
      exitMethod(monsterID);
    }

    public void SetData(IDataBase data, string id)
    {
      JsonMonsterAI jsonMonster = (JsonMonsterAI)data;

      typeID = jsonMonster.typeID;
      monsterID = id;

      enterMethod = GetMethod(jsonMonster.enter);
      executeMethod = GetMethod(jsonMonster.excuse);
      exitMethod = GetMethod(jsonMonster.exit);
    }

    // 取得方法
    private Action<string> GetMethod(string typeName)
    {
      // 名稱不為空
      if (!string.IsNullOrEmpty(typeName))
      {
        // 取得存放方法的 class
        Type type = Type.GetType(string.Format("MonsterAISystem.{0}", typeName));
        var m = Activator.CreateInstance(type);
        IMethod<string> aiMethod = (IMethod<string>)m;

        // 返回該 class的方法
        return aiMethod.Method;
      }

      return NoThing;
    }

    /// <param name="id"></param>
    public void NoThing(string id)
    {

    }

    public int GetID { get { return typeID; } }
  }

  public interface IMonsterStateBase : IAIState
  {
    /// <summary>
    /// 取得該狀態的 ID
    /// </summary>
    int GetID { get; }

    /// <summary>
    /// 設置該狀態的資料
    /// </summary>
    void SetData(IDataBase data, string id);

    /// <summary>
    /// 不做任何事
    /// </summary>
    void NoThing(string id);
  }
}



