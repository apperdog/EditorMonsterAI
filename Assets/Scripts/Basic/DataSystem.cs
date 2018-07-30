using System;
using System.Collections.Generic;

public class DataSystem
{
  private static Dictionary<Type, IDataBase> dataSystemList = new Dictionary<Type, IDataBase>();

  private DataSystem()
  {

  }

  /// <summary>
  /// 增加 class scripts
  /// </summary>
  public static void AddSystem<T>(T t) where T : IDataBase
  {
    dataSystemList.Add(typeof(T), t);
  }

  /// <summary>
  /// 取得該 class
  /// </summary>
  public static T GetSystem<T>() where T : IDataBase
  {
    IDataBase data;

    if (dataSystemList.TryGetValue(typeof(T), out data))
      return (T)data;
    else
    {
      Type type = typeof(T);
      T t = (T)Activator.CreateInstance(type);

      AddSystem<T>(t);

      return t;
    }

    return default(T);
  }

  /// <summary>
  /// 移除該 class scripts
  /// </summary>
  public static void RemoveSystem<T>() where T : IDataBase
  {
    IDataBase data;

    if (dataSystemList.TryGetValue(typeof(T), out data))
    {
      data.Destory();

      dataSystemList.Remove(typeof(T));
    }
  }
}

#region Base

public class DataList<T> : IDataBase where T : new()
{
  private List<T> saveData;

  public DataList()
  {
    saveData = new List<T>();
  }

  public DataList(IEnumerable<T> collection)
  {
    saveData = new List<T>(collection);
  }

  public void AddData(T t)
  {
    saveData.Add(t);
  }

  public T GetData(int index)
  {
    return saveData[index];
  }

  public int Count
  {
    get
    {
      return saveData.Count;
    }
  }

  public void Destory()
  {

  }
}

public class DataDictionary<T1, T2> : IDataBase  
  where T2 : new()
{
  protected Dictionary<T1, T2> saveData;

  public DataDictionary()
  {
    saveData = new Dictionary<T1, T2>();
  }

  public virtual void Destory()
  {

  }

  public T2 GetData(T1 t)
  {
    T2 getData;

    if (saveData.TryGetValue(t, out getData))
      return getData;

    return default(T2);
  }

  public void SetData(T1 value, T2 data)
  {
    saveData[value] = data;
  }

  public void Remove(T1 t)
  {
    saveData.Remove(t);
  }
}

public interface IDataBase
{
  void Destory();
}

public interface IObserverBase
{
  void OnEvent(PushOnEvent message);  // 事件傳遞
}

public struct PushOnEvent
{
  public string eventName;
  public IDataBase pushData;
}

public class PushInt : IDataBase
{
  public int pushInt;

  public void Destory()
  {
  }
}

public class PushString : IDataBase
{
  public string pushString;

  public void Destory()
  {
  }
}

#endregion